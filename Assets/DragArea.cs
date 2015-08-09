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
    Vector3 BodydStart;
    Vector3 HeadStart;


    // Use this for initialization
    void Start()
    {
        GroundStart = GameObject.Find("Ground").transform.position;
        LegStart = GameObject.Find("Leg").transform.position;
        BodydStart = GameObject.Find("Body_d").transform.position;
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
        

        GameObject Head = GameObject.Find("Head");
        Head.transform.position = new Vector3(0.0f, displayoffset, 0f) + HeadStart;

        GameObject Ground = GameObject.Find("Ground");
        Ground.transform.position = GroundStart + new Vector3(0,displayoffset - displayheight, 0);
        GameObject Leg = GameObject.Find("Leg");
        Leg.transform.position = LegStart + new Vector3(0, displayoffset - displayheight, 0);

        float GettingCutRatio;
        GettingCutRatio = 0.0f;
        

        GameObject Body_d = GameObject.Find("Body_d");
        float BodydPartSize = (1.0f - GettingCutRatio) * 0.3f * displayheight / 2.0f;
        Body_d.transform.position = BodydStart + new Vector3(0.0f,  (displayoffset - displayheight) / 2.0f , 0);
        Body_d.transform.localScale = new Vector3(Body_d.transform.localScale.x, BodydPartSize, 0.0f);

        GameObject Body_m = GameObject.Find("Body_m") ;
        float BodymPartSize = GettingCutRatio * displayheight / 2.0f;
        Body_m.transform.position = Body_d.transform.position + new Vector3(0.0f, BodymPartSize / 2.0f, 0.0f);
        Body_m.transform.localScale = new Vector3(Body_m.transform.localScale.x, BodymPartSize, Body_m.transform.localScale.z);

        GameObject Body_u = GameObject.Find("Body_u");
        float BodybPartSize = (1.0f - GettingCutRatio) * 0.7f * displayheight / 2.0f;
        Body_u.transform.position = Body_m.transform.position + new Vector3(0.0f, BodybPartSize / 2.0f, 0.0f);
        Body_u.transform.localScale = new Vector3(Body_u.transform.localScale.x, BodybPartSize, Body_u.transform.localScale.z);

        nebari -= 0.05f;
        if (nebari > 100f) { nebari = 100.0f; }
        if (nebari < 0f && accelaration>0.0f)
        {
            //GameOver

            // ちぎれる演出
            Head = GameObject.Find("Head");
            for (int i = 0; i < 64; i++)
            {
                Head.transform.position = Head.transform.position + new Vector3(0.0f, 0.1f, 0f) ;
                Head.transform.localScale = new Vector3(Head.transform.localScale.x, Head.transform.localScale.y * 0.99f, Head.transform.localScale.z);

                new WaitForSeconds(1.0f);
            }
            // プレハブからインスタンスを生成
            //Instantiate(prefab, new Vector3(Random.Range(-0.5f, 0.5f), 6 + Random.Range(0f, 0.1f), 0), Quaternion.identity);
            //new WaitForSeconds(0.05f);

        }

    }

    void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 100, 50), "Start : "  + startPosition.x.ToString() + "," + startPosition.y.ToString());
        GUI.Label(new Rect(20, 40, 100, 50), "End   : " + endPosition.x.ToString() + "," + endPosition.y.ToString());
        
        GUI.Label(new Rect(20, 60, 100, 50), "Accel   : " + accelaration.ToString());
        GUI.Label(new Rect(20, 80, 100, 50), "Nebari  : " + nebari.ToString());
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
        if (accelaration > 1.0f) { accelaration = 1.0f; }
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
