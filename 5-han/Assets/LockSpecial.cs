using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockSpecial : MonoBehaviour
{
    float scale = 1;
    List<GameObject> objList=new List<GameObject>();
    GameObject currentObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Charge();   
    }

   public List<GameObject> GetObjList()
    {
        return objList;
    }
   void Charge()
    {
        if(Input.GetButton("Y"))
        {
            scale += Time.deltaTime * 5;
        }
        transform.localScale = new Vector3(scale, scale, scale);
        if (scale >= 25) 
        {
            transform.localScale = new Vector3(25, 25, 1);
        }
    }
    
   public int GetListCount()
    {
        return objList.Count;
    }
    private void OnTriggerEnter(Collider collision)
    {

        if (collision.transform.tag == "Enemy")
        {
            bool check = false;
            if (objList.Count == 0)
            {
                objList.Add(collision.gameObject);
                currentObj = collision.gameObject;
            }
            for (int a = 0; a < objList.Count; a++)
            {
                if (objList[a] == collision.gameObject)
                {
                    check = true;
                    break;
                }
            }
            if (check == false)
            {
                objList.Add(collision.gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            for (int a = 0; a < objList.Count; a++)
            {
                if (objList[a] == collision.gameObject)
                {
                    objList.RemoveAt(a);
                }
            }
        }
    }
}
