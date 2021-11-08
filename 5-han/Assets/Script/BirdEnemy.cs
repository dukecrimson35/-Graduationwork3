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
    float speed;//移動速度

    enum state
    {
        nomal,//通常
        attack//攻撃
    }


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //上下に移動
        if(transform.position.y >= defaultY + Ymove || transform.position.y <= defaultY - Ymove)
        {
            //フラグの転換
            Upmove = !Upmove;
        }
        if(transform.position.x >= defaultX + Xmove || transform.position.x <= defaultX - Xmove)
        {
            //フラグの転換
            LeftMove = !LeftMove;
        }

        if (LeftMove)
        {
            if (Upmove)
            {
                rigidbody.velocity = new Vector3(-speed, speed, 0);
            }
            else
            {
                rigidbody.velocity = new Vector3(-speed, -speed, 0);
            }
        }
        else
        {
            if (Upmove)
            {
                rigidbody.velocity = new Vector3(speed, speed, 0);
            }
            else
            {
                rigidbody.velocity = new Vector3(speed, -speed, 0);
            }
        }

        //降下攻撃
    }
}
