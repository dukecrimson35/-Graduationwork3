using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScript : MonoBehaviour
{
    public GameObject fade;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NextSceneTitle());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            OnClickTitleButton();
        }
    }

    public void OnClickTitleButton()
    {
        //fade.GetComponent<FadeStart>().FadeOutNextScene(SceneNames.TitleScene.ToString());

        StartCoroutine(Coroutine("TitleScene"));
    }

    IEnumerator Coroutine(string str)
    {
        yield return new WaitForSecondsRealtime(1.2f);
        fade.GetComponent<FadeStart>().FadeOutNextScene(str);
        
    }

    IEnumerator NextSceneTitle()
    {

        yield return new WaitForSecondsRealtime(10);
        OnClickTitleButton();

    }
}
