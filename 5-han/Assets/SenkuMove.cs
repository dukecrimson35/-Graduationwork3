using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenkuMove : MonoBehaviour
{
    bool isMove = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      //  isMove = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Block")
        {
          
            isMove = false;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Block")
        {
         
            isMove = false;
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Block")
        {
            
            isMove = true;
        }
    }
    public bool GetIsMove()
    {
        return isMove;
    }
    public void Dead()
    {
        Destroy(gameObject);
    }
}
