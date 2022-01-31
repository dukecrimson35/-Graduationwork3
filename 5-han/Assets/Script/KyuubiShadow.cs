using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KyuubiShadow : MonoBehaviour
{
    SpriteRenderer sprite;
    bool dirR;//方向が右か左か(trueで右)
    public GameObject gazou;

    // Start is called before the first frame update
    void Start()
    {
        if (gazou != null)
        {
            sprite = gazou.GetComponent<SpriteRenderer>();
        }
        else
        {
            sprite = GetComponent<SpriteRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(dirR == true)
        {
            transform.Translate(Vector3.right * 0.025f);
        }
        else
        {
            transform.Translate(Vector3.left * 0.025f);
        }

        if(sprite.color.a > 0)
        {
            sprite.color -= new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.025f);
        }

        if (sprite.color.a <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetRight()
    {
        dirR = true;
    }

    public void SetLeft()
    {
        dirR = false;
    }
}
