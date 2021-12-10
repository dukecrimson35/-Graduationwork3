using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCallScript : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip titleCallSE;
    public AudioClip titleCallSE2;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayTitleCall());
    }

    float seVol = 1;

    [Header("タイトルコールがなるまでの待ち時間(float)")]
    public float waitTime = 1.0f;

    IEnumerator PlayTitleCall()
    {
        yield return new WaitForSecondsRealtime(waitTime);
        float v = audioSource.volume;
        audioSource.volume = seVol;
        audioSource.volume = v;
        audioSource.PlayOneShot(titleCallSE);
        audioSource.volume = v;
    }

    // Update is called once per frame
    void Update()
    {
        //**********************************************************************
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            float v = audioSource.volume;
            audioSource.volume = seVol;
            if (titleCallSE != null) audioSource.PlayOneShot(titleCallSE);
            audioSource.volume = v;

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            float v = audioSource.volume;
            audioSource.volume = seVol;
            if (titleCallSE2 != null) audioSource.PlayOneShot(titleCallSE2);
            audioSource.volume = v;
        }
        //**********************************************************************
    }
}
