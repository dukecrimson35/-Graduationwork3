using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Data : MonoBehaviour
{
    // Start is called before the first frame update
    public static readonly int maxStageNumber = 3;
    public const int maxStageNumber2 = 3;

    public static int coin = 0;
    public static int makimono = 0;
    public static int kaihuku = 0;
    public static int makimono2 = 0;
    public static int makimono3 = 0;


    public static int GetMaxStageNumber()
    {
        return maxStageNumber;
    }


    public static void GetCoin(int num)
    {
        coin += num;
    }

    public static void GetMakimono(int num)
    {
        makimono += num;
    }

    public static void UseCoin(int num)
    {
        coin -= num;
    }

    public static void UseMakimono(int num)
    {
        makimono -= num;
    }

    public static void BuyItem(string itemName)
    {
        if (itemName == "まきもの")
        {
            makimono++;
        }
        else if (itemName == "まきもの2")
        {
            makimono2++;
        }
        else if (itemName == "まきもの3")
        {
            makimono3++;
        }
        else if (itemName == "かいふく")
        {
            kaihuku++;
        }
    }



}
