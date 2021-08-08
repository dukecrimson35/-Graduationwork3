using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LooKCamera : MonoBehaviour
{
    public GameObject target;
    Vector3 length;
    // Start is called before the first frame update
    void Start()
    {
        length = new Vector3(0, 0, -30);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = length + target.transform.position;
        transform.LookAt(target.transform);
     
    }
}
