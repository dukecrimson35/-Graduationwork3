﻿using System.Collections;
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

    public AudioSource audioSource;
    public AudioClip bgm;

    public bool destroyFlag = false;

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

    void Start()
    {
        audioSource.volume = 0.1f;     

        //gameData = GetComponent<GameData>();
        sceneName = SceneManager.GetActiveScene().name;

        boss = GameObject.FindGameObjectWithTag("BossEnemy");

        //for(int i = 0; i< GameData.maxStageNumber;i++)
        //{
        //    stages.Add("stage" + (i + 1).ToString());

        //}

        if (sceneName == "Stage01" || sceneName == "Stage02" || sceneName == "Stage03")
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerControl = player.GetComponent<PlayerControl>();
            if(sceneName == "Stage01")
            {
                Data.stageNum = 1;
            }
            else if (sceneName == "Stage02")
            {
                Data.stageNum = 2;
            }
            else if (sceneName == "Stage03")
            {
                Data.stageNum = 3;
            }
        }

        if(bgm != null)
        {
            if(sceneName != "SelectScene" && sceneName != "TitleScene")
            {
                audioSource.clip = bgm;
                audioSource.Play();

                if (GameObject.Find("TitleBGM"))
                {
                    Destroy(GameObject.Find("TitleBGM"));
                }
                if (GameObject.Find("SelectBGM"))
                {
                    Destroy(GameObject.Find("SelectBGM"));
                }

            }         
        }
        else
        {
            Debug.Log("BGMが入ってないよ");
        }
    }

    float second = 100;
    float bgmDelay = 0.01f;
    float vol = 0;
    IEnumerator BGMFadeOutCoroutine()
    {   
        vol = audioSource.volume / second;

        for (int i = 0; i < second; i++)
        {
            audioSource.volume = audioSource.volume - vol;
            yield return new WaitForSecondsRealtime(bgmDelay);
        }
        audioSource.Pause();
    }

    IEnumerator BGMFadeInCoroutine()
    {

       // Vector3 move = new Vector3(0, 210f / second, 0);
        //float vol = audioSource.volume / second;
        //Vector3 move2 = new Vector3(0, 209f/second, 0);

        audioSource.UnPause();

        for (int i = 0; i < second; i++)
        {
            audioSource.volume = audioSource.volume + vol;
            yield return new WaitForSecondsRealtime(bgmDelay);
        }
    }

    public void BGMFadeOut()
    {
        StartCoroutine(BGMFadeOutCoroutine());
    }
    public void BGMFadeIn()
    {
        StartCoroutine(BGMFadeInCoroutine());
    }

    // Update is called once per frame
    void Update()
    {

        if (!oneFadeFlag && fade != null)
        {
            fade.GetComponent<FadeStart>().FadeInA();
            oneFadeFlag = true;
        }

        if (sceneName == "Stage01" || sceneName == "Stage02" || sceneName == "Stage03")
        {
            if (playerControl.GetHp() <= 0 && playerControl != null)
            {
                deadFlag = true;
            }

            if (boss == null)
            {
                clearFlag = true;
            }
        }


        PauseSceneChange();

        if (sceneName == "TitleScene")
        {
            //if (Input.GetKeyDown(KeyCode.Alpha0))
            //{
            //    //SceneManager.LoadScene(SceneNames.SelectScene.ToString());
            //    fade.GetComponent<FadeStart>().FadeOutNextScene(SceneNames.SelectScene.ToString());
            //}
           
        }
        else if( sceneName == "SelectScene")
        {
            //if (Input.GetKeyDown(KeyCode.Alpha1))
            //{
            //    //SceneManager.LoadScene(SceneNames.Stage01.ToString());
            //    fade.GetComponent<FadeStart>().FadeOutNextScene(SceneNames.Stage01.ToString());
            //}
            //else if (Input.GetKeyDown(KeyCode.Alpha2))
            //{
            //    //SceneManager.LoadScene(SceneNames.Stage02.ToString());
            //    fade.GetComponent<FadeStart>().FadeOutNextScene(SceneNames.Stage02.ToString());
            //}
            //else if (Input.GetKeyDown(KeyCode.Alpha3))
            //{
            //    //SceneManager.LoadScene(SceneNames.Stage03.ToString());
            //    fade.GetComponent<FadeStart>().FadeOutNextScene(SceneNames.Stage03.ToString());
            //}
        }
        else if (sceneName == "Stage01")
        {

       

            if ( clearFlag)
            {
                //SceneManager.LoadScene(SceneNames.GameClearScene.ToString());
                fade.GetComponent<FadeStart>().FadeOutNextScene(SceneNames.GameClearScene.ToString());
            }
            else if (deadFlag)
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
            //if(Input.GetKeyDown(KeyCode.Alpha0))
            //{
            //    //SceneManager.LoadScene(SceneNames.TitleScene.ToString());
            //    fade.GetComponent<FadeStart>().FadeOutNextScene(SceneNames.TitleScene.ToString());
            //}
        }
        else if (sceneName == "GameOverScene")
        {
            //if (Input.GetKeyDown(KeyCode.Alpha0))
            //{
            //    //SceneManager.LoadScene(SceneNames.TitleScene.ToString());
            //    fade.GetComponent<FadeStart>().FadeOutNextScene(SceneNames.TitleScene.ToString());
            //}
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
        //fade.GetComponent<FadeStart>().FadeOutNextScene(SceneNames.SelectScene.ToString());
        StartCoroutine(Coroutine(SceneNames.SelectScene.ToString()));
    }

    public void OnClickOptionButton()
    {
        Debug.Log("これからオプションのUI作る");
    }

    public void OnClickEndButton()
    {
        StartCoroutine(EndCoroutine());
//#if UNITY_EDITOR
//        UnityEditor.EditorApplication.isPlaying = false;   // UnityEditorの実行を停止する処理
//#else
//        Application.Quit();                                // ゲームを終了する処理
//#endif
    }

    public void PauseSceneChange()
    {
        if(Data.titleSceneFlag)
        {
            Data.titleSceneFlag = false;
            fade.GetComponent<FadeStart>().FadeOutNextScene(SceneNames.TitleScene.ToString());
        }
        else if(Data.selectSceneFlag)
        {
            Data.selectSceneFlag = true;
            fade.GetComponent<FadeStart>().FadeOutNextScene(SceneNames.SelectScene.ToString());
        }
    }

    public void OnClickReStartButton()
    {
       
        if(Data.stageNum == 1)
        {
            StartCoroutine(Coroutine(SceneNames.Stage01.ToString()));
        }
        if (Data.stageNum == 2)
        {
            StartCoroutine(Coroutine(SceneNames.Stage02.ToString()));
        }
        if (Data.stageNum == 3)
        {
            StartCoroutine(Coroutine(SceneNames.Stage03.ToString()));
        }

    }

    public void OnClickStage1Button()
    {
        //fade.GetComponent<FadeStart>().FadeOutNextScene(SceneNames.Stage01.ToString());
        StartCoroutine(Coroutine(SceneNames.Stage01.ToString()));
    }

    public void OnClickStage2Button()
    {
        //fade.GetComponent<FadeStart>().FadeOutNextScene(SceneNames.Stage02.ToString());
        StartCoroutine(Coroutine(SceneNames.Stage02.ToString()));
    }

    public void OnClickStage3Button()
    {
        //fade.GetComponent<FadeStart>().FadeOutNextScene(SceneNames.Stage03.ToString());
        StartCoroutine(Coroutine(SceneNames.Stage03.ToString()));
    }

    public void OnClickTitleButton()
    {
        //fade.GetComponent<FadeStart>().FadeOutNextScene(SceneNames.TitleScene.ToString());
        
        StartCoroutine(Coroutine(SceneNames.TitleScene.ToString()));
    }

    public void OnClickClearButton()
    {
        //fade.GetComponent<FadeStart>().FadeOutNextScene(SceneNames.GameClearScene.ToString());
        StartCoroutine(Coroutine(SceneNames.GameClearScene.ToString()));
    }
    public void OnClickGameOverButton()
    {
        //fade.GetComponent<FadeStart>().FadeOutNextScene(SceneNames.GameOverScene.ToString());
        StartCoroutine(Coroutine(SceneNames.GameOverScene.ToString()));
    }

    float delay = 1.2f;
    IEnumerator Coroutine(string str)
    {
        yield return new WaitForSecondsRealtime(delay);
        fade.GetComponent<FadeStart>().FadeOutNextScene(str);
        if (GameObject.Find("TitleBGM") && SceneNames.SelectScene.ToString() == sceneName)
        {
            Destroy(GameObject.Find("TitleBGM"));
        }
    }

    IEnumerator EndCoroutine()
    {

        yield return new WaitForSecondsRealtime(delay);
        fade.GetComponent<FadeStart>().FadeOut();
        yield return new WaitForSecondsRealtime(1f);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;   // UnityEditorの実行を停止する処理
#else
        Application.Quit();                                // ゲームを終了する処理
#endif

    }




    //シーンでのボタン処理----------------------------------------------------------------------------
}
