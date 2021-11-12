using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSlashScript : MonoBehaviour
{
    float life = 0.2f;
    bool hit;
    PlayerControl player;
    // Start is caled before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerControl>();
        hit = false;
        life = 0.5f;
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
            hit = true;
        }
    }
    public bool GetHitFlag()
    {
        return hit;
    }
    public int GetPlayerHitCount()
    {
        return player.GetHitCount();
    }
}
