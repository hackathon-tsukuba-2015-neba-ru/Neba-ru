﻿using UnityEngine;
using System.Collections;

public class DragArea : MonoBehaviour
{


    Vector3 startPosition;
    Vector3 endPosition;
    float DragTime;
    float DragTimeDisplay;

    float height = 0.0f;
    float accelaration = 0.0f;

    Vector3 GroundStart;
    Vector3 LegStart;
    Vector3 BodyStart;

    // Use this for initialization
    void Start()
    {
         GroundStart = GameObject.Find("Ground").transform.position;
         LegStart = GameObject.Find("Leg").transform.position;
        BodyStart = GameObject.Find("Body").transform.position;

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
            // Move ground
            GameObject Ground = GameObject.Find("Ground");
            Ground.transform.position = GroundStart + new Vector3(0,-height,0);
            //Move Leg
            GameObject Leg = GameObject.Find("Leg");
            Leg.transform.position = LegStart + new Vector3(0, -height, 0);
            // Move body and Strech
            GameObject Body = GameObject.Find("Body");
            Body.transform.position = BodyStart + new Vector3(0, -height/2.0f, 0);
            Body.transform.localScale = new Vector3(0.7f,height/2.0f + 0.2f,0f);


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
