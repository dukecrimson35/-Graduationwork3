﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinTextUIScript : MonoBehaviour
{
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "×" + Data.coin.ToString();


        if(Input.GetKey(KeyCode.A))
        {
            Data.coin += 100;
        }
    }
}
