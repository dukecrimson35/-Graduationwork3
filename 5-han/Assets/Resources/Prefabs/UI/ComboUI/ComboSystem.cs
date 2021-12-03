using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboSystem : MonoBehaviour
{

    public Text text;
    public Text text2;
    public GameObject player;
    private PlayerControl playerControl;

    private int poolNum;

    private int a = 0;


    private string[] kansuuzi10 = new string[] { "", "十", "百","千"};
    private string[] kansuuzi = new string[] {"", "一", "二", "三", "四", "五", "六", "七", "八", "九"};


    void Start()
    {
        playerControl = player.GetComponent<PlayerControl>();
        poolNum = playerControl.GetHitCount();
    }

    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    a++;
        //    Debug.Log(a);
        //}


        //if (playerControl.GetHitCount() > 1 && !Data.voiceFlag)
        //{
        //    text.color = new Color(text.color.r, text.color.g, text.color.b, 1);

        //    string strNum = "";

        //    int keta = playerControl.GetHitCount().ToString().Length;

        //    string hitString = playerControl.GetHitCount().ToString();

        //    int count = keta;
        //    for (int i = 0; i < keta; i++)
        //    {
        //        strNum += int.Parse(hitString[i].ToString());
        //        strNum += kansuuzi10[count];
        //        count--;
        //    }

        //    text.text = strNum + "連撃";
        //}
        //if (a > 1 && !Data.voiceFlag)
        //{
        //    text.color = new Color(text.color.r, text.color.g, text.color.b, 1);

        //    string strNum = "";

        //    int keta = a.ToString().Length;

        //    string hitString = a.ToString();

        //    int count = keta;

        //    for (int i = 0; i < keta; i++)
        //    {
        //        int n = int.Parse(hitString[i].ToString());

        //        if (count > 1 && n == 1)
        //        { }
        //        else
        //        {
        //            strNum += kansuuzi[n];
        //        }

        //        if (count > -1)
        //        {
        //            if (n != 0)
        //            {
        //                strNum += kansuuzi10[count - 1];
        //            }
        //        }

        //        count--;
        //    }

        //    text.text = strNum + "連撃";
        //}
        if (playerControl.GetHitCount() > 1 && !Data.voiceFlag)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
            text2.color = new Color(text.color.r, text.color.g, text.color.b, 1);

            string strNum = "";
            int keta = playerControl.GetHitCount().ToString().Length;
            int count = keta;
            string hitString = playerControl.GetHitCount().ToString();

            for (int i = 0; i < keta; i++)
            {
                int n = int.Parse(hitString[i].ToString());

                if (count > 1 && n == 1)
                { }
                else
                {
                    strNum += kansuuzi[n];
                }

                if (count > -1)
                {
                    if (n != 0)
                    {
                        strNum += kansuuzi10[count - 1];
                    }
                }

                count--;
            }

            text.text = strNum;
        }
        else
        {
            while (text.color.a > 0)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - Time.deltaTime * 1);
                text2.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - Time.deltaTime * 1);

                //text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - Time.deltaTime * 2f);
            }
        }

        if(poolNum != playerControl.GetHitCount())
        {
            text.transform.localScale = new Vector3(0.2f,0.2f,text.transform.localScale.z);

            iTween.ShakePosition(this.gameObject, iTween.Hash("x", 10f, "y", 10f, "time", 0.8f));
            StartCoroutine(TextUpAni());
        }

        poolNum = playerControl.GetHitCount();


    }
    bool checkFlag = false;
    public float stop = 0.0001f;
    IEnumerator TextUpAni()
    {

        text.transform.localScale = new Vector3(
              text.transform.localScale.x + 0.1f,
              text.transform.localScale.y + 0.1f,
              text.transform.localScale.z
              );

        if(down)
        {
            checkFlag = true;
            down = false;
        }
       

        yield return new WaitForSeconds(1);

       // StartCoroutine(TextDownAni());
       
        
    }

    bool down = false;

    float num = 0;
    IEnumerator TextDownAni()
    {
        for (int i = 0; i < 10; i++)
        {
            if(checkFlag)
            {
                down = false;
                yield return null;
            }
            
            if (text.transform.localScale.x + 0.1f< text.transform.localScale.x)
            {
                yield return null;
            }

            if (text.transform.localScale.x  < 0.2f)
            {
                yield return null;
            }
            else
            {
                text.transform.localScale = new Vector3(
                    text.transform.localScale.x - 0.01f,
                    text.transform.localScale.y - 0.01f,
                    text.transform.localScale.z
               );
            }

            down = true;
           
            yield return new WaitForSeconds(stop);
        }
    }



}