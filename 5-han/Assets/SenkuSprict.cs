using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenkuSprict : MonoBehaviour
{
    float life = 0.2f;
    bool hit;
    // Start is caled before the first frame update
    void Start()
    {

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
            hit = true;
        }
    }
    public bool GetHitFlag()
    {
        return hit;
    }
}
