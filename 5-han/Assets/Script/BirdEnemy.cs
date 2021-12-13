using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdEnemy : MonoBehaviour
{
    Rigidbody rigidbody;
    float defaultY;//高さの初期値
    public float Ymove;//上下の移動値
    float defaultX;//横の初期値
    public float Xmove;//横の移動分
    bool Upmove;//縦移動フラグ
    bool LeftMove;//横移動フラグ
    public float speed;//移動速度
    float moveX = 0.03f;
    float count;//攻撃までのカウント
    public GameObject searchRange;//索敵範囲
    SearchRange searchScript;//索敵範囲のスクリプト
    float size = 0.08f;
    public GameObject Texture;//自分の画像
    GameObject player;
    GameObject bossArea;//ボス演出判定
    bossspawn bossspawn;//ボス演出用

    Vector3 playerPos;//プレイヤーのポジション
    float alfa;//関数上の傾き

    enum State
    {
        nomal,//通常
        attack,//攻撃
        wait//攻撃待機
    }
    State state;


    // Start is called before the first frame update
    void Start()
    {
        UnityEditor.EditorApplication.isPaused = true;
        //初期値の設定
        defaultX = transform.position.x;
        defaultY = transform.position.y;
        Upmove = true;
        LeftMove = true;
        rigidbody = GetComponent<Rigidbody>();
        state = State.nomal;
        playerPos = GameObject.Find("Player").transform.position;
        alfa = (transform.position.y - playerPos.y) / Mathf.Pow(transform.position.x - playerPos.x, 2);//傾きの設定
        searchScript = searchRange.GetComponent<SearchRange>();
        player = GameObject.Find("Player");
        bossArea = GameObject.Find("BossSpawn");
        bossspawn = bossArea.GetComponent<bossspawn>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale <= 0)
        {
            return;
        }

        if (state == State.nomal)
        {
            if (transform.position.y >= defaultY + Ymove)
            {
                //フラグの転換
                Upmove = false;
            }
            else if (transform.position.y <= defaultY - Ymove)
            {
                Upmove = true;
            }
            if (transform.position.x >= defaultX + Xmove)
            {
                //フラグの転換
                LeftMove = true;
            }
            else if(transform.position.x <= defaultX - Xmove)
            {
                LeftMove = false;
            }
            //左に移動
            if (LeftMove)
            {
                //上下に移動
                if (Upmove)
                {
                    rigidbody.velocity = new Vector3(-speed*Time.deltaTime, speed * Time.deltaTime, 0);
                }
                else
                {
                    rigidbody.velocity = new Vector3(-speed * Time.deltaTime, -speed * Time.deltaTime, 0);
                }
            }
            //右に移動
            else
            {
                //上下に移動
                if (Upmove)
                {
                    rigidbody.velocity = new Vector3(speed * Time.deltaTime, speed * Time.deltaTime, 0);
                }
                else
                {
                    rigidbody.velocity = new Vector3(speed * Time.deltaTime, -speed * Time.deltaTime, 0);
                }
            }
            //画像の表示
            if(LeftMove)
            {
                Texture.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            else
            {
                Texture.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            if (searchScript.GetinRange())//プレイヤーが索敵範囲に入ったら
            {
                if (bossspawn.GetEnemyMove() && Data.voiceFlag == false)
                {
                    state = State.wait;
                }
            }
        }
        if(state == State.wait)
        {
            rigidbody.velocity = new Vector3(0,-0.2f,0);
            count += Time.deltaTime;
            //攻撃待機
            if(count >= 0.75f)//カウントが進んだら
            {
                //攻撃状態に切り替え
                if (bossspawn.GetEnemyMove() && Data.voiceFlag == false)
                {
                    state = State.attack;
                }
                count = 0;
                playerPos = GameObject.Find("Player").transform.position;
                alfa = (transform.position.y - playerPos.y) / Mathf.Pow(transform.position.x - playerPos.x, 2);//傾きの設定
                moveX = (transform.position.x - playerPos.x) * 0.005f;
                if (playerPos.x <= transform.position.x && moveX <=0)
                {
                    moveX = -moveX;
                }
                else if(playerPos.x > transform.position.x && moveX >= 0)
                {
                    moveX = -moveX;
                }
            }
            playerPos = player.transform.position;//プレイヤーの位置取得
            if (playerPos.x - transform.position.x >= 0)
            {
                Texture.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            if (playerPos.x - transform.position.x < 0)
            {
                Texture.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }

            if (searchScript.GetinRange() == false)
            {
                count = 0;
                state = State.nomal;
            }
        }
        //降下攻撃
        if(state == State.attack)
        {
            //攻撃中
            float x, y;
            x = transform.position.x - moveX * speed * Time.deltaTime;
            y = alfa * Mathf.Pow(x - playerPos.x, 2) + playerPos.y+0.2f;
            if(double.IsNaN(y) == false)
            {
                //プレイヤーを頂点とした二次関数敵挙動　y=a(x-プレイヤーX)²+プレイヤーY 　→横移動させるだけで放物線を作る
                transform.position = new Vector3(x, y, 0);
            }
            
            if(transform.position.y > defaultY)//元の高さに戻ったら
            {
                state = State.wait;
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Block")
        {
            //フラグの転換
            LeftMove = !LeftMove;
            Upmove = !Upmove;
            playerPos = new Vector3(((transform.position.x - playerPos.x) * 2) + playerPos.x, playerPos.y, 0);//左右反転の放物線作る
            moveX = -moveX;
        }
        if(collision.gameObject.tag == "Enemy")
        {
            //フラグの転換
            LeftMove = !LeftMove;
            Upmove = !Upmove;
        }
    }
}
