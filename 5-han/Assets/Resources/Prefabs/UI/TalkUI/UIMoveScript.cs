using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMoveScript : MonoBehaviour
{
    private GameObject hpBar;
    private GameObject hpUI;
    private GameObject coinUI;
    private GameObject coinObjUI;

    public GameObject kuroObiUITop;
    public GameObject kuroObiUIBottom;

    private VoiceScript voiceScript;
    bool oneFlag = false;


    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.Find("GamePlayUI"))
        {
            coinObjUI = GameObject.Find("GamePlayUI");
            coinUI = coinObjUI.transform.GetChild(0).gameObject;
        }

        if (GameObject.Find("PlayerHPUI"))
        {
            hpUI = GameObject.Find("PlayerHPUI");
            hpBar = hpUI.transform.GetChild(0).gameObject;
        }

        voiceScript = GetComponent<VoiceScript>();

        StartCoroutine(StartCoroutine());

    }
    float delay = 0.01f;

    float second = 50;

    IEnumerator StartCoroutine()
    {
      
        Vector3 move = new Vector3(0, 210f/second, 0);
        //Vector3 move2 = new Vector3(0, 209f/second, 0);

        for (int i = 0; i< second; i++)
        {
            kuroObiUITop.transform.position -= move;
            kuroObiUIBottom.transform.position += move;
            if(hpBar != null)
            {
                hpBar.transform.position -= move;
            }
            if(coinUI != null)
            {
                coinUI.transform.position -= move;
            }
            yield return new WaitForSecondsRealtime(delay);
        }
    }

    IEnumerator EndCoroutine()
    {
        Vector3 move = new Vector3(0, 210f / second, 0);

        for (int i = 0; i < second; i++)
        {
            kuroObiUITop.transform.position += move;
            kuroObiUIBottom.transform.position -= move;
            if (hpBar != null)
            {
                hpBar.transform.position += move;
            }
            if (coinUI != null)
            {
                coinUI.transform.position += move;
            }
            yield return new WaitForSecondsRealtime(delay);
        }
        Destroy(GameObject.Find("TalkUICanvas(Clone)"));
    }

    // Update is called once per frame
    void Update()
    {
       

        

        //戻す
        if (voiceScript.GetEndFlag() && !oneFlag)
        {
            oneFlag = true;
            StartCoroutine(EndCoroutine());
        }
    }
}
