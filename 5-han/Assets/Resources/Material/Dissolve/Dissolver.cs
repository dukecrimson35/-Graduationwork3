using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public class Dissolver : MonoBehaviour
{

    Material m_Material;
    YieldInstruction m_Instruction = new WaitForEndOfFrame();

    void Start()
    {
        m_Material = GetComponent<Renderer>().material;
        StartCoroutine("Animate2");
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
}