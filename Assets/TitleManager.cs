using UnityEngine;
using System.Collections;

public class TitleManager : MonoBehaviour {

    public GameObject prefab;

	// Use this for initialization
	void Start () {
        GameObject CreditImage = GameObject.Find("CreditImage");
        CreditImage.transform.position = new Vector3(5000 - CreditImage.transform.position.x, CreditImage.transform.position.y, CreditImage.transform.position.z);

	}

	// Update is called once per frame
	void Update () {

	}

    public void CreditDisplay()
    {
        GameObject CreditImage = GameObject.Find("CreditImage");
        CreditImage.transform.position = new Vector3(5000 - CreditImage.transform.position.x, CreditImage.transform.position.y, CreditImage.transform.position.z);

    }

	public void OnClick() {
    	Debug.Log("Button click!");

        // 納豆を生成
        for (int i = 0; i < 64; i++)
        {
            // プレハブからインスタンスを生成
            Instantiate(prefab, new Vector3(Random.Range(-0.5f, 0.5f), 6 + Random.Range(0f, 0.1f), 0), Quaternion.identity);
            new WaitForSeconds(0.1f);
        }

        GameObject  TitleSound = GameObject.Find("TitleSound");
        TitleSound.GetComponent<AudioSource>().Stop();

        GameManager.InGame = true;
        DragArea.DisplayHeightMax = 0.0f;

    	// 非表示にする
    	gameObject.SetActive(false);
  	}
}
