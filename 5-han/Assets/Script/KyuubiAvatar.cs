using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KyuubiAvatar : MonoBehaviour
{
    public float speed;//通常時の移動速度
    public GameObject Texture;//自分の画像
    GameObject player;
    Vector3 playerPos;
    bool leftMove;//左右どちらに移動するか
    bool onGround;//地面にいるかどうか

    Rigidbody rigidbody;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;//プレイヤーのポジション取得
        if (leftMove)//左に移動
        {
            rigidbody.velocity = new Vector3(-Mathf.Pow(speed, 2) * 1.75f, rigidbody.velocity.y, 0);
            Texture.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            if (transform.position.x - playerPos.x <= -5)
            {
                leftMove = false;
                speed = 1;
            }
        }
        if (leftMove == false)//右に移動
        {
            rigidbody.velocity = new Vector3(Mathf.Pow(speed, 2) * 1.75f, rigidbody.velocity.y, 0);
            Texture.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            if (transform.position.x - playerPos.x >= 5)
            {
                leftMove = true;
                speed = 1;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Block")
        {
            onGround = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Block")//地面と衝突したとき
        {
            onGround = true;
        }
    }
}
