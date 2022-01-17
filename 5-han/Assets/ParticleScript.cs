using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    float lifetime = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        float lifetime = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime < 0) 
        {
            Destroy(gameObject);
        }
    }
}
