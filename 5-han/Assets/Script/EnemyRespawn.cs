using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    public GameObject StopEnemy;//止まってる敵
    public GameObject FlyingEnemy;//飛ぶ敵
    public GameObject coinMidlle;//ドロップするコイン１
    public GameObject coinSmall;//ドロップするコイン２
    public GameObject stopenemyDeath;
    public GameObject flyenemyDeath;

    GameObject[] enemys;
    List<string> enemyNames = new List<string>();//敵の名前判別用
    List<Vector3> positions = new List<Vector3>();
    List<int> delays = new List<int>();
    List<int> delays2 = new List<int>();
    List<float> counts = new List<float>();
    List<float> coincounts = new List<float>();//コインドロップ用のカウント

    // Start is called before the first frame update
    void Start()
    {
        enemys = GameObject.FindGameObjectsWithTag("Enemy");//敵をすべて格納
        int i = 0;

        foreach(var x in enemys)
        {
            positions.Add(x.transform.position);
            enemyNames.Add(x.name);
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
            if (enemys[i] == null)//敵が死んでいたとき
            {
                if (delays.Contains(i) == false)
                {
                    delays.Add(i);
                    delays2.Add(i);
                    //敵の死亡アニメーション生成
                    if (enemyNames[i].Contains("boxEnemy")) 
                    {
                        DeadAnimation(positions[i], stopenemyDeath); 
                    }
                    if (enemyNames[i].Contains("ShotEnemy"))
                    {
                        DeadAnimation(positions[i], flyenemyDeath);
                    }
                }
                if(delays.Contains(i))
                {
                    counts[i] += Time.deltaTime;
                    if(counts[i] >= 3)//一定時間後
                    {
                        counts[i] = 0;
                        //敵を再生成
                        if (enemyNames[i].Contains("boxEnemy"))
                        {
                            enemys[i] = Instantiate(StopEnemy, positions[i], new Quaternion());
                        }
                        if(enemyNames[i].Contains("ShotEnemy"))
                        {
                            enemys[i] = Instantiate(FlyingEnemy, positions[i], new Quaternion());
                        }
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

    void DeadAnimation(Vector3 position,GameObject animation)//死亡アニメーション
    {
        Instantiate(animation, position, new Quaternion());
    }
}
