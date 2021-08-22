using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public int baseEnemyHp;
    private float nextTime;
    float damageInterval = 1f;
    bool damage;
    int count=0;
    Renderer renderer;
    void Start()
    {
        damage = false;
        nextTime = Time.time;
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            baseEnemyHp--;
            damage = true;
        }
        if (baseEnemyHp <= 0)
        {
            Destroy(gameObject);
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Destroy(gameObject);
        }
        if (damage)
        {
            if (Time.time > nextTime)
            {
                renderer.enabled = !renderer.enabled;

                nextTime += damageInterval;
                count++;
            }
            if(count==4)
            {
                damage = false;
                count = 0;
            }
        }
    }
}
