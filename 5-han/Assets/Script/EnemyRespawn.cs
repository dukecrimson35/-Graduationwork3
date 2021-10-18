using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    public GameObject StopEnemy;
    public GameObject coinMidlle;//ドロップするコイン１
    public GameObject coinSmall;//ドロップするコイン２
    public GameObject DeathAnimation;

    GameObject[] enemys;
    List<Vector3> positions = new List<Vector3>();
    List<int> delays = new List<int>();
    List<int> delays2 = new List<int>();
    List<float> counts = new List<float>();
    List<float> coincounts = new List<float>();//コインドロップ用のカウント

    // Start is called before the first frame update
    void Start()
    {
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        int i = 0;

        foreach(var x in enemys)
        {
            positions.Add(x.transform.position);
            i++;
        }
        for(int ii = 0;ii <enemys.Length;ii++)
        {
            counts.Add(0);
            coincounts.Add(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        #region 敵の再生成
        for(int i = 0; i < enemys.Length;i++)
        {
            if (enemys[i] == null)
            {
                if (delays.Contains(i) == false)
                {
                    delays.Add(i);
                    delays2.Add(i);
                    //敵の死亡アニメーション
                    DeadAnimation(positions[i]);
                }
                if(delays.Contains(i))
                {
                    counts[i] += Time.deltaTime;
                    if(counts[i] >= 3)
                    {
                        counts[i] = 0;
                        enemys[i] = Instantiate(StopEnemy, positions[i], new Quaternion());
                        delays.Remove(i);
                    }
                }
                if(delays2.Contains(i))
                {
                    coincounts[i] += Time.deltaTime;
                    if (coincounts[i] >= 0.5f)
                    {
                        coincounts[i] = 0;
                        //コインドロップ
                        CoinDrop(positions[i]);
                        delays2.Remove(i);
                    }
                }
            }
        }
        #endregion

        for (int i = 0; i< positions.Count;i++)
        {
            Debug.Log(positions[i]);
        }
    }

    void CoinDrop(Vector3 position)//お金ドロップ
    {

        Instantiate(coinSmall, position, new Quaternion());
        Instantiate(coinSmall, position, new Quaternion());
        Instantiate(coinMidlle, position, new Quaternion());
        Instantiate(coinMidlle, position, new Quaternion());
    }

    void DeadAnimation(Vector3 position)//死亡アニメーション
    {
        Instantiate(DeathAnimation, position, new Quaternion());
    }
}
