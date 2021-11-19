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
    public AudioClip se;//死亡時se
    void Start()
    {
        damage = false;
        nextTime = Time.time;
        renderer = GetComponent<Renderer>();
        deadFlag = false;
        player = GameObject.Find("Player");
        playerPos = player.transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
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
                Instantiate(deathAnim, this.transform.position, Texture.transform.rotation);
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
        if (collision.gameObject.tag == "SenkuGiri")
        {
            baseEnemyHp -= 10;
        }
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
}
