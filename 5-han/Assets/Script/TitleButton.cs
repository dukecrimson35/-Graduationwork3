using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleButton : MonoBehaviour
{

    public Button startButton;
    public Button optionButton;
    public Button endButton;

    // Start is called before the first frame update
    void Start()
    {
        startButton.Select();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        Debug.Log("押した");
    }
}
