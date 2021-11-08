﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public AudioClip senkugiri;
    public AudioClip damage;
    public AudioClip dead;
    public AudioClip powerslash;
    public AudioClip coin;
    AudioSource audio;
    int hp = 100;
    private GameObject itemManager;
    private ItemManager itemManagerScript;
    public GameObject shopPrefab;
    GameObject col;
    Vector3 velocity;
    Vector3 pos;
    GameObject moveColider;
    SenkuMove move;
    GameObject lookColider;
    GameObject lockSp;
    float lockSpTime;
    LookOn look;

    LockSpecial sp;
    Rigidbody rigid;
    bool kamae;
    bool hitFlag;
    bool deadFlag;
    float muteki;
    float stoptime;
    int hitCount;
    int loopCount;
    Animator anim;
    float blinktime = 0;
    enum Direc
    {
        Right,Left,
    }
    Direc currentDirec;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        stoptime = 0;
        moveColider = null;
        currentDirec = Direc.Right;
        velocity = Vector3.zero;
        pos = Vector3.zero;
        kamae = false;
        rigid = GetComponent<Rigidbody>();
        itemManager = GameObject.Find("ItemManagerOBJ");
        itemManagerScript = itemManager.GetComponent<ItemManager>();
        deadFlag = false;
        
      
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            audio.PlayOneShot(coin);
            Money moneyScript = collision.gameObject.GetComponent<Money>();
            itemManagerScript.UpCoin(moneyScript.GetMoney());
            Destroy(collision.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            Money moneyScript = collision.gameObject.GetComponent<Money>();
            itemManagerScript.UpCoin(moneyScript.GetMoney());
            Destroy(collision.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale <= 0) return;
        if (!itemManagerScript.GetShopFlag() && !Data.voiceFlag) 
        {
            Move();
            Direction();
            IsSenkuHit();
            SenkuGiri();
            Special();
            SpecialY();
            CheckDead();
            Blink();
        }
    }
    private void Move()
    {
        if (stoptime < 0)
        {
            //anim.SetTrigger("Stand");
            velocity = Vector3.zero;


            anim.SetBool("Walk", false);


            if (!kamae&&  Input.GetAxis("Horizontal") == -1)
            {

                anim.SetBool("Walk", true);
                velocity.x -= 11;
            }

            if (!kamae && Input.GetAxis("Horizontal") == 1)
            {
                anim.SetBool("Walk", true);

                velocity.x += 11;
            }
            if (Input.GetAxis("Horizontal") == -1)
            {
                currentDirec = Direc.Left;

            }

            if (( Input.GetAxis("Horizontal") == 1))
            {
                currentDirec = Direc.Right;

            }

        }
        else
        {
            //anim.SetTrigger("Stand");
            velocity = Vector3.zero;


            anim.SetBool("Walk", false);


            if (!kamae && Input.GetAxis("Horizontal") == -1)
            {

                anim.SetBool("Walk", true);
                velocity.x -= 3;
            }

            if (!kamae &&  Input.GetAxis("Horizontal") == 1)
            {
                anim.SetBool("Walk", true);

                velocity.x += 3;
            }
            if (( Input.GetAxis("Horizontal") == -1))
            {
                currentDirec = Direc.Left;

            }

            if (( Input.GetAxis("Horizontal") == 1))
            {
                currentDirec = Direc.Right;

            }
        }
    

     
        pos += velocity;
        transform.position += pos * Time.deltaTime ;
        pos = Vector3.zero;
    }
    private void Direction()
    {
        if (currentDirec == Direc.Left)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (currentDirec == Direc.Right)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void SenkuGiri()
    {
        Vector3 senku = new Vector3(Input.GetAxis("Horizontal") * 7, Input.GetAxis("Vertical") * 7, 0);

        if (stoptime < 0 || hitFlag)
        {
            
            if (Input.GetButton("A"))
            {
                anim.SetBool("Senku", true);
                anim.SetBool("Senku2", false);

                if (moveColider == null)
                {
                    moveColider = Instantiate((GameObject)Resources.Load("MoveCollider"));
                    move = moveColider.GetComponent<SenkuMove>();
                   
                }
                if (lookColider == null) 
                {
                    lookColider = Instantiate((GameObject)Resources.Load("LookOn"));
                    look = lookColider.GetComponentInChildren<LookOn>();
                }
                rigid.velocity = new Vector3(0, 0, 0);
                kamae = true;
                if (senku.magnitude == 0 && moveColider != null) 
                {
                    Destroy(moveColider);
                }
            }

            if ((Input.GetButtonUp("A") && senku.magnitude == 0) || (Input.GetButtonUp("A") && !move.GetIsMove()))
            {
                anim.SetBool("Senku", false);
                
                kamae = false;
                hitFlag = false;
                if (moveColider != null)
                {
                    Destroy(moveColider);
                }
                if (lookColider != null)
                {
                    Destroy(lookColider);
                }
            }
        
            else if (Input.GetButtonUp("A") && move.GetIsMove()) 
            {
                if (muteki < 0)
                {
                    muteki = 0.1f;
                }
                anim.SetBool("Senku2", true);

                audio.PlayOneShot(senkugiri);

                if (look.GetLookObject() != null)
                {
                    senku = (look.GetLookObject().transform.position - transform.position).normalized;
                    float len = (transform.position - look.GetLookObject().transform.position).magnitude;
                    
                    kamae = false;
                    hitFlag = false;
                    stoptime = 1.5f;
                    col = Instantiate((GameObject)Resources.Load("SenkuCollider"));

                    col.transform.position = transform.position;
                    col.transform.position += senku * len / 2;
                    float ang = Mathf.Atan2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * 180 / Mathf.PI + 180;
                    col.transform.rotation = Quaternion.Euler(0, 0, ang);

                    transform.position += senku * len + senku * 2;
                    AfterImage(-(senku * len + senku * 2), -(senku * len + senku * 2) / 2);
                 //   SenkuEffect((senku * len + senku * 2) / 2);
                    if (moveColider != null)
                    {
                        Destroy(moveColider);
                    }
                    if (lookColider != null)
                    {
                        Destroy(lookColider);
                    }
                }
                else
                {
                    if (muteki < 0)
                    {
                        muteki = 0.1f;
                    }
                    audio.PlayOneShot(senkugiri);
                    anim.SetBool("Senku2", true);

                    kamae = false;
                    hitFlag = false;
                    stoptime = 1.5f;
                    col = Instantiate((GameObject)Resources.Load("SenkuCollider"));


                    col.transform.position = transform.position;
                    col.transform.position += senku / 2;
                    float ang = Mathf.Atan2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * 180 / Mathf.PI + 180;
                    col.transform.rotation = Quaternion.Euler(0, 0, ang);

                    transform.position += senku;
                    AfterImage(-(senku), -(senku) / 2);
            //        SenkuEffect((senku) / 2);
                    if (moveColider != null)
                    {
                        Destroy(moveColider);
                    }
                    if (lookColider != null)
                    {
                        Destroy(lookColider);
                    }
                }
              
            }
         
        }
        float tes = Mathf.Atan2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * 180 / Mathf.PI + 180;
      
        float vert = Input.GetAxis("Vertical");
        //Debug.Log(vert);
        if (moveColider != null)
        {
            moveColider.transform.position = transform.position;
            moveColider.transform.rotation = Quaternion.Euler(0, 0, tes);
        }
        if(lookColider!=null)
        {
            lookColider.transform.position = transform.position;
            lookColider.transform.rotation = Quaternion.Euler(0, 0, tes);
        }
        stoptime -= Time.deltaTime;
    }
    void AfterImage(Vector3 pos1, Vector3 pos2)
    {
        GameObject after = Instantiate((GameObject)Resources.Load("Afterimage"));
        after.transform.position = transform.position;
        Afterimage af = after.GetComponent<Afterimage>();
        if (currentDirec == Direc.Left)
        {
            af.Left();
        }
        af.SetPositition(pos1, pos2);
        Debug.Log(1);
    }
 
    void SenkuEffect(Vector3 pos)
    {
        GameObject after = Instantiate((GameObject)Resources.Load("SenkuEffect"));
        after.transform.position += pos;
        if (currentDirec == Direc.Left)
        {
            SenkuEffect se = after.GetComponent<SenkuEffect>();
            se.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

    }
    void SenkuEffect()
    {
      

    }
    void Special()
    {

        if (stoptime < 0 || hitFlag)
        {
            if (Input.GetButton("X"))
            {

                anim.SetBool("Power", true);

                if (moveColider == null)
                {
                    moveColider = Instantiate((GameObject)Resources.Load("MoveCollider"));
                    move = moveColider.GetComponent<SenkuMove>();
                }
                rigid.velocity = new Vector3(0, 0, 0);
                kamae = true;
            }

            if (Input.GetButtonUp("X"))
            {
                anim.SetBool("Power2", true);
                kamae = false;
                hitFlag = false;
                stoptime = 1.5f;
                col = Instantiate((GameObject)Resources.Load("PowerSlash"));

                Vector3 senku = new Vector3(Input.GetAxis("Horizontal") * 7, Input.GetAxis("Vertical") * 7, 0);
                col.transform.position = transform.position;
                col.transform.position += senku / 2;
                float ang = Mathf.Atan2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * 180 / Mathf.PI + 180;
                col.transform.rotation = Quaternion.Euler(0, 0, ang);
                col = null;

                Destroy(moveColider);
            }
        }
    }
    void SpecialY()
    {
      
    //    if (stoptime < 0 || hitFlag)
        {
            if (Input.GetButton("Y"))
            {
                
                if (lockSp == null) 
                {
                    lockSp = Instantiate((GameObject)Resources.Load("LockSpecial"));
                    sp = lockSp.GetComponent<LockSpecial>();
                    loopCount = 0;
                }
                lockSp.transform.position = transform.position;
                anim.SetBool("Power", true);
                kamae = true;
                lockSpTime = 0;
            }

           else if (!Input.GetButton("Y"))
            {
                if (lockSpTime >= 0.1f && sp != null) 
                {
                    Debug.Log(loopCount);
                    Debug.Log(sp.GetListCount());

                    Vector3 senku;
                    if (loopCount < sp.GetListCount())
                    {
                        senku = (sp.GetObjList()[loopCount].transform.position - transform.position).normalized;
                        float len = (transform.position - sp.GetObjList()[loopCount].transform.position).magnitude;
                        transform.position += senku * len + senku * 2;
                        loopCount++;
                        lockSpTime = 0;
                        audio.PlayOneShot(senkugiri);

                    }

                }
                if (lockSp != null && sp.GetListCount() == loopCount)
                {
                    for (int i = 0; i < sp.GetListCount(); i++) 
                    {
                        BaseEnemy be = sp.GetObjList()[i].GetComponent<BaseEnemy>();
                        be.Damage(100);
                        if (be == null)
                        {

                            ShotEnemy st = sp.GetObjList()[i].GetComponent<ShotEnemy>();
                        }
                    }
                    Destroy(lockSp);
                    kamae = false;
                }
            }
            if(sp!=null)
            {
                lockSpTime += Time.deltaTime;
            }
        
        
        }
    }
    void IsSenkuHit()
    {
        if (col != null)
        {
            SenkuSprict s = col.GetComponent<SenkuSprict>();
            if(s.GetHitFlag() && !hitFlag)
            {
                hitFlag = true;
                hitCount++;
              
            }
        }
        if (stoptime < 0 && !kamae)
        {
            anim.SetBool("Senku2", false);
            anim.SetBool("Senku", false);
            anim.SetBool("Power2", false);
            anim.SetBool("Power", false);

            hitCount = 0;
        }
    }
    public int GetHitCount()
    {
        return hitCount;
    }
    public int GetHp()
    {
        return hp;
    }
    public bool GetDeadFlag()
    {
        return deadFlag;
    }
    public void Damage(int _damage)
    {
        audio.PlayOneShot(damage);
        if (muteki < 0)
        {
            hp -= _damage;
            muteki = 1.5f;
        }
    }
    public void HealHp(int num)
    {
        hp += num;
        if(hp > 100)
        {
            hp = 100;
        }
    }
    public void KnockBack(GameObject other)
    {
        Vector3 vel = (transform.position - other.transform.position).normalized;
        rigid.velocity = vel * 7;
    }
    void CheckDead()
    {
        muteki -= Time.deltaTime;
        if (muteki < 0)
        {
            anim.SetBool("Damage", false);

            gameObject.layer = 8;
        }
        else if (muteki > 0)
        {
            gameObject.layer = 17;
        }
        if (hp <= 0)
        {
            anim.SetBool("Senku", false);

            anim.SetBool("Senku2", false);

            anim.SetBool("Walk", false);

            anim.SetBool("Damage", false);
            anim.SetBool("Dead", true);

            deadFlag = true;
        }
    }
    void Blink()
    {
        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        if (muteki > 0.1) 
        {
            blinktime += Time.deltaTime;
            if (blinktime % 0.3 < 0.1f)
            {
                blinktime += 0.1f;
                if (sp.color.a == 0)
                {

                    sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 1.0f);
                }
                else if (sp.color.a == 1.0f)
                {

                    sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 0);
                }
            }
        }
        else
        {

            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 1.0f);
        }
    }
}
