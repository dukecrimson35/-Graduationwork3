using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopList : MonoBehaviour
{
    public List<string> itemList;

    public List<int> coinList;

    private List<int> haveItems;
    private List<Text> haveItemText;

    public Text yazirusiText;
    private Vector3 pos;//矢印の初期Pos

    public Text text;
    private int yazirusiCout = 0;//矢印がどの段にいるかの

    public Text coinText;

    public Text message;
    public Text message2;

    private float yazirusiDelay = 0.2f;
    private float yazirusiDelay2 = 0.2f;
    private float dire = 180;
    private float messegeDelay = 60;
    private float messeDire = 0;
    private int yazirusiMove = 90;
    private int height = 50;
    private int textWidthmove = 50;
    private int textHeightmove = 40;

    private AudioSource audioSource;
    public AudioClip senntakuSE;
    public AudioClip ketteiSE;
    public AudioClip kauSE;
    public AudioClip kaenaiSE;

    // Start is called before the first frame update
    void Start()
    {
        Data.shopFlag = true;
        haveItems = new List<int>(5);
        haveItemText = new List<Text>(5);
        //矢印の初期Pos
        pos = yazirusiText.transform.position;

        audioSource = GetComponent<AudioSource>();

        //リストに入っているテキストをメニューに並べる(アイテム名)
        for (int i = 0; i < itemList.Count; i++)
        {
            text.text = itemList[i];

         
            Text instance =
                (Text)Instantiate(text,
                new Vector3(pos.x + 50.0f +textWidthmove, pos.y - 15  - height * i - i * textHeightmove, 0.0f), Quaternion.identity, this.transform);

            instance.transform.localScale = new Vector3(0.2f, 0.2f, 0.1f);
        }

        //リストに入っているテキストをメニューに並べる(アイテム値段)
        for (int i = 0; i < coinList.Count - 1; i++)
        {
            text.text = coinList[i].ToString();

            Text instance =
                (Text)Instantiate(text,
                new Vector3(pos.x + 400.0f +textWidthmove, pos.y - 15 - height *i - i * textHeightmove, 0.0f), Quaternion.identity, this.transform);

            instance.transform.localScale = new Vector3(0.2f, 0.2f, 0.1f);
        }

        HaveNumver();
        //リストに入っているテキストをメニューに並べる(アイテム数)
        for (int i = 0; i < haveItems.Count; i++)
        {
            text.text = "(" + haveItems[i].ToString() + ")";

            Text instance =
                (Text)Instantiate(text,
                new Vector3(pos.x + 300.0f + textWidthmove, pos.y - 15  - height *i - i * textHeightmove,  0.0f), Quaternion.identity, this.transform);

            instance.transform.localScale = new Vector3(0.2f, 0.2f, 0.1f);
            haveItemText.Add(instance);
        }
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = Data.coin.ToString() + "銭";
        HaveItemUpdate();

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
        //メッセージ消えるディレイ
        if (messeDire > 0)
        {
            messeDire -= messegeDelay * Time.deltaTime;
        }
        else
        {
            message.text = "";
        }

        //スティックの縦方向取得
        float vert = Input.GetAxis("Vertical");
        float vert2 = Input.GetAxis("CrossUpDown");
        //メニューの矢印制御
        if (vert > 0.3f && yazirusiCout > 0 && yazirusiDelay <= 0)
        {
            yazirusiText.transform.position =
                new Vector3(yazirusiText.transform.position.x,
                            yazirusiText.transform.position.y + yazirusiMove,
                            yazirusiText.transform.position.z);
            yazirusiCout -= 1;
            yazirusiDelay = 60;
            SentakuSEPlay();
        }
        else if (vert2 > 0.3f && yazirusiCout > 0 && yazirusiDelay2 <= 0)
        {
            yazirusiText.transform.position =
                new Vector3(yazirusiText.transform.position.x,
                            yazirusiText.transform.position.y + yazirusiMove,
                            yazirusiText.transform.position.z);
            yazirusiCout -= 1;
            yazirusiDelay2 = 60;
            SentakuSEPlay();
        }
        else if (vert < -0.3f && yazirusiCout < itemList.Count - 1 && yazirusiDelay <= 0)
        {
            yazirusiText.transform.position =
               new Vector3(yazirusiText.transform.position.x,
                           yazirusiText.transform.position.y - yazirusiMove,
                           yazirusiText.transform.position.z);
            yazirusiCout += 1;
            yazirusiDelay = 60;
            SentakuSEPlay();
        }
        else if (vert2 < -0.3f && yazirusiCout < itemList.Count - 1 && yazirusiDelay2 <= 0)
        {
            yazirusiText.transform.position =
               new Vector3(yazirusiText.transform.position.x,
                           yazirusiText.transform.position.y - yazirusiMove,
                           yazirusiText.transform.position.z);
            yazirusiCout += 1;
            yazirusiDelay2 = 60;
            SentakuSEPlay();
        }

        //メニュー閉じる処理
        if (Input.GetKeyDown("joystick button 0") && yazirusiCout == itemList.Count - 1)
        {
            Data.shopFlag = false;
            Destroy(this.gameObject.transform.parent.parent.gameObject);
            //Time.timeScale = 1;
            return;
        }
        //アイテム買う時の処理
        //詳細はGameDataに書いてある
        if (Input.GetKeyDown("joystick button 0"))
        {
            if (Data.coin >= coinList[yazirusiCout])
            {
                BuyItem(itemList[yazirusiCout]);
                audioSource.PlayOneShot(kauSE);
            }
            else
            {
                message.text = "お金が足りません";
                audioSource.PlayOneShot(kaenaiSE);
                MesseDelaySet();
            }
        }

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

    public void SentakuSEPlay()
    {
        audioSource.PlayOneShot(senntakuSE);
    }

    public void KetteiSEPlay()
    {
        audioSource.PlayOneShot(ketteiSE);
    }

    public void BuyItem(string itemName)
    {
        if (itemName == "まきもの")
        {
            if(Data.makimono < 1)
            {
                Data.makimono++;
                Data.coin -= coinList[yazirusiCout];
                message.text = itemName + "を買いました";
            }
            else
            {
                message.text = "すでに持っています";
            }
            MesseDelaySet();

        }
        else if (itemName == "まきもの2")
        {
            
            if (Data.makimono2 < 1)
            {
                Data.makimono2++;
                Data.coin -= coinList[yazirusiCout];
                message.text = itemName + "を買いました";
            }
            else
            {
                message.text = "すでに持っています";
            }
            MesseDelaySet();
        }
        else if (itemName == "まきもの3")
        {          
            if (Data.makimono3< 1)
            {
                Data.makimono3++;
                Data.coin -= coinList[yazirusiCout];
                message.text = itemName + "を買いました";
            }
            else
            {
                message.text = "すでに持っています";
            }
            MesseDelaySet();
        }
        else if (itemName == "かいふく")
        {
            
            if (Data.kaihuku < 9)
            {
                Data.kaihuku++;
                Data.coin -= coinList[yazirusiCout];
                message.text = itemName + "を買いました";
            }
            else
            {
                message.text = "これ以上買えません";
            }
            MesseDelaySet();
        }
        else if (itemName == "かいふく2")
        {

            if (Data.kaihuku2 < 9)
            {
                Data.kaihuku2++;
                Data.coin -= coinList[yazirusiCout];
                message.text = itemName + "を買いました";
            }
            else
            {
                message.text = "これ以上買えません";
            }
            MesseDelaySet();
        }


    }

    public void ItemExposition()
    {
        if (itemList[yazirusiCout] == "かいふく")
        {

            message2.text = "効果:HP10回復する";
        }
        if (itemList[yazirusiCout] == "かいふく2")
        {

            message2.text = "効果:HP20回復する";
        }

        if (itemList[yazirusiCout] == "まきもの")
        {
            message2.text = "効果:必殺技1を使えるようになる";
        }
        if (itemList[yazirusiCout] == "まきもの2")
        {
            message2.text = "効果:必殺技2を使えるようになる";
        }
        if (itemList[yazirusiCout] == "まきもの3")
        {
            message2.text = "効果:必殺技3を使えるようになる";
        }
    }

    public void HaveNumver()
    {
        if(haveItems != null) haveItems.Clear();

        for (int i = 0; i< itemList.Count; i++)
        {            
            switch(itemList[i])
            {
                case "まきもの":
                    haveItems.Add(Data.makimono);
                    break;
                case "まきもの2":
                    haveItems.Add(Data.makimono2);
                    break;
                case "まきもの3":
                    haveItems.Add(Data.makimono3);
                    break;
                case "かいふく":
                    haveItems.Add(Data.kaihuku);
                    break;
                case "かいふく2":
                    haveItems.Add(Data.kaihuku2);
                    break;
            }
        }
    }

    public void HaveItemUpdate()
    {
        HaveNumver();
        for(int i = 0; i< haveItemText.Count; i++)
        {
            haveItemText[i].text = "(" + haveItems[i].ToString() + ")";
        }
    }
    public void MesseDelaySet()
    {
        messeDire = 40;
    }
}
