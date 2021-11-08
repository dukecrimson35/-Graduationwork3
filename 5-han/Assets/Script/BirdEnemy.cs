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
    int a = 1;

    public Vector3 playerPos;//プレイヤーのポジション
    float alfa;//関数上の傾き

    enum State
    {
        nomal,//通常
        attack//攻撃
    }
    State state;


    // Start is called before the first frame update
    void Start()
    {
        //初期値の設定
        defaultX = transform.position.x;
        defaultY = transform.position.y;
        Upmove = true;
        LeftMove = true;
        rigidbody = GetComponent<Rigidbody>();
        state = State.attack;
    }

    // Update is called once per frame
    void Update()
    {
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
                    rigidbody.velocity = new Vector3(-speed, speed, 0);
                }
                else
                {
                    rigidbody.velocity = new Vector3(-speed, -speed, 0);
                }
            }
            //右に移動
            else
            {
                //上下に移動
                if (Upmove)
                {
                    rigidbody.velocity = new Vector3(speed, speed, 0);
                }
                else
                {
                    rigidbody.velocity = new Vector3(speed, -speed, 0);
                }
            }
        }
        //降下攻撃
        if(state == State.attack)
        {
            alfa = (transform.position.y - playerPos.y) / Mathf.Pow(transform.position.x - playerPos.x, 2);
            Debug.Log(alfa);
            //プレイヤーを頂点とした二次関数敵挙動　y=a(x-プレイヤーX)²+プレイヤーY 　→横移動させるだけで放物線を作る
            transform.position = new Vector3(transform.position.x-0.03f*speed,alfa*Mathf.Pow(transform.position.x-playerPos.x,2)+playerPos.y,0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Block")
        {
            //フラグの転換
            LeftMove = !LeftMove;
            Upmove = !Upmove;
        }
        if(collision.gameObject.tag == "Enemy")
        {
            //フラグの転換
            LeftMove = !LeftMove;
            Upmove = !Upmove;
        }
    }
}
