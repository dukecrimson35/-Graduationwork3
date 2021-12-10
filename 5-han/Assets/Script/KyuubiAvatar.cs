using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KyuubiAvatar : MonoBehaviour
{
    public float speed;//通常時の移動速度
    public GameObject Texture;//自分の画像
    Animator animator;//画像のアニメーター
    GameObject player;
    Vector3 playerPos;
    bool leftMove;//左右どちらに移動するか
    bool moveFlag;
    bool onGround;//地面にいるかどうか
    public float junpP;//ジャンプ力

    float count;//時間カウント

    Rigidbody rigidbody;

    float runoutdis;//走り抜ける処理


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        playerPos = player.transform.position;
        onGround = false;
        moveFlag = false;
        animator = Texture.GetComponent<Animator>();
        runoutdis = Random.Range(6, 10);
        speed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime;
        if (moveFlag)
        {
            animator.Play("Walk");
            if (speed <= 3)
            {
                speed += Time.deltaTime;
            }

            playerPos = player.transform.position;//プレイヤーのポジション取得
            if (leftMove)//左に移動
            {
                rigidbody.velocity = new Vector3(-Mathf.Pow(speed, 2) * 1.75f, rigidbody.velocity.y, 0);
                Texture.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                if (transform.position.x - playerPos.x <= -runoutdis)
                {
                    leftMove = false;
                    speed = 1;
                }
            }
            if (leftMove == false)//右に移動
            {
                rigidbody.velocity = new Vector3(Mathf.Pow(speed, 2) * 1.75f, rigidbody.velocity.y, 0);
                Texture.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                if (transform.position.x - playerPos.x >= runoutdis)
                {
                    leftMove = true;
                    speed = 1;
                }
            }

            if (onGround == true)
            {
                if (transform.position.x - playerPos.x >= 0)
                {
                    rigidbody.AddForce(new Vector3(-700, 300 * junpP, 0));
                    //Texture.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }
                if (transform.position.x - playerPos.x < 0)
                {
                    rigidbody.AddForce(new Vector3(700, 300 * junpP, 0));
                    //Texture.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                }
            }
        }

        if (count >= 2)
        {
            if (moveFlag == false)
            {
                moveFlag = true;
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
