using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMoveScript : MonoBehaviour
{
    private bool flag = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            flag = true;
        }
        if(Data.bossWallStartFlag)
        {
            if(transform.position.y > 2.5f)
            {
                transform.position -= new Vector3(0, 1.5f, 0) * Time.deltaTime;
            }
        }
    }
}
