using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Renderer))]
public class Dissolver : MonoBehaviour
{

    Material m_Material;
    YieldInstruction m_Instruction = new WaitForEndOfFrame();

    public Text text1;
    public Text text2;
    public Text text3;

    public Text text4;
    public Text text5;
    public Text text6;
    public Text text7;



    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;

    public Image image1;
    public Image image2;
    public Image image3;
    public Image image4;


    void Start()
    {
        m_Material = GetComponent<Renderer>().material;
        image1 = button1.GetComponent<Image>();
        image2 = button2.GetComponent<Image>();
        image3 = button3.GetComponent<Image>();
        image4 = button4.GetComponent<Image>();
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.V))
        //{
        //    StartCoroutine(Up());
        //}
    }

    IEnumerator Animate()
    {
        float time = 0;
        float duration = 1f;
        int dir = 1;

        while (true)
        {
            yield return m_Instruction;

            time += Time.deltaTime * dir;
            var t = time / duration;

            if (t > 1f)
            {
                dir = 1;
            }
            else if (t < 0)
            {
                //dir = 1;
                break;
            }
            if(t == 1)
            {
                break;
            }

            m_Material.SetFloat("_CutOff", t);
        }
    }

    IEnumerator Animate2()
    {
        //float time = 0;
        //float duration = -1f;
        //int dir = 1;
        float t = 1;
        while (true)
        {
            yield return m_Instruction;

            //yield return new WaitForSeconds(0.05f);

            //time += Time.deltaTime * dir;
            //var t = time / duration;

            //if (t > 1f)
            //{
            //    dir = -1;
            //}
            //else if (t < 0)
            //{
            //    dir = -1;

            //}
            //if(t == 0)
            //{
            //    break;
            //}

            

            t -= Time.deltaTime;

            if(t <= 0)
            {
                break;
            }
            

            m_Material.SetFloat("_CutOff", t);
        }
    }

    bool oneFlag = false;

    public void CutStart(string sceneName)
    {
        if(!oneFlag)
        {
            StartCoroutine(Up(sceneName));
            oneFlag = true;
        }
       
    }

    IEnumerator Up(string sceneName)
    {
        float count = 0;
        while(count < 1)
        {
            float alpha = 0;
            alpha += Time.deltaTime * 1f;
            count += alpha;
            text1.color = new Color(text1.color.r, text1.color.g, text1.color.b, text1.color.a - alpha);
            text2.color = new Color(text2.color.r, text2.color.g, text2.color.b, text2.color.a - alpha);
            text3.color = new Color(text3.color.r, text3.color.g, text3.color.b, text3.color.a - alpha);

            text6.color = new Color(text6.color.r, text6.color.g, text6.color.b, text6.color.a - alpha);
            text4.color = new Color(text4.color.r, text4.color.g, text4.color.b, text4.color.a - alpha);
            text5.color = new Color(text5.color.r, text5.color.g, text5.color.b, text5.color.a - alpha);
            text7.color = new Color(text7.color.r, text7.color.g, text7.color.b, text7.color.a - alpha);

            image1.color = new Color(image1.color.r, image1.color.g, image1.color.b, image1.color.a - alpha);
            image2.color = new Color(image2.color.r, image2.color.g, image2.color.b, image2.color.a - alpha);
            image3.color = new Color(image3.color.r, image3.color.g, image3.color.b, image3.color.a - alpha);           
            image4.color = new Color(image4.color.r, image4.color.g, image4.color.b, image4.color.a - alpha);  
            


            if(count > 1)
            {
                float reset = 0;
                text1.color = new Color(text1.color.r, text1.color.g, text1.color.b, reset);
                text2.color = new Color(text2.color.r, text2.color.g, text2.color.b, reset);
                text3.color = new Color(text3.color.r, text3.color.g, text3.color.b, reset);
                text6.color = new Color(text6.color.r, text6.color.g, text6.color.b, reset);
                text4.color = new Color(text4.color.r, text4.color.g, text4.color.b, reset);
                text5.color = new Color(text5.color.r, text5.color.g, text5.color.b, reset);
                text7.color = new Color(text7.color.r, text7.color.g, text7.color.b, reset);

                image1.color = new Color(image1.color.r, image1.color.g, image1.color.b, reset);
                image2.color = new Color(image2.color.r, image2.color.g, image2.color.b, reset);
                image3.color = new Color(image3.color.r, image3.color.g, image3.color.b, reset);
                image4.color = new Color(image4.color.r, image4.color.g, image4.color.b, reset);
            }
        }


        float t = 0;
        bool fl = false;
        float num = 5f;

        while (true)
        {
            yield return m_Instruction;

            //yield return new WaitForSeconds(0.05f);
            //100,0.48
            //200,0.465f
            //300, 0.48
            if (t>= 0.48f && !fl)
            {
                yield return new WaitForSeconds(0.7f);
                fl = true;
                num = 1f;
            }

            t += Time.deltaTime * (num + t/2);
       
            if (t >= 2)
            {
                break;
            }


            m_Material.SetFloat("_CutOff", t);
        }

        SceneManager.LoadScene(sceneName);

        //yield return new WaitForSeconds(0.7f);
        //m_Material.SetFloat("_CutOff", 0);
        //yield return new WaitForSeconds(0.7f);
        //StartCoroutine(Up());
    }
}