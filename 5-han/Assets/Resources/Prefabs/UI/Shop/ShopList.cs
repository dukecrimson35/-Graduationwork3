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

    public List<int> delItem = new List<int>();

    public Text kesisen;
    public Text kesisen2;

    // Start is called before the first frame update
    void Start()
    {
        Data.shopFlag = true;
        haveItems = new List<int>(5);
        haveItemText = new List<Text>(5);
        //矢印の初期Pos
        pos = yazirusiText.transform.position;

        audioSource = GetComponent<AudioSource>();

        if(Data.bSkill)
        {
            kesisen.color = new Color(kesisen.color.r, kesisen.color.g, kesisen.color.b,1);
        }
        if(Data.xSkill)
        {
            kesisen2.color = new Color(kesisen2.color.r, kesisen2.color.g, kesisen2.color.b,1);
        }

        //リストに入っているテキストをメニューに並べる(アイテム名)
        for (int i = 0; i < itemList.Count; i++)
        {
            if(itemList[i] == Data.bSkillName && Data.bSkill)
            {
                delItem.Add(i);             
            }
            if(itemList[i] == Data.xSkillName && Data.xSkill)
            {
                delItem.Add(i);
            }

            text.text = itemList[i];
         
            Text instance =
                (Text)Instantiate(text,
                new Vector3(pos.x + 50.0f +textWidthmove, pos.y - 15  - height * i - i * textHeightmove, 0.0f), Quaternion.identity, this.transform);

            instance.transform.localScale = new Vector3(0.2f, 0.2f, 0.1f);
        }

        //リストに入っているテキストをメニューに並べる(アイテム値段)
        for (int i = 0; i < coinList.Count - 1; i++)
        {
            bool check = false;
            for(int j = 0; j< delItem.Count; j++)
            {
                if(delItem[j] == i)
                {
                    text.text = "売り切れ";

                    Text instance2 =
                        (Text)Instantiate(text,
                        new Vector3(pos.x + 360.0f + textWidthmove, pos.y - 15 - height * i - i * textHeightmove, 0.0f), Quaternion.identity, this.transform);

                    instance2.transform.localScale = new Vector3(0.2f, 0.2f, 0.1f);
                    check = true;
                }
            }

            if (check) continue;

            text.text = coinList[i].ToString();

            Text instance =
                (Text)Instantiate(text,
                new Vector3(pos.x + 445.0f +textWidthmove, pos.y - 15 - height *i - i * textHeightmove, 0.0f), Quaternion.identity, this.transform);

            instance.transform.localScale = new Vector3(0.2f, 0.2f, 0.1f);
        }

        HaveNumver();
        //リストに入っているテキストをメニューに並べる(アイテム数)
        for (int i = 0; i < haveItems.Count; i++)
        {
            bool check = false;
            for (int j = 0; j < delItem.Count; j++)
            {
                if (delItem[j] == i)
                {
                    check = true;
                }
            }
            if (check) continue;

            text.text = "(" + haveItems[i].ToString() + ")";

            Text instance =
                (Text)Instantiate(text,
                new Vector3(pos.x + 360.0f + textWidthmove, pos.y - 15  - height *i - i * textHeightmove,  0.0f), Quaternion.identity, this.transform);

            instance.transform.localScale = new Vector3(0.2f, 0.2f, 0.1f);
            haveItemText.Add(instance);
        }
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = "×" + Data.coin.ToString() + "";
        HaveItemUpdate();
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
        if (itemName == Data.bSkillName)
        {
            if(Data.bSkillCount < 1 && !Data.bSkill)
            {
                Data.bSkillCount++;
                Data.coin -= coinList[yazirusiCout];
                message.text = itemName + "を買いました";
                audioSource.PlayOneShot(kauSE);
            }
            else
            {
                message.text = "すでに持っています";
                audioSource.PlayOneShot(kaenaiSE);
            }
            MesseDelaySet();

        }
        else if (itemName == Data.xSkillName)
        {
            
            if (Data.xSkillCount < 1 && !Data.xSkill)
            {
                Data.xSkillCount++;
                Data.coin -= coinList[yazirusiCout];
                message.text = itemName + "を買いました";
                audioSource.PlayOneShot(kauSE);
            }
            else
            {
                message.text = "すでに持っています";
                audioSource.PlayOneShot(kaenaiSE);
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
        else if (itemName == Data.kaihukuStr)
        {
            
            if (Data.kaihuku < 9)
            {
                Data.kaihuku++;
                Data.coin -= coinList[yazirusiCout];
                message.text = itemName + "を買いました";
                audioSource.PlayOneShot(kauSE);
            }
            else
            {
                message.text = "これ以上買えません";
            }
            MesseDelaySet();
        }
        else if (itemName == Data.kaihuku2Str)
        {

            if (Data.kaihuku2 < 9)
            {
                Data.kaihuku2++;
                Data.coin -= coinList[yazirusiCout];
                message.text = itemName + "を買いました";
                audioSource.PlayOneShot(kauSE);
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
        if (itemList[yazirusiCout] == Data.kaihukuStr)
        {

            message2.text = "効果:HP10回復する";
        }
        if (itemList[yazirusiCout] == Data.kaihuku2Str)
        {

            message2.text = "効果:HP20回復する";
        }

        if (itemList[yazirusiCout] == Data.bSkillName)
        {
            if (Data.bSkill)
            {
                message2.text = "すでに買っています。";
            }
            else
            {
                message2.text = "効果:連撃数に応じて\n　　 威力が上がる技";
            }
        }
        if (itemList[yazirusiCout] == Data.xSkillName)
        {
            message2.text = "効果:長押しで範囲攻撃を\n　　 する技";
        }
        if (itemList[yazirusiCout] == "まきもの3")
        {
            message2.text = "効果:必殺技3を使えるようになる";
        }
    }

    //ここは手動でアイテムの名前変更
    public void HaveNumver()
    {
        if(haveItems != null) haveItems.Clear();

        for (int i = 0; i< itemList.Count; i++)
        {            
            switch(itemList[i])
            {
                case "紅月刀の書":
                    haveItems.Add(Data.bSkillCount);
                    break;
                case "青天刀の書":
                    haveItems.Add(Data.xSkillCount);
                    break;
                case "まきもの3":
                    haveItems.Add(Data.makimono3);
                    break;
                case "回復薬":
                    haveItems.Add(Data.kaihuku);
                    break;
                case "上回復薬":
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
