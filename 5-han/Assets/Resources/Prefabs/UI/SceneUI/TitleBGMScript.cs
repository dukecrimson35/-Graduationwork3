using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleBGMScript : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip bgm;

    public bool fl = false;

    private string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        //if(!fl)
        //{
        //    if (GameObject.Find("TitleBGM"))
        //    {
                
        //    }
        //    else
        //    {
               

        //        if(SceneManagement.SceneNames.SelectScene.ToString() == "SelectScene")
        //        {
        //            audioSource.volume = 0.1f;
        //            audioSource.clip = bgm;
        //            audioSource.Play();
        //        }
        //        //DontDestroyOnLoad(this);
        //    }
        //}
        //if(SceneManagement.SceneNames.TitleScene.ToString() == "TitleScene")
        //{
        //    audioSource.volume = 0.1f;
        //    audioSource.clip = bgm;
        //    audioSource.Play();
        //    DontDestroyOnLoad(this);
        //}

        audioSource.volume = 0.2f;
        audioSource.clip = bgm;
        audioSource.Play();
        DontDestroyOnLoad(this);

    }

    // Update is called once per frame
    void Update()
    {
        sceneName = SceneManager.GetActiveScene().name;
        if ( sceneName!= "SelectScene" && sceneName!= "TitleScene")
        {
            Data.titleBGMFlag = false;
            Destroy(this.gameObject);
           
        }
    }
}
