using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectImageChange : MonoBehaviour
{
    // Start is called before the first frame update

    public ButtonSelectScript bss;

    public int pos;

    public Sprite[] sprites;

    public Image image;
    
    void Start()
    {
        pos = bss.GetPos();
    }

    // Update is called once per frame
    void Update()
    {
        pos = bss.GetPos();

        if(pos < 3)
        {
            image.sprite = sprites[pos];
        }
        
    }
}
