using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndManager : MonoBehaviour {

	// public GameObject scoreText;
	// public GameObject resultImage;
	// public GameObject contentsText;
	public Text scoreText;
	// public GameObject resultImage;
	public Image contentsImage;
	public Text contentsText;

	// 受け取ったスコア（スクリプト間の通信が必要）
	// public float score = 50.0f;
	float score;

	// Use this for initialization
	void Start () {
		score = 50.0f;

		if (0 <= score && score < 300) {
			// スコアの点数を表示するラベル、それぞれの茨城コンテンツ画像、茨城コンテンツコメントの順で。
			// それぞれのラベルはpublicにしておいて、Unityのインスペクタから指定できるように
			// ConfigureResultMenu(score.ToString(), Image, "a");
			//ConfigureResultMenu(score.ToString(), ,"a");
		} else if (300 <= score && score < 600) {
			// ConfigureResultMenu(score, Image, Text);
			ConfigureResultMenu(score.ToString(), "b");
		}
	}

	// Update is called once per frame
	void Update () {

	}

	public void OnClick() {
		Application.LoadLevel("Main");
	}

	// void ConfigureResultMenu(String scoreText, ,String contentsText) {
	void ConfigureResultMenu(string scoreText, string contentsText) {
		this.scoreText.text = scoreText;
		this.contentsText.text = contentsText;
	}
}
