using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenkuSprict : MonoBehaviour
{
    float life = 0.2f;
    bool hit;
    private GameObject itemManager;
    private ItemManager itemManagerScript;
    // Start is caled before the first frame update
    void Start()
    {
        itemManager = GameObject.Find("ItemManagerOBJ");
        itemManagerScript = itemManager.GetComponent<ItemManager>();
        hit = false;
        life = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        life -= Time.deltaTime;
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            GameObject after = Instantiate((GameObject)Resources.Load("SenkuEffect"));
            after.transform.position = collision.transform.position;
            float ang = Mathf.Atan2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * 180 / Mathf.PI;
            after.transform.rotation = Quaternion.Euler(0, 0, ang);
            hit = true;
        }
        if (collision.transform.tag == "Bullet")
        {
            GameObject after = Instantiate((GameObject)Resources.Load("SenkuEffect"));
            after.transform.position = collision.transform.position;
            float ang = Mathf.Atan2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * 180 / Mathf.PI;
            after.transform.rotation = Quaternion.Euler(0, 0, ang);
            hit = true;
        }
        if (collision.transform.tag == "BossEnemy")
        {
            GameObject after = Instantiate((GameObject)Resources.Load("SenkuEffect"));
            after.transform.position = collision.transform.position;
            float ang = Mathf.Atan2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * 180 / Mathf.PI ;
            after.transform.rotation = Quaternion.Euler(0, 0, ang);

            hit = true;
        }

        if (collision.gameObject.tag == "Coin")
        {
            Money moneyScript = collision.gameObject.GetComponent<Money>();
            itemManagerScript.UpCoin(moneyScript.GetMoney());
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Bullet")
        {
            GameObject after = Instantiate((GameObject)Resources.Load("SenkuEffect"));
            after.transform.position = other.transform.position;
            float ang = Mathf.Atan2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * 180 / Mathf.PI;
            after.transform.rotation = Quaternion.Euler(0, 0, ang);
            hit = true;
        }
    }
    public bool GetHitFlag()
    {
        return hit;
    }
}
