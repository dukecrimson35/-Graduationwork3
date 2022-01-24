using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    public int BossEnemyHp = 200;
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
    float deadTimer;
    //public Sprite aliveBoss;
    //public Sprite deadBoss;
    public float scenechangetime;
    public bool hitGround;
    public bossspawn bossspawn;
    Rigidbody rigid;
    ParticleSystem particle;
    BoxCollider collider;
    float changeColSize;
    bool cleaItemSpawnFlag;
    public float chargeTime;
    public GameObject talkUI;
    void Start()
    {
        chargeTime = 0; 
        changeColSize = 0.5f;
        anim = GetComponent<Animator>();
        damage = false;
        deadFlag = false;
        nextTime = 0;
        renderer = GetComponent<Renderer>();
        spr = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();
        rigid.useGravity = false;
        //spr.sprite = aliveBoss;
        LMove = true;
        RMove = false;
        MoveMode = false;
        AtkModeMelee = false;
        AtkModeRange = false;
        pos = Vector3.zero;
        Meleesecond = 0;
        Rangesecond = 0;
        ResetMelee = 0;
        scenechangetime = 0f;
        MeleeWepon.SetActive(false);
        RMeleeWepon.SetActive(false);
        wepRot = 2.0f;
        particle = GetComponent<ParticleSystem>();
        anim.SetBool("Dead", false);
        //anim.Play("OniStand");
        //particle.Stop();
        cleaItemSpawnFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(bossspawn.hitBossSpawn)
        {
            rigid.useGravity = true;
        }
        if (Time.timeScale <= 0) return;
        if (deadFlag)
        {
            deadTimer += Time.deltaTime;
            if (deadTimer >= 9)
            {
                anim.SetBool("Dead", true);
                collider.size = new Vector3(1, 0.4f, 1);
            }
            if (deadTimer >= 11)
            {
                Destroy(gameObject);
                if (!cleaItemSpawnFlag)
                {
                    cleaItemSpawnFlag = true;
                    GameObject drop = Instantiate((GameObject)Resources.Load("ClearItem"));
                    drop.transform.position = transform.position;
                }
            }
        }
        //移動処理
        if (bossspawn.GetEnemyMove() && Data.voiceFlag == false)
        {
            if (BossEnemyHp <= 190)
            {
                MoveMode = true;
                Meleesecond += Time.deltaTime;
                Destroy(particle);
            }
            //MoveMode = true;
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
            if (Meleesecond >= 5)
            {
                renderer.material.color = Color.green;
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
                //anim.SetBool("Melee", true);
                ResetMelee += Time.deltaTime;
                //if (LMove && !RMove)
                //{
                //    pos.x -= 0.03f;
                //    //武器表示
                //    MeleeWepon.SetActive(true);
                //    RMeleeWepon.SetActive(false);
                //    //武器回転
                //    MeleeWepon.transform.Rotate(0, 0, wepRot);
                //}
                anim.SetBool("Melee", true);
                if (LMove && !RMove)
                {
                    pos.x -= 0.03f;
                    //武器表示
                    MeleeWepon.SetActive(true);
                    RMeleeWepon.SetActive(false);
                    //武器回転
                    MeleeWepon.transform.Rotate(0, 0, wepRot);
                }
                if (!LMove && RMove)
                {
                    pos.x += 0.03f;
                    MeleeWepon.SetActive(false);
                    RMeleeWepon.SetActive(true);
                    RMeleeWepon.transform.Rotate(0, 0, wepRot);
                }
                if (ResetMelee >= 2)
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
                    anim.SetBool("Melee", false);
                    AtkModeMelee = false;
                    renderer.material.color = Color.white;
                }

            }
            //遠距離攻撃処理
            Rangesecond += Time.deltaTime;
            if (hitGround)
            {
                if (Rangesecond >= 1.5f)
                {
                    anim.SetBool("Shot",true);
                    pos.x = 0.0f;
                }
                if (Rangesecond >= 2.5f)
                {
                    Instantiate(bullet, this.transform.position, Quaternion.identity);
                    Rangesecond = 0;
                }
            }
            if(Rangesecond==0.0f)
            {
                anim.SetBool("Shot", false);
            }
            //終わり
            //if (Input.GetKeyDown(KeyCode.W))
            //{
            //    BossEnemyHp--;
            //    damage = true;
            //}
            if (BossEnemyHp <= 0)
            {
                Destroy(MeleeWepon);
                Destroy(RMeleeWepon);
                deadFlag = true;
                //if (!cleaItemSpawnFlag)
                //{
                //    cleaItemSpawnFlag = true;
                //    GameObject drop = Instantiate((GameObject)Resources.Load("ClearItem"));
                //    drop.transform.position = transform.position;
                //}
                if (GameObject.Find("TalkUICanvas(Clone)") == null)
                {
                    GameObject instance =
                       (GameObject)Instantiate(talkUI,
                       new Vector3(0, 0, 0.0f), Quaternion.identity);
                    VoiceScript voiceScript = instance.GetComponent<VoiceScript>();
                    voiceScript.SetOniEndFlag();
                    //voiceScript.SetToriEndFlag();   ←使うボスに応じて
                    //voiceScript.SetKituneEndFlag();
                    Data.voiceFlag = true;
                }
            }
            //if (Input.GetKey(KeyCode.Q))
            //{
            //    BossEnemyHp = 0;
            //}
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
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "SenkuGiri")
        {
            damage = true;
            BossEnemyHp -= 10;
        }
        if (collision.gameObject.tag == "PowerSlash")
        {
            PowerSlashScript power = collision.gameObject.GetComponent<PowerSlashScript>();

            BossEnemyHp-= 13 * (power.GetPlayerHitCount() + 1);
            //  Debug.Log(13 * (power.GetPlayerHitCount() + 1));
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PowerSlash")
        {
            PowerSlashScript power = collision.gameObject.GetComponent<PowerSlashScript>();

            BossEnemyHp -= 13 * (power.GetPlayerHitCount() + 1);
              Debug.Log(13 * (power.GetPlayerHitCount() + 1));
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
            particle.Play();
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
