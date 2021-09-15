using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int toughness, hp, san;
    public UIManager uiManager;
    public int sanCut = 0;
    // Start is called before the first frame update
    void Start()
    {
        hp = 500;
        toughness = 100;
        san = 10;
        uiManager.InitUIManager(san);
    }
    public void printf(string s)
    {
        print(s);
    }
    public bool toughCheck(int toughKill)
    {
        toughness -= toughKill;
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
    private void FixedUpdate()
    {
        if (sanCut > 0)
        {
            san = sanCut;
            uiManager.sanDown(sanCut);
            sanCut = 0;
        }
    }
}
