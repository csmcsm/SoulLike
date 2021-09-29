using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonFlash : MonoBehaviour
{

    private CanvasGroup moonCanvasGroup;
    private float flashSpeed = 0.25f;//光晕闪动速度
    private float maxAlpha = 1f;//显示的最高alpha值
    void Start()
    {
        moonCanvasGroup = GetComponent<CanvasGroup>();
    }

    public bool moonFlash(float timeReduce)
    {
        if (moonCanvasGroup.alpha < maxAlpha)
        {
            moonCanvasGroup.alpha += flashSpeed * Time.deltaTime * (1 + timeReduce);
            return false;
        }
        else
        {
            moonCanvasGroup.alpha = maxAlpha;
            return true;
        }
    }
    private void OnDisable()
    {
        moonCanvasGroup.alpha = 0f;
    }
}