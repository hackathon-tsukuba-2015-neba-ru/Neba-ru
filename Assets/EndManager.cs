using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndManager : MonoBehaviour {

	public Text scoreText;
	public Image contentsImage;
	public Text contentsText;
	public Sprite level1Sprite_1;
	public Sprite level1Sprite_2;
	public Sprite level2Sprite_1;
	public Sprite level2Sprite_2;
	public Sprite level2Sprite_3;
	public Sprite level2Sprite_4;
	public Sprite level3Sprite;
	public Sprite level4Sprite_1;
	public Sprite level4Sprite_2;

	public static float score;

	// Use this for initialization
	void Start () {
		score = DragArea.displayheight;

		if (0 <= score && score < 10) {
			int randomValue = Random.Range(0, 2);
			if (randomValue == 0) {
				ConfigureResultMenu(score.ToString(), level1Sprite_1, "世界最大級の仏像。立像です。");
			} else if (randomValue == 1) {
				ConfigureResultMenu(score.ToString(), level1Sprite_2, "1年間に納豆に対して払う金額はだいたい全国1位！毎年、全国平均の約2倍納豆にお金をかけているよ！");
			}
		} else if (10 <= score && score < 100) {
			int randomValue = Random.Range(0, 4);
			if (randomValue == 0) {
				ConfigureResultMenu(score.ToString(), level2Sprite_1, "メロンの１人当りの消費量日本一！メロンの生産量は全国２位。地元の旬を楽しめるよ！");
			} else if (randomValue == 1) {
				ConfigureResultMenu(score.ToString(), level2Sprite_2, "日本三公園のひとつ。世界で2番目に大きい公園！梅の季節（●月）には、毎年〇●人の人が訪れるよ！");
			} else if (randomValue == 2) {
				// ConfigureResultMenu(score.ToString(), level2Sprite_3, "水戸タワー");
				ConfigureResultMenu(score.ToString(), level2Sprite_4, "本州一の長さ橋から水面までの高さは100ｍ。バンジージャンプもできるよ！");
			} else if (randomValue == 3) {
				ConfigureResultMenu(score.ToString(), level2Sprite_4, "本州一の長さ橋から水面までの高さは100ｍ。バンジージャンプもできるよ！");
			}
		} else if (100 <= score && score < 1000) {
			ConfigureResultMenu(score.ToString(), level3Sprite, "日本三公園のひとつ。世界で2番目に大きい公園！");
		} else if (1000 <= score) {
			int randomValue = Random.Range(0, 2);
			if (randomValue == 0) {
				// ConfigureResultMenu(score.ToString(), level4Sprite_1, "イトカワ");
				ConfigureResultMenu(score.ToString(), level4Sprite_2, "橋から水面まで100mの間をバンジージャンプ！高層ビルで30階相当の高さを落ちます！");
			} else if (randomValue == 1) {
				ConfigureResultMenu(score.ToString(), level4Sprite_2, "橋から水面まで100mの間をバンジージャンプ！高層ビルで30階相当の高さを落ちます！");
			}
		}
	}

	// Update is called once per frame
	void Update () {

	}

	public void OnClick() {
		Application.LoadLevel("Main");
	}

	// void ConfigureResultMenu(String scoreText, ,String contentsText) {
	void ConfigureResultMenu(string scoreText, Sprite contentsSprite, string contentsText) {
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

		this.scoreText.text = adjustedDisplayHeight.ToString() + " " + meterLabel + "だねば〜！";
		this.contentsImage.sprite = contentsSprite;
		this.contentsText.text = contentsText;
	}
}
