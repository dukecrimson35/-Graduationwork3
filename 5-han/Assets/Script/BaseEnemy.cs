using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public int baseEnemyHp = 10;
    private float nextTime;
    float damageInterval = 1f;
    bool damage;
    bool deadFlag;
    int count=0;
    Renderer renderer;
    void Start()
    {
        damage = false;
        nextTime = Time.time;
        renderer = GetComponent<Renderer>();
        deadFlag = false;
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
            deadFlag = true;
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Destroy(gameObject);
            deadFlag = true;
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "SenkuGiri")
        {
            baseEnemyHp -= 10;
        }
    }

    public int GetHp()
    {
        return baseEnemyHp;
    }

    public bool GetDeadFlag()
    {
        return deadFlag;
    }
}
