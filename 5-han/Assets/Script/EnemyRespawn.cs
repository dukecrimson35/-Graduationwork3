using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    public GameObject StopEnemy;//止まってる敵
    public GameObject FlyingEnemy;//飛ぶ敵
    public GameObject BirdEnemy;//鳥の敵
    public GameObject GoblinEnemy;//鬼ザコ敵
    public GameObject DogEnemy;//犬の敵
    public GameObject coinMidlle;//ドロップするコイン１
    public GameObject coinSmall;//ドロップするコイン２
    public GameObject stopenemyDeath;
    public GameObject flyenemyDeath;
    public GameObject Enemy_hole;//敵出現予兆

    GameObject[] enemys;
    List<string> enemyNames = new List<string>();//敵の名前判別用
    List<Vector3> positions1 = new List<Vector3>();//敵の初期地点(リスポーン用
    List<Vector3> positions2 = new List<Vector3>();//敵の現在位置(アニメーション用
    List<int> delays = new List<int>();
    List<int> delays2 = new List<int>();
    List<float> counts = new List<float>();
    List<float> coincounts = new List<float>();//コインドロップ用のカウント
    List<GameObject> holes = new List<GameObject>();//出現予兆管理用リスト

    // Start is called before the first frame update
    void Start()
    {
        enemys = GameObject.FindGameObjectsWithTag("Enemy");//敵をすべて格納
        int i = 0;

        foreach(var x in enemys)
        {
            positions1.Add(x.transform.position);
            positions2.Add(x.transform.position);
            enemyNames.Add(x.name);
            i++;
        }
        for(int ii = 0;ii <enemys.Length;ii++)
        {
            counts.Add(0);
            coincounts.Add(0);
            holes.Add(null);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < enemys.Length; i++)
        {
            if (enemys[i] != null)
            {
                positions2[i] = enemys[i].transform.position;
            }
        }

        #region 敵の再生成
        for (int i = 0; i < enemys.Length;i++)
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
                        DeadAnimation(positions2[i], stopenemyDeath); 
                    }
                    if (enemyNames[i].Contains("ShotEnemy"))
                    {
                        DeadAnimation(positions2[i], flyenemyDeath);
                    }
                }
                if(delays.Contains(i))
                {
                    counts[i] += Time.deltaTime;
                    if(counts[i] >= 5)//一定時間後
                    {
                        counts[i] = 0;
                        //敵を再生成
                        if (enemyNames[i].Contains("boxEnemy"))
                        {
                            enemys[i] = Instantiate(StopEnemy, positions1[i], new Quaternion());
                        }
                        if(enemyNames[i].Contains("ShotEnemy"))
                        {
                            enemys[i] = Instantiate(FlyingEnemy, positions1[i], new Quaternion());
                        }
                        if (enemyNames[i].Contains("BirdEnemy"))
                        {
                            enemys[i] = Instantiate(BirdEnemy, positions1[i], new Quaternion());
                        }
                        if (enemyNames[i].Contains("GoblinEnemy"))
                        {
                            enemys[i] = Instantiate(GoblinEnemy, positions1[i], new Quaternion());
                        }
                        if (enemyNames[i].Contains("BeastEnemy"))
                        {
                            enemys[i] = Instantiate(DogEnemy, positions1[i], new Quaternion());
                        }
                        delays.Remove(i);
                    }
                    if(counts[i] >= 3)
                    {
                        if (holes[i] == null)
                        {
                            //敵出現予兆の生成
                            holes[i] = Instantiate(Enemy_hole, positions1[i], new Quaternion());
                        }
                    }
                }
                if(delays2.Contains(i))
                {
                    coincounts[i] += Time.deltaTime;
                    if (coincounts[i] >= 0.5f)
                    {
                        coincounts[i] = 0;
                        //コインドロップ
                        CoinDrop(positions2[i]);
                        delays2.Remove(i);
                    }
                }
            }
        }
        #endregion

        for (int i = 0; i< positions2.Count;i++)
        {
            //Debug.Log(positions2[i]);
        }
    }

    void CoinDrop(Vector3 position)//お金ドロップ
    {
        Vector3 pos = position;
        Instantiate(coinSmall, pos, new Quaternion());
        //pos.y += 0.05f;
        //Instantiate(coinSmall, pos, new Quaternion());
        //pos.y -= 0.05f;
        //Instantiate(coinMidlle, pos, new Quaternion());
        //pos.x += 0.05f;
        //Instantiate(coinMidlle, pos, new Quaternion());
    }

    void DeadAnimation(Vector3 position,GameObject animation)//死亡アニメーション
    {
        Instantiate(animation, position, new Quaternion());
    }
}
