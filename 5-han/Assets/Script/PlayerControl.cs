using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
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
    enum Direc
    {
        Right,Left,
    }
    Direc currentDirec;
    // Start is called before the first frame update
    void Start()
    {
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
        if(!itemManagerScript.GetShopFlag())
        {
            Move();
            Direction();
            IsSenkuHit();
            SenkuGiri();
            Special();
            CheckDead();
        }
      
    }
    private void Move()
    {
        if (stoptime < 0)
        {
            velocity = Vector3.zero;
            if (!kamae && (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("Horizontal") == -1))
            {
               
                velocity.x -= 11;
            }

            if (!kamae && (Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("Horizontal") == 1))
            {
            
                velocity.x += 11;
            }
            if ( (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("Horizontal") == -1))
            {
                currentDirec = Direc.Left;
             
            }

            if ((Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("Horizontal") == 1))
            {
                currentDirec = Direc.Right;
               
            }

        }

        if (Input.GetKey(KeyCode.Space) || Input.GetButtonDown("Y")) 
        {
            if (GameObject.Find("ShopPrefab(Clone)") == null)
            {
                GameObject instance =
                   (GameObject)Instantiate(shopPrefab,
                   new Vector3(0, 0, 0.0f), Quaternion.identity);
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

                    transform.position += senku * len + senku ;
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
                Debug.Log(hitCount);
            }
        }
        if (stoptime < 0 && !kamae)
        {
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
        if (hp <= 0)
        {
            deadFlag = true;
        }
    }
}
