using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossspawn : MonoBehaviour
{
    public bool hitBossSpawn;
    public bool EnemyMove;
    public BossEnemy boss;
    public Camera BossCamera;
    float count;
    // Start is called before the first frame update
    void Start()
    {
        BossCamera.depth = -2;
        EnemyMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hitBossSpawn)
        {
            BossCamera.depth = 0;
            if(boss.hitGround)
            {
                BossCamera.transform.position = new Vector3(BossCamera.transform.position.x, Mathf.PingPong(Time.time, 0.2f), BossCamera.transform.position.z);
                count = count + Time.deltaTime;
                if(count>=4.0)
                {
                    BossCamera.depth = -2;
                }
            }
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            hitBossSpawn = true;
        }
    }

    private bool GetBossSpawn()
    {
        return hitBossSpawn;
    }
}
