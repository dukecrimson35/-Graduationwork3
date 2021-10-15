using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionCamera : MonoBehaviour
{
    public GameObject player;//プレイヤー
    PlayerControl playerControl;//プレイヤーのスクリプト
    public GameObject otherCamera;//他のカメラオブジェクト
    Camera me;//このカメラ
    Camera other;//他のカメラ

    public int hitcount;//

    // Start is called before the first frame update
    void Start()
    {
        me = GetComponent<Camera>();
        other = otherCamera.GetComponent<Camera>();

        if(me.enabled == true)
        {
            me.enabled = false;
            other.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //連撃終了後、連撃の回数がn回以上なら
        /*if(playerControl.GetHitCount() >= hitcount)
        {
            other.enabled = false;
            me.enabled = true;
            //演出カメラを使用
            DirectionMove();
        }*/
    }

    void DirectionMove()
    {
        //倒した一番遠い敵とプレイヤーの位置の中間に移動し、両方をカメラ内にとらえるように引く
        //連撃のエフェクトを倒した順番に行う
        //カメラを元に戻す
    }
}
