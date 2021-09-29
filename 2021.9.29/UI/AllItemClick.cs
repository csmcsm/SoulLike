using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllItemClick : MonoBehaviour
{
    UIManager uIManager;
    int num;
    public void click()
    {
        uIManager.clickTrigger(num);
    }
    public void initItem(UIManager a,int b)
    {
        uIManager = a;
        num = b;
    }
}
