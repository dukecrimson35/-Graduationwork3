using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KyuubiBoss : MonoBehaviour
{
    // Start is called before the first frame update
    //Animator anim;
    public int BossEnemyHp = 200;//ボスの体力
    private float nextTime;//ダメージを受けるまでのクールタイム
    bool damage;//ダメージを受けたクールタイム中かどうか
    bool deadFlag;
    float count = 0;//タイムカウント
    Renderer renderer;
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
    public bool hitGround;//地面に当たっている判定
    public bossspawn bossspawn;
    Rigidbody rigid;
    float changeColSize;

    public GameObject avatar;//生成する分身
    GameObject[] avatars;//分身格納配列

    Animator animator;//自身のアニメーター

    enum State
    {
        Call,
        Onibi,
        Bunsin,
        Down,
    }
    State state;//現在の状態

    void Start()
    {
        //anim = GetComponent<Animator>();
        damage = false;
        deadFlag = false;
        nextTime = 0;
        renderer = GetComponent<Renderer>();
        rigid = GetComponent<Rigidbody>();
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
        state = State.Bunsin;//初期ステート設定
        animator = GetComponent<Animator>();//アニメーターの取得
        avatars = new GameObject[2];
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale <= 0) return;//タイムスケールが0の時は実行しない

        #region　分身
        if (state == State.Bunsin)//分身モード
        {
            count += Time.deltaTime;

            //遠吠えをする
            animator.Play("Call");

            //分身を作る
            for(int i = 0; i < 2; i++) 
            {
                if (avatars[i] == null)
                {
                    if (i == 0)
                    {
                        avatars[i] = Instantiate(avatar, new Vector3(transform.position.x + 3, transform.position.y, transform.position.z), new Quaternion());
                    }
                    if (i == 1)
                    {
                        avatars[i] = Instantiate(avatar, new Vector3(transform.position.x - 3, transform.position.y, transform.position.z), new Quaternion());
                    }
                }
            }

            //分身と一緒に攻撃

            //ステート切り替え
            if (avatars[0] == null && avatars[1] == null)
            {
                state = State.Onibi;//鬼火モード切替
                count = 0;
            }
        }
        #endregion

        #region 鬼火
        if (state == State.Onibi)//鬼火モード
        {
            count += Time.deltaTime;

            //鬼火発射

            //ステート切り替え
            if(count >= 5)
            {
                state = State.Call;//呼びモード切替
                count = 0;
            }
        }
        #endregion

        #region ザコ呼び出し
        if(state == State.Call)//呼びモード
        {
            count += Time.deltaTime;

            //遠吠えして仲間よび
            animator.Play("Call");


            //ステート切り替え
            if(count >= 5)//五秒で切り替え
            {
                state = State.Down;//ダウン状態切替
                count = 0;
            }
        }
        #endregion

        #region ダウン状態
        if(state == State.Down)//ダウン状態
        {
            if(count >= 10)//十秒で切り替え
            {
                state = State.Bunsin;//分身モード切替
                count = 0;
            }
        }
        #endregion

        if (bossspawn.hitBossSpawn)
        {
            rigid.useGravity = true;
        }
        #region 死亡&ダメージ処理
        //終わり
        if (BossEnemyHp <= 0)
        {
            deadFlag = true;
            Destroy(gameObject, 2.0f);
        }
        if (damage)//ダメージを受けたとき
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
        #endregion
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
