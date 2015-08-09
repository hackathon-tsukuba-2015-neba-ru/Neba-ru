using UnityEngine;
using System.Collections;

public class DragArea : MonoBehaviour
{

    public static float nebari;

    Vector3 startPosition;
    Vector3 endPosition;
    float DragTime;
    float DragTimeDisplay;

    float height = 0.0f;
    float accelaration = 0.0f;
    public static float displayheight;

    Vector3 GroundStart;
    Vector3 LegStart;
    Vector3 BodyStart;
    Vector3 HeadStart;


    // Use this for initialization
    void Start()
    {
         GroundStart = GameObject.Find("Ground").transform.position;
         LegStart = GameObject.Find("Leg").transform.position;
        BodyStart = GameObject.Find("Body").transform.position;
        HeadStart = GameObject.Find("Head").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        DragTime += Time.deltaTime;
        accelaration -= 0.01f;
        height += accelaration;
        if (height < 0.0f)
        {
            accelaration = 0.0f;
            height = 0.0f;
        }

        displayheight = (float)System.Math.Pow((double)height, 1.5);

        //GameObject Body = GameObject.Find("Body");
        //Body.transform.localScale   = new Vector3(0.0f, displayheight, 0f);
        //Body.transform.position = new Vector3(0.0f, displayheight / 2.0f, 0f) + BodyStart;

        float displayoffset = 2.0f;
        if (displayheight < 10.0f)
        {
            displayoffset = 2.0f - ((float)System.Math.Pow(10.0 - (double)displayheight, 2.0) / 50.0f);
        }

        displayoffset = 0.0f;

        GameObject Head = GameObject.Find("Head");
        Head.transform.position = new Vector3(0.0f, displayoffset, 0f) + HeadStart;

        GameObject Ground = GameObject.Find("Ground");
        Ground.transform.position = GroundStart + new Vector3(0,displayoffset - displayheight, 0);
        GameObject Leg = GameObject.Find("Leg");
        Leg.transform.position = LegStart + new Vector3(0, displayoffset - displayheight, 0);

        float GettingCutRatio;
        GettingCutRatio = 0.0f;
        float BodyPartSize;

        GameObject Body = GameObject.Find("Body_d");
        BodyPartSize = (1.0f - GettingCutRatio) * 0.3f;
        Body.transform.position = BodyStart + new Vector3(0,  (displayoffset - displayheight) / 2.0f , 0);
        Body.transform.localScale = new Vector3(Body.transform.localScale.x, displayheight / 2.0f * 0.35f, Body.transform.localScale.z);


    }

    void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 100, 50), "Start : "  + startPosition.x.ToString() + "," + startPosition.y.ToString());
        GUI.Label(new Rect(20, 40, 100, 50), "End   : " + endPosition.x.ToString() + "," + endPosition.y.ToString());
        
        GUI.Label(new Rect(20, 60, 100, 50), "DragTime   : " + DragTimeDisplay.ToString());
        GUI.Label(new Rect(20, 80, 100, 50), "Accel   : " + accelaration.ToString());
    }
    

    public void OnMouseDown()
    {
        startPosition = Input.mousePosition;
        DragTime = 0;
    }

    public void OnMouseUp()
    {
        endPosition = Input.mousePosition;
        accelaration += (((float)endPosition.y - (float)startPosition.y) / DragTime)/3000f;
        DragTimeDisplay = DragTime;
    }

    public void OnMouseOver()
    {

    }

    public void OnMouseExit()
    {

    }

    public void OnMouseEnter()
    {

    }

    public void OnMouseDrag()
    {

    }
}
