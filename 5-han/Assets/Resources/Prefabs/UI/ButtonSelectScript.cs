using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonSelectScript : MonoBehaviour
{

    public Button[] buttons;

    private float yazirusiDelay = 0.2f;
    private float yazirusiDelay2 = 0.2f;
    private bool delayFlag = false;
    private int pos = 0;

    private AudioSource audioSource;
    public AudioClip ketteiSE;
    public AudioClip senntakuSE;
    private bool onSEFlag = false;

    public int num = 0;

    float delay = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        buttons[0].Select();
        audioSource = GetComponent<AudioSource>();
        if(SceneManager.GetActiveScene().name == "SelectScene")
        {
            delay = 0.4f;
        }
    }


    
    IEnumerator Coroutine()
    {
        delayFlag = true;
        yield return new WaitForSecondsRealtime(delay);
        delayFlag = false;
    }

    IEnumerator Timer()
    {
        onSEFlag = true;
        yield return new WaitForSecondsRealtime(2);
        onSEFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        //スティックの縦方向取得
        float vert = Input.GetAxis("Vertical");
        float vert2 = Input.GetAxis("CrossUpDown");

        audioSource.volume = Data.seVol;


        if (vert > 0.3f && !delayFlag && pos > 0)
        {

            StartCoroutine(Coroutine());
            pos--;
            buttons[pos].Select();
            SentakuSEPlay();
            //buttons[pos].image.color =  
            //yazirusiDelay = 60;
        }
        else if (vert2 > 0.3f && !delayFlag && pos > 0)
        {

            StartCoroutine(Coroutine());
            pos--;
            buttons[pos].Select();
            SentakuSEPlay();
            //yazirusiDelay2 = 60;
        }
        else if (vert < -0.3f && !delayFlag && pos < buttons.Length - 1)
        {

            StartCoroutine(Coroutine());
            pos++;
            buttons[pos].Select();
            SentakuSEPlay();
            //yazirusiDelay = 60;
        }
        else if (vert2 < -0.3f && !delayFlag && pos < buttons.Length - 1)
        {

            StartCoroutine(Coroutine());
            pos++;
            buttons[pos].Select();
            SentakuSEPlay();
            //yazirusiDelay2 = 60;
        }

        if(Input.GetKeyDown("joystick button 0") && !onSEFlag )
        {
            
            ButtonAniScript bas = buttons[pos].GetComponent<ButtonAniScript>();

            if (bas.GetButtonStopFlag()) return;

            bas.SetBackFlag(true);
            audioSource.volume = 2.0f;
            KetteiSEPlay();
            audioSource.volume = 1.0f;
            StartCoroutine(Timer());
        }

        buttons[pos].Select();


    }

    public int GetPos()
    {
        return pos;
    }

    public void SentakuSEPlay()
    {
        audioSource.PlayOneShot(senntakuSE);
    }

    public void KetteiSEPlay()
    {
        audioSource.PlayOneShot(ketteiSE);
    }
}
