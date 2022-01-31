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
    SpriteRenderer sprite;

    float count;//時間カウント
    int delayTime;//動くまでの時間

    Rigidbody rigidbody;

    float runoutdis;//走り抜ける処理
    bool hitflag;
    float hitcount;//左右壁に当たってからの時間

    public GameObject reverse;//反転判定
    WallRange range;


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
        if (reverse != null)
        {
            range = reverse.GetComponent<WallRange>();
        }
        sprite = Texture.GetComponent<SpriteRenderer>();
        //delayTime = 3;//動くまでの時間の初期値100秒
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale <= 0) return;//ポーズ中は動かない

        if(sprite.color.a < 0.75f)
        {
            sprite.color += new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.01f);
        }

        count += Time.deltaTime;
        if (hitflag)
        {
            hitcount += Time.deltaTime;
            if (hitcount >= 0.1f)
            {
                hitflag = false;
                hitcount = 0;
            }
        }
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

            if (onGround == true)//ちょっと浮かす処理
            {
                if (transform.position.x - playerPos.x >= 0)
                {
                    transform.position = new Vector3(transform.position.x,transform.position.y + 0.01f, transform.position.z);
                    //Texture.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }
                if (transform.position.x - playerPos.x < 0)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
                    //rigidbody.AddForce(new Vector3(700, 300 * junpP, 0));
                    //Texture.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                }
            }
        }

        if (count >= delayTime)
        {
            if (moveFlag == false)
            {
                moveFlag = true;
            }
        }

        if (range != null)//左右壁判定処理
        {
            if (range.GetinRange())//壁に当たったら
            {
                if (hitflag == false)
                {
                    leftMove = !leftMove;
                    hitflag = true;
                }
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

    public void SetDelayTime(int delay)//動くまでの時間設定
    {
        delayTime = delay;
    }
}
