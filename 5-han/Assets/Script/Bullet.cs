﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bossEnemy;
    Vector3 pos;
    public bool RMove;
    public bool LMove;
    public float deadSecond;
    // Start is called before the first frame update
    void Start()
    {
        pos = Vector3.zero;
        bossEnemy = GameObject.Find("BossEnemy");
        RMove = bossEnemy.GetComponent<BossEnemy>().GetRight();
        LMove = bossEnemy.GetComponent<BossEnemy>().GetLeft();
        deadSecond = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (LMove && !RMove)
        {
            pos.x -= 0.01f;
        }
        if (!LMove && RMove)
        {
            pos.x += 0.01f;
        }
        deadSecond += Time.deltaTime;
        if (deadSecond >= 3)
        {
            Destroy(gameObject);
        }
        transform.position += pos;
        pos = Vector3.zero;
    }
}
