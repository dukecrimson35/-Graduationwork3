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

    bool cuted;//斬られた
    SpriteRenderer sprite;

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
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cuted == false)
        {
            rigidbody.velocity = toPlayer * speed;
        }
        if(cuted)
        {
            rigidbody.velocity = new Vector3();
        }
        //そのうち消える処理
        c += Time.deltaTime;
        if(c >= 5)
        {
            Destroy(this.gameObject);
            Destroy(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerScript = other.gameObject.GetComponent<PlayerControl>();
            playerScript.Damage(10);
            Destroy(this.gameObject);
            Destroy(this);
        }

        if(other.gameObject.tag == "SenkuGiri")
        {
            cuted = true;
            sprite.enabled = false;
        }
        if (other.gameObject.tag == "PowerSlash")
        {
            cuted = true;
            sprite.enabled = false;
        }

        if(other.gameObject.tag == "Block")
        {
            Destroy(this.gameObject);
            Destroy(this);
        }
    }

    public bool GetCut()
    {
        return cuted;
    }
}
