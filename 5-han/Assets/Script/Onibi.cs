using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onibi : MonoBehaviour
{
    public GameObject mother;//中心とする親オブジェクト

    public float speed;//速度
    bool leftMove;//左右移動どちらか
    bool upMove;//上下移動どちらか
    Vector3 axis;
    PlayerControl playerScript;//プレイヤーのスクリプト(ダメージ与えるよう)
    Vector3 movetoPos;//直線移動時の移動先

    enum Movetype
    {
        rotate,//円運動
        bullet,//直線移動
        stay//とどまる
    }
    Movetype type;//移動パターン

    Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        axis = Vector3.forward;
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (type == Movetype.rotate)
        {
            //円軌道
            var tr = transform;
            // 回転のクォータニオン作成
            var angleAxis = Quaternion.AngleAxis(360 / speed * Time.deltaTime, axis);

            // 円運動の位置計算
            var pos = tr.position;

            pos -= mother.transform.position;
            pos = angleAxis * pos;
            pos += mother.transform.position;

            tr.position = pos;
        }

        if(type == Movetype.bullet)
        {
            if (transform.position != movetoPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, movetoPos, speed * Time.deltaTime);
            }
            else
            {
                type = Movetype.stay;
            }
        }

        if(type == Movetype.stay)
        {

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerScript = other.gameObject.GetComponent<PlayerControl>();
            playerScript.Damage(10);
        }
    }

    public void SetMom(GameObject mother)//中心オブジェクト設定
    {
        this.mother = mother;
    }

    public void SetMovetoPos(Vector3 toPos)
    {
        movetoPos = toPos;
    }

    public void SetType(string Typename)
    {
        if (Typename == "bullet")
        {
            type = Movetype.bullet;
        }
        if (Typename == "rotate")
        {
            type = Movetype.rotate;
        }
        if (Typename == "stay")
        {
            type = Movetype.stay;
        }
    }
}
