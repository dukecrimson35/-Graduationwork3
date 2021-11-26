using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KumoMove : MonoBehaviour
{
    // Start is called before the first frame update

    private float count = 0;
    public float speed;
    public GameObject cameraPos;
    public int num;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        count += speed * Time.deltaTime;
        transform.position -= new Vector3(speed, 0, 0) * Time.deltaTime;

        if(count > 42 * num)
        {
            if(num == 1)
            {
                num = 2;
            }
            count = 0;
            transform.position = new Vector3(cameraPos.transform.position.x + 50, 0, 0);
        }
    }
}
