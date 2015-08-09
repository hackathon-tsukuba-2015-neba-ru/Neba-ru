using UnityEngine;
using System.Collections;

public class TitleManager : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void OnClick() {
    	Debug.Log("Button click!");

    	// 非表示にする
    	gameObject.SetActive(false);
  	}
}
