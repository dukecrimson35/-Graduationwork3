using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SyouninAni : MonoBehaviour
{
    public bool hitFlag = false;
    public GameObject obj;
    private SpriteRenderer spriteRenderer;

    public Sprite sprite;
    public Sprite hitSprite;
    public Sprite hitSprite2;

    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = obj.GetComponent<SpriteRenderer>();
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(hitFlag)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        }
        else
        {
            //text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        }
    }
    IEnumerator HitAni()
    {
        hitFlag = true;
        spriteRenderer.sprite = hitSprite;
        yield return new WaitForSeconds(1);
        spriteRenderer.sprite = hitSprite2;
        StartCoroutine(TextAlphaDown());
        yield return new WaitForSeconds(0.3f);
        hitFlag = false;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.sprite = sprite;
    }

    IEnumerator TextAlphaDown()
    {
        for(int i = 0; i< 10;i++)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - 0.1f);
            yield return new WaitForSeconds(0.05f);
        }

        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SenkuGiri")
        {
            StartCoroutine(HitAni());
        }
    }
}
