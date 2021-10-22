using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    //プロパティ
    public int amount;//金額
    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        if(amount == 0)
        {
            SetAount(20);
        }
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Data.shopFlag == true)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
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
