using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboSystem : MonoBehaviour
{

    public Text text;
    public GameObject player;
    private PlayerControl playerControl;

    private int poolNum;
   
 

    void Start()
    {
        playerControl = player.GetComponent<PlayerControl>();
        poolNum = playerControl.GetHitCount();
    }

    void Update()
    {

        if(playerControl.GetHitCount()> 0 && !Data.voiceFlag)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
            text.text = playerControl.GetHitCount().ToString() + "連撃";
        }
        else
        {
            while (text.color.a > 0)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - Time.deltaTime * 1);
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