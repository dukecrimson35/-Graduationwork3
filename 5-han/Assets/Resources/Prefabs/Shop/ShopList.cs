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

    // Start is called before the first frame update
    void Start()
    {
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
        //メニューの矢印制御
        if (Input.GetKeyDown(KeyCode.UpArrow) && yazirusiCout > 0)
        {
            yazirusiText.transform.position =
                new Vector3(yazirusiText.transform.position.x,
                            yazirusiText.transform.position.y + 35,
                            yazirusiText.transform.position.z);
            yazirusiCout -= 1;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && yazirusiCout < itemList.Count -1)
        {
            yazirusiText.transform.position =
               new Vector3(yazirusiText.transform.position.x,
                           yazirusiText.transform.position.y - 35,
                           yazirusiText.transform.position.z);
            yazirusiCout += 1;
        }

        //メニュー閉じる処理
        if(Input.GetKeyDown(KeyCode.Return) && yazirusiCout == itemList.Count -1)
        {
            Destroy(this.gameObject.transform.parent.parent.gameObject);
            return;
        }

        //アイテム買う時の処理
        //詳細はGameDataに書いてある
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(Data.coin >= coinList[yazirusiCout ])
            {
                
                BuyItem(itemList[yazirusiCout]);
            }
            else
            {
                message.text = "お金が足りません...";
            }
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
}
