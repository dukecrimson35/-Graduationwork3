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
    void Start()
    {
        for(int i = 1; i< count+1;i++)
        {
            string str = "Prefabs/UI/makimonoAni/(" + i.ToString() + ")";
            Sprite sp = Resources.Load<Sprite>(str);
            sprites.Add(sp);
        }
        StartCoroutine(Coroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if(backFlag)
        {
            StartCoroutine(BackCoroutine());
        }
    }

    float delay = 0.08f;
    IEnumerator Coroutine()
    {
        for(int i = 0; i< count;i++)
        {
            image.sprite = sprites[i];
            yield return new WaitForSecondsRealtime(delay);
        }
       
        
    }

    float delay2 = 0.05f;

    IEnumerator BackCoroutine()
    {
        for (int i = count-1; i >-1 ; i--)
        {
            image.sprite = sprites[i];
            yield return new WaitForSecondsRealtime(delay2);
        }


    }
}
