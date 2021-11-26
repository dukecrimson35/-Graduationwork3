using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHole : MonoBehaviour
{
    SpriteRenderer sprite;
    bool fadein;//フェードインかアウトか

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        fadein = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadein == true)
        {
            sprite.color += new Color(0, 0, 0, Time.deltaTime * 0.5f);
        }
        if (fadein == false)
        {
            sprite.color -= new Color(0, 0, 0, Time.deltaTime *5f);
        }
        if (fadein == true && sprite.color.a >= 1)
        {
            fadein = false;
        }
        if(fadein == false && sprite.color.a <= 0)
        {
            Destroy(this.gameObject);
            Destroy(this);
        }

        transform.Rotate(new Vector3(0,0,1));
        transform.localScale = new Vector3(transform.localScale.x + Time.deltaTime,transform.localScale.y + Time.deltaTime,transform.localScale.z);
    }
}
