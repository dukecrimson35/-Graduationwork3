using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fpsDisplay : MonoBehaviour
{
    float fps;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fps = 1f / Time.deltaTime;
        text.text = fps.ToString();
    }
}
