using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Player player;
    public Transform lamps;
    private Transform[] lamp;
    private MoonFlash[] lightUI;
    public int lightUITop = 0;
    private int lampNum;
    private float moonFlashReduce = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 80;
    }

    public void InitUIManager(int lampNum)
    {
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
    }
}
