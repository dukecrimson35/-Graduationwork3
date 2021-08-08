using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public int baseEnemyHp;
    void Start()
    {
        baseEnemyHp = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            baseEnemyHp--;
        }
        if (baseEnemyHp <= 0)
        {
            Destroy(gameObject);
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Destroy(gameObject);
        }
    }
}
