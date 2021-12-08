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
    public SpriteRenderer haikeisprite;

    public GameObject haikei;

    
    void Start()
    {
        pos = bss.GetPos();
        haikeisprite = haikei.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        pos = bss.GetPos();

        if(pos < 3)
        {
            //image.sprite = sprites[pos];
            haikeisprite.sprite = sprites[pos];

        }
        
    }
}
