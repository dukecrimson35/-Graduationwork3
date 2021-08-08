using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{

    private string sceneName = "";

    public enum SceneNames
    {
        TitleScene,
        GameScene,
        GameClearScene,
        GameOverScene,
    };

    // Start is called before the first frame update
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
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
            else if (sceneName == SceneNames.GameOverScene.ToString())
            {
                SceneManager.LoadScene(SceneNames.TitleScene.ToString());
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (sceneName == SceneNames.GameScene.ToString())
            {
                SceneManager.LoadScene(SceneNames.GameOverScene.ToString());
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
