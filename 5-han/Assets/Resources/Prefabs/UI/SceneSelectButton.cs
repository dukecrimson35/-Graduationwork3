using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneSelectButton : MonoBehaviour
{

    public Button stage1Button;
    public Button stage2Button;
    public Button stage3Button;
    public Button titleButton;

    public GameObject sceneManagerOBJ;
    private SceneManagement sceneManagement;
    // Start is called before the first frame update
    void Start()
    {
        stage1Button.Select();

        sceneManagement = sceneManagerOBJ.GetComponent<SceneManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickStage1Button()
    {
        sceneManagement.OnClickStage1Button();
    }
    public void OnClickStage2Button()
    {
        sceneManagement.OnClickStage2Button();
    }
    public void OnClickStage3Button()
    {
        sceneManagement.OnClickStage3Button();
    }
    public void OnClickTitleButton()
    {
        sceneManagement.OnClickTitleButton();
    }


}
