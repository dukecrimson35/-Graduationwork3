using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OverButtonScript : MonoBehaviour
{
    public Button reStartButton;
    public Button selectButton;

    private GameObject sceneManagerOBJ;
    private SceneManagement sceneManagement;

    // Start is called before the first frame update
    void Start()
    {
        sceneManagerOBJ = GameObject.Find("SceneManagerObject");
        reStartButton.Select();

        sceneManagement = sceneManagerOBJ.GetComponent<SceneManagement>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickRestartButton()
    {
        ButtonAniScript bas = reStartButton.GetComponent<ButtonAniScript>();
        if (bas.GetButtonStopFlag()) return;
        sceneManagement.OnClickReStartButton();
    }

    public void OnClickSelectButton()
    {
        ButtonAniScript bas = selectButton.GetComponent<ButtonAniScript>();
        if (bas.GetButtonStopFlag()) return;
        sceneManagement.OnClickSelectButton();
    }
}
