using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Vector3 velocity;
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector3.zero;
        pos = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void Move()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        { 
            pos.x -= 0.1f;  
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            pos.x += 0.1f;
        }

        if (Input.GetKey(KeyCode.Space))
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
        //   velocity = Vector3.zero;
        transform.position += pos;
        pos = Vector3.zero;
    }
}
