using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockSpecial : MonoBehaviour
{
    float scale = 1;
    List<GameObject> objList = new List<GameObject>();
    List<GameObject> markList = new List<GameObject>();

    GameObject currentObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     //   Charge();
        DeadCheck();
    }

    public List<GameObject> GetObjList()
    {
        return objList;
    }
    void DeadCheck()
    {
        for (int a = 0; a < objList.Count; a++)
        {
            if (markList.Count < objList.Count)
            {
                markList.Add(Instantiate((GameObject)Resources.Load("LockMark")));
                markList[a].transform.position = objList[a].transform.position;
                markList[a].transform.parent = this.gameObject.transform;
                float scale = 1 / transform.localScale.x;
                markList[a].transform.localScale = new Vector3(scale, scale, scale);


            }
            else if (markList.Count == objList.Count)
            {
                markList[a].transform.position = objList[a].transform.position;
                markList[a].transform.parent = this.gameObject.transform;
                float scale = 1 / transform.localScale.x;
                markList[a].transform.localScale = new Vector3(scale, scale, scale);
            }
            else if (markList.Count > objList.Count)
            {
                if (a > markList.Count - 1)
                {
                    Destroy(markList[a]);
                }
            }

          
            if (objList[a]==null)
            {
                objList.RemoveAt(a);
                continue;
            }
            BaseEnemy ba = objList[a].GetComponent<BaseEnemy>();
            if(ba.GetHp()<=0)
            {
                objList.RemoveAt(a);
            }
        }
    }
   public void Charge(int hitcount)
    {
        if(Input.GetButton("Y"))
        {
            scale += Time.deltaTime * 15 *( hitcount+1);
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
                    markList.RemoveAt(a);
                }
            }
        }
    }
}
