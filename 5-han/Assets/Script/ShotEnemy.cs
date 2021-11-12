using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEnemy : MonoBehaviour
{
    public GameObject bullet;//攻撃に使う弾
    float timeCount;//時間のカウント
    public GameObject searchRange;
    SearchRange search;//索敵範囲スクリプト
    GameObject player;
    Vector3 playerPos;
    public GameObject Texture;

    bossspawn bossspawn;//ボス演出用
    GameObject bossArea;//ボス演出判定

    private AudioSource audioSource;
    public AudioClip SE;

    // Start is called before the first frame update
    void Start()
    {
        timeCount = 0;
        search = searchRange.GetComponent<SearchRange>();
        audioSource = GetComponent<AudioSource>();
        bossArea = GameObject.Find("BossSpawn");
        bossspawn = bossArea.GetComponent<bossspawn>();
        player = GameObject.Find("Player");
        playerPos = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;//プレイヤーの位置取得
        if (playerPos.x - transform.position.x >= 0)
        {
            Texture.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        if (playerPos.x - transform.position.x < 0)
        {
            Texture.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

        if (bossspawn.GetEnemyMove() && Data.voiceFlag == false)
        {
            if (search.GetinRange())
            {
                timeCount += Time.deltaTime;
                if (timeCount >= 2)
                {
                    Attack();
                    timeCount = 0;
                    //攻撃音
                    if (SE != null)
                    {
                        audioSource.PlayOneShot(SE);
                    }
                }
            }
        }
    }

    void Attack()//攻撃
    {
        Instantiate(bullet,transform.position,new Quaternion());
    }

}
