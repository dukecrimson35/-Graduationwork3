using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRange : MonoBehaviour
{
    bool inRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Block")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Block")
        {
            inRange = false;
        }
    }

    public bool GetinRange()
    {
        return inRange;
    }
}
