using System.Collections;
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
    float wepRot;
    void Start()
    {
        damage = false;
        deadFlag = false;
        nextTime = Time.time;
        renderer = GetComponent<Renderer>();
        LMove = true;
        RMove = false;
        MoveMode = false;
        AtkModeMelee = false;
        AtkModeRange = false;
        pos = Vector3.zero;
        Meleesecond = 0;
        Rangesecond = 0;
        ResetMelee = 0;
        MeleeWepon.SetActive(false);
        wepRot = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        //移動処理
        if (BossEnemyHp <= 25)
        {
            MoveMode = true;
        }
        if (MoveMode)
        {
            if (LMove && !RMove)
            {
                pos.x -= 0.01f;
            }
            if (!LMove && RMove)
            {
                pos.x += 0.01f;
            }
        }
        //近接攻撃処理
        Meleesecond += Time.deltaTime;
        if (Meleesecond >= 10)
        {
            //MeleeSecが10になったら近接攻撃モードON
            AtkModeMelee = true;
        }
        if(AtkModeMelee)
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
            //武器表示
            MeleeWepon.SetActive(true);
            //武器回転
            MeleeWepon.transform.Rotate(0, 0, wepRot);
            if(ResetMelee>=5)
            {
                MeleeWepon.transform.rotation = Quaternion.Euler(0, 0, 28);
                //武器非表示
                MeleeWepon.SetActive(false);
                //モード変更とモードリセットの時間をリセットで再度使用できるようにしておく
                Meleesecond = 0;
                ResetMelee = 0;
                //攻撃モードをOFFにする
                AtkModeMelee = false;
            }

        }
        //遠距離攻撃処理
        Rangesecond += Time.deltaTime;
        if(Rangesecond>=15)
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
            Destroy(gameObject);
            deadFlag = true;
        }
        if (Input.GetKeyDown(KeyCode.Q))
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
             if (count == 4)
             {
                 damage = false;
                 count = 0;
             }
        }
        transform.position += pos;
        pos = Vector3.zero;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "SenkuGiri")
        {
            BossEnemyHp -= 10;
        }
        //if (collision.gameObject.tag == "LSide")
        //{
        //    AtkModeMelee = false;
        //    Meleesecond = 0;
        //    LMove = false;
        //    RMove = true;
        //}
        //if (collision.gameObject.tag == "RSide")
        //{
        //    AtkModeMelee = false;
        //    Meleesecond = 0;
        //    LMove = true;
        //    RMove = false;
        //}
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
