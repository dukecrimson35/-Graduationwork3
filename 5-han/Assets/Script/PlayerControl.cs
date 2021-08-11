using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Vector3 velocity;
    Vector3 pos;
    enum Direc
    {
        Right,Left,
    }
    Direc currentDirec;
    // Start is called before the first frame update
    void Start()
    {
        currentDirec = Direc.Right;
        velocity = Vector3.zero;
        pos = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Direction();
        SenkuGiri();
    }
    private void Move()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("Horizontal") == -1)
        {
            currentDirec = Direc.Left;
            pos.x -= 0.1f; 
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("Horizontal") == 1)
        {
            currentDirec = Direc.Right;
            pos.x += 0.1f;
        }

        if (Input.GetKey(KeyCode.Space) || Input.GetButtonDown("Y")) 
        {
            velocity = new Vector3(0, 0.5f, 0);
        }

        if (Mathf.Abs(velocity.y) <= 0.1f)
        {
            velocity.y = 0;
        }
        else if (velocity.y > 0.1f)
        {
            velocity.y -= 0.1f;
        }
        else if (velocity.y < -0.1f)
        {
            velocity.y += 0.1f;
        }
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
        if (Input.GetButtonDown("A"))
        {
            Vector3 senku = new Vector3(7, 0, 0);
            if (currentDirec == Direc.Right)
            {
                transform.position += senku;
            }
            if (currentDirec == Direc.Left)
            {
                transform.position -= senku;
            }
        }
        
    }
}
