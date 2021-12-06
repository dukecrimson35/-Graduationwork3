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

    //分身
    public GameObject avatar;//生成する分身
    GameObject[] avatars;//分身格納配列
    int Type;//行動ループの現在位置
    public float speed;//通常時の移動速度
    public GameObject Texture;//自分の画像
    GameObject player;
    Vector3 playerPos;
    bool leftMove;//左右どちらに移動するか
    bool moveFlag;
    bool onGround;//地面にいるかどうか
    public float junpP;//ジャンプ力

    //ザコ呼び出し
    public GameObject beastEnemy;//呼び出すザコ敵
    public GameObject holeBase;//呼び出す予兆基礎
    public GameObject hole01;//呼び出す予兆1
    public GameObject hole02;//呼び出す予兆2
    Vector3 spawnP01;//敵発生位置１
    Vector3 spawnP02;//敵発生位置１

    Rigidbody rigidbody;

    float runoutdis;//走り抜ける処理

    Animator animator;//自身のアニメーター

    enum State
    {
        Wait,
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
        state = State.Wait;//初期ステート設定
        animator = GetComponent<Animator>();//アニメーターの取得
        avatars = new GameObject[2];

        //分身モード移動用
        rigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        playerPos = player.transform.position;
        onGround = false;
        moveFlag = false;
        runoutdis = Random.Range(6, 10);

        //敵呼び出し用
        spawnP01 = hole01.transform.position;
        spawnP02 = hole02.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //デバッグ用ステート切り替え
        if(Input.GetKeyDown(KeyCode.A))
        {
            state = State.Bunsin;
        }

        if (Time.timeScale <= 0) return;//タイムスケールが0の時は実行しない

        #region　分身
        if (state == State.Bunsin)//分身モード
        {
            count += Time.deltaTime;

            //遠吠えをする
            if (Type == 0)
            {
                animator.Play("Call");
                Type++;
            }
            //分身を作る
            if (Type == 1)
            {
                for (int i = 0; i < 2; i++)
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
                Type++;
            }

            //分身と一緒に攻撃
            if(Type == 2)
            {
                animator.Play("Walk");
                if (speed <= 3)
                {
                    speed += Time.deltaTime;
                }

                playerPos = player.transform.position;//プレイヤーのポジション取得
                if (leftMove)//左に移動
                {
                    rigidbody.velocity = new Vector3(-Mathf.Pow(speed, 2) * 1.75f, rigidbody.velocity.y, 0);
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    if (transform.position.x - playerPos.x <= -runoutdis)
                    {
                        leftMove = false;
                        speed = 1;
                    }
                }
                if (leftMove == false)//右に移動
                {
                    rigidbody.velocity = new Vector3(Mathf.Pow(speed, 2) * 1.75f, rigidbody.velocity.y, 0);
                    transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                    if (transform.position.x - playerPos.x >= runoutdis)
                    {
                        leftMove = true;
                        speed = 1;
                    }
                }

                if (onGround == true)
                {
                    if (transform.position.x - playerPos.x >= 0)
                    {
                        rigidbody.AddForce(new Vector3(-700, 300 * junpP, 0));
                        //Texture.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    }
                    if (transform.position.x - playerPos.x < 0)
                    {
                        rigidbody.AddForce(new Vector3(700, 300 * junpP, 0));
                        //Texture.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                    }
                }

                //ステート切り替え
                if (avatars[0] == null && avatars[1] == null)//分身がいなくなったら
                {
                    state = State.Onibi;//鬼火モード切替
                    count = 0;
                    Type = 0;
                }
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
            if (Type == 0)
            {
                animator.Play("Call");
                Instantiate(holeBase, spawnP01, new Quaternion());//予兆作成
                Instantiate(holeBase, spawnP02, new Quaternion());//予兆作成
                if(count >= 2)
                {
                    Instantiate(beastEnemy, spawnP01, new Quaternion());
                    Instantiate(beastEnemy, spawnP02, new Quaternion());
                    Instantiate(holeBase, spawnP01, new Quaternion());//予兆作成
                    Instantiate(holeBase, spawnP02, new Quaternion());//予兆作成
                }
                if (count >= 4)
                {
                    Instantiate(beastEnemy, spawnP01, new Quaternion());
                    Instantiate(beastEnemy, spawnP02, new Quaternion());
                }
                Type++;
            }

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
            count += Time.deltaTime;

            if(count >= 2)//十秒で切り替え
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
