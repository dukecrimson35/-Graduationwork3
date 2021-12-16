using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SousaUIScript : MonoBehaviour
{
    public Image y;
    public Image x;
    public Image b;

    public Sprite y2;
    public Sprite x2;
    public Sprite b2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Data.xSkill)
        {
            x.sprite = x2;
        }
       
        if (Data.bSkill)
        {
            b.sprite = b2;
        }
    }
}
