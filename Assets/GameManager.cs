using UnityEngine;
using System.Collections;

using UnityEngine.UI;


public class GameManager : MonoBehaviour {

	Vector3 startPosition;
	Vector3 endPosition;

    public static bool InGame;

	// Use this for initialization
	void Start () {
        InGame = false;

}

// Update is called once per frame
void Update () 
    {
        // エスケープキー取得（Androidのバックボタン）
        if (Input.GetKey(KeyCode.Escape))
        {
            // 押されていればアプリケーション終了
            Application.Quit();
            return;
        }

    
        //ゲーム中でなければなにもしない
        if (InGame == false ) 
        { return; }
 
		// comment
        // comment by windows
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch(0);
			switch (touch.phase) {
				case TouchPhase.Began:
					startPosition = touch.position;
					break;
				case TouchPhase.Moved:
					// startPosition = touch.position;
					break;
				case TouchPhase.Ended:
					endPosition = touch.position;
					Debug.Log(endPosition);
					break;

			}
		}


	}

	void OnGUI() {

		float adjustedDisplayHeight = DragArea.displayheight;
		string meterLabel = "cm";

		if (adjustedDisplayHeight >= 100.0f) {
			adjustedDisplayHeight /= 100.0f;
			meterLabel = "m";

			if (adjustedDisplayHeight >= 1000.0f) {
				adjustedDisplayHeight /= 1000.0f;
				meterLabel = "km";
			}
		}

        Text  ScoreDisp = GameObject.Find("Score/Text").GetComponent<Text>();
        ScoreDisp.text = adjustedDisplayHeight.ToString("0.00") + meterLabel;
//		GUI.Label(new Rect(0, 0, (float)Screen.width , (float)Screen.height * 0.1f), adjustedDisplayHeight.ToString("0.0") + meterLabel);
	}
}
