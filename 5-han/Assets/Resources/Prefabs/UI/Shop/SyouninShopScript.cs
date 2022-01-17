using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyouninShopScript : MonoBehaviour
{

    private GameObject itemManager;
    private ItemManager itemManagerScript;
    public GameObject shopPrefab;

    // Start is called before the first frame update
    void Start()
    {
        itemManager = GameObject.Find("ItemManagerOBJ");
        itemManagerScript = itemManager.GetComponent<ItemManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKey(KeyCode.B) && Input.GetKeyDown(KeyCode.Alpha3) )
        //{
        //    if (GameObject.Find("ShopPrefab(Clone)") == null)
        //    {
        //        GameObject instance =
        //           (GameObject)Instantiate(shopPrefab,
        //           new Vector3(0, 0, 0.0f), Quaternion.identity);
        //    }
        //}
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(!Data.shopFlag)
            {
                Time.timeScale = 0;
                GameObject instance =
                   (GameObject)Instantiate(shopPrefab,
                   new Vector3(0, 0, 0.0f), Quaternion.identity);
                
            }
            //if (GameObject.Find("ShopPrefab(Clone)") == null)
            //{
            //    GameObject instance =
            //       (GameObject)Instantiate(shopPrefab,
            //       new Vector3(0, 0, 0.0f), Quaternion.identity);
            //    //Time.timeScale = 0;
            //}
        }
    }


}
