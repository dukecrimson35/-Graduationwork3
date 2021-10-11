using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButtonManager : MonoBehaviour
{

    public GameObject ui;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown("joystick button 7"))
        {
            GameObject instance =
               (GameObject)Instantiate(ui,
               new Vector3(0, 0, 0.0f), Quaternion.identity);
        }

      
    }
}
