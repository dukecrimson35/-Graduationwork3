using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBoss : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject talkUI;
    public GameObject player;
    Animator anim;
    public int BossEnemyHp = 150;
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
    public float singleRangesec;
    public float ResetMelee;
    Vector3 pos;
    public GameObject bullet;
    //public Sprite aliveBoss;
    //public Sprite deadBoss;
    public bool hitGround;
    public bossspawn bossspawn;
    Rigidbody rigid;
    BoxCollider collider;
    float changeColSize;
    bool clearItemSpawnFlag;
    public bool down;
    public float downTimer;
    bool firstdown;
    void Start()
    {
        anim = GetComponent<Animator>();
        damage = false;
        deadFlag = false;
        nextTime = 0;
        renderer = GetComponent<Renderer>();
        spr = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();
        rigid.useGravity = false;
        LMove = true;
        RMove = false;
        MoveMode = false;
        down = false;
        AtkModeMelee = false;
        AtkModeRange = false;
        firstdown = true;
        pos = Vector3.zero;
        Meleesecond = 0;
        Rangesecond = 0;
        singleRangesec = 0;
        ResetMelee = 0;
        downTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (bossspawn.hitBossSpawn)
        {
            rigid.useGravity = true;
        }
        if (Time.timeScale <= 0) return;
        //移動処理
        if (bossspawn.GetEnemyMove() && Data.voiceFlag == false)
        {
            if (!down)
            {
                rigid.useGravity = false;
                if (transform.localPosition.y <= -2.0f)
                {
                    pos.y += 0.01f;
                }
                if (transform.localPosition.y == -2.0f)
                {
                    pos.y = 0;
                }
                if (BossEnemyHp <= 130)
                {
                    MoveMode = true;
                }
                if (MoveMode)
                {
                    Meleesecond += Time.deltaTime;
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
                //近接攻撃(突撃)
                if (Meleesecond >= 7.0f)
                {
                    AtkModeMelee = true;
                }
                if (AtkModeMelee)
                {
                    ResetMelee += Time.deltaTime;
                    anim.SetBool("Melee", true);
                    rigid.useGravity = true;
                    if (LMove && !RMove)
                    {
                        pos.x -= 0.06f;
                        this.transform.rotation = new Quaternion(0, 0, 0, 0);
                    }
                    if (!LMove && RMove)
                    {
                        pos.x += 0.06f;
                        this.transform.rotation = new Quaternion(0, 180, 0, 0);
                    }
                    if (ResetMelee >= 5.0f)
                    {
                        Meleesecond = 0;
                        ResetMelee = 0;
                        anim.SetBool("Melee", false);
                        AtkModeMelee = false;
                        rigid.useGravity = false;
                    }
                }
                Rangesecond += Time.deltaTime;
                singleRangesec += Time.deltaTime;
                ////遠距離攻撃処理
                if (hitGround)
                {
                    if (singleRangesec >= 1.5f)
                    {
                        anim.SetBool("Shot", true);
                    }
                    if (singleRangesec >= 2.5f)
                    {
                        Instantiate(bullet, this.transform.position, Quaternion.identity);
                        //Rangesecond = 0;
                        anim.SetBool("Shot", false);
                        singleRangesec = 0;
                    }
                }
                if (hitGround)
                {
                    if (Rangesecond >= 4.0f)
                    {
                        anim.SetBool("Shot", true);
                    }
                    if (Rangesecond >= 5.0f)
                    {
                        //Instantiate(bullet, new Vector3(transform.position.x, transform.position.y - 2, transform.position.z), Quaternion.identity);
                        Instantiate(bullet, new Vector3(transform.position.x - 1, transform.position.y + 2, transform.position.z), Quaternion.identity);
                        Instantiate(bullet, new Vector3(transform.position.x + 1, transform.position.y + 2, transform.position.z), Quaternion.identity);
                        Rangesecond = 0;
                    }
                }
                if (Rangesecond == 0.0f)
                {
                    anim.SetBool("Shot", false);
                }
                if (singleRangesec == 0.0f)
                {
                    anim.SetBool("Shot", false);
                }
            }
            if(BossEnemyHp<=50)
            {
                down = true;
                rigid.useGravity = true;
            }
            if (firstdown)
            {
                if (down)
                {
                    downTimer += Time.deltaTime;
                    anim.SetBool("Down", true);
                }
            }
            if (downTimer >= 5.0f)
            {
                down = false;
                anim.SetBool("Down", false);
                firstdown = false;
                rigid.useGravity = false;
            }
            //終わり
            if (BossEnemyHp <= 0)
            {
                deadFlag = true;
                if (!clearItemSpawnFlag)
                {
                    clearItemSpawnFlag = true;
                    GameObject drop = Instantiate((GameObject)Resources.Load("ClearItem2"));
                    drop.transform.position = transform.position;
                }
                if (GameObject.Find("TalkUICanvas(Clone)") == null)
                {
                    GameObject instance =
                       (GameObject)Instantiate(talkUI,
                       new Vector3(0, 0, 0.0f), Quaternion.identity);
                    VoiceScript voiceScript = instance.GetComponent<VoiceScript>();
                    //voiceScript.SetOniEndFlag();
                    voiceScript.SetToriEndFlag();   //←使うボスに応じて
                    //voiceScript.SetKituneEndFlag();
                    Data.voiceFlag = true;
                }
                Destroy(gameObject, 2.0f);
            }
            if (Input.GetKey(KeyCode.Q))
            {
                BossEnemyHp = 0;
            }
            if(Input.GetKeyDown(KeyCode.W))
            {
                BossEnemyHp = BossEnemyHp - 10;
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
            if (deadFlag)
            {
                //scenechangetime += Time.deltaTime;
                anim.SetBool("Dead", true);
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

            BossEnemyHp -= 13 * (power.GetPlayerHitCount() + 1);
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
            if (collision.gameObject.tag == "Block")
            {
                hitGround = true;
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
}
