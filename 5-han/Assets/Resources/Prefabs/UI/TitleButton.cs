using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleButton : MonoBehaviour
{

    public Button startButton;
    //public Button optionButton;
    public Button endButton;

    public GameObject sceneManagerOBJ;
    private SceneManagement sceneManagement;

    // Start is called before the first frame update
    void Start()
    {
        startButton.Select();

        sceneManagement = sceneManagerOBJ.GetComponent<SceneManagement>();

        //Screen.lockCursor = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        Debug.Log("押した");
    }

    

    public void OnClickTitleStartButton()
    {
        sceneManagement.OnClickSelectButton();
    }

    public void OnClickEndButton()
    {
        sceneManagement.OnClickEndButton();
    }

    public void OnClickTitleOptionButton()
    {
        sceneManagement.OnClickOptionButton();
    }
}
