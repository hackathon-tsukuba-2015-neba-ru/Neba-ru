using UnityEngine;
using System.Collections;

public class DragArea : MonoBehaviour
{


    Vector3 startPosition;
    Vector3 endPosition;
    float DragTime;
    float DragTimeDisplay;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DragTime += Time.deltaTime;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 100, 50), "Start : "  + startPosition.x.ToString() + "," + startPosition.y.ToString());
        GUI.Label(new Rect(20, 60, 100, 50), "End   : " + endPosition.x.ToString() + "," + endPosition.y.ToString());
        
        GUI.Label(new Rect(20, 100, 100, 50), "DragTime   : " + DragTimeDisplay.ToString());
    }

    public void OnMouseDown()
    {
        startPosition = Input.mousePosition;
        DragTime = 0;
    }

    public void OnMouseUp()
    {
        endPosition = Input.mousePosition;
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
