using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public AudioClip senkugiri;
    public AudioClip damage;
    public AudioClip damageVoice;
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
    Vector3 lastinp;
    Vector3 currentinp;

    Vector3 secondinp;
    Vector3 thirdinp;

    GameObject moveColider;
    SenkuMove move;
    GameObject lookColider;
    GameObject lockSp;
    float lockSpTime;
    LookOn look;
    LooKCamera camera;
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
    bool movieFlag;
    float movieCurrentTime;
    float movieStartTime;

    Vector3 moviePos;
    float blinktime = 0;
    bool clearFlag;
    float clearTime;
    StartMovie startMovie;
    enum Direc
    {
        Right,Left,
    }
    enum InputControl
    { 
        A,B,X,Y,N
    }

    Direc currentDirec;
    InputControl inC;
    // Start is called before the first frame update
    void Start()
    {
        inC = InputControl.N;
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
        camera = GameObject.Find("Main Camera").GetComponent<LooKCamera>();
        movieFlag = false;
        movieCurrentTime = 0;
        movieStartTime = 5;
        moviePos = Vector3.zero;
        clearTime = 0;
        startMovie = GetComponentInChildren<StartMovie>();
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
        if (collision.gameObject.tag == "MovieArea")
        {
            //audio.PlayOneShot(coin);
            movieFlag = true;
            moviePos = (transform.position - collision.transform.position) / movieStartTime * Time.deltaTime;

            movieCurrentTime = movieStartTime;

            anim.SetBool("Senku", false);
            anim.SetBool("Senku2", false);
            anim.SetBool("Walk", true);
            anim.SetBool("Damage", false);
            anim.SetBool("Power", false);
            anim.SetBool("Power2", false);
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "MovieArea")
        {
            
            anim.SetBool("Senku", false);
            anim.SetBool("Senku2", false);
            anim.SetBool("Walk", true);
            anim.SetBool("Damage", false);
            anim.SetBool("Power", false);
            anim.SetBool("Power2", false);
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
        if (collision.gameObject.tag == "ClearItem")
        {
          //  audio.PlayOneShot(coin);
            clearTime = 1;
            clearFlag = true; 
            movieCurrentTime = 0;
            anim.SetBool("Senku", false);
            anim.SetBool("Senku2", false);
            anim.SetBool("Walk", false);
            anim.SetBool("Damage", false);
            anim.SetBool("Power", false);
            anim.SetBool("Power2", false);
            anim.SetBool("Clear", true);

            Destroy(collision.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(GetClearFlag());
        Physics.Simulate(Time.deltaTime);
        audio.volume = Data.seVol;
        if (Time.timeScale <= 0) return;
        if (!itemManagerScript.GetShopFlag() && !Data.voiceFlag)
        {
            Direction();
            if (!movieFlag && startMovie.GetMovieEnd()) 
            {
                Move();
               
                SenkuGiri();
                if(Data.bSkill)
                {
                    Special();
                }
                if(Data.xSkill)
                {
                    SpecialY();
                }
                IsSenkuHit();
                CheckDead();
                Blink();
            }            
            else if(movieFlag)
            {
                muteki = 0.1f;
            //    CheckDead();
                MovieUpdate();

                Blink();
              
            }
        }
        else
        {
            muteki = 0.1f;
            Blink();
            anim.SetBool("Senku", false);
            anim.SetBool("Senku2", false);
            anim.SetBool("Walk", false);
            anim.SetBool("Damage", false);
            anim.SetBool("Power", false);
            anim.SetBool("Power2", false);
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
    private void MovieUpdate()
    {
        if (clearTime > 0) 
        {
            clearTime -= Time.deltaTime;
        }
        movieCurrentTime -= Time.deltaTime;
        if (moviePos.x<0)
        {
            currentDirec = Direc.Right;
        }
        else
        {
            currentDirec = Direc.Left;
        }
        if (movieCurrentTime >= 0) 
        {
            //  Vector3 vel = ((transform.position - moviePos)  / (movieStartTime) * Time.deltaTime);
            rigid.velocity = new Vector3(0, rigid.velocity.y, 0);
            transform.position -= moviePos;  
        }
        else
        {
            
          //  movieFlag = false;
        }
    }
    private void SenkuGiri()
    {
        float tes = Mathf.Atan2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * 180 / Mathf.PI + 180;

        Vector3 senku = new Vector3(Input.GetAxis("Horizontal") * 7, Input.GetAxis("Vertical") * 7, 0);
        thirdinp = secondinp;
        secondinp = lastinp;
        lastinp = currentinp;
        currentinp = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized;
        if (stoptime < 0 || hitFlag)
        {
            
            if (Input.GetButton("A") && (inC == InputControl.A || inC == InputControl.N))
            {
                inC = InputControl.A;
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
                if (look.GetLookObject() != null)
                {
                    Vector3 vel = look.GetLookObject().transform.position - transform.position;
                    float ang = Mathf.Atan2(vel.y, vel.x) * 180 / Mathf.PI + 180;
                    float len = (transform.position - look.GetLookObject().transform.position).magnitude;
                   // move.gameObject.transform.localScale = new Vector3(len + 3, 1, 1);
                    moveColider.transform.rotation = Quaternion.Euler(0, 0, ang);
                }
                //else
                //{
                //    move.gameObject.transform.localScale = new Vector3(9, 1, 1);
                //}
                rigid.velocity = new Vector3(0, 0, 0);
                kamae = true;

                if (moveColider != null)
                {
                    moveColider.transform.position = transform.position;
                    if (lookColider != null)
                    {
                        if (look.GetLookObject() == null)
                        {
                            moveColider.transform.rotation = Quaternion.Euler(0, 0, tes);
                        }
                    }
                }
                if (lookColider != null)
                {
                    lookColider.transform.position = transform.position;
                    lookColider.transform.rotation = Quaternion.Euler(0, 0, tes);

                }
                if (senku.magnitude == 0 && moveColider != null) 
                {
                    Destroy(moveColider);
                }
            
            }

            if ((Input.GetButtonUp("A") && senku.magnitude == 0) || ((Input.GetButtonUp("A") && !move.GetIsMove()) && look.GetLookObject() == null))
            {
                anim.SetBool("Senku", false);

                inC = InputControl.N;
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
            else if (((Input.GetButtonUp("A") && !move.GetIsMove()) && look.GetLookObject() != null) && inC == InputControl.A)
            {
                inC = InputControl.N;
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

                    if (look.GetLookObject().tag == "BossEnemy")
                    {
                        transform.position += senku * len - senku * 2;
              //          AfterImage(-(senku * len + senku * 2), -(senku * len + senku * 3) / 2);
                    }
                    else
                    {
                        transform.position += senku * len - senku * 2;
                 //       AfterImage(-(senku * len - senku * 2), -(senku * len - senku * 2) / 2);
                    }
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
            }
            else if (Input.GetButtonUp("A") && move.GetIsMove() && inC == InputControl.A)
            {
                inC = InputControl.N;
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

                    if (look.GetLookObject().tag == "BossEnemy")
                    {
                        transform.position += senku * len + senku * 4;
                        AfterImage(-(senku * len + senku * 4), -(senku * len + senku * 4) / 2);
                    }
                    else
                    {
                        transform.position += senku * len + senku * 2;
                        AfterImage(-(senku * len + senku * 2), -(senku * len + senku * 2) / 2);
                    }
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

                    //    if (currentinp == lastinp) 
                    {
                        col.transform.position = transform.position;
                        col.transform.position += lastinp * 7 / 2;
                        float ang = Mathf.Atan2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * 180 / Mathf.PI + 180;
                        col.transform.rotation = Quaternion.Euler(0, 0, ang);

                        transform.position += lastinp * 7;
                        AfterImage(-(lastinp * 7), -(lastinp * 7) / 2);
                    }
                    //else
                    //{

                    //    col.transform.position = transform.position;
                    //    col.transform.position += secondinp * 7 / 2;
                    //    float ang = Mathf.Atan2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * 180 / Mathf.PI + 180;
                    //    col.transform.rotation = Quaternion.Euler(0, 0, ang);

                    //    transform.position += secondinp * 7;
                    //    AfterImage(-(secondinp * 7), -(secondinp * 7) / 2);
                    //}

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
      
        float vert = Input.GetAxis("Vertical");
        //Debug.Log(vert);
       
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
   
    }

    void SenkuEffect(Vector3 pos)
    {
        GameObject after = Instantiate((GameObject)Resources.Load("SenkuEffect"));
        after.transform.position = pos;
        float ang = Mathf.Atan2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * 180 / Mathf.PI;
        after.transform.rotation = Quaternion.Euler(0, 0, ang);
        if (currentDirec == Direc.Left)
        {
            SenkuEffect se = after.GetComponent<SenkuEffect>();
            se.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

    }
    void PowerSlashEffect(Vector3 pos)
    {
        GameObject after = Instantiate((GameObject)Resources.Load("PowerSlashEffect"));
        after.transform.position = transform.position;
        after.transform.position += pos;

        float ang = Mathf.Atan2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * 180 / Mathf.PI;
        after.transform.rotation = Quaternion.Euler(0, 0, ang);
        if (currentDirec == Direc.Left)
        {
            SenkuEffect se = after.GetComponent<SenkuEffect>();
            se.gameObject.GetComponent<SpriteRenderer>().flipY = true;
        }
        camera.StartCameraShock();

    }
    void SenkuEffect(Vector3 pos, float ang)
    {
        GameObject after = Instantiate((GameObject)Resources.Load("SenkuEffect"));
        after.transform.position = pos;
        // float ang = Mathf.Atan2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * 180 / Mathf.PI;
        after.transform.rotation = Quaternion.Euler(0, 0, ang);
        SenkuEffect se = after.GetComponent<SenkuEffect>();
        se.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        if (currentDirec == Direc.Right)
        {
           se = after.GetComponent<SenkuEffect>();
            se.gameObject.GetComponent<SpriteRenderer>().flipY = true;
        }
    }


    void Special()
    {
        float tes = Mathf.Atan2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * 180 / Mathf.PI + 180;

        if (stoptime < 0 || hitFlag)
        {
            if (Input.GetButton("B") && (inC == InputControl.X || inC == InputControl.N)) 
            {
                inC = InputControl.X;
                anim.SetBool("Power", true);
                anim.SetBool("Senku2", false);
                anim.SetBool("Senku", false);
                anim.SetBool("Power2", false);
                if (moveColider == null)
                {
                    moveColider = Instantiate((GameObject)Resources.Load("MoveCollider"));
                    move = moveColider.GetComponent<SenkuMove>();
                }
                rigid.velocity = new Vector3(0, 0, 0);
                kamae = true;

                if (moveColider != null)
                {
                    moveColider.transform.position = transform.position;
                    moveColider.transform.rotation = Quaternion.Euler(0, 0, tes);
                    
                }
            }

            if (Input.GetButtonUp("B") && inC == InputControl.X)
            {
                audio.PlayOneShot(powerslash);
                inC = InputControl.N;
                anim.SetBool("Power2", true);
                kamae = false;
                hitFlag = false;
                stoptime = 1.5f;
                col = Instantiate((GameObject)Resources.Load("PowerSlash"));
               
                Vector3 senku = new Vector3(Input.GetAxis("Horizontal") * 7, Input.GetAxis("Vertical") * 7, 0);
                col.transform.position = transform.position;
                col.transform.position += senku / 2;
                PowerSlashEffect(senku / 2);
                float ang = Mathf.Atan2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * 180 / Mathf.PI + 180;
                col.transform.rotation = Quaternion.Euler(0, 0, ang);
                col = null;

                Destroy(moveColider);
            }
        }
    }
    void SpecialY()
    {
      
        if (stoptime < 0 || hitFlag)
        {
            if (Input.GetButton("X") && (inC == InputControl.Y || inC == InputControl.N) && loopCount == 0) 
            {
               
                rigid.velocity = new Vector3(0, 0, 0);
                inC = InputControl.Y;
                if (lockSp == null)
                {
                    lockSp = Instantiate((GameObject)Resources.Load("LockSpecial"));
                    sp = lockSp.GetComponent<LockSpecial>();
                   
                }
                sp.Charge(hitCount);
                lockSp.transform.position = transform.position;
                anim.SetBool("Senku", true);
                anim.SetBool("Senku2", false);
                anim.SetBool("Power2", false);
                anim.SetBool("Power", false);
                kamae = true;
                lockSpTime = 0;
            }

        }
        if (!Input.GetButton("X") && inC == InputControl.Y)
        {
            if (moveColider == null && sp != null)
            {
                if (loopCount < sp.GetListCount())
                {
                    SpriteRenderer rend;
                    moveColider = Instantiate((GameObject)Resources.Load("MoveCollider"));
                    move = moveColider.GetComponent<SenkuMove>();
                    rend = moveColider.GetComponentInChildren<SpriteRenderer>();
                    rend.enabled = false;
                    muteki = 0.1f;
                }
            }
            if (moveColider != null && sp != null) 
            {
                if (loopCount < sp.GetListCount())
                {
                    Vector3 vel = sp.GetObjList()[loopCount].transform.position - transform.position;
                    moveColider.transform.localScale = new Vector3(vel.magnitude + 3, 1, 1);
                    float ang = Mathf.Atan2(vel.y, vel.x) * 180 / Mathf.PI + 180;
                    moveColider.transform.position = transform.position;
                    moveColider.transform.rotation = Quaternion.Euler(0, 0, ang);
                    muteki = 0.1f;
                }
               
            }

            if (lockSpTime >= 0.3f && sp != null)
            {
                rigid.velocity = new Vector3(0, 0, 0);
                anim.SetBool("Senku2", true);
                stoptime = 1.5f;

                Vector3 senku;
                if (loopCount < sp.GetListCount())
                {
                    senku = (sp.GetObjList()[loopCount].transform.position - transform.position).normalized;
                    if (senku.x < 0)
                    {
                        currentDirec = Direc.Left;
                    }
                    else if (senku.x > 0)
                    {
                        currentDirec = Direc.Right;
                    }
                    if (move.GetIsMove())
                    {
                        float len = (transform.position - sp.GetObjList()[loopCount].transform.position).magnitude;
                        AfterImage(Vector3.zero, Vector3.zero);

                        Vector3 vel = sp.GetObjList()[loopCount].transform.position - transform.position;
                        float ang = Mathf.Atan2(vel.y, vel.x) * 180 / Mathf.PI + 180;
                        SenkuEffect(sp.GetObjList()[loopCount].transform.position,ang);
                        transform.position += senku * len + senku * 2;
                        loopCount++;
                        lockSpTime = 0;
                        audio.PlayOneShot(senkugiri);
                    }
                   
                    else
                    {
                        Vector3 vel = sp.GetObjList()[loopCount].transform.position - transform.position;
                        float ang = Mathf.Atan2(vel.y, vel.x) * 180 / Mathf.PI + 180;
                        float len = (transform.position - sp.GetObjList()[loopCount].transform.position).magnitude;
                        AfterImage(Vector3.zero, Vector3.zero);
                        SenkuEffect(sp.GetObjList()[loopCount].transform.position,ang);
                        transform.position += senku * len - senku * 2;
                        loopCount++;
                        lockSpTime = 0;
                        audio.PlayOneShot(senkugiri);

                    }

                }
                

            }
            if (lockSp != null && sp.GetListCount() <= loopCount)
            {
                for (int i = 0; i < sp.GetListCount(); i++)
                {
                    BaseEnemy be = sp.GetObjList()[i].GetComponent<BaseEnemy>();
                    be.Damage(10);
                    if (be == null)
                    {
                        ShotEnemy st = sp.GetObjList()[i].GetComponent<ShotEnemy>();
                    }
                }

                inC = InputControl.N;
                stoptime = 1.5f;
                Destroy(lockSp);
                Destroy(moveColider);
                loopCount = 0;
                kamae = false;
            }
        }
        if (sp != null)
        {
            lockSpTime += Time.deltaTime;
            inC = InputControl.Y;
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
            anim.SetBool("Senku2", false);
            anim.SetBool("Senku", false);
            anim.SetBool("Power2", false);
            anim.SetBool("Power", false);
            inC = InputControl.N;
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

        anim.SetBool("Senku", false);
        anim.SetBool("Senku2", false);
        anim.SetBool("Walk", false);
        anim.SetBool("Damage", true);
        anim.SetBool("Power", false);
        anim.SetBool("Power2", false);
      
        if (muteki < 0)
        {
            hp -= _damage;
            muteki = 1.5f;
            audio.PlayOneShot(damage);
            audio.PlayOneShot(damageVoice);

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

            gameObject.layer = 8;
        }
        else if (muteki > 0)
        {
            gameObject.layer = 17;
        }
        if (muteki < 0.5f)
        {
            anim.SetBool("Damage", false);
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
        if (muteki > 0.1f) 
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
    public bool GetClearFlag()
    {
        if (clearTime <= 0 && clearFlag) 
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
