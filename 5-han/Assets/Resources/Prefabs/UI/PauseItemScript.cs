using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseItemScript : MonoBehaviour
{

    public List<string> itemList;

    private List<int> haveItems;
    private List<Text> haveItemText;

    public Text yazirusiText;
    private Vector3 pos;//矢印の初期Pos

    public Text text;
    private int yazirusiCout = 0;//矢印がどの段にいるかの


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
                new Vector3(pos.x + 50.0f + textWidthmove, pos.y - 15 - height * i - i * textHeightmove, 0.0f), Quaternion.identity, this.transform);

            instance.transform.localScale = new Vector3(0.2f, 0.2f, 0.1f);
        }
        HaveNumver();

        //リストに入っているテキストをメニューに並べる(アイテム数)
        for (int i = 0; i < haveItems.Count; i++)
        {
            text.text = "(" + haveItems[i].ToString() + ")";

            Text instance =
                (Text)Instantiate(text,
                new Vector3(pos.x + 300.0f + textWidthmove, pos.y - 15 - height * i - i * textHeightmove, 0.0f), Quaternion.identity, this.transform);

            instance.transform.localScale = new Vector3(0.2f, 0.2f, 0.1f);
            haveItemText.Add(instance);
        }
    }

    public void HaveItemUpdate()
    {
        HaveNumver();
        for (int i = 0; i < haveItemText.Count; i++)
        {
            haveItemText[i].text = "(" + haveItems[i].ToString() + ")";
        }
    }

    // Update is called once per frame
    void Update()
    {
        HaveItemUpdate();
        // ディレイ関係
        //矢印移動ディレイ
        if (yazirusiDelay > 0)
        {
            yazirusiDelay -= dire * Time.deltaTime;
        }

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
        else if (vert < -0.3f && yazirusiCout < itemList.Count - 1 && yazirusiDelay <= 0)
        {
            yazirusiText.transform.position =
               new Vector3(yazirusiText.transform.position.x,
                           yazirusiText.transform.position.y - yazirusiMove,
                           yazirusiText.transform.position.z);
            yazirusiCout += 1;
            yazirusiDelay = 60;
        }

        //メニュー閉じる処理
        if (Input.GetKeyDown("joystick button 0") && yazirusiCout == itemList.Count - 1)
        {
            Destroy(this.gameObject.transform.parent.parent.gameObject);
            return;
        }
       
        if (vert > -0.3f && vert < 0.3f)
        {
            yazirusiDelay = 0;
        }
    }

    public void HaveNumver()
    {
        if (haveItems != null) haveItems.Clear();

        for (int i = 0; i < itemList.Count; i++)
        {
            switch (itemList[i])
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
}
