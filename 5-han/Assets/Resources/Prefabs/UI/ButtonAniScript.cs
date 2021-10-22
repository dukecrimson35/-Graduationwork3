using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAniScript : MonoBehaviour
{
    // Start is called before the first frame update
    private List<Sprite> sprites = new List<Sprite>();

    public Image image;
    private int count = 20;

    private bool backFlag = false;

    public Text text;

    private bool textFlag = false;

    private float alpha = 0;

    private bool stopButtonFlag = false;

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        for(int i = 1; i< count+1;i++)
        {
            string str = "Prefabs/UI/makimonoAni/(" + i.ToString() + ")";
            Sprite sp = Resources.Load<Sprite>(str);
            sprites.Add(sp);
        }
        StartCoroutine(Coroutine());
        alpha = text.color.a;
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);

    }

    // Update is called once per frame
    void Update()
    {

        if (text.color.a < 1 && !textFlag)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + Time.deltaTime * 0.5f);
        }
       

        if (backFlag)
        {
            backFlag = false;
            StartCoroutine(BackCoroutine());
            text.fontSize = 350;
            textFlag = true;
            text.color = new Color(text.color.r, text.color.g, text.color.b,alpha);
        }

        if(textFlag)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - Time.deltaTime * 1);
        }

        //if (stopButtonFlag)
        //{
        //    button.interactable = false;
        //}
        //else
        //{
        //    button.interactable = true;
        //}


    }

    public void SetBackFlag(bool fl)
    {
        backFlag = fl;
    }
    public bool GetBackFlag()
    { return backFlag; }

    float delay = 0.08f;
    IEnumerator Coroutine()
    {
        stopButtonFlag = true;
        for(int i = 0; i< count;i++)
        {
            image.sprite = sprites[i];
            yield return new WaitForSecondsRealtime(delay);
        }

        stopButtonFlag = false;
    }

    float delay2 = 0.03f;

    IEnumerator BackCoroutine()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        for (int i = count-1; i >-1 ; i--)
        {
            image.sprite = sprites[i];
            yield return new WaitForSecondsRealtime(delay2);
        }


    }

    IEnumerator TextCoroutine()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        
        while(text.color.a >0)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - Time.deltaTime * 1);
            //text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - Time.deltaTime * 2f);
        }

    }

    public bool GetButtonStopFlag()
    {
        return stopButtonFlag;
    }

}
