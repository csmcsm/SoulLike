using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public int wearingNum = 0;
    public int rightHandNum = 4;
    public Player player;
    public PlayerKeyBoardController pc;
    public Transform lamps;
    private Transform[] lamp;
    private MoonFlash[] lightUI;
    public int lightUITop = 0;
    private int lampNum;
    private float moonFlashReduce = 0f;
    public GameObject PlayerWindow, ItemWindow;
    bool UIState = false;
    AllItemStruct.Item[] items;
    public GameObject itemsWindow,rightHandWindow,bodyWindow;
    public GameObject[] soulWindows;
    public int soulWindowsFlag = -1;
    int[] soulFlagHaven;
    public Transform scrollContent;
    public Text description1, description2;
    public bool trigger=false;
    public int triggerType = -1;
    // Start is called before the first frame update
    void Awake()
    {
        items = new AllItemStruct.Item[14] { new AllItemStruct.PunishmentKnight(),
            new AllItemStruct.WhiteKnight(), new AllItemStruct.WhiteKnightSword()
             , new AllItemStruct.WeakOne(), new AllItemStruct.SwordRod()
             , new AllItemStruct.Knife(), new AllItemStruct.LawKnightRod()
             , new AllItemStruct.StrangeHammer(), new AllItemStruct.PunishmentKnightHammer()
             , new AllItemStruct.PunishmentKnightKiller(), new AllItemStruct.PunishmentKnightPike()
             , new AllItemStruct.HuntingClaw(), new AllItemStruct.SmallBallHammer()
             ,new AllItemStruct.LightMog()
            };
        Application.targetFrameRate = 80;
    }

    public void InitUIManager(int lampNum)
    {
        soulFlagHaven = new int[5] { -1,-1,-1,-1,-1};
        lamp = new Transform[10];
        lightUI = new MoonFlash[10];
        for (int i = 0; i < 10; i++)
        {
            lamp[i] = lamps.GetChild(i);
            lightUI[i] = lamp[i].GetChild(0).GetComponent<MoonFlash>();
        }
        this.lampNum = lampNum;
        for (int i = 0; i < lampNum; i++)
        {
            lamp[i].gameObject.SetActive(true);
        }
        lightUITop = lampNum;
        
        GameObject temp = null;
        for (int i=0;i<items.Length;i++)
        {
            temp=Instantiate(itemsWindow, scrollContent);
            temp.GetComponent<AllItemClick>().initItem(this, i);
            temp.GetComponent<Image>().sprite = Resources.Load("Sprite/" +
                   items[i].getImageName(),typeof(Sprite)) as Sprite;
        }
        wearingNum = 0;rightHandNum = 4;
        rightHandWindow.GetComponent<Image>().sprite = Resources.Load("Sprite/" +
                 items[rightHandNum].getImageName(), typeof(Sprite)) as Sprite;
        bodyWindow.GetComponent<Image>().sprite = Resources.Load("Sprite/" +
                 items[wearingNum].getImageName(), typeof(Sprite)) as Sprite;
        pc.wearing(items[wearingNum].getImageName(), items[rightHandNum].getImageName(), "");
        for (int i = 0; i < 8; i++) {
            if (i < 2)
            {
                pc.AllPoint[i] = items[wearingNum].getPower()[i];
            }
            else
            {
                pc.AllPoint[i] = items[rightHandNum].getPower()[i];
            } 
        }
    }
    public void sanDown(int newSan)
    {
        for (int i = newSan; i < lampNum; i++)
        {
            lightUI[i].enabled = false;
            lightUITop = newSan;
        }
    }
    public void setMoonFlashReduce(float moonFlashReduce)
    {
        this.moonFlashReduce= moonFlashReduce;
    }
    private void Update()
    {
        if (lightUITop != lampNum)
        {
            if (lightUI[lightUITop].moonFlash(moonFlashReduce))
            {
                lightUITop++;
                player.san=lightUITop;
            }
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (!UIState)
            {
                PlayerWindow.SetActive(true);
                Cursor.visible = true;//光标是否应可见
                Cursor.lockState = CursorLockMode.None;
                UIState = true;
            }
            else
            {
                PlayerWindow.SetActive(false);
                ItemWindow.SetActive(false);
                Cursor.visible = false;//光标是否应可见
                Cursor.lockState = CursorLockMode.Locked;
                UIState = false;
            }
        }
    }
    public void PlayerWindowToItemWindow(int i)
    {
        if (i == 0)
        {
            PlayerWindow.SetActive(false);
            ItemWindow.SetActive(true);
        }
        else
        {
            PlayerWindow.SetActive(true);
            ItemWindow.SetActive(false);
        }
        trigger = false;
    }
    public void clickTrigger(int num)
    {
        if (trigger)
        {
            if (triggerType == 0&& items[num].getType()=="rightHand")
            {
                rightHandWindow.GetComponent<Image>().sprite= Resources.Load("Sprite/" +
                   items[num].getImageName(), typeof(Sprite)) as Sprite;
                pc.wearing("null", items[num].getImageName(), "");
                for (int i = 0; i < 8; i++)
                {
                    if (i > 1)
                    {
                        pc.AllPoint[i] = items[num].getPower()[i];
                    }
                }
                ItemWindow.SetActive(false);
                trigger = false;
                return;
            }else if (triggerType == 1 && items[num].getType() == "body")
            {
                bodyWindow.GetComponent<Image>().sprite = Resources.Load("Sprite/" +
                   items[num].getImageName(), typeof(Sprite)) as Sprite;
                pc.wearing(items[num].getImageName(),"null", "");
                for (int i = 0; i < 8; i++)
                {
                    if (i < 2)
                    {
                        pc.AllPoint[i] = items[num].getPower()[i];
                    }
                }
                ItemWindow.SetActive(false);
                trigger = false;
                return;
            }
            else if (triggerType == 2 && items[num].getType() == "soul")
            {
                for(int i = 0; i < 5; i++)
                {
                    if (soulFlagHaven[i] == num)
                    {
                        AllItemStruct.Item temp0 = items[num];
                        description1.text = temp0.getName() + "\n" + temp0.getDescription1();
                        description2.text = temp0.getDescription2();
                        return;
                    }
                }
                soulWindows[soulWindowsFlag].GetComponent<Image>().sprite = Resources.Load("Sprite/" +
                   items[num].getImageName(), typeof(Sprite)) as Sprite;
                ItemWindow.SetActive(false);
                trigger = false;
                soulFlagHaven[soulWindowsFlag] = num;
                return;
            }
        }
        AllItemStruct.Item temp = items[num];
        description1.text = temp.getName() + "\n" + temp.getDescription1();
        description2.text = temp.getDescription2();
    }
    public void RightHandWearing()
    {
        ItemWindow.SetActive(true);
        trigger = true;
        triggerType = 0;
    }
    public void BodyWearing()
    {
        ItemWindow.SetActive(true);
        trigger = true;
        triggerType = 1;
    }
    public void SoulWearing(int i)
    {
        ItemWindow.SetActive(true);
        trigger = true;
        triggerType = 2;
        soulWindowsFlag = i;
    }
}
