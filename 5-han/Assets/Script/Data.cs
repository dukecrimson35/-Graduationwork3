using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Data : MonoBehaviour
{
    // Start is called before the first frame update
    public static readonly int maxStageNumber = 3;
    public const int maxStageNumber2 = 3;

    public static List<string> dataItemStringList = new List<string>();
    public static List<int> dataItemIntList = new List<int>();

    public static bool shopFlag = false;
    //デバックのため後で0に戻す
    public static int coin = 10000;
    public static int makimono = 0;
    public static int kaihuku = 0;
    public static int kaihuku2 = 0;
    public static int makimono2 = 0;
    public static int makimono3 = 0;
    public static bool pauseWindFlag = false;
    public static bool pauseItemListFlag = false;

    //急遽作ったやつ、後で別仕様に変更
    public static bool titleSceneFlag = false;
    public static bool selectSceneFlag = false;

    //public static bool shopOpenFlag = false;


    public static void GetPauseMenuItemCount()
    {
        dataItemIntList.Clear();
        dataItemStringList.Clear();
        if(makimono>0)
        {
            dataItemIntList.Add(makimono);
            dataItemStringList.Add("まきもの");
        }
        if(makimono2 > 0)
        {
            dataItemIntList.Add(makimono2);
            dataItemStringList.Add("まきもの2");
        }
        if (makimono3 > 0)
        {
            dataItemIntList.Add(makimono3);
            dataItemStringList.Add("まきもの3");
        }
        if (kaihuku > 0)
        {
            dataItemIntList.Add(kaihuku);
            dataItemStringList.Add("かいふく");
        }
        if (kaihuku2 > 0)
        {
            dataItemIntList.Add(kaihuku2);
            dataItemStringList.Add("かいふく2");
        }
        dataItemStringList.Add("もどる");
    }

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

    



}
