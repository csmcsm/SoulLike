using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class enemyBlood : MonoBehaviour
{
    public float xOffset;
    public float yOffset;
    public RectTransform recTransform;
    Quaternion q;
    public Transform cam;
    int MaxHp, PastHp;
    float Cent;
    public TrollAttribute pAttributes;
    public float bloodValue;
    public Slider hp;
    Vector2 v;
    public float x, y;
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Transform>();
        MaxHp = pAttributes.Hp;
        PastHp = MaxHp;
        bloodValue = 100;
        Cent = 100;
    }
    void Update()
    {
        x = Camera.main.WorldToScreenPoint(pAttributes.transform.localPosition).x;
        y = Camera.main.WorldToScreenPoint(pAttributes.transform.localPosition).y;

        v.x = x - Screen.width / 2+620;

        v.y = y - Screen.height / 2+650;
        Cent = pAttributes.Hp * 100 / MaxHp;

        bloodValue = Mathf.Lerp(bloodValue, Cent, 0.05f);
        hp.value = bloodValue;

        if (PastHp != pAttributes.Hp)
        {
            PastHp = pAttributes.Hp;
            recTransform.gameObject.SetActive(true);
            return;

        }
        transform.position=v;
    }
}
