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

    private List<Button> buttons = new List<Button>();

    public GameObject sceneManagerOBJ;
    private SceneManagement sceneManagement;

    private float yazirusiDelay = 0.2f;
    private float yazirusiDelay2 = 0.2f;
    private bool delayFlag = false;
    private int pos = 0;

    // Start is called before the first frame update
    void Start()
    {
        //stage1Button.Select();

        sceneManagement = sceneManagerOBJ.GetComponent<SceneManagement>();


        buttons.Add(stage1Button);
        //buttons.Add(stage2Button);
        //buttons.Add(stage3Button);
        buttons.Add(titleButton);
        //矢印の初期Pos
       
    }

    float delay = 0.2f;
    IEnumerator Coroutine()
    {
        delayFlag = true;
        yield return new WaitForSecondsRealtime(delay);
        delayFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        // ディレイ関係
        //矢印移動ディレイ
       

        //スティックの縦方向取得
        //float vert = Input.GetAxis("Vertical");
        //float vert2 = Input.GetAxis("CrossUpDown");


        //if (vert > 0.3f  && !delayFlag  && pos >0)
        //{
     
        //    StartCoroutine(Coroutine());
        //    pos--;
        //    buttons[pos].Select();
           
        //    //yazirusiDelay = 60;
        //}
        //else if (vert2 > 0.3f && !delayFlag && pos > 0)
        //{
         
        //    StartCoroutine(Coroutine());
        //    pos--;
        //    buttons[pos].Select();
           
        //    //yazirusiDelay2 = 60;
        //}
        //else if (vert < -0.3f && !delayFlag && pos< buttons.Count-1)
        //{
          
        //    StartCoroutine(Coroutine());
        //    pos++;
        //    buttons[pos].Select();
            
        //    //yazirusiDelay = 60;
        //}
        //else if (vert2 < -0.3f && !delayFlag && pos < buttons.Count-1)
        //{
          
        //    StartCoroutine(Coroutine());
        //    pos++;
        //    buttons[pos].Select();
          
        //    //yazirusiDelay2 = 60;
        //}
    }

    public void OnClickStage1Button()
    {
        ButtonAniScript bas = stage1Button.GetComponent<ButtonAniScript>();
        if (bas.GetButtonStopFlag()) return;
        sceneManagement.OnClickStage1Button();
    }
    public void OnClickStage2Button()
    {
        ButtonAniScript bas = stage2Button.GetComponent<ButtonAniScript>();
        if (bas.GetButtonStopFlag()) return;
        sceneManagement.OnClickStage2Button();
    }
    public void OnClickStage3Button()
    {
        ButtonAniScript bas = stage3Button.GetComponent<ButtonAniScript>();
        if (bas.GetButtonStopFlag()) return;
        sceneManagement.OnClickStage3Button();
    }
    public void OnClickTitleButton()
    {
        ButtonAniScript bas = titleButton.GetComponent<ButtonAniScript>();
        if (bas.GetButtonStopFlag()) return;
        sceneManagement.OnClickTitleButton();
    }


}
