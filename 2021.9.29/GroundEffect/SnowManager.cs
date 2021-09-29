using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowManager : MonoBehaviour
{
    public PlayerKeyBoardController playerBC;
    public RenderTexture replaceRt;
    public RenderTexture rt;
    public Texture defaultImg;
    Transform[] childTrans;
    RaycastHit hit;
    Snow historySnow = null;
    string historySnowName = "";
    public Texture slowDefaultImg;
    public Transform moveControler;
    Vector3 historyPos = Vector3.zero;
    public Texture drawImg;
    bool ifActive = false;
    int mask;
    // Start is called before the first frame update
    void Start()
    {
        mask = 1 << LayerMask.NameToLayer("Collision");
        RenderTexture.active = replaceRt;
        GL.PushMatrix();
        GL.LoadPixelMatrix(0, replaceRt.width, replaceRt.height, 0);
        Rect rect = new Rect(0, 0, replaceRt.width, replaceRt.height);
        Graphics.DrawTexture(rect, defaultImg);
        GL.PopMatrix();
        RenderTexture.active = null;
        childTrans = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            childTrans[i] = transform.GetChild(i);
            childTrans[i].GetComponent<Snow>().enabled = false;
        }
        StartCoroutine(DrawDefault());
    }
    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(moveControler.position,
            moveControler.up * (-0.1f), out hit, mask))
        {
            if ((historyPos - hit.transform.position).magnitude < 0.01)
            {
                return;
            }
            try
            {
                historyPos = hit.transform.position;
                if (historySnowName ==
                        hit.transform.name) return;
                changeRT();
                hit.transform.GetComponent<Snow>().enabled = true;
                historySnow = hit.transform.GetComponent<Snow>();
                historySnowName = hit.transform.name;
            }
            catch
            {
                print("error");
            }
        }
    }
    public void Draw(int x, int y)
    {
        RenderTexture.active = rt;
        GL.PushMatrix();
        GL.LoadPixelMatrix(0, rt.width, rt.height, 0);
        x -= (int)(drawImg.width * 0.075f);
        y -= (int)(drawImg.height * 0.075f);
        Rect rect = new Rect(x, y, drawImg.width * 0.15f, drawImg.height * 0.15f);
        Graphics.DrawTexture(rect, drawImg);
        if (ifActive)
        {
            rect = new Rect(0, 0, rt.width, rt.height);
            Graphics.DrawTexture(rect, slowDefaultImg);
            rect = new Rect(0, 0, replaceRt.width, replaceRt.height);
            Graphics.DrawTexture(rect, slowDefaultImg);
        }
        GL.PopMatrix();
        RenderTexture.active = null;
    }
    IEnumerator DrawDefault()
    {
        while (true)
        {
            ifActive = true;
            yield return new WaitForSeconds(0.03f);
        }
    }

    void changeRT()
    {
        RenderTexture.active = replaceRt;
        GL.PushMatrix();
        GL.LoadPixelMatrix(0, replaceRt.width, replaceRt.height, 0);
        Rect rect = new Rect(0, 0, replaceRt.width, replaceRt.height);
        Graphics.DrawTexture(rect, rt);
        GL.PopMatrix();
        RenderTexture.active = null;
        if (historySnow != null) historySnow.enabled = false;
    }

}
