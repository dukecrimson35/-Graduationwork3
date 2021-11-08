using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSEScript : MonoBehaviour
{
    public List<bool> seFlagList = new List<bool>();

    public List<int> timeList = new List<int>();

    private bool[] seFlags = new bool[10];


    public AudioSource audioSource;
    public AudioClip se;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySE()
    {
        audioSource.PlayOneShot(se);
        seFlagList.Add(true);

    }
}
