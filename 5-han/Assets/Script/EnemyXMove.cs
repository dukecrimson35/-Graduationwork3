using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyXMove : MonoBehaviour
{
    // Start is called before the first frame update
    public int baseEnemyHp = 10;
    private float nextTime;
    float damageInterval = 1f;
    bool damage;
    bool deadFlag;
    int count = 0;
    Renderer renderer;
    bool LMove;
    bool RMove;
    Vector3 pos;
    void Start()
    {
        damage = false;
        deadFlag = false;
        nextTime = Time.time;
        renderer = GetComponent<Renderer>();
        LMove = true;
        RMove = false;
        pos = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (LMove&&!RMove)
        {
            pos.x -= 0.01f;
        }
        if(!LMove&&RMove)
        {
            pos.x += 0.01f;
        }
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
        if (Input.GetKeyDown(KeyCode.Q))
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
            if (count == 4)
            {
                damage = false;
                count = 0;
            }
        }
        transform.position += pos;
        pos = Vector3.zero;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "SenkuGiri")
        {
            baseEnemyHp -= 10;
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "LSide")
        {
            LMove = false;
            RMove = true;
        }
        if (collision.gameObject.tag == "RSide")
        {
            LMove = true;
            RMove = false;
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
