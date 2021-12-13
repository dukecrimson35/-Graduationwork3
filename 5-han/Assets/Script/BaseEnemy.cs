using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public int baseEnemyHp = 10;
    public GameObject Texture;//自分の画像
    private float nextTime;
    float damageInterval = 1f;
    bool damage;
    bool deadFlag;
    int count=0;
    Renderer renderer;
    GameObject player;//プレイヤー
    Vector3 playerPos;//プレイヤーのポジション

    public int enemyType;//種類
    public GameObject deathAnim;//死亡アニメーションオブジェクト
    private AudioSource audioSource;
    public GameObject Ora;//金持ってるぞオーラ
    public AudioClip se;//死亡時se
    public bool isBossDeath;//ボスを倒した時にしぬやつ
    GameObject boss;
    KyuubiBoss kyuubisc;//それぞれのボスのスクリプト
    BirdBoss birdBosc;
    BossEnemy bosssc;

    
    SpriteRenderer sprite;//フェードイン用スプライト

    void Start()
    {
        damage = false;
        nextTime = Time.time;
        renderer = GetComponent<Renderer>();
        deadFlag = false;
        player = GameObject.Find("Player");
        playerPos = player.transform.position;
        audioSource = GetComponent<AudioSource>();

        sprite = Texture.GetComponent<SpriteRenderer>();
        if (enemyType != 10)//九尾ボスの分身以外なら
        {
            //α値を0に
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
        }

        if (isBossDeath)
        {
            if(GameObject.Find("BossBird") != null)
            {
                boss = GameObject.Find("BossBird");
                birdBosc = boss.GetComponent<BirdBoss>();
            }
            if (GameObject.Find("oni_tati") != null)
            {
                boss = GameObject.Find("oni_tati");
                bosssc = boss.GetComponent<BossEnemy>();
            }
            if (GameObject.Find("BossBird") != null)
            {
                boss = GameObject.Find("kyuubi");
                kyuubisc = boss.GetComponent<KyuubiBoss>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (bosssc != null)
        {
            if (bosssc.GetHp() <= 0)
            {
                Destroy(this.gameObject);
            }
        }
        if (birdBosc != null)
        {
            if (birdBosc.GetHp() <= 0)
            {
                Destroy(this.gameObject);
            }
        }
        if (kyuubisc != null)
        {
            if (kyuubisc.GetHp() <= 0)
            {
                Destroy(this.gameObject);
            }
        }
        

        if (enemyType != 10)//九尾ボスの分身以外なら
        {
            if (sprite.color.a <= 1)
            {
                sprite.color += new Color(0, 0, 0, Time.deltaTime * 4);
            }
        }
        playerPos = player.transform.position;//プレイヤーの位置取得
        //if(playerPos.x - transform.position.x >= 0)
        //{
        //    Texture.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        //}
        //if (playerPos.x - transform.position.x < 0)
        //{
        //    Texture.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        //}
        if (Time.timeScale <= 0) return;
        if (Input.GetKeyDown(KeyCode.W))
        {
            baseEnemyHp--;
            damage = true;
        }
        if (baseEnemyHp <= 0)
        {
            if (se != null)
            {
                audioSource.PlayOneShot(se);
            }
            else
            {

            }
            if (enemyType != 0)
            {
                Instantiate(deathAnim, Texture.transform.position, Texture.transform.rotation);
            }
            Destroy(gameObject);
            deadFlag = true;
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Destroy(gameObject);
            deadFlag = true;
        }
        if (damage)
        {
            if (Time.time > nextTime)
            {
                renderer.enabled = !renderer.enabled;

                nextTime += damageInterval;
                count++;
            }
            if(count==4)
            {
                damage = false;
                count = 0;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.tag == "SenkuGiri")
        //{
        //    baseEnemyHp -= 10;
        //}
        if (collision.gameObject.tag == "PowerSlash")
        {
            PowerSlashScript power = collision.gameObject.GetComponent<PowerSlashScript>();

            baseEnemyHp -= 13 * (power.GetPlayerHitCount() + 1);
          //  Debug.Log(13 * (power.GetPlayerHitCount() + 1));
        }
        if (collision.gameObject.tag == "Player")
        {
            PlayerControl p = collision.gameObject.GetComponent<PlayerControl>();
            p.Damage(10);
            p.KnockBack(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SenkuGiri")
        {
            baseEnemyHp -= 10;
        }
    }

    public int GetHp()
    {
        return baseEnemyHp;
    }

    public bool GetDeadFlag()
    {
        return deadFlag;
    }
    public void Damage(int damage)
    {
        baseEnemyHp -= damage;
    }

    public void OraNon()
    {
        ParticleSystem p = transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        p.Stop();
        //Ora.SetActive(false);
        //Destroy(transform.GetChild(1).gameObject);
    }
}
