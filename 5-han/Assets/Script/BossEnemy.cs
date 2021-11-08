﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public int BossEnemyHp = 50;
    private float nextTime;
    float damageInterval = 1f;
    bool damage;
    bool deadFlag;
    int count = 0;
    Renderer renderer;
    SpriteRenderer spr;
    bool LMove;
    bool RMove;
    bool MoveMode;
    bool AtkModeMelee;
    bool AtkModeRange;
    public float Meleesecond;
    public float Rangesecond;
    public float ResetMelee;
    Vector3 pos;
    public GameObject bullet;
    public GameObject MeleeWepon;
    public GameObject RMeleeWepon;
    float wepRot;
    public Sprite aliveBoss;
    public Sprite deadBoss;
    float scenechangetime;
    public bool hitGround;
    public bossspawn bossspawn;
    Rigidbody rigid;
    ParticleSystem particle;
    void Start()
    {
        damage = false;
        deadFlag = false;
        nextTime = 0;
        renderer = GetComponent<Renderer>();
        spr = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody>();
        rigid.useGravity = false;
        spr.sprite = aliveBoss;
        LMove = true;
        RMove = false;
        MoveMode = false;
        AtkModeMelee = false;
        AtkModeRange = false;
        pos = Vector3.zero;
        Meleesecond = 0;
        Rangesecond = 0;
        ResetMelee = 0;
        scenechangetime = 0;
        MeleeWepon.SetActive(false);
        RMeleeWepon.SetActive(false);
        wepRot = 0.3f;
        particle = GetComponent<ParticleSystem>();
        //particle.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if(bossspawn.hitBossSpawn)
        {
            rigid.useGravity = true;
        }
        if (Time.timeScale <= 0) return;
        //移動処理
        if (bossspawn.GetEnemyMove() && Data.voiceFlag == false)
        {
            if (BossEnemyHp <= 180)
            {
                MoveMode = true;
            }
            if (MoveMode)
            {
                if (LMove && !RMove)
                {
                    pos.x -= 0.03f;
                    this.transform.rotation = new Quaternion(0, 0, 0, 0);
                }
                if (!LMove && RMove)
                {
                    pos.x += 0.03f;
                    this.transform.rotation = new Quaternion(0, 180, 0, 0);
                }
            }
            //近接攻撃処理
            Meleesecond += Time.deltaTime;
            if (Meleesecond >= 5)
            {
                //MeleeSecが10になったら近接攻撃モードON
                AtkModeMelee = true;
            }
            if (AtkModeMelee)
            {
                //if (LMove && !RMove)
                //{
                //    pos.x -= 0.01f;
                //}
                //if (!LMove && RMove)
                //{
                //    pos.x += 0.01f;
                //}
                //モードリセットの時間計算
                ResetMelee += Time.deltaTime;
                if (LMove && !RMove)
                {
                    //武器表示
                    MeleeWepon.SetActive(true);
                    RMeleeWepon.SetActive(false);
                    //武器回転
                    MeleeWepon.transform.Rotate(0, 0, wepRot);
                }
                if (!LMove && RMove)
                {
                    MeleeWepon.SetActive(false);
                    RMeleeWepon.SetActive(true);
                    RMeleeWepon.transform.Rotate(0, 0, wepRot);
                }
                if (ResetMelee >= 5)
                {
                    MeleeWepon.transform.rotation = Quaternion.Euler(0, 0, 28);
                    RMeleeWepon.transform.rotation = Quaternion.Euler(0, 180, 28);
                    //武器非表示
                    MeleeWepon.SetActive(false);
                    RMeleeWepon.SetActive(false);
                    //モード変更とモードリセットの時間をリセットで再度使用できるようにしておく
                    Meleesecond = 0;
                    ResetMelee = 0;
                    //攻撃モードをOFFにする
                    AtkModeMelee = false;
                }

            }
            //遠距離攻撃処理
            Rangesecond += Time.deltaTime;
            if (Rangesecond >= 2.5f)
            {
                Instantiate(bullet, this.transform.position, Quaternion.identity);
                Rangesecond = 0;
            }
            //終わり
            if (Input.GetKeyDown(KeyCode.W))
            {
                BossEnemyHp--;
                damage = true;
            }
            if (BossEnemyHp <= 0)
            {
                scenechangetime += Time.deltaTime;
                spr.sprite = deadBoss;
                //if(scenechangetime>=2)
                //{
                deadFlag = true;
                Destroy(gameObject);
                //}
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                scenechangetime += Time.deltaTime;
                spr.sprite = deadBoss;
                //if (scenechangetime >= 2)
                //{
                deadFlag = true;
                Destroy(gameObject);
                //}
            }
            if (damage)
            {
                renderer.material.color = Color.red;
                nextTime += Time.deltaTime;
                if (nextTime >= 2)
                {
                    damage = false;
                    nextTime = 0;
                }
            }
            if (!damage)
            {
                renderer.material.color = Color.white;
            }
            transform.position += pos;
            pos = Vector3.zero;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "SenkuGiri")
        {
            damage = true;
            BossEnemyHp -= 10;
        }
        if (collision.gameObject.tag == "LSide")
        {
            //AtkModeMelee = false;
            //Meleesecond = 0;
            LMove = false;
            RMove = true;
        }
        if (collision.gameObject.tag == "RSide")
        {
            //AtkModeMelee = false;
            //Meleesecond = 0;
            LMove = true;
            RMove = false;
        }
        if (collision.gameObject.tag == "Player")
        {
            PlayerControl p = collision.gameObject.GetComponent<PlayerControl>();
            p.Damage(10);
            p.KnockBack(gameObject);
        }
        if(collision.gameObject.tag=="Block")
        {
            hitGround = true;
            //particle.Play();
        }
    }
    public int GetHp()
    {
        return BossEnemyHp;
    }

    public bool GetDeadFlag()
    {
        return deadFlag;
    }

    public bool GetLeft()
    {
        return LMove;
    }
    public bool GetRight()
    {
        return RMove;
    }
    public ParticleSystem Getparticle()
    {
        return particle;
    }
}
