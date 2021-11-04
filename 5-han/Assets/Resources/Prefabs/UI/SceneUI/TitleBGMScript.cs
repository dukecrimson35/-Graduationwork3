using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBGMScript : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip bgm;

    public bool fl = false;
    // Start is called before the first frame update
    void Start()
    {
        if(fl)
        {
            if (GameObject.Find("TitleBGM"))
            {
                
            }
            else
            {
                audioSource.volume = 0.1f;
                audioSource.clip = bgm;
                audioSource.Play();
                DontDestroyOnLoad(this);
            }
        }
        else
        {
            audioSource.volume = 0.1f;
            audioSource.clip = bgm;
            audioSource.Play();
            DontDestroyOnLoad(this);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
