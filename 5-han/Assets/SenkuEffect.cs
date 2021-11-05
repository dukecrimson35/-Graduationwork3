using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenkuEffect : MonoBehaviour
{
  float  life=0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
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
