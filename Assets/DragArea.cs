using UnityEngine;
using System.Collections;

public class DragArea : MonoBehaviour
{


    Vector3 startPosition;
    Vector3 endPosition;
    float DragTime;
    float DragTimeDisplay;

    float height = 0.0f;
    float accelaration = 0.0f;


    // Use this for initialization
    void Start()
    {

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

        if (height < 10.0f)
        {
            GameObject Ground = GameObject.Find("Ground");
            Ground.transform.position = new Vector3(Ground.transform.position.x,  - 3.35f - height, 0);
        }
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
        accelaration += (((float)endPosition.y - (float)startPosition.y) / DragTime)/1000f;
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
