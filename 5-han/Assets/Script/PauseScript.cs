using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{

    [SerializeField]
    //　ポーズした時に表示するUIのプレハブ
    private GameObject pauseUIPrefab;
    //　ポーズUIのインスタンス
    private GameObject pauseUIInstance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("joystick button 7") && !Data.voiceFlag && !Data.shopFlag)
        {
   
            if (pauseUIInstance == null)
            {
                pauseUIInstance = GameObject.Instantiate(pauseUIPrefab) as GameObject;
                Time.timeScale = 0f;
            }
            else
            {
                Destroy(pauseUIInstance);
                if(GameObject.Find("PauseItemList(Clone)"))
                {
                    Destroy(GameObject.Find("PauseItemList(Clone)"));
                    Data.pauseWindFlag = false;
                }
                if (GameObject.Find("PauseSoundUI(Clone)"))
                {
                    Destroy(GameObject.Find("PauseSoundUI(Clone)"));
                    Data.pauseWindFlag = false;
                }

                Time.timeScale = 1f;
            }

        }
    }
}
