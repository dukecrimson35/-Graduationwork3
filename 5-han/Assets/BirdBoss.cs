using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBoss : MonoBehaviour
{
    // Start is called before the first frame update
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
        AtkModeMelee = false;
        AtkModeRange = false;
        pos = Vector3.zero;
        Meleesecond = 0;
        Rangesecond = 0;
        ResetMelee = 0;
        
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
            if (BossEnemyHp <= 130)
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
            //近接攻撃(突撃)
            Meleesecond += Time.deltaTime;
            if (Meleesecond >= 7 && BossEnemyHp <= 50)
            {
                AtkModeMelee = true;
            }
            if (AtkModeRange)
            {
                ResetMelee += Time.deltaTime;
                if (LMove && !RMove)
                {
                    pos.x -= 0.06f;
                    this.transform.rotation = new Quaternion(0, 0, 0, 0);
                }
                if (!LMove && RMove)
                {
                    pos.x += 0.03f;
                    this.transform.rotation = new Quaternion(0, 180, 0, 0);
                }
                if (ResetMelee >= 5)
                {
                    Meleesecond = 0;
                    ResetMelee = 0;
                    AtkModeMelee = false;
                }
            }
            //遠距離攻撃処理
            Rangesecond += Time.deltaTime;
            if (hitGround)
            {
                if (Rangesecond >= 0.5f)
                {

                }
                if (Rangesecond >= 1.5f)
                {
                    Instantiate(bullet, this.transform.position, Quaternion.identity);
                    Rangesecond = 0;
                }
            }
            if (Rangesecond == 0.0f)
            {

            }
            //終わり
            if (BossEnemyHp <= 0)
            {
                deadFlag = true;
                Destroy(gameObject, 2.0f);
            }
            if (Input.GetKey(KeyCode.Q))
            {
                BossEnemyHp = 0;
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
