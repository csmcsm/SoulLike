using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodFollow : MonoBehaviour
{

    public float bloodValue;

    private float tmpValue;
    public float EnemybloodValue;
    private float EnemytmpValue;
    void Start()
    {
        tmpValue = 1.0f;
        bloodValue = 1.0f;
    }
    private void OnGUI()
    {
        BloodChange(1, 1);
    }
    public void BloodChange(int fullCON, int NewCON)
    {
        //点击加血

        tmpValue = NewCON / fullCON;
        bloodValue = Mathf.Lerp(bloodValue, tmpValue, 0.01f);
        GUI.color = Color.red; //血条，设置为红色
        GUI.HorizontalScrollbar(new Rect(110, 50, 300, 20),
            0.0f, bloodValue, 0.0f, 1.0f, GUI.skin
            .GetStyle("HorizontalScrollbar"));
        GUI.color = Color.white; //血条，设置为红色
        GUI.HorizontalScrollbar(new Rect(110, 70, 300, 20),
            0.0f, bloodValue, 0.0f, 1.0f, GUI.skin
            .GetStyle("HorizontalScrollbar"));

    }
    public void DefendChange(int fullHp, int NewHp)
    {
        //点击加血

        tmpValue = NewHp / fullHp;
        bloodValue = Mathf.Lerp(bloodValue, tmpValue, 0.01f);

        GUI.color = Color.white; //血条，设置为红色
        GUI.HorizontalScrollbar(new Rect(20, 50, 200, 20), 0.0f, bloodValue, 0.0f, 1.0f, GUI.skin.GetStyle("HorizontalScrollbar"));

    }

    public void EnemyBloodChange(int fullHp, int NewHp)
    {
        //点击加血

        tmpValue = NewHp / fullHp;
        EnemybloodValue = Mathf.Lerp(bloodValue, tmpValue, 0.01f);

        GUI.color = Color.white; //血条，设置为红色
        GUI.HorizontalScrollbar(new Rect(20, 100, 200, 20), 0.0f, bloodValue, 0.0f, 1.0f, GUI.skin.GetStyle("HorizontalScrollbar"));

    }

}

