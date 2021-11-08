using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class VoiceScript : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("読み初めまでの待ち時間")]
    public float startTime = 0.0f;

    [Header("テキスト表示スピード")]
    public float novelSpeed;//表示する速さ

    [Header("鬼ボス開始セリフ")]
    public bool oniBossStartFlag = false;
    [Header("鬼ボス終了セリフ")]
    public bool oniBossEndFlag = false;

    //[Header("鬼ボス開始セリフ")]
    //public string[] bossStartTexts;
    //[Header("鬼ボス終了セリフ")]
    //public string[] bossEndTexts;
  


    [SerializeField] List<string> messageList = new List<string>();//会話文リスト
    [SerializeField] Text text;
  
    int novelListIndex = 0; //配列の何個目を呼んでいるか

    private bool oneFlag = false;//一回しか呼ばないため

    private bool endFlag = false;//会話終了フラグ

    public TextAsset oniStartTxt;
    public TextAsset oniEndTxt;

    public AudioSource audioSource;
    public AudioClip katakata;

    void Start()
    {
        StartCoroutine(Novel());
        audioSource.volume = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (oneFlag) return;//一回しか呼ばないため


        if(oniBossStartFlag)
        {          

            StringReader reader = new StringReader(oniStartTxt.text);
            while (reader.Peek() != -1) 
            {
                string line = reader.ReadLine(); // 一行ずつ読み込み
                messageList.Add(line);
            }

            oneFlag = true;
        }
        else if(oniBossEndFlag)
        {
            StringReader reader = new StringReader(oniEndTxt.text);
            while (reader.Peek() != -1)
            {
                string line = reader.ReadLine(); // 一行ずつ読み込み
                messageList.Add(line);
            }
            oneFlag = true;
        }

        if (oneFlag) return;//テキストが読み込めなかった場合

        Debug.Log("テキストが読み込めない");

    }
    bool oneBGMFlag = false;

    IEnumerator Novel()
    {
       

        int messageCount = 0; 
        text.text = "";
        bool endChack = true;
       
        if(!oneBGMFlag)
        {
            yield return new WaitForSeconds(startTime);//読み初めまでの待ち時間
            StartCoroutine(BGMFadeInCoroutine());
        }
      

        while (messageList[novelListIndex].Length > messageCount)
        {
            if (messageList[novelListIndex][messageCount] == 'n')//nを発見したら改行させる
            {
                text.text += "\n　　";//改行+スペースで次の開始位置調整
            }
            else
            {
                text.text += messageList[novelListIndex][messageCount];//追加していく
                
            }
            
            messageCount++;//現在の文字数
            yield return new WaitForSeconds(novelSpeed);//読むスピード
        }

        yield return new WaitForSeconds(1.3f);//1行出してからの待ち時間

        novelListIndex++; //次の配列へ

        if (novelListIndex < messageList.Count)//配列がすべて読み終わっていないならもう一回
        {
            StartCoroutine(Novel());
            endChack = false;
        }
        if(endFlag)
        {
            StartCoroutine(BGMFadeOutCoroutine());
        }
       


        yield return new WaitForSeconds(0.4f);//待ち時間


        if (endChack)
        {
            endFlag = true;
            text.text = "";
        }
        
    }

    float second = 200;
    float bgmDelay = 0.01f;
    float vol = 0;
    IEnumerator BGMFadeOutCoroutine()
    {
        vol = audioSource.volume / second;

        for (int i = 0; i < second; i++)
        {
            audioSource.volume = audioSource.volume - vol;
            yield return new WaitForSecondsRealtime(bgmDelay);
        }
        audioSource.Pause();
    }

    IEnumerator BGMFadeInCoroutine()
    {
       

        if (!oneBGMFlag)
        {
            vol = audioSource.volume / second;
            audioSource.volume = 0.0f;
            audioSource.clip = katakata;
            audioSource.Play();
            //audioSource.volume = 0.5f;
            oneBGMFlag = true;
        }

        for (int i = 0; i < second; i++)
        {
            audioSource.volume = audioSource.volume + vol;
            yield return new WaitForSecondsRealtime(bgmDelay);
        }
        
    }

    public bool GetEndFlag()
    {
        return endFlag;
    }

    public void SetOniStartFlag()
    {
        oniBossStartFlag = true;
    }
    public void SetOniEndFlag()
    {
        oniBossEndFlag = true;
    }


}
