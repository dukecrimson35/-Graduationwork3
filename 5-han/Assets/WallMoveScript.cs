using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMoveScript : MonoBehaviour
{
    private bool flag = false;

    public GameObject kumo;
    public GameObject kumo2;

    private KumoMove kumoMove1;
    private KumoMove kumoMove2;



    // Start is called before the first frame update
    void Start()
    {
        kumoMove1 = kumo.GetComponent<KumoMove>();
        kumoMove2 = kumo2.GetComponent<KumoMove>();
    }

    // Update is called once per frame
    void Update()
    {
      
        if(Data.bossWallStartFlag)
        {
            if(transform.position.y > 2.5f)
            {
                transform.position -= new Vector3(0, 1.5f, 0) * Time.deltaTime;
                kumoMove1.SetFl(true);
                kumoMove2.SetFl(true);
            }
            else
            {
                kumoMove1.SetFl(true);
                kumoMove2.SetFl(true);
            }
        }
    }
}
