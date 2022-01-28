using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SousaUIItemAlphaScript : MonoBehaviour
{

    public Text text;
    public Text text2;

    public Image img1;
    public Image img2;
    public Image img3;

    bool oneFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!Data.oneUIFlag && Data.kaihuku == 0 && Data.kaihuku2 == 0)
        { 
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
            text2.color = new Color(text2.color.r, text2.color.g, text2.color.b, 0);

            img1.color = new Color(img1.color.r, img1.color.g, img1.color.b, 0);
            img2.color = new Color(img2.color.r, img2.color.g, img2.color.b, 0);
            img3.color = new Color(img3.color.r, img3.color.g, img3.color.b, 0);
            
        }
        else
        {
            if(text.color.a<1)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + Time.deltaTime * 2);
                text2.color = new Color(text2.color.r, text2.color.g, text2.color.b, text2.color.a + Time.deltaTime * 2);

                img1.color = new Color(img1.color.r, img1.color.g, img1.color.b, img1.color.a + Time.deltaTime * 2);
                img2.color = new Color(img2.color.r, img2.color.g, img2.color.b, img2.color.a + Time.deltaTime * 2);
                img3.color = new Color(img3.color.r, img3.color.g, img3.color.b, img3.color.a + Time.deltaTime * 2);
            }
          
            Data.oneUIFlag = true;
        }
    }
}
