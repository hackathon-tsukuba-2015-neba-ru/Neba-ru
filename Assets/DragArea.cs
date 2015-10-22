using UnityEngine;
using System.Collections;

public class DragArea : MonoBehaviour
{

    public static float nebari;
    public GameObject resultPrefab;

    bool HasMusicStarted;

    Vector3 startPosition;
    Vector3 endPosition;
    float DragTime;
    float DragTimeDisplay;
    float TotalGameTime;

    float height = 0.0f;
    float accelaration = 0.0f;
    public static float displayheight;
    public static float DisplayHeightMax;

    Vector3 GroundStart;
    Vector3 LegStart;
    Vector3 BodydStart;
    Vector3 HeadStart;

    int InstStep = 0;
    float InstPosX, InstPosY, InstPerm;

    float Dying;
    bool ResultDisplayed;
    bool PlaneDone,RocketDone,UFODone;

    float Cloud1X, Cloud1Y = -100,Cloud1Spd;
    float Cloud2X, Cloud2Y = -100,Cloud2Spd;


    // Use this for initialization
    void Start()
    {
        GroundStart = GameObject.Find("Ground").transform.position;
        LegStart = GameObject.Find("Leg").transform.position;
        BodydStart = GameObject.Find("Body").transform.position;
        HeadStart = GameObject.Find("HeadDown").transform.position;

        HasMusicStarted = false;
        height = 0;
        displayheight = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //既に死んでいるときは死んでいる演出
        if (Dying > 0)
        {
            Dying += Time.deltaTime;

            GameObject GameBGM = GameObject.Find("GameBGM");
            GameBGM.GetComponent<AudioSource>().volume = (2.0f - Dying) / 2.0f;


            // ちぎれる演出
            GameObject DyingHead = GameObject.Find("HeadUp");
            DyingHead.transform.position = DyingHead.transform.position + new Vector3(0.0f, 0.1f, 0f);
            DyingHead.transform.localScale = new Vector3(DyingHead.transform.localScale.x * 0.99f, DyingHead.transform.localScale.y, DyingHead.transform.localScale.z);
            GameObject DyingBody = GameObject.Find("doutai_ext_02");
            DyingBody.transform.position = DyingBody.transform.position + new Vector3(0.0f, 0.1f, 0f);
            DyingBody.transform.localScale = new Vector3(DyingBody.transform.localScale.x * 0.99f, DyingBody.transform.localScale.y, DyingBody.transform.localScale.z);

            if (Dying>3.0f )
            {
                if (ResultDisplayed==false)
                {
                    // 茨城豆知識表示
                    Instantiate(resultPrefab, new Vector3(0, 0, 0), Quaternion.identity);

                    GameObject GameOverBGM = GameObject.Find("GameOverBGM");
                    GameOverBGM.GetComponent<AudioSource>().Play();

                    ResultDisplayed = true;

                }
            }

            return;
        }

        //ゲーム中でなければなにもしない
        if (GameManager.InGame == false)
        {
            return;
        }


        DragTime += Time.deltaTime;
        accelaration -= (Time.deltaTime * 0.01f);
        height += accelaration;
        if (height < 0.0f)
        {
            accelaration = 0.0f;
            height = 0.0f;
        }

        displayheight = (float)System.Math.Pow((double)height, 1.5);
        if (displayheight>DisplayHeightMax )
        {
            DisplayHeightMax = displayheight;
        }

        //GameObject Body = GameObject.Find("Body");
        //Body.transform.localScale   = new Vector3(0.0f, displayheight, 0f);
        //Body.transform.position = new Vector3(0.0f, displayheight / 2.0f, 0f) + BodyStart;


        //少し伸びてきたらBGMスタート
        if (  height > 100)
        {
            GameObject TitleSound = GameObject.Find("GameBGM");

            if (TitleSound.GetComponent<AudioSource>().volume < 1.0f)
            {
                TitleSound.GetComponent<AudioSource>().volume = TitleSound.GetComponent<AudioSource>().volume + 0.001f;
            }

            if (HasMusicStarted == false)
            {
                TitleSound.GetComponent<AudioSource>().Play();
                TitleSound.GetComponent<AudioSource>().volume = 0.0f;
                HasMusicStarted = true;
            }
        }


        //各種画面効果
        // 伸び始めのスクロール調整
        float displayoffsety = 2.0f;
        if (displayheight < 10.0f)
        {
            displayoffsety = 2.0f - ((float)System.Math.Pow(10.0 - (double)displayheight, 2.0) / 50.0f);
        }
        // スピードが上がってきたときにブレる
        displayoffsety += (float)System.Math.Sin((double)displayheight / 100.0) * 0.2f;
        float displayoffsetx = (float)System.Math.Sin((double)displayheight / 150.0) * 0.2f;
        if (height > 2000.0f)
        {
            displayoffsety += Random.Range(-height / 30000.0f, height / 30000.0f);
            displayoffsetx += Random.Range(-height / 30000.0f, height / 30000.0f);
        }
        // かなり伸びてきたときにカメラを引く
        float CameraSize = 6.0f;
        if (height > 1000.0f)
        {
            CameraSize = 6.0f + (height - 1000.0f) / 3000.0f;
        }
        GameObject MyCamera = GameObject.Find("Main Camera");
        MyCamera.GetComponent<Camera>().orthographicSize = CameraSize;

        //ねばーるくんの体の部品の位置調整
        GameObject Head;
        if (accelaration > 0.0f)
        {
            Head = GameObject.Find("HeadDown");
            Head.transform.position = new Vector3(1000.0f, 1000.0f, 0f) + HeadStart;
            Head = GameObject.Find("HeadUp");
        }
        else
        {
            Head = GameObject.Find("HeadUp");
            Head.transform.position = new Vector3(1000.0f, 1000.0f, 0f) + HeadStart;
            Head = GameObject.Find("HeadDown");
        }
        SpriteRenderer HeadSPRen = Head.GetComponent<SpriteRenderer>();
        Head.transform.position = new Vector3(displayoffsetx, displayoffsety, 0f) + HeadStart;
        Transform MyTran = Head.transform;
        //Head.transform.eulerAngles = new Vector3(0, 0, 0);


        float GettingCutRatio =  (100.0f - nebari) / 100.0f;

        float BodyCutPartSize;
        GameObject BodyExt;
        BodyCutPartSize = GettingCutRatio * Head.transform.localScale.y * 6.0f;
        if (displayheight < 6.0f) { BodyCutPartSize = GettingCutRatio * Head.transform.localScale.y * displayheight; }
        BodyExt = GameObject.Find("doutai_ext_01");
        BodyExt.transform.localScale = new Vector3(BodyExt.transform.localScale.x, BodyCutPartSize, 0.0f);
        SpriteRenderer BodyExtSPRen = BodyExt.GetComponent<SpriteRenderer>();
        BodyExt.transform.position = Head.transform.position - new Vector3(0.0f, HeadSPRen.bounds.size.y / 2.0f + BodyExtSPRen.bounds.size.y / 2.0f, 0);

        GameObject BodyExt2;
        BodyExt2 = GameObject.Find("doutai_ext_02");
//        BodyExt.transform.localScale = new Vector3(BodyExt.transform.localScale.x * (0.75f + GettingCutRatio * 0.25f), BodyCutPartSize * 0.95f, 0.0f);
        BodyExt2.transform.localScale = new Vector3(BodyExt.transform.localScale.x * (1.5f - GettingCutRatio * 0.7f), BodyCutPartSize * 0.95f, 0.0f);
        BodyExt2.transform.position = Head.transform.position - new Vector3(0.0f, HeadSPRen.bounds.size.y / 2.1f + BodyExtSPRen.bounds.size.y / 2.0f, 0);


        GameObject Body = GameObject.Find("Body");
        float BodyPartSize = displayheight * 0.35f / 2.0f;
        if (BodyPartSize < 0.0f) { BodyPartSize = 0.0f; }
        Body.transform.localScale = new Vector3(Body.transform.localScale.x, BodyPartSize, 0.0f);
        SpriteRenderer BodySPRen = Body.GetComponent<SpriteRenderer>();
        Body.transform.position = BodyExt.transform.position - new Vector3(0.0f, BodyExtSPRen.bounds.size.y / 2.0f + BodySPRen.bounds.size.y / 2.0f, 0);

        GameObject Leg = GameObject.Find("Leg");
        SpriteRenderer LegSPRen = Leg.GetComponent<SpriteRenderer>();
        Leg.transform.position = Body.transform.position - new Vector3(0.0f, BodySPRen.bounds.size.y / 2.0f + LegSPRen.bounds.size.y / 2.0f, 0);
        GameObject Ground = GameObject.Find("Ground");
        Ground.transform.position = Leg.transform.position - new Vector3(0.0f, 1.0f, 0);

        //進行状況に応じたインストラクション
        GameObject InstSE;
        GameObject Inst;
        switch (InstStep)
            {
            case 0:
                InstSE = GameObject.Find("InstSE");
                InstSE.GetComponent<AudioSource>().Play();
                InstPerm += Time.deltaTime;
                if (InstPerm>2)
                {
                    InstPerm = 0;
                    InstStep = 1;
                    TotalGameTime = 0;
                }
                break;
            case 1:
                Inst = GameObject.Find("Inst1");
                InstPerm += Time.deltaTime;
                Inst.transform.position = new Vector3(0.0f, (float)System.Math.Sin((double)InstPerm * 10.0 ) * 0.5f - 2.0f, 0f);
                if (nebari > 50)
                {
                    Destroy(Inst);
                    InstSE = GameObject.Find("InstSE");
                    InstSE.GetComponent<AudioSource>().Play();
                    InstPerm = 0;
                    InstStep = 2;
                }
                break;
            case 2:
                Inst = GameObject.Find("Inst2");
                InstPerm += Time.deltaTime;
                Inst.transform.position = new Vector3(0.0f, (float)System.Math.Sin((double)InstPerm * 10.0 ) * 0.5f + 3.0f, 0f);
                if (height > 50.0f)
                {
                    Destroy(Inst);
                    InstPerm = 0;
                    InstStep = 3;
                }

                break;
            case 3:
                if (nebari < 30)
                {
                    InstSE = GameObject.Find("InstSE");
                    InstSE.GetComponent<AudioSource>().Play();
                    InstPerm = 0;
                    InstStep = 4;
                }

                break;
            case 4:
                Inst = GameObject.Find("Inst3");
                InstPerm += Time.deltaTime;
                Inst.transform.position = new Vector3(0.0f, (float)System.Math.Sin((double)InstPerm * 10.0 ) * 0.5f + 3.0f, 0f);
                if (nebari > 50)
                {
                    Destroy(Inst);
                    InstStep = 5;
                }
                break;


            }

        //高さに応じた演出
        ////凧
        if (1000.0f < displayheight && displayheight < 3000.0f)//10-30メートル
        {
            GameObject Kite = GameObject.Find("凧");
            Kite.transform.position = new Vector3(displayheight / 100.0f - 15.0f,  4.0f - displayheight / 300.0f  , 0.0f);
            //Kite.transform.position = new Vector3(0.1f, 0.1f, 0.0f);
        }

        //高さに応じた演出
        ////牛久大仏
        if (6000.0f < displayheight && displayheight < 18000.0f)//60-180メートル
        {
            GameObject Kite = GameObject.Find("牛久大仏");
            Kite.transform.position = new Vector3(40.0f - displayheight / 300.0f , 20.0f - displayheight / 300.0f, 0.0f);
            //Kite.transform.position = new Vector3(0.1f, 0.1f, 0.0f);
        }


        //高さに応じた演出
        ////筑波山
        if (60000.0f < displayheight && displayheight < 120000.0f)//500-1200メートル
        {
            GameObject MtTsukuba = GameObject.Find("筑波山");
            MtTsukuba.transform.position = new Vector3(0.0f, (100000.0f - displayheight) / 1200.0f - 25.0f, 0.0f);
            SpriteRenderer MtTsukubaSPRen = MtTsukuba.GetComponent<SpriteRenderer>();
            Color MtTsukubaCol = MtTsukubaSPRen.color;
            MtTsukubaCol.a = 1.0f;
            if (displayheight < 80000.0f)
            {
                MtTsukubaCol.a = (displayheight - 60000.0f) / 20000.0f;
            }
            MtTsukubaSPRen.color = MtTsukubaCol;

        }

        //高さに応じた演出
        ////雲
        if (100000.0f < displayheight && displayheight < 1600000.0f)//1000-12000メートルの間は雲を動かす
        {
            if (110000.0f < displayheight && displayheight < 1550000.0f)//1100-9000メートルの間は必要に応じて雲を発生させる
            {
                if (Cloud1Y  <  - 20 ||  20 < Cloud1Y )
                {
                    Cloud1Y = ((int)(Random.Range(0, 2)+0.5)) * 40 - 20  ;
                    Cloud1X = Random.Range(-6.0f, 6.0f);
                    Cloud1Spd = Random.Range(0, 20) + 10;
                }
                if (Cloud2Y <  - 20 || 20 < Cloud2Y)
                {
                    Cloud2Y = ((int)(Random.Range(0, 2)+0.5)) * 40 - 20;
                    Cloud2X = Random.Range(-6.0f, 6.0f);
                    Cloud2Spd = Random.Range(0, 20) + 10;
                }
            }

            GameObject Cloud;
            Cloud1Y -= (displayheight * Cloud1Spd / 20000000.0f) * accelaration;
            Cloud = GameObject.Find("雲１");
            Cloud.transform.localScale = new Vector3((float)Cloud1Spd / 6.0f, (float)Cloud1Spd / 6.0f, 1.0f);
            Cloud.transform.position = new Vector3(Cloud1X, Cloud1Y, 0.0f);

            Cloud2Y -= (displayheight * Cloud2Spd / 20000000.0f) * accelaration;
            Cloud = GameObject.Find("雲２");
            Cloud.transform.localScale = new Vector3((float)Cloud2Spd / 6.0f, (float)Cloud2Spd / 6.0f, 1.0f);
            Cloud.transform.position = new Vector3(Cloud2X, Cloud2Y, 0.0f);
        }

        //高さに応じた演出
        ////星屑
        if (1600000.0f < displayheight && displayheight < 3000000.0f)//1000-12000メートルの間は雲を動かす
        {
            if (1650000.0f < displayheight && displayheight < 2950000.0f)//1100-9000メートルの間は必要に応じて雲を発生させる
            {
                if (Cloud1Y  <  - 20 ||  20 < Cloud1Y )
                {
                    Cloud1Y = ((int)(Random.Range(0, 2)+0.5)) * 40 - 20  ;
                    Cloud1X = Random.Range(-5.0f, 5.0f);
                    Cloud1Spd = Random.Range(0, 20) + 10;
                }
                if (Cloud2Y <  - 20 || 20 < Cloud2Y)
                {
                    Cloud2Y = ((int)(Random.Range(0, 2)+0.5)) * 40 - 20;
                    Cloud2X = Random.Range(-5.0f, 5.0f);
                    Cloud2Spd = Random.Range(0, 20) + 10;
                }
            }

            GameObject Cloud;
            Cloud1Y -= (displayheight * Cloud1Spd / 50000000.0f) * accelaration;
            Cloud = GameObject.Find("星屑１");
            Cloud.transform.localScale = new Vector3((float)Cloud1Spd / 6.0f, (float)Cloud1Spd / 6.0f, 1.0f);
            Cloud.transform.position = new Vector3(Cloud1X, Cloud1Y, 0.0f);

            Cloud2Y -= (displayheight * Cloud2Spd / 50000000.0f) * accelaration;
            Cloud = GameObject.Find("星屑２");
            Cloud.transform.localScale = new Vector3((float)Cloud2Spd / 6.0f, (float)Cloud2Spd / 6.0f, 1.0f);
            Cloud.transform.position = new Vector3(Cloud2X, Cloud2Y, 0.0f);
        }

        //高さに応じた演出
        ////飛行機
        if (500000.0f < displayheight && PlaneDone==false )//5000メートル
        {
            GameObject Plane = GameObject.Find("飛行機");
            Plane.transform.position = new Vector3(20.0f, 3.0f, 0.0f);
            Rigidbody2D  PlaneRigidBody = Plane.GetComponent<Rigidbody2D>();
            PlaneRigidBody.AddForce(new Vector2(-10.0f, -1.0f),ForceMode2D.Impulse );

            GameObject PlaneSE = GameObject.Find("PlaneSE");
            PlaneSE.GetComponent<AudioSource>().Play();

            PlaneDone = true;

        }

        //高さに応じた演出
        ////ロケット
        if (1500000.0f < displayheight && RocketDone == false)//15000メートル
        {
            GameObject Rocket = GameObject.Find("H2Aロケット");
            Rocket.transform.position = new Vector3(-30.0f, -25.0f, 0.0f);
            Rigidbody2D RocketRigidBody = Rocket.GetComponent<Rigidbody2D>();
            RocketRigidBody.AddForce(new Vector2(10.0f, 8.0f), ForceMode2D.Impulse);

            GameObject RocektSE = GameObject.Find("RocketSE");
            RocektSE.GetComponent<AudioSource>().Play();

            RocketDone = true;

        }

        //高さに応じた演出
        ////宇宙
        if (1500000.0f < displayheight && displayheight < 2500000.0f)//500-1200メートル
        {
            GameObject Sky = GameObject.Find("空");
            SpriteRenderer SkySPRen = Sky.GetComponent<SpriteRenderer>();
            Color SkyCol = SkySPRen.color;
            SkyCol.a = (2500000.0f - displayheight) / 1000000.0f;
            SkySPRen.color = SkyCol;
        }



        //高さに応じた演出
        ////UFO
        if (3000000.0f < displayheight && UFODone == false)//30000メートル
        {
            GameObject UFO = GameObject.Find("UFO");
            UFO.transform.position = new Vector3(-20.0f, 2.0f, 0.0f);
            Rigidbody2D UFORigidBody = UFO.GetComponent<Rigidbody2D>();
            UFORigidBody.AddForce(new Vector2(10.0f, -1.0f), ForceMode2D.Impulse);

            GameObject UFOSE = GameObject.Find("UFOSE");
            UFOSE.GetComponent<AudioSource>().Play();

            UFODone = true;

        }

        TotalGameTime += Time.deltaTime;
        //nebari -= (0.05f + TotalGameTime * 0.0002f);
        nebari -= (Time.deltaTime * 5.0f + TotalGameTime * Time.deltaTime *  0.02f);
        if (nebari > 100f) { nebari = 100.0f; }
        if (nebari < 0f && accelaration>0.0f)
        {
            //GameOver
            GameManager.InGame = false;
            Dying = 0.1f;
            ResultDisplayed = false;

            GameObject GameBGM = GameObject.Find("DeathSound");
            GameBGM.GetComponent<AudioSource>().Play();


        }

        if (nebari < 0f) { nebari = 0.0f;  }

    }

    void OnGUI()
    {
        //ゲーム中でなければなにもしない
        if (GameManager.InGame == false)
        {
            return;
        }

       
        //GUI.Label(new Rect(0, (int)(Screen.height * 0.0), Screen.width , (int)(Screen.height*0.1)), "Start : "  + startPosition.x.ToString() + "," + startPosition.y.ToString());
        //GUI.Label(new Rect(0, (int)(Screen.height * 0.1), Screen.width, (int)(Screen.height * 0.1)), "End   : " + endPosition.x.ToString() + "," + endPosition.y.ToString());

        GUI.Label(new Rect(0, (int)(Screen.height * 0.2), Screen.width, (int)(Screen.height * 0.1)), "Accel   : " + accelaration.ToString());
        GUI.Label(new Rect(0, (int)(Screen.height * 0.3), Screen.width , (int)(Screen.height*0.1)), "Nebari  : " + nebari.ToString());
    }


    public void OnMouseDown()
    {
        //ゲーム中でなければなにもしない
        if (GameManager.InGame == false)
        {
            return;
        }

        startPosition = Input.mousePosition;
        DragTime = 0;
    }

    public void OnMouseUp()
    {
        //ゲーム中でなければなにもしない
        if (GameManager.InGame == false)
        {
            return;
        }

        //インストステップ2に達していないときはなにもしない
        if (InstStep <2)
        {
            return;
        }


        endPosition = Input.mousePosition;
        accelaration += (((float)endPosition.y - (float)startPosition.y) / DragTime)/50000f;
        if (accelaration > 1.0f) { accelaration = 1.0f; }

        GameObject ExtSE = GameObject.Find("ExtSE");
        ExtSE.GetComponent<AudioSource>().volume=0.4f;
        ExtSE.GetComponent<AudioSource>().Play();


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
