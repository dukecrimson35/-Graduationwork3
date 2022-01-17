using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Data : MonoBehaviour
{
    public static readonly string kaihukuStr = "回復薬";
    public static readonly string kaihuku2Str = "上回復薬";
    public static readonly string ySkillName = "";
    public static readonly string bSkillName = "紅月の書";
    public static readonly string xSkillName = "青天の書";


    public static readonly int maxStageNumber = 3;
    public const int maxStageNumber2 = 3;

    public static List<string> dataItemStringList = new List<string>();
    public static List<int> dataItemIntList = new List<int>();

    public static bool shopFlag = false;
    //デバックのため後で0に戻す
    public static int coin = 0;

    public static int bSkillCount = 0;
    public static int kaihuku = 0;
    public static int kaihuku2 = 0;
    public static int xSkillCount = 0;
    public static int makimono3 = 0;

    public static bool pauseWindFlag = false;
    public static bool pauseItemListFlag = false;

    //急遽作ったやつ、後で別仕様に変更
    public static bool titleSceneFlag = false;
    public static bool selectSceneFlag = false;

    //プレイしたステージ番号　
    public static int stageNum = 0;
    //
    public static bool voiceFlag = false;

    public static int bgm = 2;
    public static int se = 2;
    public static float bgmVol = 1;
    public static float seVol = 1;

    public static bool titleBGMFlag = false;

    public static bool bossWallStartFlag = false;

    public static bool selectBgmFlag = false;

    public static bool bSkill = false;
    public static bool xSkill = false;
    public static bool ySkill = false;

    public static bool stage1 = true;
    public static bool stage2 = false;
    public static bool stage3 = false;

    public static bool oneShopFlag = false;

    //public static bool shopOpenFlag = false;


    public static void GetPauseMenuItemCount()
    {
        dataItemIntList.Clear();
        dataItemStringList.Clear();
        if(bSkillCount>0)
        {
            dataItemIntList.Add(bSkillCount);
            dataItemStringList.Add(bSkillName);
        }
        if(xSkillCount > 0)
        {
            dataItemIntList.Add(xSkillCount);
            dataItemStringList.Add(xSkillName);
        }
        if (makimono3 > 0)
        {
            dataItemIntList.Add(makimono3);
            dataItemStringList.Add("まきもの3");
        }
        if (kaihuku > 0)
        {
            dataItemIntList.Add(kaihuku);
            dataItemStringList.Add(Data.kaihukuStr);
        }
        if (kaihuku2 > 0)
        {
            dataItemIntList.Add(kaihuku2);
            dataItemStringList.Add(Data.kaihuku2Str);
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
        bSkillCount += num;
    }

    public static void UseCoin(int num)
    {
        coin -= num;
    }

    public static void UseMakimono(int num)
    {
        bSkillCount -= num;
    }

    



}
