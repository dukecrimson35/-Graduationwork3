using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{

    private string sceneName = "";

    private Data gameData;

    public GameObject fade;

    private bool oneFadeFlag = false;

    private GameObject boss;

    private bool clearFlag = false;
    private bool deadFlag = false;

    private GameObject player;
    private PlayerControl playerControl;

    public enum SceneNames
    {
        TitleScene,
        SelectScene,
        Stage01,
        Stage02,
        Stage03,
        GameScene,
        GameClearScene,
        GameOverScene,
    };

    //public List<string> stages = new List<string>();

    //private int maxStageNumber = 3;

    // Start is called before the first frame update
    void Start()
    {
        //gameData = GetComponent<GameData>();
        sceneName = SceneManager.GetActiveScene().name;

        boss = GameObject.FindGameObjectWithTag("BossEnemy");

        //for(int i = 0; i< GameData.maxStageNumber;i++)
        //{
        //    stages.Add("stage" + (i + 1).ToString());
        //}
        if(GameObject.FindGameObjectWithTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerControl = player.GetComponent<PlayerControl>();
        }

       
    }

    // Update is called once per frame
    void Update()
    {
        if(!oneFadeFlag && fade != null)
        {
            fade.GetComponent<FadeStart>().FadeInA();
            oneFadeFlag = true;
        }

        if(playerControl.GetHp()<=0 && playerControl != null)
        {
            deadFlag = true;
        }

        if(boss == null)
        {
            clearFlag = true;
        }

        //if(Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    if (sceneName == SceneNames.TitleScene.ToString())
        //    {
        //        SceneManager.LoadScene(SceneNames.SelectScene.ToString());
        //    }
        //    else if (sceneName == SceneNames.SelectScene.ToString())
        //    {
        //        SceneManager.LoadScene(SceneNames.GameScene.ToString());
        //    }
        //    else if (sceneName == SceneNames.GameScene.ToString())
        //    {
        //        SceneManager.LoadScene(SceneNames.GameClearScene.ToString());
        //    }
        //    else if (sceneName == SceneNames.GameClearScene.ToString())
        //    {
        //        SceneManager.LoadScene(SceneNames.TitleScene.ToString());
        //    }
        //    else if (sceneName == SceneNames.GameOverScene.ToString())
        //    {
        //        SceneManager.LoadScene(SceneNames.TitleScene.ToString());
        //    }
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    if (sceneName == SceneNames.GameScene.ToString())
        //    {
        //        SceneManager.LoadScene(SceneNames.GameOverScene.ToString());
        //    }
            
        //}


        if(sceneName == "TitleScene")
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                //SceneManager.LoadScene(SceneNames.SelectScene.ToString());
                fade.GetComponent<FadeStart>().FadeOutNextScene(SceneNames.SelectScene.ToString());
            }
           
        }
        else if( sceneName == "SelectScene")
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                //SceneManager.LoadScene(SceneNames.Stage01.ToString());
                fade.GetComponent<FadeStart>().FadeOutNextScene(SceneNames.Stage01.ToString());
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                //SceneManager.LoadScene(SceneNames.Stage02.ToString());
                fade.GetComponent<FadeStart>().FadeOutNextScene(SceneNames.Stage02.ToString());
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                //SceneManager.LoadScene(SceneNames.Stage03.ToString());
                fade.GetComponent<FadeStart>().FadeOutNextScene(SceneNames.Stage03.ToString());
            }
        }
        else if (sceneName == "Stage01")
        {
            if (Input.GetKeyDown(KeyCode.Alpha0) || clearFlag)
            {
                //SceneManager.LoadScene(SceneNames.GameClearScene.ToString());
                fade.GetComponent<FadeStart>().FadeOutNextScene(SceneNames.GameClearScene.ToString());
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9) || deadFlag)
            {
                //SceneManager.LoadScene(SceneNames.GameOverScene.ToString());
                fade.GetComponent<FadeStart>().FadeOutNextScene(SceneNames.GameOverScene.ToString());
            }
        }
        else if (sceneName == "Stage02")
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                //SceneManager.LoadScene(SceneNames.GameClearScene.ToString());
                fade.GetComponent<FadeStart>().FadeOutNextScene(SceneNames.GameClearScene.ToString());
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9) || deadFlag)
            {
                //SceneManager.LoadScene(SceneNames.GameOverScene.ToString());
                fade.GetComponent<FadeStart>().FadeOutNextScene(SceneNames.GameOverScene.ToString());
            }
        }
        else if (sceneName == "Stage03")
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                //SceneManager.LoadScene(SceneNames.GameClearScene.ToString());
                fade.GetComponent<FadeStart>().FadeOutNextScene(SceneNames.GameClearScene.ToString());
            }
            else if(Input.GetKeyDown(KeyCode.Alpha9) || deadFlag)
            {
                //SceneManager.LoadScene(SceneNames.GameOverScene.ToString());
                fade.GetComponent<FadeStart>().FadeOutNextScene(SceneNames.GameOverScene.ToString());
            }
        }
        else if (sceneName == "GameClearScene")
        {
            if(Input.GetKeyDown(KeyCode.Alpha0))
            {
                //SceneManager.LoadScene(SceneNames.TitleScene.ToString());
                fade.GetComponent<FadeStart>().FadeOutNextScene(SceneNames.TitleScene.ToString());
            }
        }
        else if (sceneName == "GameOverScene")
        {
            if (Input.GetKeyDown(KeyCode.Alpha0) )
            {
                //SceneManager.LoadScene(SceneNames.TitleScene.ToString());
                fade.GetComponent<FadeStart>().FadeOutNextScene(SceneNames.TitleScene.ToString());
            }
        }



    }


    //↓後で使いやすくする用
    public void NextScene()
    {
        if (sceneName == SceneNames.TitleScene.ToString())
        {
            SceneManager.LoadScene(SceneNames.GameScene.ToString());
        }
        else if (sceneName == SceneNames.GameScene.ToString())
        {
            SceneManager.LoadScene(SceneNames.GameClearScene.ToString());
        }   
        else if (sceneName == SceneNames.GameClearScene.ToString())
        {
            SceneManager.LoadScene(SceneNames.TitleScene.ToString());
        }
        else if (sceneName == SceneNames.GameScene.ToString())
        {
            SceneManager.LoadScene(SceneNames.GameOverScene.ToString());
        }
        else if (sceneName == SceneNames.GameOverScene.ToString())
        {
            SceneManager.LoadScene(SceneNames.TitleScene.ToString());
        }
    }

    //シーンでのボタン処理----------------------------------------------------------------------------
    public void OnClickSelectButton()
    {
        fade.GetComponent<FadeStart>().FadeOutNextScene(SceneNames.SelectScene.ToString());        
    }

    public void OnClickOptionButton()
    {
        Debug.Log("これからオプションのUI作る");
    }

    public void OnClickEndButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;   // UnityEditorの実行を停止する処理
#else
        Application.Quit();                                // ゲームを終了する処理
#endif
    }

    public void OnClickStage1Button()
    {
        fade.GetComponent<FadeStart>().FadeOutNextScene(SceneNames.Stage01.ToString());
    }

    public void OnClickStage2Button()
    {
        fade.GetComponent<FadeStart>().FadeOutNextScene(SceneNames.Stage02.ToString());
    }

    public void OnClickStage3Button()
    {
        fade.GetComponent<FadeStart>().FadeOutNextScene(SceneNames.Stage03.ToString());
    }

    public void OnClickTitleButton()
    {
        fade.GetComponent<FadeStart>().FadeOutNextScene(SceneNames.TitleScene.ToString());
    }

    public void OnClickClearButton()
    {
        fade.GetComponent<FadeStart>().FadeOutNextScene(SceneNames.GameClearScene.ToString());
    }
    public void OnClickGameOverButton()
    {
        fade.GetComponent<FadeStart>().FadeOutNextScene(SceneNames.GameOverScene.ToString());
    }




    //シーンでのボタン処理----------------------------------------------------------------------------
}
