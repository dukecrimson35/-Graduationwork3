using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearButtonScript : MonoBehaviour
{

    public Button titleButton;
    public Button selectButton;

    private GameObject sceneManagerOBJ;
    private SceneManagement sceneManagement;

    // Start is called before the first frame update
    void Start()
    {
        sceneManagerOBJ = GameObject.Find("SceneManagerObject");
        titleButton.Select();

        sceneManagement = sceneManagerOBJ.GetComponent<SceneManagement>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickTitleButton()
    {
        ButtonAniScript bas = titleButton.GetComponent<ButtonAniScript>();
        if (bas.GetButtonStopFlag()) return;
        sceneManagement.OnClickTitleButton();
    }

    public void OnClickSelectButton()
    {
        ButtonAniScript bas = selectButton.GetComponent<ButtonAniScript>();
        if (bas.GetButtonStopFlag()) return;
        sceneManagement.OnClickSelectButton();
    }
}
