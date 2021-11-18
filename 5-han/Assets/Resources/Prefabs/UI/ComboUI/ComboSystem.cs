using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboSystem : MonoBehaviour
{

    public Text text;

    private int count = 0;

    private bool downFlag = false;
 

    void Start()
    {
       
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            downFlag = true;
            count++;
            num = 1;
            text.transform.localScale = new Vector3(0.2f,0.2f,text.transform.localScale.z);

            iTween.ShakePosition(this.gameObject, iTween.Hash("x", 10f, "y", 10f, "time", 0.8f));
            StartCoroutine(TextUpAni());
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            count = 0;
        }

       
        
    }
    bool a = false;
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
            a = true;
            down = false;
        }
       

        yield return new WaitForSeconds(1);

        StartCoroutine(TextDownAni());
       
        
    }

    bool down = false;

    float num = 0;
    IEnumerator TextDownAni()
    {
        for (int i = 0; i < 10; i++)
        {
            if(a)
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