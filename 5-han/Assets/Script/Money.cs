using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    //プロパティ
    public int amount;//金額


    // Start is called before the first frame update
    void Start()
    {
        if(amount == 0)
        {
            SetAount(20);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAount(int amount)
    {
        this.amount = amount;
    }

    public int GetMoney()
    {
        return amount;
    }
}
