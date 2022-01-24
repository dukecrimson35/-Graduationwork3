using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastEnemy : MonoBehaviour
{
    public float speed;//通常時の移動速度
    public GameObject Texture;//自分の画像
    SearchRange range;//索敵範囲スクリプト
    public GameObject searchRange;//索敵範囲
    float count;
    bool moveFlag;
    int direction;//左右移動どちらにするか
    Rigidbody rigidbody;
    Animator animator;
    GameObject player;
    Vector3 playerPos;
    public int junpP;//ジャンプ力
    bool leftMove;//左右どちらに移動するか

    bool onGround;//地面にいるかどうか

    enum State
    {
        normal,
        careful,
        attack,
    }
    State state;

    // Start is called before the first frame update
    void Start()
    {
        state = State.normal;
        count = 0;
        moveFlag = false;
        rigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        playerPos = player.transform.position;
        if (searchRange != null)
        {
            range = searchRange.GetComponent<SearchRange>();
        }
        onGround = true;
    }

    // Update is called once per frame
    void Update()
    {
        #region 通常モード
        if (state == State.normal)
        {
            count += Time.deltaTime;
            if (count >= direction)
            {
                moveFlag = !moveFlag;
                count = 0;
                direction = Random.Range(1, 6);
            }
            //移動
            if (moveFlag)
            {
                if (direction % 2 == 1)//方向の値が奇数
                {
                    rigidbody.velocity = new Vector3(-speed, rigidbody.velocity.y, 0);
                    Texture.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }
                if (direction % 2 == 0)//方向の値が偶数
                {
                    rigidbody.velocity = new Vector3(speed, rigidbody.velocity.y, 0);
                    Texture.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                }
            }
            else//移動しない
            {

            }

            if (range != null)
            {
                if (range.GetinRange())//プレイヤーが索敵範囲に入ったら
                {
                    state = State.careful;//攻撃モードに
                }
            }
        }
        #endregion

        #region 警戒モード
        if (state == State.careful)
        {
            if(speed <= 2)
            {
                speed += Time.deltaTime;
            }
            playerPos = player.transform.position;//プレイヤーのポジション取得
            //if (Mathf.Abs(transform.position.x - playerPos.x) > 3)//遠くにいるなら
            //{
            //    //プレイヤーに向かう
            //    if (transform.position.x - playerPos.x >= 0)//プレイヤーが左側
            //    {
            //        rigidbody.velocity = new Vector3(-speed * 1.75f, rigidbody.velocity.y, 0);
            //        Texture.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            //    }
            //    if (transform.position.x - playerPos.x < 0)//プレイヤーが右側
            //    {
            //        rigidbody.velocity = new Vector3(speed * 1.75f, rigidbody.velocity.y, 0);
            //        Texture.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            //    }
            //}
            if(leftMove)//左に移動
            {
                rigidbody.velocity = new Vector3(-Mathf.Pow(speed ,2) * 1.75f, rigidbody.velocity.y, 0);
                Texture.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                if(transform.position.x - playerPos.x <= -5)
                {
                    leftMove = false;
                    speed = 1;
                }
            }
            if (leftMove == false)//右に移動
            {
                rigidbody.velocity = new Vector3(Mathf.Pow(speed,2) * 1.75f, rigidbody.velocity.y, 0);
                Texture.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                if (transform.position.x - playerPos.x >= 5)
                {
                    leftMove = true;
                    speed = 1;
                }
            }

            if (range != null)
            {
                if (range.GetinRange() == false)//プレイヤーが索敵範囲にいない
                {
                    state = State.normal;//通常モードに
                }
            }

            if (onGround == true)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
            }
        }
        #endregion

        #region 攻撃モード
        if (state == State.attack)
        {
            if (onGround == true)
            {
                count += Time.deltaTime;
                if (transform.position.x - playerPos.x >= 0)
                {
                    Texture.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }
                if (transform.position.x - playerPos.x < 0)
                {
                    Texture.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                }
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
            }
            playerPos = player.transform.position;
            if (count >= 1)//攻撃に入って３秒後
            {
                //ジャンプ攻撃
                if (transform.position.x - playerPos.x >= 0)
                {
                    rigidbody.AddForce(new Vector3(-700, 300, 0)*junpP);
                    Texture.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }
                if (transform.position.x - playerPos.x < 0)
                {
                    rigidbody.AddForce(new Vector3(700, 300, 0)*junpP);
                    Texture.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                }
                count = 0;
            }

            if (onGround == true && count < 0.3f && Mathf.Abs(transform.position.x - playerPos.x) > 5)
            {
                if (count >= 0.2f)
                {
                    state = State.careful;
                }
            }
        }
        #endregion
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
