using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    private int coin = 0;
    private int makimono = 0;
    private int makimono2 = 0;
    private int makimono3 = 0;
    private int kaihuku = 0;
    
    //private int item = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coin = GameData.coin;
        makimono = GameData.makimono;
    }

    public int GetCoin()
    {
        return coin;
    }

    public int GetMakimono()
    {
        return makimono;
    }

    public int GetMakimono2()
    {
        return makimono2;
    }

    public int GetMakimono3()
    {
        return makimono3;
    }

    public int GetKaihuku()
    {
        return kaihuku;
    }

    public void UpCoin(int num)
    {
        GameData.GetCoin(num);
    }

    public void UpMakimono(int num)
    {
        GameData.GetMakimono(num);
    }

    public void UseKaihuku(int num)
    {

    }

}
