using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDebugScript : MonoBehaviour
{

    public Button clearButton;
    public Button gameOverButton;

    private GameObject sceneManagerOBJ;
    private SceneManagement sceneManagement;

    // Start is called before the first frame update
    void Start()
    {

        sceneManagerOBJ = GameObject.Find("SceneManagerObject");
        clearButton.Select();

        sceneManagement = sceneManagerOBJ.GetComponent<SceneManagement>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickClearButton()
    {
        sceneManagement.OnClickClearButton();
    }
    public void OnClickGameOverButton()
    {
        sceneManagement.OnClickGameOverButton();
    }
}
