using UnityEngine;
using System.Collections;

public class nattou : MonoBehaviour
{

    private Vector2 StartPosition;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseEnter()
    {
        StartPosition = Input.mousePosition;
    }

    public void OnMouseExit()
    {
        Vector2 CurrentPosition = Input.mousePosition;

        GetComponent<Rigidbody2D>().AddForce(CurrentPosition - StartPosition,ForceMode2D.Impulse );
        DragArea.nebari += Time.deltaTime * 60; //ほんとは混ぜてる勢いを足したい

        //ネチャネチャ音
        GameObject NechaSE = GameObject.Find("Necha");
        NechaSE.GetComponent<AudioSource>().volume = 0.6f;
        NechaSE.GetComponent<AudioSource>().Play();

    }
}
