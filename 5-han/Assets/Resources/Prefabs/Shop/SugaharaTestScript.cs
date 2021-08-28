using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugaharaTestScript : MonoBehaviour
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
        //コインを増やすコマンド
        if (Input.GetKeyDown(KeyCode.A))
        {
            itemManagerScript.UpCoin(100);
        }
        //ショップを開くコマンド
        if (Input.GetKeyDown(KeyCode.S) && GameObject.Find("ShopPrefab(Clone)") == null)
        {
            GameObject instance =
               (GameObject)Instantiate(shopPrefab,
               new Vector3(0, 0, 0.0f), Quaternion.identity);
        }

    }
}
