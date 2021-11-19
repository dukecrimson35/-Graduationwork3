using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kyuubiBullet : MonoBehaviour
{
    public GameObject bossEnemy;
    Vector3 pos;
    public bool RMove;
    public bool LMove;
    public float deadSecond;
    // Start is called before the first frame update
    void Start()
    {
        pos = Vector3.zero;
        bossEnemy = GameObject.FindGameObjectWithTag("BossEnemy");
        RMove = bossEnemy.GetComponent<KyuubiBoss>().GetRight();
        LMove = bossEnemy.GetComponent<KyuubiBoss>().GetLeft();
        deadSecond = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale <= 0) return;
        if (LMove && !RMove)
        {
            pos.x -= 0.1f;
        }
        if (!LMove && RMove)
        {
            pos.x += 0.1f;
        }
        deadSecond += Time.deltaTime;
        if (deadSecond >= 2)
        {
            Destroy(gameObject);
        }
        transform.position += pos * Time.deltaTime * 100;
        pos = Vector3.zero;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerControl p = collision.gameObject.GetComponent<PlayerControl>();
            p.Damage(10);
            //p.KnockBack(gameObject);
        }
    }
}
