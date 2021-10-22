using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseItemScript : MonoBehaviour
{

    public List<string> itemList;

    //private List<int> haveItems;
    //private List<Text> haveItemText;

    public Text yazirusiText;
    private Vector3 pos;//矢印の初期Pos

    public Text text;
    public Text message;

    private int yazirusiCout = 0;//矢印がどの段にいるかの

    private List<string> itemHavelistName = new List<string>();
    private List<int> itemHavelistNum = new List<int>();
    private List<Text> itemStringTexts = new List<Text>();
    private List<Text> itemNumTexts = new List<Text>();



    private float yazirusiDelay = 0.2f;
    private float yazirusiDelay2 = 0.2f;
    private float dire = 180;
    private float messegeDelay = 60;
    private float messeDire = 0;
    private int yazirusiMove = 90;
    private int height = 50;
    private int textWidthmove = 50;  
    private int textHeightmove = 40;


    private bool delayFlag = false;

    private GameObject player;
    private PlayerControl playerControl;

    public GameObject playerHPUI;
    public PlayerHpGauge hpGauge;

    private AudioSource audioSource;
    public AudioClip ketteiSE;
    public AudioClip senntakuSE;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerControl = player.GetComponent<PlayerControl>();

        if(GameObject.Find("HPBar"))
        {
            playerHPUI = GameObject.Find("HPBar");
            hpGauge = playerHPUI.GetComponent<PlayerHpGauge>();
        }

        //矢印の初期Pos
        pos = yazirusiText.transform.position;
        Data.GetPauseMenuItemCount();

        //リストに入っているテキストをメニューに並べる(アイテム名)
        for (int i = 0; i < Data.dataItemStringList.Count ; i++)
        {
            text.text = Data.dataItemStringList[i];

            Text instance =
                (Text)Instantiate(text,
                new Vector3(pos.x + 50.0f + textWidthmove, pos.y - 15 - height * i - i * textHeightmove, 0.0f), Quaternion.identity, this.transform);

            instance.transform.localScale = new Vector3(0.2f, 0.2f, 0.1f);
            itemHavelistName.Add(Data.dataItemStringList[i]);
            itemStringTexts.Add(instance);
        }

        //リストに入っているテキストをメニューに並べる(アイテム数)
        for (int i = 0; i < Data.dataItemIntList.Count; i++)
        {
            text.text = "(" + Data.dataItemIntList[i].ToString() + ")";

            Text instance =
                (Text)Instantiate(text,
                new Vector3(pos.x + 300.0f + textWidthmove, pos.y - 15 - height * i - i * textHeightmove, 0.0f), Quaternion.identity, this.transform);

            instance.transform.localScale = new Vector3(0.2f, 0.2f, 0.1f);
            //haveItemText.Add(instance);
            itemNumTexts.Add(instance);
            itemHavelistNum.Add(Data.dataItemIntList[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
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
        //メッセージディレイ
        if (messeDire > 0)
        {
            messeDire -= messegeDelay * Time.deltaTime;
        }
        else
        {
            message.text = "";
        }

        ItemCheckUpdate();

        //スティックの縦方向取得
        float vert = Input.GetAxis("Vertical");
        float vert2 = Input.GetAxis("CrossUpDown");
        //メニューの矢印制御
        if (vert > 0.3f && yazirusiCout > 0 && yazirusiDelay <= 0 && !delayFlag)
        {
            yazirusiText.transform.position =
                new Vector3(yazirusiText.transform.position.x,
                            yazirusiText.transform.position.y + yazirusiMove,
                            yazirusiText.transform.position.z);
            yazirusiCout -= 1;
            StartCoroutine(Coroutine());
            SentakuSEPlay();
        }
        else if (vert2 > 0.3f && yazirusiCout > 0 && yazirusiDelay2 <= 0 && !delayFlag)
        {
            yazirusiText.transform.position =
                new Vector3(yazirusiText.transform.position.x,
                            yazirusiText.transform.position.y + yazirusiMove,
                            yazirusiText.transform.position.z);
            yazirusiCout -= 1;
            StartCoroutine(Coroutine());
            SentakuSEPlay();
        }
        else if (vert < -0.3f && yazirusiCout < Data.dataItemStringList.Count - 1 && yazirusiDelay <= 0 && !delayFlag)
        {
            yazirusiText.transform.position =
               new Vector3(yazirusiText.transform.position.x,
                           yazirusiText.transform.position.y - yazirusiMove,
                           yazirusiText.transform.position.z);
            yazirusiCout += 1;
            StartCoroutine(Coroutine());
            SentakuSEPlay();
        }
        else if (vert2 < -0.3f && yazirusiCout < Data.dataItemStringList.Count - 1 && yazirusiDelay2 <= 0 && !delayFlag)
        {
            yazirusiText.transform.position =
               new Vector3(yazirusiText.transform.position.x,
                           yazirusiText.transform.position.y - yazirusiMove,
                           yazirusiText.transform.position.z);
            yazirusiCout += 1;
            StartCoroutine(Coroutine());
            SentakuSEPlay();
        }




        //メニュー閉じる処理
        if (Input.GetKeyDown("joystick button 0") && yazirusiCout == Data.dataItemStringList.Count - 1)
        {
            Destroy(this.gameObject.transform.parent.parent.gameObject);
            Data.pauseWindFlag = false;
            return;
        }


        if (Input.GetKeyDown("joystick button 0"))
        {
            KetteiSEPlay();

            if (Data.dataItemStringList[yazirusiCout] == "かいふく")
            {
                if (Data.kaihuku > 0)
                {
                    int num = itemHavelistName.IndexOf("かいふく");
                    itemHavelistNum[num]--;
                    Data.kaihuku--;
                    hpGauge.Heal(10);
                    playerControl.HealHp(10);
                }


            }
            if (Data.dataItemStringList[yazirusiCout] == "かいふく2")
            {
                if (Data.kaihuku2 > 0)
                {
                    int num = itemHavelistName.IndexOf("かいふく2");
                    itemHavelistNum[num]--;
                    Data.kaihuku2--;
                    hpGauge.Heal(20);
                    playerControl.HealHp(20);
                }


            }

            if (Data.dataItemStringList[yazirusiCout] == "まきもの")
            {
                if (Data.makimono > 0)
                {
                    int num = itemHavelistName.IndexOf("まきもの");
                    itemHavelistNum[num]--;
                    Data.makimono--;
                }
            }
            if (Data.dataItemStringList[yazirusiCout] == "まきもの2")
            {
                if (Data.makimono2 > 0)
                {
                    int num = itemHavelistName.IndexOf("まきもの2");
                    itemHavelistNum[num]--;
                    Data.makimono2--;
                }
            }
            if (Data.dataItemStringList[yazirusiCout] == "まきもの3")
            {
                if (Data.makimono3 > 0)
                {
                    int num = itemHavelistName.IndexOf("まきもの3");
                    itemHavelistNum[num]--;
                    Data.makimono3--;
                }
            }
        }

        //アイテムの説明
        ItemExposition();

        if (vert > -0.3f && vert < 0.3f)
        {
            yazirusiDelay = 0;
        }
        if (vert2 > -0.3f && vert2 < 0.3f)
        {
            yazirusiDelay2 = 0;
        }
    }

    public void MesseDelaySet()
    {
        messeDire = 40;
    }

    public void ItemCheckUpdate()
    {
        bool fl = false;
        for (int i = 0; i < itemHavelistNum.Count; i++)
        {
            if (itemHavelistNum[i] == 0)
            {
                itemHavelistName[itemHavelistName.Count - 1] = "";
                itemStringTexts[itemStringTexts.Count - 1].text = "";
                itemNumTexts[itemNumTexts.Count - 1].text = "";
                itemHavelistNum.RemoveAt(i);
                itemHavelistName.RemoveAt(i);
                
                itemStringTexts.RemoveAt(itemStringTexts.Count - 1);
                itemNumTexts.RemoveAt(itemNumTexts.Count - 1);
                Data.GetPauseMenuItemCount();
                fl = true;
            }
        }

        for (int i = 0; i < itemHavelistName.Count -1; i++)
        {
            itemStringTexts[i].text = itemHavelistName[i];

            if(itemStringTexts.Count > 1)
            {
                itemNumTexts[i].text = "(" + itemHavelistNum[i].ToString() + ")";
            }
            else
            {
                itemNumTexts[i].text = "";
            }
            
        }
        if(fl)
        {
            itemHavelistName[itemHavelistName.Count - 1] = "もどる";
            itemStringTexts[itemStringTexts.Count - 1].text = "もどる";
        }

    }

    public void ItemExposition()
    {
        if (Data.dataItemStringList[yazirusiCout] == "かいふく")
        {         
            message.text = "HP10回復する";
        }
        if (Data.dataItemStringList[yazirusiCout] == "かいふく2")
        {
            message.text = "HP20回復する";
        }

        if (Data.dataItemStringList[yazirusiCout] == "まきもの")
        {
            message.text = "必殺技1を使えるようになる";
        }
        if (Data.dataItemStringList[yazirusiCout] == "まきもの2")
        {
            message.text = "必殺技2を使えるようになる";
        }
        if (Data.dataItemStringList[yazirusiCout] == "まきもの3")
        {
            message.text = "必殺技3を使えるようになる";
        }
    }

    float delay = 0.2f;
    IEnumerator Coroutine()
    {
        delayFlag = true;
        yield return new WaitForSecondsRealtime(delay);
        delayFlag = false;
    }

    public void SentakuSEPlay()
    {
        audioSource.PlayOneShot(senntakuSE);
    }

    public void KetteiSEPlay()
    {
        audioSource.PlayOneShot(ketteiSE);
    }

}
