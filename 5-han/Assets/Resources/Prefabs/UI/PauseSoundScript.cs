using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseSoundScript : MonoBehaviour
{

    public List<string> itemList;



    public Text yazirusiText;
    private Vector3 pos;//矢印の初期Pos

    public Text text;

    public Text message;

    private int yazirusiCout = 0;//矢印がどの段にいるかの


    private float yazirusiDelay = 0.2f;
    private float yazirusiDelay2 = 0.2f;
    private float dire = 180;
   // private float messegeDelay = 60;
    private float messeDire = 0;
    private int yazirusiMove = 90;
    private int height = 50;
    private int textWidthmove = 50;
    private int textHeightmove = 40;


    private bool delayFlag = false;
    private bool delayFlag2 = false;

    private AudioSource audioSource;
    public AudioClip senntakuSE;
    public AudioClip ketteiSE;

    public Text seCursor;
    public Text bgmCursor;

    private float alpha;
    private float alpha2;
    private bool alphaFlag = false;
    private bool alphaFlag2 = false;


    // Start is called before the first frame update
    void Start()
    {

        audioSource = GetComponent<AudioSource>();
        //矢印の初期Pos
        pos = yazirusiText.transform.position;

        //リストに入っているテキストをメニューに並べる(アイテム名)
        for (int i = 0; i < itemList.Count; i++)
        {
            text.text = itemList[i];


            Text instance =
                (Text)Instantiate(text,
                new Vector3(pos.x + 50.0f + textWidthmove, pos.y - 15 - height * i - i * textHeightmove, 0.0f), Quaternion.identity, this.transform);

            instance.transform.localScale = new Vector3(0.2f, 0.2f, 0.1f);
        }


    }

    float delay = 1.0f;
    IEnumerator Coroutine()
    {
        yield return new WaitForSecondsRealtime(delay);
        delayFlag = false;

        //ここに実行したい処理
    }

    // Update is called once per frame
    void Update()
    {
        if (!delayFlag)
        {
            message.text = "";
        }

        switch (Data.bgm)
        {
           
            case 0:
                bgmCursor.transform.position = new Vector3(910, 760, 0);
                break;
            case 1:
                bgmCursor.transform.position = new Vector3(1030, 760, 0);
                break;
            case 2:
                bgmCursor.transform.position = new Vector3(1150, 760, 0);//1150
                break;
            case 3:
                bgmCursor.transform.position = new Vector3(1270, 760, 0);//1150
                break;

        }


        switch (Data.se)
        {

            case 0:
                seCursor.transform.position = new Vector3(910, 660, 0);
                break;
            case 1:
                seCursor.transform.position = new Vector3(1030, 660, 0);
                break;
            case 2:
                seCursor.transform.position = new Vector3(1150, 660, 0);//1150
                break;
            case 3:
                seCursor.transform.position = new Vector3(1270, 660, 0);//1150
                break;

        }

        audioSource.volume = Data.seVol;



        // ディレイ関係
        //矢印移動ディレイ
        if (yazirusiDelay > 0)
        {
            yazirusiDelay -= dire * Time.deltaTime;
        }
        if (yazirusiDelay2 > 0)
        {
            yazirusiDelay2 -= dire * Time.deltaTime;
        }
        //スティックの縦方向取得
        float vert = Input.GetAxis("Vertical");
        float vert2 = Input.GetAxis("CrossUpDown");

        float hol = Input.GetAxis("Horizontal");
        float hol2 = Input.GetAxis("CrossUpDown");

        float stick = 0.5f;

        //メニューの矢印制御
        if ( vert > stick &&yazirusiCout > 0 && yazirusiDelay <= 0  && !delayFlag2)
        {
            yazirusiText.transform.position =
                new Vector3(yazirusiText.transform.position.x,
                            yazirusiText.transform.position.y + yazirusiMove,
                            yazirusiText.transform.position.z);
            yazirusiCout -= 1;
            StartCoroutine(Coroutine2());
            SentakuSEPlay();
        }
        else if (vert2 > stick && (yazirusiCout > 0 && yazirusiDelay2 <= 0) && !delayFlag2)
        {
            yazirusiText.transform.position =
                new Vector3(yazirusiText.transform.position.x,
                            yazirusiText.transform.position.y + yazirusiMove,
                            yazirusiText.transform.position.z);
            yazirusiCout -= 1;
            StartCoroutine(Coroutine2());
            SentakuSEPlay();
        }
        else if (vert < -stick && yazirusiCout < itemList.Count - 1 && yazirusiDelay <= 0 && !delayFlag2)
        {
            yazirusiText.transform.position =
               new Vector3(yazirusiText.transform.position.x,
                           yazirusiText.transform.position.y - yazirusiMove,
                           yazirusiText.transform.position.z);
            yazirusiCout += 1;
            StartCoroutine(Coroutine2());
            SentakuSEPlay();
        }
        else if (vert2 < -stick && yazirusiCout < itemList.Count - 1 && yazirusiDelay2 <= 0 && !delayFlag2)
        {
            yazirusiText.transform.position =
               new Vector3(yazirusiText.transform.position.x,
                           yazirusiText.transform.position.y - yazirusiMove,
                           yazirusiText.transform.position.z);
            yazirusiCout += 1;
            StartCoroutine(Coroutine2());
            SentakuSEPlay();
        }

        if (Input.GetKeyDown("joystick button 0"))
        {
           
            StartCoroutine(Coroutine());
        }


     
        if (itemList[yazirusiCout] == "BGM")
        {
            if (alphaFlag == false)
            {
                StartCoroutine(CoroutineDown());
                
            }
            else if (alphaFlag == true)
            {
                StartCoroutine(CoroutineUp());
            }
            bgmCursor.color = new Color(bgmCursor.color.r, bgmCursor.color.g, bgmCursor.color.b,alpha);

            

            if (hol > stick && Data.bgm < 3 && yazirusiDelay <= 0 && !delayFlag2)
            {
                Data.bgm++;
                StartCoroutine(Coroutine2());
                SentakuSEPlay();
            }
            //else if (hol2 > stick && (yazirusiCout > 0 && yazirusiDelay2 <= 0) && !delayFlag2)
            //{
             
            //    StartCoroutine(Coroutine2());
               
            //}
            else if (hol < -stick && Data.bgm > 0 && yazirusiDelay <= 0 && !delayFlag2)
            {
                Data.bgm--;
                StartCoroutine(Coroutine2());
                SentakuSEPlay();
            }
            //else if (hol2 < -stick && yazirusiCout < itemList.Count - 1 && yazirusiDelay2 <= 0 && !delayFlag2)
            //{
            //    yazirusiText.transform.position =
            //       new Vector3(yazirusiText.transform.position.x,
            //                   yazirusiText.transform.position.y - yazirusiMove,
            //                   yazirusiText.transform.position.z);
            //    yazirusiCout += 1;
            //    StartCoroutine(Coroutine2());
            //    SentakuSEPlay();
            //}

        }
        else
        {
            if(alpha < 1)
            {
                StartCoroutine(CoroutineUp());
                bgmCursor.color = new Color(bgmCursor.color.r, bgmCursor.color.g, bgmCursor.color.b, alpha);
            }
           
        }

        if (itemList[yazirusiCout] == "SE")
        {

            if (alphaFlag2 == false)
            {
                StartCoroutine(CoroutineDown2());

            }
            else if (alphaFlag2 == true)
            {
                StartCoroutine(CoroutineUp2());
            }
            seCursor.color = new Color(seCursor.color.r, seCursor.color.g, seCursor.color.b, alpha2);

            if (hol > stick && Data.se < 3 && yazirusiDelay <= 0 && !delayFlag2)
            {
                Data.se++;
                StartCoroutine(Coroutine2());
                SentakuSEPlay();

            }
            //else if (hol2 > stick && (yazirusiCout > 0 && yazirusiDelay2 <= 0) && !delayFlag2)
            //{

            //    StartCoroutine(Coroutine2());

            //}
            else if (hol < -stick && Data.se > 0 && yazirusiDelay <= 0 && !delayFlag2)
            {
                Data.se--;
                StartCoroutine(Coroutine2());
                SentakuSEPlay();

            }
            //else if (hol2 < -stick && yazirusiCout < itemList.Count - 1 && yazirusiDelay2 <= 0 && !delayFlag2)
            //{
            //    yazirusiText.transform.position =
            //       new Vector3(yazirusiText.transform.position.x,
            //                   yazirusiText.transform.position.y - yazirusiMove,
            //                   yazirusiText.transform.position.z);
            //    yazirusiCout += 1;
            //    StartCoroutine(Coroutine2());
            //    SentakuSEPlay();
            //}
        }
        else
        {
            if(alpha2 < 1)
            {
                StartCoroutine(CoroutineUp2());
                seCursor.color = new Color(seCursor.color.r, seCursor.color.g, seCursor.color.b, alpha2);
            }
          
        }



        //メニュー閉じる処理
        if (Input.GetKeyDown("joystick button 0") && yazirusiCout == itemList.Count - 1)
        {
            Data.pauseWindFlag = false;
            Destroy(this.gameObject.transform.parent.parent.gameObject);
            //Time.timeScale = 1f;
            return;
        }


        if (vert > -0.3f && vert < 0.3f)
        {
            yazirusiDelay = 0;
        }
        if (vert2 > -0.3f && vert2 < 0.3f)
        {
            yazirusiDelay2 = 0;
        }
    }

    float delay2 = 0.2f;
    IEnumerator Coroutine2()
    {
        delayFlag2 = true;
        yield return new WaitForSecondsRealtime(delay2);
        delayFlag2 = false;
    }


    float delay3 = 0.075f;
    IEnumerator CoroutineUp()
    {
        while(alpha <= 1)
        {
            yield return new WaitForSecondsRealtime(delay3);
            alpha += 0.01f;
        }
        alphaFlag = false;
    }
    IEnumerator CoroutineDown()
    {
        while (alpha > 0.2f)
        {
            yield return new WaitForSecondsRealtime(delay3);
            alpha -= 0.01f;
        }
        alphaFlag = true;
    }


    IEnumerator CoroutineUp2()
    {
        while (alpha2 < 1)
        {
            yield return new WaitForSecondsRealtime(delay3);
            alpha2 += 0.01f;
        }
        alphaFlag2 = false;
    }
    IEnumerator CoroutineDown2()
    {
        while (alpha2 > 0.2f)
        {
            yield return new WaitForSecondsRealtime(delay3);
            alpha2 -= 0.01f;
        }
        alphaFlag2 = true;
    }


    public void SentakuSEPlay()
    {
        audioSource.PlayOneShot(senntakuSE);
    }

    public void KetteiSEPlay()
    {
        audioSource.PlayOneShot(ketteiSE);
    }

    float time;
    float speed = 1.0f;

    Color GetAlphaColor(Color color)
    {
        time += Time.deltaTime * 5 * speed;
        color.a = Mathf.Sign(time);
        return color;
    }

}
