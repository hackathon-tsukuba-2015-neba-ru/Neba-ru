using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	Vector3 startPosition;
	Vector3 endPosition;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
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
		GUI.Label(new Rect(20, 20, 100, 50), endPosition.x.ToString());
		GUI.Label(new Rect(20, 40, 100, 50), endPosition.y.ToString());
	}
}
