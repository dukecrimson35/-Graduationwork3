using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkDebudScript : MonoBehaviour
{

    public GameObject talkUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            if (GameObject.Find("TalkUICanvas(Clone)") == null)
            {
                GameObject instance =
                   (GameObject)Instantiate(talkUI,
                   new Vector3(0, 0, 0.0f), Quaternion.identity);
                VoiceScript voiceScript = instance.GetComponent<VoiceScript>();
                voiceScript.SetOniStartFlag();
                //voiceScript.SetOniEndFlag();
            }
        }
       
    }
}
