using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfBullet : MonoBehaviour
{
    public GameObject mother;//親
    WasShoot wasShoot;//親のスクリプト
    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        wasShoot = mother.GetComponent<WasShoot>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(wasShoot.GetCut())//斬られた時
        {
            //α値を消しながら、ローカルXをマイナスする。
            sprite.color -= new Color(0,0,0,0.01f);
            transform.Translate(Vector3.left * 0.005f);
            if(sprite.color.a <= 0)
            {
                Destroy(this.gameObject);
                Destroy(mother);
            }
        }
    }
}
