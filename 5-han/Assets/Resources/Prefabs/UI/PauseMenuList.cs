﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PauseMenuList : MonoBehaviour
{

    public GameObject pauseItemList;
    public GameObject pauseSoundUI;

    public List<string> itemList;

    private bool delayFlag = false;

    public Text yazirusiText;
    private Vector3 pos;//矢印の初期Pos

    public Text text;
    private int yazirusiCout = 0;//矢印がどの段にいるかの


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


        //スティックの縦方向取得
        float vert = Input.GetAxis("Vertical");
        float vert2 = Input.GetAxis("CrossUpDown");
        //メニューの矢印制御vert2 > 0.3f 

        if (vert > 0.3f && yazirusiCout > 0 && yazirusiDelay <= 0 && !Data.pauseWindFlag && !delayFlag)
        {
            yazirusiText.transform.position =
                new Vector3(yazirusiText.transform.position.x,
                            yazirusiText.transform.position.y + yazirusiMove,
                            yazirusiText.transform.position.z);
            yazirusiCout -= 1;
            StartCoroutine(Coroutine());
            SentakuSEPlay();
            //yazirusiDelay = 60;
        }
        else if (vert2 > 0.3f && yazirusiCout > 0 && yazirusiDelay2 <= 0 && !Data.pauseWindFlag && !delayFlag)
        {
            yazirusiText.transform.position =
                new Vector3(yazirusiText.transform.position.x,
                            yazirusiText.transform.position.y + yazirusiMove,
                            yazirusiText.transform.position.z);
            yazirusiCout -= 1;
            StartCoroutine(Coroutine());
            SentakuSEPlay();
            //yazirusiDelay2 = 60;
        }
        else if (vert < -0.3f && yazirusiCout < itemList.Count - 1 && yazirusiDelay <= 0 && !Data.pauseWindFlag && !delayFlag)
        {
            yazirusiText.transform.position =
               new Vector3(yazirusiText.transform.position.x,
                           yazirusiText.transform.position.y - yazirusiMove,
                           yazirusiText.transform.position.z);
            yazirusiCout += 1;
            StartCoroutine(Coroutine());
            SentakuSEPlay();
            //yazirusiDelay = 60;
        }
        else if (vert2 < -0.3f && yazirusiCout < itemList.Count - 1 && yazirusiDelay2 <= 0 && !Data.pauseWindFlag && !delayFlag)
        {
            yazirusiText.transform.position =
               new Vector3(yazirusiText.transform.position.x,
                           yazirusiText.transform.position.y - yazirusiMove,
                           yazirusiText.transform.position.z);
            yazirusiCout += 1;
            StartCoroutine(Coroutine());
            SentakuSEPlay();
            //yazirusiDelay2 = 60;
        }

        if (Input.GetKeyDown("joystick button 0") && !Data.pauseWindFlag)
        {
            if(itemList[yazirusiCout] == "もちもの")
            {
                GameObject instance =
              (GameObject)Instantiate(pauseItemList,
              new Vector3(0, 0, 0.0f), Quaternion.identity);

                Data.pauseWindFlag = true;
                KetteiSEPlay();
            }
        }
        if (Input.GetKeyDown("joystick button 0") && !Data.pauseWindFlag)
        {
            if (itemList[yazirusiCout] == "音量")
            {
                GameObject instance =
              (GameObject)Instantiate(pauseSoundUI,
              new Vector3(0, 0, 0.0f), Quaternion.identity);

                Data.pauseWindFlag = true;
                KetteiSEPlay();
            }
        }


        //メニュー閉じる処理
        if (Input.GetKeyDown("joystick button 0") && yazirusiCout == itemList.Count - 1)
        {
            KetteiSEPlay();
            Data.pauseWindFlag = false;
            Destroy(this.gameObject.transform.parent.parent.gameObject);
            Time.timeScale = 1f;
            
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

    public void SentakuSEPlay()
    {
        audioSource.PlayOneShot(senntakuSE);
    }

    public void KetteiSEPlay()
    {
        audioSource.PlayOneShot(ketteiSE);
    }

    public void MesseDelaySet()
    {
        messeDire = 40;
    }

    float delay = 0.2f;
    IEnumerator Coroutine()
    {
        delayFlag = true;
        yield return new WaitForSecondsRealtime(delay);
        delayFlag = false;
    }

}
