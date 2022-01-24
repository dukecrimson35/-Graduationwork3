using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KyuubiBoss : MonoBehaviour
{
    // Start is called before the first frame update
    //Animator anim;
    public int BossEnemyHp;//ボスの体力
    private float nextTime;//ダメージを受けるまでのクールタイム
    bool damage;//ダメージを受けたクールタイム中かどうか
    bool deadFlag;
    float count = 0;//タイムカウント
    Renderer renderer;
    bool LMove;
    bool RMove;
    Vector3 pos;
    //public Sprite aliveBoss;
    //public Sprite deadBoss;
    public bool hitGround;//地面に当たっている判定
    public bossspawn bossspawn;
    Rigidbody rigid;
    bool onFlag;//行動ループ開始フラグ
    public GameObject talkUI;
    public GameObject reverse;//反転判定
    WallRange range;
    bool voicedflag;//セリフが呼ばれた後かどうか

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
    bool hitflag;
    float hitcount;//左右壁に当たってからの時間

    //鬼火
    public GameObject onibiBase;//呼び出す鬼火
    GameObject[] onibis;
    Onibi[] onibiscripts;//鬼火のスクリプト配列
    public GameObject[] TeleportPositions;//移動先オブジェクト配列

    //ザコ呼び出し
    public GameObject beastEnemy;//呼び出すザコ敵
    public GameObject holeBase;//呼び出す予兆基礎
    public GameObject hole01;//呼び出す予兆1
    public GameObject hole02;//呼び出す予兆2
    Vector3 spawnP01;//敵発生位置１
    Vector3 spawnP02;//敵発生位置１
    GameObject[] beasts;//ザコ配列
    BaseEnemy[] scripts;//パーティクル消すよう
    List<GameObject> enemys;//敵消すよう

    Rigidbody rigidbody;

    float runoutdis;//走り抜ける処理

    Animator animator;//自身のアニメーター
    private int maxHp;

    bool cleaItemSpawnFlag = false;
    enum State
    {
        Wait,
        Call,
        Onibi,
        Bunsin,
        Down,
        //以下強化モード
        PowBunsin,
        PowOnibi,
        PowDown,
        PowWait,
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
        pos = Vector3.zero;
        state = State.Wait;//初期ステート設定
        animator = GetComponent<Animator>();//アニメーターの取得
        avatars = new GameObject[2];
        onFlag = false;
        voicedflag = false;
        maxHp = BossEnemyHp;

        //分身モード移動用
        rigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        playerPos = player.transform.position;
        onGround = false;
        moveFlag = false;
        runoutdis = Random.Range(6, 10);
        if (reverse != null)
        {
            range = reverse.GetComponent<WallRange>();
        }

        //鬼火モード用
        onibis = new GameObject[4];
        onibiscripts = new Onibi[4];

        //敵呼び出し用
        spawnP01 = hole01.transform.position;
        spawnP02 = hole02.transform.position;
        beasts = new GameObject[2];
        scripts = new BaseEnemy[2];
        enemys = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        //デバッグ用ステート切り替え
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    state = State.Bunsin;
        //}
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    state = State.Onibi;
        //}
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    state = State.Call;
        //}

        if (Time.timeScale <= 0) return;//タイムスケールが0の時は実行しない

        if (onFlag == true && Data.voiceFlag == false && voicedflag == true)
        {
            if (state == State.Wait)
            {
                state = State.Bunsin;
            }
        }
        if (Data.voiceFlag == true)
        {
            voicedflag = true;
        }
        if (deadFlag)
        {
            state = State.Wait;
        }

        if (hitflag)
        {
            hitcount += Time.deltaTime;
            if(hitcount >= 0.1f)
            {
                hitflag = false;
                hitcount = 0;
            }
        }

        if (BossEnemyHp <= 300)//AI強化モード
        {
            if ((state == State.Bunsin) || (state == State.Onibi) || (state == State.Call) || (state == State.Down) || (state == State.Wait))
            {
                state = State.PowWait;//強化モード待機状態に移行
                Type = 0;
                count = 0;
            }
        }

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
                            avatars[i] = Instantiate(avatar, new Vector3(transform.position.x + 5, transform.position.y, transform.position.z), new Quaternion());
                            KyuubiAvatar kyuubiAvatar = avatars[0].GetComponent<KyuubiAvatar>();
                            kyuubiAvatar.SetDelayTime(3);
                        }
                        if (i == 1)
                        {
                            avatars[i] = Instantiate(avatar, new Vector3(transform.position.x - 5, transform.position.y, transform.position.z), new Quaternion());
                            KyuubiAvatar kyuubiAvatar = avatars[1].GetComponent<KyuubiAvatar>();
                            kyuubiAvatar.SetDelayTime(1);
                        }
                    }
                }
                if (count >= 1)
                {
                    //Type++;
                    state = State.Onibi;//鬼火モード切替
                    count = 0;
                    Type = 0;
                }
            }

            //分身と一緒に攻撃
            if (Type == 2)
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

                if (hitGround == true)
                {
                    if (transform.position.x - playerPos.x >= 0)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
                        //Texture.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    }
                    if (transform.position.x - playerPos.x < 0)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
                        //Texture.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                    }
                }

                if (range != null)//左右壁判定処理
                {
                    if (range.GetinRange())//壁に当たったら
                    {
                        if(hitflag == false)
                        {
                            leftMove = !leftMove;
                            hitflag = true;
                        }
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

        #region 強化分身
        if(state == State.PowBunsin)
        {
            count += Time.deltaTime;

            //遠吠え
            if(Type == 0)
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
                            avatars[i] = Instantiate(avatar, new Vector3(transform.position.x + 5, transform.position.y, transform.position.z), new Quaternion());
                            KyuubiAvatar kyuubiAvatar = avatars[0].GetComponent<KyuubiAvatar>();
                            kyuubiAvatar.SetDelayTime(3);
                        }
                        if (i == 1)
                        {
                            avatars[i] = Instantiate(avatar, new Vector3(transform.position.x - 5, transform.position.y, transform.position.z), new Quaternion());
                            KyuubiAvatar kyuubiAvatar = avatars[1].GetComponent<KyuubiAvatar>();
                            kyuubiAvatar.SetDelayTime(1);
                        }
                    }
                }
                if (count >= 1)//1秒後
                {
                    Type++;
                }
            }
            //分身に攻撃させ、自身は消える
            if(Type == 2)
            {
                transform.position = new Vector3(220, 200, 0);

                if (avatars[0] == null && avatars[1] == null)//分身がいなくなったら
                {
                    state = State.PowOnibi;//強化鬼火モード切替
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

            //遠吠えモーションと移動停止
            if (Type == 0)
            {
                animator.Play("Call");
                rigidbody.velocity = Vector3.zero;
                if (count >= 0.2)
                {
                    Type++;
                }
            }

            //鬼火生成
            if (Type == 1)
            {
                onibis[0] = Instantiate(onibiBase, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), new Quaternion());
                onibiscripts[0] = onibis[0].GetComponent<Onibi>();
                onibis[1] = Instantiate(onibiBase, new Vector3(transform.position.x + 3, transform.position.y, transform.position.z), new Quaternion());
                onibiscripts[1] = onibis[1].GetComponent<Onibi>();
                onibis[2] = Instantiate(onibiBase, new Vector3(transform.position.x - 3, transform.position.y, transform.position.z), new Quaternion());
                onibiscripts[2] = onibis[2].GetComponent<Onibi>();
                onibis[3] = Instantiate(onibiBase, new Vector3(transform.position.x, transform.position.y - 3, transform.position.z), new Quaternion());
                onibiscripts[3] = onibis[3].GetComponent<Onibi>();
                for (int i = 0; i < 4; i++)
                {
                    onibiscripts[i].SetMom(this.gameObject);
                    onibiscripts[i].SetMovetoPos(TeleportPositions[i].transform.position);
                }
                Type++;
            }
            //鬼火4発発射
            if (Type == 2)
            {
                if (count >= 2)//2秒後
                {
                    onibiscripts[0].SetType("bullet");
                }
                if (count >= 3)
                {
                    onibiscripts[1].SetType("bullet");
                }
                if (count >= 4)
                {
                    onibiscripts[2].SetType("bullet");
                }
                if (count >= 5)
                {
                    onibiscripts[3].SetType("bullet");
                }

                if (count >= 8)
                {
                    Type++;
                }
            }
            //瞬間移動
            if (Type == 3)
            {
                int rand = Random.Range(0, 3);
                if (rand == 0)
                {
                    transform.position = TeleportPositions[0].transform.position;
                }
                if (rand == 1)
                {
                    transform.position = TeleportPositions[1].transform.position;
                }
                if (rand == 2)
                {
                    transform.position = TeleportPositions[2].transform.position;
                }
                if (rand == 3)
                {
                    transform.position = TeleportPositions[3].transform.position;
                }

                Type++;
            }

            //ステート切り替え
            if (Type == 4)
            {
                foreach (var x in onibis)
                {
                    Destroy(x);
                }
                count = 0;
                state = State.Call;//呼びモード切替
                Type = 0;
            }
        }
        #endregion

        #region 強化鬼火
        if (state == State.PowOnibi)
        {
            count += Time.deltaTime;
            //テレポートと鬼火展開
            if(Type == 0)
            {
                //アニメーション変更

                //テレポート
                if (player.transform.position.x <= 220)
                {
                    rigid.velocity = new Vector3(0, 0, 0);
                    transform.position = TeleportPositions[1].transform.position;
                }
                if (player.transform.position.x > 220)
                {
                    rigid.velocity = new Vector3(0, 0, 0);
                    transform.position = TeleportPositions[0].transform.position;
                }
                //鬼火展開
                onibis[0] = Instantiate(onibiBase, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), new Quaternion());
                onibiscripts[0] = onibis[0].GetComponent<Onibi>();
                onibis[1] = Instantiate(onibiBase, new Vector3(transform.position.x + 3, transform.position.y, transform.position.z), new Quaternion());
                onibiscripts[1] = onibis[1].GetComponent<Onibi>();
                onibis[2] = Instantiate(onibiBase, new Vector3(transform.position.x - 3, transform.position.y, transform.position.z), new Quaternion());
                onibiscripts[2] = onibis[2].GetComponent<Onibi>();
                onibis[3] = Instantiate(onibiBase, new Vector3(transform.position.x, transform.position.y - 3, transform.position.z), new Quaternion());
                onibiscripts[3] = onibis[3].GetComponent<Onibi>();
                for (int i = 0; i < 4; i++)
                {
                    onibiscripts[i].SetMom(this.gameObject);
                    onibiscripts[i].SetMovetoPos(TeleportPositions[i].transform.position);
                }
                Type++;
                count = 0;
            }
            //プレイヤーに向けて発射
            if(Type == 1)
            {
                if (count >= 1)
                {
                    //鬼火1発目
                    onibiscripts[0].SetSpeed(15);
                    onibiscripts[0].SetPlayer();
                    onibiscripts[0].SetType("bust");
                    Type++;
                    count = 0;
                }
            }
            if (Type == 2)
            {
                if (count >= 1)
                {
                    //鬼火2発目
                    onibiscripts[1].SetSpeed(15);
                    onibiscripts[1].SetPlayer();
                    onibiscripts[1].SetType("bust");
                    Type++;
                    count = 0;
                }
            }
            if (Type == 3)
            {
                if (count >= 1)
                {
                    //鬼火3発目
                    onibiscripts[2].SetSpeed(15);
                    onibiscripts[2].SetPlayer();
                    onibiscripts[2].SetType("bust");
                    Type++;
                    count = 0;
                }
            }
            if (Type == 4)
            {
                if (count >= 1)
                {
                    //鬼火4発目
                    onibiscripts[3].SetSpeed(15);
                    onibiscripts[3].SetPlayer();
                    onibiscripts[3].SetType("bust");
                    Type++;
                    count = 0;
                }
            }
            if(Type==5)
            {
                state = State.PowDown;
                Type = 0;
                count = 0;
            }

        }
        #endregion

        #region ザコ呼び出し
        if (state == State.Call)//呼びモード
        {
            count += Time.deltaTime;

            //遠吠えして仲間よび
            if (Type == 0)
            {
                animator.Play("Call");
                Instantiate(holeBase, spawnP01, new Quaternion());//予兆作成
                Instantiate(holeBase, spawnP02, new Quaternion());//予兆作成
                Type++;
            }
            if (Type == 1)
            {
                if (count >= 2)
                {
                    if (enemys.Count < 4)
                    {
                        beasts[0] = Instantiate(beastEnemy, spawnP01, new Quaternion());
                        scripts[0] = beasts[0].GetComponent<BaseEnemy>();
                        beasts[1] = Instantiate(beastEnemy, spawnP02, new Quaternion());
                        scripts[1] = beasts[1].GetComponent<BaseEnemy>();
                        Instantiate(holeBase, spawnP01, new Quaternion());//予兆作成
                        Instantiate(holeBase, spawnP02, new Quaternion());//予兆作成
                        foreach (var x in scripts)
                        {
                            x.OraNon();
                        }
                        for (int i = 0; i < 2; i++)
                        {
                            enemys.Add(beasts[i]);
                        }
                    }
                    Type++;
                }
            }
            if (Type == 2)
            {
                if (count >= 4)
                {
                    if (enemys.Count < 4)
                    {
                        beasts[0] = Instantiate(beastEnemy, spawnP01, new Quaternion());
                        scripts[0] = beasts[0].GetComponent<BaseEnemy>();
                        beasts[1] = Instantiate(beastEnemy, spawnP02, new Quaternion());
                        scripts[1] = beasts[1].GetComponent<BaseEnemy>();
                        foreach (var x in scripts)
                        {
                            x.OraNon();
                        }
                        for (int i = 0; i < 2; i++)
                        {
                            enemys.Add(beasts[i]);
                        }
                    }
                    Type++;
                }
            }

            //ステート切り替え
            if (count >= 5)//五秒で切り替え
            {
                state = State.Down;//ダウン状態切替
                count = 0;
                Type = 0;
            }
        }
        #endregion

        #region ダウン状態
        if (state == State.Down)//ダウン状態
        {
            count += Time.deltaTime;
            animator.Play("Down");

            if (count >= 2)//2秒で切り替え
            {
                state = State.Bunsin;//分身モード切替
                count = 0;
                Type = 0;
            }
        }
        #endregion

        #region 強化ダウン
        if (state == State.PowDown)
        {
            count += Time.deltaTime;
            animator.Play("Down");

            if (count >= 2)//2秒で切り替え
            {
                state = State.PowBunsin;//強化分身モード切替
                count = 0;
                Type = 0;
            }
        }
        #endregion

        #region 強化待機状態
        if(state == State.PowWait)
        {
            count += Time.deltaTime;
            //影のように消えテレポート
            if (Type == 0)
            {
                //いろんなもの消す
                foreach (var x in onibis)
                {
                    Destroy(x);
                }
                foreach (var x in avatars)
                {
                    Destroy(x);
                }
                foreach (var x in enemys)
                {
                    Destroy(x);
                }
                //テレポート
                transform.position =  new Vector3(220, 10, 0);
                //if (player.transform.position.x <= 220)
                //{
                //    transform.position = TeleportPositions[1].transform.position;
                //}
                //if (player.transform.position.x > 220)
                //{
                //    transform.position = TeleportPositions[0].transform.position;
                //}
                //※注意 voiceFlagに変なの起こったらやめる
                Data.voiceFlag = true;
                Type++;
            }

            //演出
            if(Type == 1)
            {
                //強化演出
                transform.position = new Vector3(220, 10, 0);

                if (count >= 1)//演出時間後
                {
                    Data.voiceFlag = false;
                    Type++;
                }
            }

            //強化分身モードに
            if(Type == 2)
            {
                state = State.PowBunsin;
                Type = 0;
                count = 0;
            }
        }
        #endregion

        if (bossspawn.hitBossSpawn)
        {
            rigid.useGravity = true;
            onFlag = true;
        }
        #region 死亡&ダメージ処理
        //終わり
        if (BossEnemyHp <= 0)
        {
            deadFlag = true;
            transform.position = new Vector3(220,0,0);
            foreach (var x in onibis)
            {
                Destroy(x);
            }
            foreach (var x in avatars)
            {
                Destroy(x);
            }
            foreach(var x in enemys)
            {
                Destroy(x);
            }
            //死亡演出
            if (GameObject.Find("TalkUICanvas(Clone)") == null)
            {
                GameObject instance =
                   (GameObject)Instantiate(talkUI,
                   new Vector3(0, 0, 0.0f), Quaternion.identity);
                VoiceScript voiceScript = instance.GetComponent<VoiceScript>();
                voiceScript.SetKituneEndFlag();
                Data.voiceFlag = true;
            }
            renderer.material.color = Color.white;
            animator.Play("Dead");
            if (!cleaItemSpawnFlag)
            {
                cleaItemSpawnFlag = true;
                GameObject drop = Instantiate((GameObject)Resources.Load("ClearItem3"));
                drop.transform.position = transform.position;
            }
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

        //デバッグ用
        Debug.Log(state);
        if(Input.GetKeyDown(KeyCode.K))
        {
            Data.voiceFlag = true;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Data.voiceFlag = false;
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

    public float GetPercentHP()
    {
        return (BossEnemyHp / maxHp) * 100;
    }
}
