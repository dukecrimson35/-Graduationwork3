using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("鬼ボス開始セリフ")]
    public string[] bossStartTexts;
    [Header("鬼ボス終了セリフ")]
    public string[] bossEndTexts;
  


    [SerializeField] List<string> messageList = new List<string>();//会話文リスト
    [SerializeField] Text text;
  
    int novelListIndex = 0; //配列の何個目を呼んでいるか

    private bool oneFlag = false;

   


    void Start()
    {
        StartCoroutine(Novel());
    }

    // Update is called once per frame
    void Update()
    {
        if (oneFlag) return;//一回しか呼ばないため


        if(oniBossStartFlag)
        {
            for(int i = 0; i< bossStartTexts.Length;i++)
            {
                messageList.Add(bossStartTexts[i]);
            }
            oneFlag = true;
        }
        else if(oniBossEndFlag)
        {
            for (int i = 0; i < bossEndTexts.Length; i++)
            {
                messageList.Add(bossEndTexts[i]);
            }
            oneFlag = true;
        }

        if (oneFlag) return;//テキストが読み込めなかった場合

        Debug.Log("テキストが読み込めない");

    }

    IEnumerator Novel()
    {
        yield return new WaitForSeconds(startTime);//読み初めまでの待ち時間

        int messageCount = 0; 
        text.text = ""; 

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

        yield return new WaitForSeconds(1.5f);//1行出してからの待ち時間

        novelListIndex++; //次の配列へ

        if (novelListIndex < messageList.Count)//配列がすべて読み終わっていないならもう一回
        {
            StartCoroutine(Novel());
        }

       
    }


}
