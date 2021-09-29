using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerKeyBoardController : MonoBehaviour
{
    public Transform magics;
    public bool unableThisFunction = false;
    public Animator ani;
    public int aniAttackBasePoint = 0;
    public Player player;
    bool runLock = false;
    bool attackLock = false;
    int attack2Lock = 0;
    int specialAttackLock = -1;
    bool fastMoveFlag = false;
    KeyCode currentKey;
    private GameObject[] wearingSets;
    private string[] setsPartName;
    public Transform[] setsPartTransform;
    int selectToShowWearingSets, nowWearingSets;
    public GameObject CameraPos1, CameraPos2;
    //temp
    private bool stopMoving = false;
    public float curseSensity = 0.005f;
    bool controlStopMove = false;
    Vector3 CameraHignVec3;
    //move
    int moveFlag = -1;
    Quaternion moveTemp;
    bool moveTempFlag = false;
    bool attackTempFlag = false;
    //attackCheck
    private CollisionTrigger attackCheckPoint;
    public ParticleSystem collisionEffect;
    int tempValue = -1;
    //beingAttack
    Vector3 dir; float angle;
    float y = 0; float x = 0;
    //skill
    int skillDirection = -1;
    public List<Skill> skillList;
    int lastAniTagHash;
    int nowAnitagHash;
    string pastWearingName,pastRightHandName;
    public int[] AllPoint;
    // Start is called before the first frame update
    private void Awake()
    {
        AllPoint = new int[8];
        CameraHignVec3 = new Vector3(0, 5f, 0);
        setsPartName = new string[]
        {
            "Spine1Item","SpineItem","NeckItem","BodyItem","RightArmItem",
            "RightForeArmItem","LeftArmItem","LeftForeArmItem",
            "RightUpLegItem","RightLegItem","LeftUpLegItem",
            "LeftLegItem"
        };
        wearingSets = new GameObject[setsPartName.Length+2];
    }
    void skillIMGMake()
    {
        for (int i = 0; i < 4; i++)
        {
            try
            {
                makeSkillIMG(skillList[i].IMG(), i);
            }
            catch
            {
                print("error");
            }
        }
    }
    void makeSkillIMG(string skillName, int i)
    {
        Texture2D tex = Resources.Load("SkillIMG/" +
                    skillName) as Texture2D;
        Sprite temp = Sprite.Create(tex, new Rect(0,
            0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        magics.GetChild(i).GetComponent<Image>().sprite = temp;
    }
    void Start()
    {
        skillList = new List<Skill>() { new Skill.LibraryUse(player),new Skill.Escape(player),
            new Skill.BloodGiven(player),new Skill.Escape(player),new Skill.LibraryUse(player),new Skill.Escape(player),
            new Skill.BloodGiven(player),new Skill.Escape(player)};
        x = -5810; y = 570;
        moveTemp = transform.rotation;
        Cursor.visible = false;//光标是否应可见

        //光标如何处理        锁定光标到游戏窗口的中心。

        Cursor.lockState = CursorLockMode.Locked;
        //开启缓慢滞后控制
        skillIMGMake();
    }
    public void beingAttack(Transform enemyTrans, int toughnessKill)
    {
        dir = enemyTrans.position - transform.position;
        angle = Mathf.Acos(Vector3.Dot(transform.forward.
            normalized, dir.normalized)) * Mathf.Rad2Deg;
        if (player.toughCheck(toughnessKill))
        {
            if (angle <= 95)
            {
                //ani.SetInteger("BATypePoint", 1);
                ani.SetInteger("WeakBATypePoint", 1);
                moveTemp = Quaternion.Euler(0, angle * (-1), 0);
            }
            else
            {
                //ani.SetInteger("BATypePoint", 2);
                ani.SetInteger("WeakBATypePoint", 2);
                moveTemp = Quaternion.Euler(0, angle * (-1) - 180, 0);
            }
        }
        else
        {
            if (angle <= 95)
            {
                ani.SetInteger("BATypePoint", 1);
                moveTemp = Quaternion.Euler(0, angle * (-1), 0);
            }
            else
            {
                ani.SetInteger("BATypePoint", 2);
                moveTemp = Quaternion.Euler(0, angle * (-1) - 180, 0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        //单击鼠标右键，进行形态切换
        if (Input.GetMouseButton(1))
        {
            stopMoving = true;
            Cursor.visible = true;//光标是否应可见
            Cursor.lockState = CursorLockMode.Confined;
            if (Input.mousePosition.x / Screen.width > 0.65)
            {
                magics.GetChild(2).GetComponent<CanvasGroup>().alpha = 1;
                magics.GetChild(0).GetComponent<CanvasGroup>().alpha = 0.8f;
                magics.GetChild(1).GetComponent<CanvasGroup>().alpha = 0.8f;
                magics.GetChild(3).GetComponent<CanvasGroup>().alpha = 0.8f;
                skillDirection = 2;
            }
            else if (Input.mousePosition.x / Screen.width < 0.35)
            {
                magics.GetChild(0).GetComponent<CanvasGroup>().alpha = 1;
                magics.GetChild(2).GetComponent<CanvasGroup>().alpha = 0.8f;
                magics.GetChild(1).GetComponent<CanvasGroup>().alpha = 0.8f;
                magics.GetChild(3).GetComponent<CanvasGroup>().alpha = 0.8f;
                skillDirection = 0;
            }
            else if (Input.mousePosition.y / Screen.height > 0.65)
            {
                magics.GetChild(1).GetComponent<CanvasGroup>().alpha = 1;
                magics.GetChild(0).GetComponent<CanvasGroup>().alpha = 0.8f;
                magics.GetChild(2).GetComponent<CanvasGroup>().alpha = 0.8f;
                magics.GetChild(3).GetComponent<CanvasGroup>().alpha = 0.8f;
                skillDirection = 1;
            }
            else if (Input.mousePosition.y / Screen.height < 0.35)
            {
                magics.GetChild(3).GetComponent<CanvasGroup>().alpha = 1;
                magics.GetChild(0).GetComponent<CanvasGroup>().alpha = 0.8f;
                magics.GetChild(1).GetComponent<CanvasGroup>().alpha = 0.8f;
                magics.GetChild(2).GetComponent<CanvasGroup>().alpha = 0.8f;
                skillDirection = 3;
            }
            else
            {
                magics.GetChild(3).GetComponent<CanvasGroup>().alpha = 0.8f;
                magics.GetChild(0).GetComponent<CanvasGroup>().alpha = 0.8f;
                magics.GetChild(1).GetComponent<CanvasGroup>().alpha = 0.8f;
                magics.GetChild(2).GetComponent<CanvasGroup>().alpha = 0.8f;
                selectToShowWearingSets = -1;
                skillDirection = -1;
            }
        }
        else if (Input.GetMouseButtonUp(1))
        {
            if (skillDirection != -1)
            {
                skillList[skillDirection].SkillStart();
            }
            else return;
            skillList.Add(skillList[skillDirection]);
            skillList[skillDirection] = skillList[5];
            skillList.RemoveAt(5);
            makeSkillIMG(skillList[skillDirection].IMG(), skillDirection);
            skillDirection = -1;
            stopMoving = false;
            magics.GetChild(3).GetComponent<CanvasGroup>().alpha = 0.8f;
            magics.GetChild(0).GetComponent<CanvasGroup>().alpha = 0.8f;
            magics.GetChild(1).GetComponent<CanvasGroup>().alpha = 0.8f;
            magics.GetChild(2).GetComponent<CanvasGroup>().alpha = 0.8f;
            Cursor.visible = false;//光标是否应可见
            Cursor.lockState = CursorLockMode.Locked;
            if (selectToShowWearingSets != -1)
            {
                nowWearingSets = selectToShowWearingSets;
            }
            else
            {
                //打开物品菜单
                openItemManu();
            }
        }
        if (!stopMoving)
        {
            CameraPos1.transform.position = transform.position + CameraHignVec3;
            x += Input.GetAxis("Mouse X") * 100;
            float temp = y - Input.GetAxis("Mouse Y") * 100;
            if (temp > -140 && temp < 4200) { y = temp; }
            Quaternion rotation = Quaternion.Euler(y * Mathf.Deg2Rad, x * Mathf.Deg2Rad, 0);
            CameraPos1.transform.rotation = rotation;
            if (moveFlag != -1)
            {
                if (moveFlag == 2)
                {
                    moveTemp = Quaternion.Euler(0, x * Mathf.Deg2Rad - 90, 0);
                }
                else if (moveFlag == 1)
                {
                    moveTemp = Quaternion.Euler(0, x * Mathf.Deg2Rad - 180, 0);
                }
                else if (moveFlag == 3)
                {
                    moveTemp = Quaternion.Euler(0, x * Mathf.Deg2Rad + 90, 0);
                }
                else if (moveFlag == 0)
                {
                    moveTemp = Quaternion.Euler(0, x * Mathf.Deg2Rad, 0);
                }
            }

        }
        //进行攻击，闪避等行为的切换
        if (Input.GetMouseButtonDown(0))
        {
            runLock = true;
            if (!attackLock && specialAttackLock > 10) { specialAttackSign(); }
            else attackSign();
        }
    }
    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Slerp(
            transform.rotation, moveTemp, 0.15f);
        if (Input.GetKeyDown(KeyCode.Q))
        {
            specialAttackLock = 100;
        }
        else if (specialAttackLock > 0) specialAttackLock--;
    }
    private void LateUpdate()
    {
        if (ani.GetCurrentAnimatorStateInfo(1).IsTag("BA"))
        {
            ani.SetInteger("WeakBATypePoint", 0);
        }

        if (ani.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            if (!attackTempFlag && attackCheckPoint.collisionTrigger())
            {
                attackTempFlag = true;
            }
            if (attackTempFlag) {
                attackCheckPoint.moveEffect();
            }
        }

        //once a time
        nowAnitagHash = ani.GetCurrentAnimatorStateInfo(0).tagHash;
        if (nowAnitagHash == lastAniTagHash) return;
        lastAniTagHash = nowAnitagHash;
        if (ani.GetCurrentAnimatorStateInfo(0).IsTag("BA"))
        {
            ani.SetInteger("BATypePoint", 0);
            runLock = true;
            attackLock = true;
        }
        else
        if (ani.GetCurrentAnimatorStateInfo(0).IsTag("AfterAttack"))
        {
            attack2Lock += 1;
            attackCheckPoint.stopEffect2();
            attackTempFlag = false;
        }
        else
        if (ani.GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
        {
            attack2Lock = 0;
            attackLock = false;
            ani.SetInteger("idleTypePoint", -1);
            moveFlag = -1;
            runLock = false;
        }
        else if (ani.GetCurrentAnimatorStateInfo(0).IsTag("over"))
        {
            attack2Lock = 0;
            runLock = false;
            attackLock = false;
        }else if (ani.GetCurrentAnimatorStateInfo(0).IsTag("BeforeAttack"))
        {
            attackLock = true;
            runLock = true;
            ani.SetInteger("attackTypePoint", -1);
        }
        else if (ani.GetCurrentAnimatorStateInfo(0).IsTag("Evade"))
        {
            attackLock = true;
            runLock = true;
            ani.SetInteger("evadeTypePoint", -1);
        }
        else if (ani.GetCurrentAnimatorStateInfo(0).IsTag("AfterEvade"))
        {
            player.defense /= 10;
        }

    }
    void OnGUI()
    {
        controlMove();
    }

    void controlMove()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (ani.GetCurrentAnimatorStateInfo(0).IsTag("Idle")||
                ani.GetCurrentAnimatorStateInfo(0).IsTag("Run")
                )
            {
                attackLock = true;
                runLock = true;
                ani.SetInteger("evadeTypePoint", AllPoint[0]);
                player.defense *= 10;
                return;
            }
        }
        if (runLock)
        {
            ani.SetInteger("runTypePoint", -1);
            ani.SetInteger("idleTypePoint", -1);
            return;
        }else
        if (Input.GetKey(KeyCode.W))
        {
            moveFlag = 0;
            if (!ani.GetCurrentAnimatorStateInfo(0).IsTag("Run"))
                ani.SetInteger("runTypePoint", AllPoint[2]);
            return;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveFlag = 1;
            if (!ani.GetCurrentAnimatorStateInfo(0).IsTag("Run"))
                ani.SetInteger("runTypePoint", AllPoint[2]);
            return;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            moveFlag = 2;
            if (!ani.GetCurrentAnimatorStateInfo(0).IsTag("Run"))
                ani.SetInteger("runTypePoint", AllPoint[2]);
            return;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveFlag = 3;
            if (!ani.GetCurrentAnimatorStateInfo(0).IsTag("Run"))
                ani.SetInteger("runTypePoint", AllPoint[2]);
            return;
        }
        else
        {
            ani.SetInteger("runTypePoint", -1);
            ani.SetInteger("idleTypePoint", AllPoint[2]);
        }
    }

    void signClear()
    {

    }
    void specialAttackSign()
    {
        attackLock = true;
        ani.SetInteger("attackTypePoint", AllPoint[5]);
    }

    void attackSign()
    {
        ani.SetInteger("attackTypePoint", AllPoint[4]);
        if (attack2Lock == 2)
        {
            ani.SetInteger("attackTypePoint", AllPoint[3]);
            attackLock = true;
            attack2Lock = 0;
            return;
        }
        if (attackLock) {
            return;
        }
        ani.SetInteger("attackTypePoint", AllPoint[3]);
        attackLock = true;
    }
    void trumbleSign()
    {
        print("trumbleSign");
    }
    public void wearing(string wearingName, string rightHandName,
        string leftHandName)
    {
        GameObject temp;
        if (pastWearingName != wearingName)
        {
            if (wearingName != "null")
            {
                GameObject tempDestroy = null;
                for(int i = 2; i < setsPartName.Length+1; i++)
                {
                    tempDestroy = wearingSets[i];
                    wearingSets[i] = null;
                    Destroy(tempDestroy);
                }
                for (int i = 2; i < setsPartName.Length; i++)
                {
                    try
                    {
                        temp = Resources.Load("Equipment/" + setsPartName[i] + "/"
                            + wearingName) as GameObject;
                        GameObject child = Instantiate(temp, setsPartTransform[i]);
                        wearingSets[i] = child;
                    }
                    catch
                    {
                        print("创建" + wearingName + "_" + setsPartName[i] + "失败");
                    }
                }
                if (leftHandName != "")
                {
                    try
                    {
                        temp = Resources.Load("Equipment/A_W_Left/" + leftHandName)
                            as GameObject;
                        GameObject child = Instantiate(temp, setsPartTransform[
                            setsPartTransform.Length - 1]);
                        wearingSets[setsPartName.Length] = child;
                    }
                    catch
                    {
                        print("创建" + "Equipment/A_W_Left/"
                            + rightHandName + "失败");
                    }

                }
            }
            
        }
        if (rightHandName !=pastRightHandName&& rightHandName!="")
        {
            if (rightHandName != "null")
            {
                GameObject tempDestroy = wearingSets[wearingSets.Length-1];
                wearingSets[wearingSets.Length - 1] = null;
                Destroy(tempDestroy);
                try
                {
                    temp = Resources.Load("Equipment/A_W_Right/" +
                        rightHandName) as GameObject;
                    GameObject child = Instantiate(temp, setsPartTransform[
                        setsPartTransform.Length - 2]);
                    attackCheckPoint = child.GetComponent<CollisionTrigger>();
                    attackCheckPoint.enemyName = "Enemy";
                    aniAttackBasePoint = attackCheckPoint.baseAttackPoint;
                    attackCheckPoint.collisionEffect2 = collisionEffect;
                    wearingSets[setsPartName.Length + 1] = child;
                }
                catch
                {
                    print("创建" + "Equipment/A_W_Right/"
                        + rightHandName + "失败");
                }
            }
        }
      
        pastWearingName = wearingName;
        pastRightHandName = rightHandName;
    }
    void openItemManu()
    {

    }
}
