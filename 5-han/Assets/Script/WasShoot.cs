using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasShoot : MonoBehaviour
{
    Rigidbody rigidbody;//自身のリジッドボディ

    public float speed;//弾の速度   
    GameObject player;
    PlayerControl playerScript;//プレイヤーのスクリプト(ダメージ与えるよう)
    Vector3 toPlayer;//プレイヤーの単位ベクトル

    float c;

    // Start is called before the first frame update
    void Start()
    {
        if(speed <= 0)
        {
            speed = 1;
        }
        rigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        //プレイヤーがいる方向の単位ベクトルを取得
        toPlayer = Vector3.Normalize(player.transform.position - transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = toPlayer*speed;

        //そのうち消える処理
        c += Time.deltaTime;
        if(c >= 5)
        {
            Destroy(this.gameObject);
            Destroy(this);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerScript = collision.gameObject.GetComponent<PlayerControl>();
            playerScript.Damage(10);
            Destroy(this.gameObject);
            Destroy(this);
        }
    }
}
