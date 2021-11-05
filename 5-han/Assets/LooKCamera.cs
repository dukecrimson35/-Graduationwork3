using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LooKCamera : MonoBehaviour
{
    public GameObject target;
    Rigidbody rigid;
    Vector3 length;
    Vector3 lengthX;

    Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        length = new Vector3(0, 0, -26);
        lengthX = new Vector3(2, 2, 0);

        transform.position = target.transform.position +  length;
        rigid = target.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = target.transform.position - (transform.position - length - lengthX);
        velocity = velocity.normalized * 3;
        //    velocity = new Vector3(velocity.x, rigid.velocity.y, velocity.z);
        if (Mathf.Abs((target.transform.position - (transform.position - length - lengthX)).magnitude) > 1f &&
           Mathf.Abs((target.transform.position - (transform.position - length - lengthX)).magnitude) < 1.5f)
        {
            transform.position += velocity * Time.deltaTime;
        }
        
        else if (Mathf.Abs((target.transform.position - (transform.position - length - lengthX)).magnitude) > 1.5f &&
           Mathf.Abs((target.transform.position - (transform.position - length - lengthX)).magnitude) < 2f)
        {
            velocity = velocity.normalized * 11;
            transform.position += velocity * Time.deltaTime;
        }
        else if (Mathf.Abs((target.transform.position - (transform.position - length - lengthX)).magnitude) > 2) 
        {
            velocity = velocity.normalized * 20;
           transform.position += velocity * Time.deltaTime;
        }
      
        transform.LookAt(transform.position - length);
     
    }
}
