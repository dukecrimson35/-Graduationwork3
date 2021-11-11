using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSEScript : MonoBehaviour
{
    public List<bool> seFlagList = new List<bool>();

    public List<int> timeList = new List<int>();

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
        for(int i = 0; i< seFlags.Length;i++)
        {
            if(seCheckFlags[i] == false && seFlags[i] == true)
            {
                seCheckFlags[i] = true;               
            }
        }
    }

   
    IEnumerator FlagsCheckCoroutine(int num)
    {
      
        yield return new WaitForSecondsRealtime(0.25f);
        seFlags[num] = false;
        seCheckFlags[num] = false;

    }

    public void PlaySE()
    {    
        if (seFlags[count] == false)
        {
            seFlags[count] = true;
            StartCoroutine(FlagsCheckCoroutine(count));
            count++;
            audioSource.PlayOneShot(se);
           
            if (count>seFlags.Length-1)
            {
                count = 0;
            }
        }
    }
}
