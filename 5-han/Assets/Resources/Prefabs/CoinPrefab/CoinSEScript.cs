using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSEScript : MonoBehaviour
{
    private const int Num = 2;
    private bool[] seFlags = new bool[Num];
    private bool[] seCheckFlags = new bool[Num];
    private int count = 0;
    public AudioSource audioSource;
    public AudioClip se;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i< seFlags.Length;i++)//フラグの更新
        {
            if(seCheckFlags[i] == false && seFlags[i] == true)
            {
                seCheckFlags[i] = true;               
            }
        }
    }

    IEnumerator FlagsCheckCoroutine(int num)//クールタイム
    {
        yield return new WaitForSecondsRealtime(0.25f);
        seFlags[num] = false;
        seCheckFlags[num] = false;
    }

    public void PlaySE()//SE鳴らす
    {    
        if (seFlags[count] == false && !Data.voiceFlag)//鳴らす空きがあれば
        {
            seFlags[count] = true;
            StartCoroutine(FlagsCheckCoroutine(count));//クールタイム
            count++;
            
            audioSource.PlayOneShot(se);
           
            if (count>seFlags.Length-1)
            {
                count = 0;
            }
        }
    }
}
