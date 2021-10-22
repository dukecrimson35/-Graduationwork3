﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEnemy : MonoBehaviour
{
    public GameObject bullet;//攻撃に使う弾
    float timeCount;//時間のカウント
    public GameObject searchRange;
    SearchRange search;//索敵範囲スクリプト

    // Start is called before the first frame update
    void Start()
    {
        timeCount = 0;
        search = searchRange.GetComponent<SearchRange>();
    }

    // Update is called once per frame
    void Update()
    {
        if (search.GetinRange())
        {
            timeCount += Time.deltaTime;
            if (timeCount >= 2)
            {
                Attack();
                timeCount = 0;
            }
        }
    }

    void Attack()//攻撃
    {
        Instantiate(bullet,transform.position,new Quaternion());
    }

}
