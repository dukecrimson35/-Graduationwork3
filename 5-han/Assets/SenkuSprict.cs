using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenkuSprict : MonoBehaviour
{
    float life = 0.2f;
    // Start is caled before the first frame update
    void Start()
    {
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
}
