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
    Rigidbody rigid;
    bool kamae;
    float stoptime;
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

      
    }
    private void OnCollisionEnter(Collision collision)
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        Direction();
        IsSenkuHit();
        SenkuGiri();
    }
    private void Move()
    {
        if (stoptime < 0)
        {
            if (!kamae && (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("Horizontal") == -1)) 
            {
                currentDirec = Direc.Left;
                pos.x -= 0.1f;
            }

            if (!kamae && (Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("Horizontal") == 1)) 
            {
                currentDirec = Direc.Right;
                pos.x += 0.1f;
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
            //velocity = new Vector3(0, 0.5f, 0);
        }

        //if (Mathf.Abs(velocity.y) <= 0.1f)
        //{
        //    velocity.y = 0;
        //}
        //else if (velocity.y > 0.1f)
        //{
        //    velocity.y -= 0.1f;
        //}
        //else if (velocity.y < -0.1f)
        //{
        //    velocity.y += 0.1f;
        //}
        pos += velocity;
        transform.position += pos;
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

        if (stoptime < 0)
        {
            if (Input.GetButton("A"))
            {
                if (moveColider == null)
                {
                    moveColider = Instantiate((GameObject)Resources.Load("MoveCollider"));
                    move = moveColider.GetComponent<SenkuMove>();
                }
                rigid.velocity = new Vector3(0, 0, 0);
                kamae = true;
            }
        
            if (Input.GetButtonUp("A") && move.GetIsMove())
            {
                kamae = false;
                stoptime = 1.5f;
                col= Instantiate((GameObject)Resources.Load("SenkuCollider"));
              
                Vector3 senku = new Vector3(Input.GetAxis("Horizontal") * 7,Input.GetAxis("Vertical") * 7, 0);
                col.transform.position = transform.position;
                col.transform.position += senku / 2;
                float ang = Mathf.Atan2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * 180 / Mathf.PI + 180;
                col.transform.rotation = Quaternion.Euler(0, 0, ang);

                transform.position += senku;
                Destroy(moveColider);
            }
        }
        float tes = Mathf.Atan2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * 180 / Mathf.PI + 180;
        Debug.Log(tes);
        float vert = Input.GetAxis("Vertical");
        //Debug.Log(vert);
        if (moveColider != null)
        {
            moveColider.transform.position = transform.position;
            moveColider.transform.rotation = Quaternion.Euler(0, 0, tes);
        }

        stoptime -= Time.deltaTime;
    }
    void IsSenkuHit()
    {
        if (col != null)
        {
            SenkuSprict s = col.GetComponent<SenkuSprict>();
            if(s.GetHitFlag())
            {
                stoptime = 0;
            }
        }
    }
    public int GetHp()
    {
        return hp;
    }
}
