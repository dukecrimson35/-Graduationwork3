using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpGauge : MonoBehaviour
{
    [SerializeField]
    public Image GreenGauge;
    [SerializeField]
    public Image RedGauge;
    [SerializeField]
    public Image Waku;
    float greenDamage = 0;
    float redDamage = 0;
    //int poolDamage = 0;
    float alpha = 0;
    bool UIStartFlag = false;
    float d = 0;


    public GameObject boss;
    private BossEnemy bossScript;
    private float bossHp;
    private float oldBossHp;
    private float bossMaxHP;
    // Start is called before the first frame update
    void Start()
    {
        //boss = GameObject.FindGameObjectWithTag("Player");
        bossScript = boss.GetComponent<BossEnemy>();
        
        bossMaxHP = bossScript.GetHp();
        bossHp = (bossMaxHP / bossMaxHP) * 100;
        oldBossHp = bossHp;
        GreenGauge.color = new Color(GreenGauge.color.r, GreenGauge.color.g, GreenGauge.color.b, alpha);
        RedGauge.color = new Color(RedGauge.color.r, RedGauge.color.g, RedGauge.color.b, alpha);
        Waku.color = new Color(Waku.color.r, Waku.color.g, Waku.color.b, alpha);
    }

    // Update is called once per frame
    void Update()
    {
        //ポーズの時に止める
        if (Time.timeScale <= 0) return;



        if (bossHp < oldBossHp)
        {
            float damage = oldBossHp - bossHp;
            Damage(damage);

        }


        //ゲームが始まったら出てくる
        if (!UIStartFlag)
        {
            //StartUIAlpha();
        }

        if (greenDamage != 0)
        {
            //float num = greenDamage / 100;
            //GreenGauge.fillAmount -= num;
            //greenDamage = 0;
        }
        if (RedGauge.fillAmount >= GreenGauge.fillAmount)
        {
            //Debug.Log(redDamage);
            RedGauge.fillAmount -= redDamage * Time.deltaTime;

            if (RedGauge.fillAmount == GreenGauge.fillAmount)
            {
                redDamage = 0;
            }
        }
        else
        {
            RedGauge.fillAmount = GreenGauge.fillAmount;
        }
        oldBossHp = bossHp;
        bossHp = (bossScript.GetHp()/ bossMaxHP) * 100;
    }

    public void Damage(float dame)
    {
        greenDamage = dame;
        //redDamage = dame / 100f;
        float d = dame / 100;
        float num = greenDamage / 100;
        GreenGauge.fillAmount -= num;
        greenDamage = 0;
        redDamage = RedGauge.fillAmount - GreenGauge.fillAmount;
    }

    public void Heal(float heal)
    {
        GreenGauge.fillAmount += heal / 100;
    }

    //public void StartUIAlpha()
    //{
    //    GreenGauge.color = new Color(GreenGauge.color.r, GreenGauge.color.g, GreenGauge.color.b, alpha);
    //    RedGauge.color = new Color(RedGauge.color.r, RedGauge.color.g, RedGauge.color.b, alpha);
    //    Waku.color = new Color(Waku.color.r, Waku.color.g, Waku.color.b, alpha);

    //    alpha += 2.5f * Time.deltaTime;

    //    if (alpha >= 1.0f)
    //    {
    //        UIStartFlag = true;
    //    }
    //}

    public void SetStartUIflag(bool fl)
    {
        UIStartFlag = fl;
    }
}
