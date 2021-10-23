using System.Collections;
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
    LookOn look;
    Rigidbody rigid;
    bool kamae;
    bool hitFlag;
    bool deadFlag;
    float muteki;
    float stoptime;
    int hitCount;
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
        if (!itemManagerScript.GetShopFlag())
        {
            Move();
            Direction();
            IsSenkuHit();
            SenkuGiri();
            Special();
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
    public void Damage(int damage)
    {

        if (muteki < 0)
        {
            hp -= damage;
            muteki = 1.5f;
        }
    }
    public void HealHp(int num)
    {
        hp += num;
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
        if (muteki > 0)

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
