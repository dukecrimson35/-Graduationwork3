﻿using System.Collections;
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
    private int yazirusiCout = 0;//矢印がどの段にいるかの

    private List<string> itemHavelistName = new List<string>();
    private List<int> itemHavelistNum = new List<int>();
    private List<Text> itemStringTexts = new List<Text>();
    private List<Text> itemNumTexts = new List<Text>();



    private float yazirusiDelay = 0.2f;
    private float dire = 180;
    private float messegeDelay = 60;
    private float messeDire = 0;
    private int yazirusiMove = 90;
    private int height = 50;
    private int textWidthmove = 50;  
    private int textHeightmove = 40;

    // Start is called before the first frame update
    void Start()
    {
        
        //haveItems = new List<int>();
        //haveItemText = new List<Text>();
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
        HaveNumver();

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


        ////リストに入っているテキストをメニューに並べる(アイテム名)
        //for (int i = 0; i < itemList.Count; i++)
        //{
        //    text.text = itemList[i];


        //    Text instance =
        //        (Text)Instantiate(text,
        //        new Vector3(pos.x + 50.0f + textWidthmove, pos.y - 15 - height * i - i * textHeightmove, 0.0f), Quaternion.identity, this.transform);

        //    instance.transform.localScale = new Vector3(0.2f, 0.2f, 0.1f);
        //}
        //HaveNumver();

        ////リストに入っているテキストをメニューに並べる(アイテム数)
        //for (int i = 0; i < haveItems.Count; i++)
        //{
        //    text.text = "(" + haveItems[i].ToString() + ")";

        //    Text instance =
        //        (Text)Instantiate(text,
        //        new Vector3(pos.x + 300.0f + textWidthmove, pos.y - 15 - height * i - i * textHeightmove, 0.0f), Quaternion.identity, this.transform);

        //    instance.transform.localScale = new Vector3(0.2f, 0.2f, 0.1f);
        //    haveItemText.Add(instance);
        //}
    }

    public void HaveItemUpdate()
    {
        //HaveNumver();
        //for (int i = 0; i < haveItemText.Count; i++)
        //{
        //    haveItemText[i].text = "(" + haveItems[i].ToString() + ")";
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //HaveItemUpdate();
        // ディレイ関係
        //矢印移動ディレイ
        if (yazirusiDelay > 0)
        {
            yazirusiDelay -= dire * Time.deltaTime;
        }

        ItemCheckUpdate();

        //スティックの縦方向取得
        float vert = Input.GetAxis("Vertical");

        //メニューの矢印制御
        if (vert > 0.3f && yazirusiCout > 0 && yazirusiDelay <= 0)
        {
            yazirusiText.transform.position =
                new Vector3(yazirusiText.transform.position.x,
                            yazirusiText.transform.position.y + yazirusiMove,
                            yazirusiText.transform.position.z);
            yazirusiCout -= 1;
            yazirusiDelay = 60;
        }
        else if (vert < -0.3f && yazirusiCout < Data.dataItemStringList.Count - 1 && yazirusiDelay <= 0)
        {
            yazirusiText.transform.position =
               new Vector3(yazirusiText.transform.position.x,
                           yazirusiText.transform.position.y - yazirusiMove,
                           yazirusiText.transform.position.z);
            yazirusiCout += 1;
            yazirusiDelay = 60;
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
            if (Data.dataItemStringList[yazirusiCout] == "かいふく")
            {
                if (Data.kaihuku > 0)
                {
                    int num = itemHavelistName.IndexOf("かいふく");
                    itemHavelistNum[num]--;
                    Data.kaihuku--;
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



        if (vert > -0.3f && vert < 0.3f)
        {
            yazirusiDelay = 0;
        }
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

    public void HaveNumver()
    {
        //if (haveItems != null) haveItems.Clear();

        //for (int i = 0; i < itemList.Count; i++)
        //{
        //    switch (itemList[i])
        //    {
        //        case "まきもの":
        //            haveItems.Add(Data.makimono);
        //            break;
        //        case "まきもの2":
        //            haveItems.Add(Data.makimono2);
        //            break;
        //        case "まきもの3":
        //            haveItems.Add(Data.makimono3);
        //            break;
        //        case "かいふく":
        //            haveItems.Add(Data.kaihuku);
        //            break;
        //    }
        //}
    }
}