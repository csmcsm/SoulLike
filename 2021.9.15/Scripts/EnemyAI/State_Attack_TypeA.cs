using System.Collections.Generic;
using System;
public class State_Attack_TypeA : IState_Attack
{
    Troll troll;
    public Dictionary<string, string> openTheAni { get; set; }
    public Dictionary<string, string> closeTheAni { get; set; }
    public string[] exitAniTag;
    string nowAnitag;
    int lastAniTagHash;
    int nowAnitagHash;
    string StateName;
    int attackPoint;
    CollisionTrigger collideTrigger;
    bool attackTempFlag = false;
    bool attackAt = false;
    public void Excute()
    {
        nowAnitagHash = troll.ani.GetCurrentAnimatorStateInfo(0).tagHash;
        attack();
        if (nowAnitagHash == lastAniTagHash) return;
        troll.useSpecialEffect(0);
        nowAnitag = troll.getCurrentAniStateTag(0);
        attackAt = false;
        if (troll.ani.GetCurrentAnimatorStateInfo(0).IsTag("BeforeAttack"))
        {
            attackAt = true;
            troll.ani.SetInteger("attackTypePoint", 0);
            troll.attackCheckPoint.stopEffect2();
            attackTempFlag = false;
        }
        if (openTheAni.ContainsKey(nowAnitag))
        {
            troll.animatorSwitch(openTheAni[nowAnitag],
                closeTheAni[nowAnitag]);
        }
        for (int i = 0; i < exitAniTag.Length; i++)
        {
            if (nowAnitag == exitAniTag[i]
                )
            {
                startChangeState();
            }
        }
        lastAniTagHash = nowAnitagHash;
    }
    void startChangeState()
    {
        if (!troll.angry())
        {
            troll.printState(nowAnitag);
            troll.changeState(
          StateName, nowAnitag);
        }
        else
        {
            Exit();
            Enter();
        }
    }
    public void Enter()
    {
        troll.attackCheckPoint.enabled = true;
        troll.printState(StateName);
        attackPoint = new Random().Next(1, 6);
        if (!checkForAngle(attackPoint)) return;
        troll.ani.SetInteger("attackTypePoint", attackPoint);
        nowAnitagHash = troll.ani.GetCurrentAnimatorStateInfo(0).tagHash;
        nowAnitag = troll.getCurrentAniStateTag(0);
        troll.useSpecialEffect(0);
        nowAnitagHash = lastAniTagHash;
    }
    public void Exit()
    {
        troll.attackCheckPoint.enabled = false;
        nowAnitag = "";
        troll.ani.SetInteger("attackTypePoint", -1);
        if (collideTrigger != null)
        {
            collideTrigger.gameObject.SetActive(false);
        }
        troll.attackCheckPoint.stopEffect2();
        attackTempFlag = false;
    }
    bool checkForAngle(int point)
    {
        for (int i = 0; i < troll.
            readAttackPointToAttackAngleDiactionary.Length; i++)
        {
            if (point <=
                troll.readAttackPointToAttackAngleDiactionary[i])
            {
                int temp = troll.attackPointToAttackAngleDiactionary[
                    troll.readAttackPointToAttackAngleDiactionary[i - 1]];
            }
        }
        return true;
    }
    public void SetTroll(Troll troll)
    {
        this.troll = troll;
    }
    public void attack()
    {
        if (attackAt) troll.attackAimAt();
        if (!attackTempFlag && troll.ani.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            if (troll.attackCheckPoint.collisionTrigger())
            {
                attackTempFlag = true;
            }
        }
        if (attackTempFlag) troll.attackCheckPoint.moveEffect();
    }
    public State_Attack_TypeA(Dictionary<string, string> openTheAni,
     Dictionary<string, string> closeTheAni,
     string[] exitAniTag, int maxAttackPoint = 30)
    {
        StateName = "State_Attack_TypeA";
        this.openTheAni = openTheAni;
        this.closeTheAni = closeTheAni;
        this.exitAniTag = exitAniTag;
    }
}