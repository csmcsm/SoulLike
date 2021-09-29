using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snow : MonoBehaviour
{
    public SnowManager sm;
    public Transform moveControler;
    public RenderTexture rt;
    public Texture drawImg;
    public Material replace;
    public Material rtMaterial;
    public Texture defaultImg;
    RaycastHit hit;
    public SnowManager snowManager;
    // Start is called before the first frame update
    void Start()
    {
        DrawDefault();
    }
    public void Draw(int x, int y)
    {
        RenderTexture.active = rt;
        GL.PushMatrix();
        GL.LoadPixelMatrix(0, rt.width, rt.height, 0);
        x -= (int)(drawImg.width * 0.075f);
        y -= (int)(drawImg.height * 0.075f);
        Rect rect = new Rect(x, y, drawImg.width*0.15f, drawImg.height*0.15f);
        Graphics.DrawTexture(rect, drawImg);
        GL.PopMatrix();
        RenderTexture.active = null;
    }
    public void DrawDefault()
    {
        RenderTexture.active = rt;
        GL.PushMatrix();
        GL.LoadPixelMatrix(0, rt.width, rt.height, 0);
        Rect rect = new Rect(0, 0, rt.width, rt.height);
        Graphics.DrawTexture(rect, defaultImg);
        GL.PopMatrix();
        RenderTexture.active = null;
    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(moveControler.position,
            moveControler.up * (-1), out hit, 1))
        {
            int x = (int)(hit.textureCoord.x * rt.width);
            int y = (int)(rt.height - hit.textureCoord.y * rt.height);
            snowManager.Draw(x, y);
        }

    }
    private void OnEnable()
    {
        GetComponent<Renderer>().material = rtMaterial;
    }
    private void OnDisable()
    {
        GetComponent<Renderer>().material= replace;
    }
}
