using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpAlphaScript : MonoBehaviour
{
    public Image a;
    public Image b;
    public Image c;
    bool oneFlag = false;
    bool alphaFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Data.voiceFlag && !oneFlag)
        {
            alphaFlag = true;
        }
        if(!Data.voiceFlag && alphaFlag)
        {
            if (a.color.a < 1)
            {
                a.color = new Color(a.color.r, a.color.g, a.color.b, a.color.a + Time.deltaTime * 2);
                b.color = new Color(b.color.r, b.color.g, b.color.b, b.color.a + Time.deltaTime * 2);
                c.color = new Color(c.color.r, c.color.g, c.color.b, c.color.a + Time.deltaTime * 2);
            }
        }

        
    }
}
