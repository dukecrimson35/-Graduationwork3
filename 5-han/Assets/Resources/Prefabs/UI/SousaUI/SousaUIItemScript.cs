using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SousaUIItemScript : MonoBehaviour
{
    public Text kaihukuNum;
    public Text kaihuku2Num;

    public bool flag; 
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            kaihukuNum.text = Data.kaihuku.ToString();
        }
        else
        {
            kaihuku2Num.text = Data.kaihuku2.ToString();
        }
    }
}
