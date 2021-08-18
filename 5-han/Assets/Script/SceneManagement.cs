using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{

    private string sceneName = "";

    private GameData gameData;

    public GameObject fade;

    private bool oneFadeFlag = false;

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

        //for(int i = 0; i< GameData.maxStageNumber;i++)
        //{
        //    stages.Add("stage" + (i + 1).ToString());
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if(!oneFadeFlag && fade != null)
        {
            fade.GetComponent<FadeStart>().FadeInA();
            oneFadeFlag = true;
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
                SceneManager.LoadScene(SceneNames.Stage01.ToString());
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SceneManager.LoadScene(SceneNames.Stage02.ToString());
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SceneManager.LoadScene(SceneNames.Stage03.ToString());
            }
        }
        else if (sceneName == "Stage01")
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                SceneManager.LoadScene(SceneNames.GameClearScene.ToString());
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                SceneManager.LoadScene(SceneNames.GameOverScene.ToString());
            }
        }
        else if (sceneName == "Stage02")
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                SceneManager.LoadScene(SceneNames.GameClearScene.ToString());
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                SceneManager.LoadScene(SceneNames.GameOverScene.ToString());
            }
        }
        else if (sceneName == "Stage03")
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                SceneManager.LoadScene(SceneNames.GameClearScene.ToString());
            }
            else if(Input.GetKeyDown(KeyCode.Alpha9))
            {
                SceneManager.LoadScene(SceneNames.GameOverScene.ToString());
            }
        }
        else if (sceneName == "GameClearScene")
        {
            if(Input.GetKeyDown(KeyCode.Alpha0))
            {
                SceneManager.LoadScene(SceneNames.TitleScene.ToString());
            }
        }
        else if (sceneName == "GameOverScene")
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                SceneManager.LoadScene(SceneNames.TitleScene.ToString());
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
}
