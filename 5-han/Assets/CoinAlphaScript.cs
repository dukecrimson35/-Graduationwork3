using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinAlphaScript : MonoBehaviour
{
    public Text text;
    public Image a;

    bool oneFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Data.voiceFlag)
        {
            oneFlag = true;
        }
        if(oneFlag)
        {
            if(text.color.a >0)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - Time.deltaTime);
                a.color = new Color(a.color.r, a.color.g, a.color.b, a.color.a - Time.deltaTime);
            }
          

        }
    }
}
