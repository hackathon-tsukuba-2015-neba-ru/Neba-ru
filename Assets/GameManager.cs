using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameObject prefab;
	Vector3 startPosition;
	Vector3 endPosition;

	// Use this for initialization
	void Start () {

        // 納豆を生成
        for (int i = 0; i < 64; i++)
        {
            // プレハブからインスタンスを生成
            Instantiate(prefab, new Vector3(Random.Range(-0.5f,0.5f),6 + Random.Range(0f,0.1f),0), Quaternion.identity);
            new WaitForSeconds(0.05f);
        }

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
