using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSound : MonoBehaviour
{
    public GameObject titleBGM;
    // Start is called before the first frame update
    void Start()
    {
        if(!GameObject.Find("TitleBGM"))
        {
            GameObject instance =
                    (GameObject)Instantiate(titleBGM,
                    new Vector3(0, 0, 0.0f), Quaternion.identity);
        }
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
