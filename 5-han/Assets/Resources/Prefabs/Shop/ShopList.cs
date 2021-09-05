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

    private float yazirusiDelay = 0.2f;
    private float dire = 180;
    private float messegeDelay = 60;
    private float messeDire = 0;

    // Start is called before the first frame update
    void Start()
    {
        Data.shopFlag = true;
        haveItems = new List<int>();
        haveItemText = new List<Text>();
        //矢印の初期Pos
        pos = yazirusiText.transform.position;       

        //リストに入っているテキストをメニューに並べる(アイテム名)
        for (int i = 0; i < itemList.Count; i++)
        {
            text.text = itemList[i];

            Text instance =
                (Text)Instantiate(text,
                new Vector3(pos.x + 50.0f, pos.y - 15 - i * 35, 0.0f), Quaternion.identity, this.transform);

            instance.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }

        //リストに入っているテキストをメニューに並べる(アイテム値段)
        for (int i = 0; i < coinList.Count - 1; i++)
        {
            text.text = coinList[i].ToString();

            Text instance =
                (Text)Instantiate(text,
                new Vector3(pos.x + 400.0f, pos.y - 15 - i * 35, 0.0f), Quaternion.identity, this.transform);

            instance.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }

        HaveNumver();
        //リストに入っているテキストをメニューに並べる(アイテム数)
        for (int i = 0; i < haveItems.Count; i++)
        {
            text.text = "(" + haveItems[i].ToString() + ")";

            Text instance =
                (Text)Instantiate(text,
                new Vector3(pos.x + 300.0f, pos.y - 15 - i * 35, 0.0f), Quaternion.identity, this.transform);

            instance.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
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

        //メニューの矢印制御
        if (vert < -0.3f && yazirusiCout > 0 && yazirusiDelay <= 0)
        {
            yazirusiText.transform.position =
                new Vector3(yazirusiText.transform.position.x,
                            yazirusiText.transform.position.y + 35,
                            yazirusiText.transform.position.z);
            yazirusiCout -= 1;
            yazirusiDelay = 60;
        }
        else if (vert > 0.3f && yazirusiCout < itemList.Count - 1 && yazirusiDelay <= 0)
        {
            yazirusiText.transform.position =
               new Vector3(yazirusiText.transform.position.x,
                           yazirusiText.transform.position.y - 35,
                           yazirusiText.transform.position.z);
            yazirusiCout += 1;
            yazirusiDelay = 60;
        }

        //メニュー閉じる処理
        if (Input.GetKeyDown("joystick button 0") && yazirusiCout == itemList.Count - 1)
        {
            Data.shopFlag = false;
            Destroy(this.gameObject.transform.parent.parent.gameObject);
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
                MesseDelaySet();
            }
        }

        if(vert > -0.3f && vert < 0.3f)
        {
            yazirusiDelay = 0;
        }

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
