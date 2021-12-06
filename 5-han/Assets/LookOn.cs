using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookOn : MonoBehaviour
{
    List<GameObject> LookList= new List<GameObject>();
    GameObject LookObject;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
        NullCheck();
        SetLookObject();
    }
    void NullCheck()
    {
        if (LookList != null)
        {
            for (int a = 0; a < LookList.Count; a++)
            {
                if (LookList[a] == null)
                {
                    LookList.RemoveAt(a);
                }
            }
        }
    }
    void SetLookObject()
    {

        if (LookObject != null)
        {
            for (int a = 0; a < LookList.Count; a++)
            {
                if (LookList[a] != null && transform != null)
                {
                    if (Mathf.Abs((LookList[a].transform.position - transform.position).magnitude) <
                   Mathf.Abs((LookObject.transform.position - transform.position).magnitude))
                    {
                        LookObject = LookList[a];
                    }
                }
            }
        }
        else
        {
            for (int a = 0; a < LookList.Count; a++)
            {
                if (LookList[a] != null && gameObject != null) 
                {
                   
                        LookObject = LookList[a];
                        break;
                  
                }

            }
        }
     
    }
    public GameObject GetLookObject()
    {
        return LookObject;
    }
    
    private void OnTriggerStay(Collider collision)
    {

        if (collision.transform.tag == "Enemy")
        {
            bool check = false;
            if (LookList.Count == 0)
            {
                LookList.Add(collision.gameObject);
                LookObject = collision.gameObject;
            }
            for (int a = 0; a < LookList.Count; a++)
            {
                if (LookList[a] == collision.gameObject)
                {
                    check = true;
                    break;
                }
            }
            if (check == false)
            {
                LookList.Add(collision.gameObject);
            }
        }
        if (collision.transform.tag == "BossEnemy")
        {
            bool check = false;
            if (LookList.Count == 0)
            {
                LookList.Add(collision.gameObject);
                LookObject = collision.gameObject;
            }
            for (int a = 0; a < LookList.Count; a++)
            {
                if (LookList[a] == collision.gameObject)
                {
                    check = true;
                    break;
                }
            }
            if (check == false)
            {
                LookList.Add(collision.gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            for (int a = 0; a < LookList.Count; a++)
            {
                if (LookList[a] == collision.gameObject)
                {
                    LookList.RemoveAt(a);
                }
            }
        }
        if (collision.transform.tag == "BossEnemy")
        {
            for (int a = 0; a < LookList.Count; a++)
            {
                if (LookList[a] == collision.gameObject)
                {
                    LookList.RemoveAt(a);
                }
            }
        }
    }
 
}
