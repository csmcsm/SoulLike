using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class Troll : MonoBehaviour
{
    public int toughness = 100;
    public CollisionTrigger attackCheckPoint;
    public Player player;
    public IState nowState, sleep, attack, move, die, evade;
    public Dictionary<string, int> allAniNameToEffectsNumberDiactionary;
    public Dictionary<string, InitTroll.EnhanceTheFunction> allAniNameToEnhanceTheFunctionDiactionary;
    public Dictionary<string, int> attackAniNameToTriggerDiactionary;
    public Dictionary<string, Dictionary<string, string>> exitAniTag;
    public int[] readAttackPointToAttackAngleDiactionary;
    public Dictionary<int, int> attackPointToAttackAngleDiactionary;
    public string[] aniStateTag;
    public string[] aniStateName_EffectOrAttack;
    public ParticleSystem[] specialEffects;
    public CollisionTrigger[] collisionTriggers;
    public bool IfAttack, IfIdle, Ifdie;
    public int hurt;
    //public BattleManager battleManager;
    public TrollAttribute trollAttribute;
    public Animator ani;
    public DecisionTree decisionTree;
    public DecisionTree.ControlWindows warmWindows, deterWindows;
    public Rigidbody rigidBody;
    event InitTroll.EnhanceTheFunction eventCome;
    float rotateSpeed, backRotateSpeed, backMoveSpeed, runOrBackPoint;
    //function para
    //runOrBack
    Vector3 dir;
    float angle;
    //runMove
    Quaternion runMoveTempPara;
    //angry
    Vector3 angryTempPara;
    //forceToChangeState
    Vector3 tempV3;
    //decisionState
    System.Random random = new System.Random();
    //escape
    Vector3 escapeTemp;
    //scared
    Vector3 vaxis = Vector3.zero;
    public string afterScaredTag;
    int backMoveAim = 1;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>

    void Start()
    {
        //collisionTrigger.is_trigger;
        initTroll();
    }
    bool toughCheck(int toughKill)
    {
        toughness -= toughKill;
        print(toughness);
        if (toughness > 0)
        {
            return true;
        }
        else
        {
            toughness = 100;
            return false;
        }
    }
    public void beingAttack(Transform enemyTrans, int toughnessKill)
    {
        dir = enemyTrans.position - transform.position;
        angle = Mathf.Acos(Vector3.Dot(transform.forward.
            normalized, dir.normalized)) * Mathf.Rad2Deg;
        if (toughCheck(toughnessKill))
        {
            if (angle <= 95)
            {
                //ani.SetInteger("BATypePoint", 1);
                ani.SetInteger("WeakBATypePoint", 1);

            }
            else
            {
                //ani.SetInteger("BATypePoint", 2);
                ani.SetInteger("WeakBATypePoint", 2);
            }
        }
        else
        {
            if (angle <= 95)
            {
                ani.SetInteger("BATypePoint", 1);
            }
            else
            {
                ani.SetInteger("BATypePoint", 2);
            }
        }
    }
    void Update()
    {
        //print((player.transform.position - transform.position).magnitude);
        lock (nowState)
        {
            nowState.Excute();
        }
        if (ani.GetCurrentAnimatorStateInfo(1).IsTag("BA"))
        {
            ani.SetInteger("BATypePoint", 0);
        }
        if (ani.GetCurrentAnimatorStateInfo(2).IsTag("BA"))
        {
            ani.SetInteger("WeakBATypePoint", 0);
        }
    }
    public void changeState(string stateName, string exitAniName)
    {
        lock (nowState)
        {
            nowState.Exit();
            if (scared() && makeDecisionToEvade())
            {
                switchState("evade");
            }
            else
            {
                switchState(exitAniTag
                [stateName][exitAniName]);
            }
            nowState.Enter();
        }
    }
    public void forceToChangeState(string stateName = "")
    {
        if (stateName == "evade")
        {
            StartCoroutine(forceToChangeStateAsync());
            return;
        }
        lock (nowState)
        {
            nowState.Exit();
            switchState(stateName);
            nowState.Enter();
        }
    }
    IEnumerator forceToChangeStateAsync()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (!ani.GetCurrentAnimatorStateInfo(0).
                IsTag("AfterEvade"))
            {
                lock (nowState)
                {
                    nowState.Exit();
                    switchState("evade");
                    nowState.Enter();
                }
                break;
            }
        }
        yield break;
    }
    bool makeDecisionToEvade()
    {
        dir = player.transform.position - transform.position;
        dir.y = 0f;
        angle = Mathf.Acos(Vector3.Dot(transform.forward.
            normalized, dir.normalized)) * Mathf.Rad2Deg;
        dir = transform.position - player.transform.position;
        dir.y = 0f;
        float enemyAngle = Mathf.Acos(Vector3.Dot(player.transform.forward.
            normalized, dir.normalized)) * Mathf.Rad2Deg;
        //player.Speed
        int ran = random.Next(1, 100);
        //print(decisionTree.evadeByDecisionTree(
        //    angle, enemyAngle, 1, deterWindows.windowsWidth,
        //    warmWindows.windowsWidth) + " : " + ran);
        if (ran < decisionTree.evadeByDecisionTree(
              angle, enemyAngle, 1, deterWindows.windowsWidth,
              warmWindows.windowsWidth))
        {
            tempV3 = transform.position +
            warmWindows.windowsWidth * transform.forward * (-1);
            return true;
        }
        return false;
    }
    public void animatorSwitch(string setTruePara, string setFalsePara = "")
    {
        if (setTruePara != "") ani.SetBool(setTruePara, true);
        if (setFalsePara != "") ani.SetBool(setFalsePara, false);
    }
    //if( Quaternion.LookatRotation(player.transform.position-transform.position),
    //  rotateSpeed*Time.deltatime);)
    public void attackIt()
    {
        //battleManager.sendInfo(info);
    }
    public void runMove(bool angleCheck=true,int speedMul=5)
    {
        if (angleCheck&&angle < 5) return;
        vaxis = Vector3.Cross(transform.forward, transform.right);
        transform.Rotate(vaxis, backMoveAim * Time.deltaTime * backRotateSpeed * speedMul);
    }
    public void backMove_aimAt()
    {
        if (angle < runOrBackPoint) return;
        attackAimAt();
    }
    public float[] escapeAimAt()
    {
        Vector3[] aimAts = new Vector3[7];
        aimAts[0] = transform.forward * (-1);
        aimAts[1] = Vector3.Slerp(transform.forward * (-1), transform.right, 0.5f).normalized;
        aimAts[2] = Vector3.Slerp(transform.forward * (-1), transform.right * (-1), 0.5f).normalized;
        aimAts[3] = transform.right;
        aimAts[4] = transform.right * (-1);
        aimAts[5] = Vector3.Slerp(transform.forward, transform.right, 0.5f).normalized;
        aimAts[6] = Vector3.Slerp(transform.forward, transform.right * (-1), 0.5f).normalized;
        for (int i = 0; i < aimAts.Length; i++)
        {
            if (
            Physics.Raycast(transform.position,
                aimAts[i], warmWindows.windowsWidth))
            {
                return new float[] { aimAts[i].x, aimAts[i].y, aimAts[i].z };
            }
        }
        return new float[] { aimAts[0].x, aimAts[0].y, aimAts[0].z };
    }
    public void attackAimAt()
    {
        Quaternion temp = Quaternion.Lerp(transform.rotation,
            Quaternion.LookRotation(player.transform.position
            - transform.position), 0.3f);
        temp.x = 0;
        temp.z = 0;
        transform.rotation = temp;
    }
    public void escape(float[] aimAt)
    {
        runMoveTempPara = Quaternion.Slerp(
           transform.rotation,
           Quaternion.LookRotation(player.transform.position - transform.position),
           backRotateSpeed * Time.deltaTime
        );
        runMoveTempPara.x = 0f;
        runMoveTempPara.Normalize();
        rigidBody.MoveRotation(runMoveTempPara);
        rigidBody.MovePosition(this.transform.position + Time.deltaTime *
            new Vector3(aimAt[0], aimAt[1], aimAt[2]) *
            backMoveSpeed * (-1));
        //if (escapeTemp.magnitude > warmWindows.windowsWidth)
        //{
        //    return true;
        //}
        //return false;
    }
    public bool aimAtOtherTroll(float aimAtiPoint = 1f, bool runOrBack = true)
    {
        if (runOrBack) aimAtiPoint = runOrBackPoint;
        dir = player.transform.position - transform.position;
        dir.y = 0f;
        if (Vector3.Dot(transform.right.
            normalized, dir.normalized) > 0)
        {
            backMoveAim = 1;
        }
        else { backMoveAim = -1; }
        angle = Mathf.Acos(Vector3.Dot(transform.forward.
            normalized, dir.normalized)) * Mathf.Rad2Deg;
        if (angle > aimAtiPoint)
        {
            return false;
        }
        return true;
    }
    public bool angry()
    {
        angryTempPara = transform.position - player.transform.position;
        angryTempPara.y = 0;
        if (angryTempPara.magnitude < deterWindows.windowsWidth)
        {
            return true;
        }
        return false;
    }
    public bool scared()
    {
        if (!ani.GetCurrentAnimatorStateInfo(0).
            IsTag("AfterEvade") &&
            !ani.GetBool("ifEvade"))
        {
            return true;
        }
        return false;
    }
    void initTroll()
    {
        attackCheckPoint.enemyName = "Player";
        ani = this.GetComponent<Animator>();
        rigidBody = this.GetComponent<Rigidbody>();
        trollAttribute = this.GetComponent<TrollAttribute>();
        rotateSpeed = trollAttribute.rotateSpeed;
        backRotateSpeed = trollAttribute.backRotateSpeed;
        runOrBackPoint = trollAttribute.runOrBackPoint;
        InitTroll.TypeA.initTroll(this);
        decisionTree.deterWindowsHistory = deterWindows.windowsWidth;
        decisionTree.warmWindowsHistory = warmWindows.windowsWidth;
    }
    void switchState(string stateName)
    {
        switch (stateName)
        {
            case "attack":
                nowState = attack;
                break;
            case "sleep":
                nowState = sleep;
                break;
            case "move":
                nowState = move;
                break;
            case "die":
                nowState = die;
                break;
            case "evade":
                nowState = evade;
                break;
            default:
                break;
        }
    }
    public string getCurrentAniStateTag(int layer)
    {
        for (int i = 0; i < aniStateTag.Length; i++)
        {
            if (ani.GetCurrentAnimatorStateInfo(0).IsTag(
                aniStateTag[i]))
            {
                return aniStateTag[i];
            }
        }
        return "";
    }
    public string getCurrentAniStateName(int layer)
    {
        for (int i = 0; i < aniStateName_EffectOrAttack.Length; i++)
        {
            if (ani.GetCurrentAnimatorStateInfo(0).IsName(
                aniStateName_EffectOrAttack[i]))
            {
                return aniStateName_EffectOrAttack[i];
            }
        }
        return "";
    }
    public void useSpecialEffect(int layer, string stateName = "")
    {
        ParticleSystem temp;
        if (stateName == "") stateName = getCurrentAniStateName(0);
        if (allAniNameToEffectsNumberDiactionary.ContainsKey
            (stateName))
        {
            temp = specialEffects[
                allAniNameToEffectsNumberDiactionary[stateName]];
            if (!temp.isPlaying)
            {
                temp.Play();
            }
        }
        if (allAniNameToEnhanceTheFunctionDiactionary.ContainsKey
            (stateName))
        {
            eventCome = allAniNameToEnhanceTheFunctionDiactionary[stateName];
            eventCome(this);
        }
    }
    public void printState(string name)
    {
        //print(name + " : " + Time.time);
    }

    public IEnumerator renewDecisionTree()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            if (warmWindows.windowsWidth > deterWindows.windowsWidth)
            {
                warmWindows.Convergence();
                deterWindows.Convergence();
                if (false)
                {
                    break;
                }
            }
        }
        yield break;
    }
    public void testBeAttacked()
    {

    }
}
