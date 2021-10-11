using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LooKCamera : MonoBehaviour
{
    public GameObject target;
    Rigidbody rigid;
    Vector3 length;
    Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        length = new Vector3(0, 0, -30);
        transform.position = target.transform.position + length;
        rigid = target.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = target.transform.position - (transform.position - length);
        velocity = velocity.normalized * 11;
        //    velocity = new Vector3(velocity.x, rigid.velocity.y, velocity.z);
        if (Mathf.Abs((target.transform.position - (transform.position - length)).magnitude) > 1f &&
           Mathf.Abs((target.transform.position - (transform.position - length)).magnitude) < 2)
        {
            transform.position += velocity * Time.deltaTime;
        }
        else if (Mathf.Abs((target.transform.position - (transform.position - length)).magnitude) > 2)
        {
            velocity = velocity.normalized * 20;
            transform.position += velocity * Time.deltaTime;
        }
        // transform.position += length + target.transform.position;
        transform.LookAt(transform.position - length);
     
    }
}
