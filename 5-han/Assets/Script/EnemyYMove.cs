using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyYMove : MonoBehaviour
{
    // Start is called before the first frame update
    public int baseEnemyHp = 10;
    private float nextTime;
    float damageInterval = 1f;
    bool damage;
    int count = 0;
    Renderer renderer;
    float nowPosY;
    void Start()
    {
        damage = false;
        nextTime = Time.time;
        renderer = GetComponent<Renderer>();
        nowPosY = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //PingPong関数:指定した振幅や周期で往復させる
        //PingPong(時間,値) 値は-値にすることはできない
        //今の位置から＋値の数値まで行って往復してくる処理
        transform.position = new Vector3(transform.position.x, nowPosY + Mathf.PingPong(Time.time / 2, 3), transform.position.z);
        if (Input.GetKeyDown(KeyCode.W))
        {
            baseEnemyHp--;
            damage = true;
        }
        if (baseEnemyHp <= 0)
        {
            Destroy(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.Q))
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
            if (count == 4)
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
}
