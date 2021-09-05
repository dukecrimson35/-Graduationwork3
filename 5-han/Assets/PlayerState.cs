using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    int item;
    int hp = 100;
    bool deadFlag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DeadCheck();
    }

    void DeadCheck()
    {
        if (hp <= 0)
        {
            deadFlag = true;
        }
        else
        {
            deadFlag = false;
        }
    }

    public void Damage(int damage)
    {
        hp -= damage;
    }
    public bool GetDeadFlag()
    {
        return deadFlag;
    }
}
