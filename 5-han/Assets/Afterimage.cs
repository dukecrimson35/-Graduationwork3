using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afterimage : MonoBehaviour
{
    float life = 1.0f;
    SpriteRenderer[] sp;
    public GameObject after1;
    public GameObject after2;

    // Start is called before the first frame update
    void Start()
    {

        sp = GetComponentsInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDead();
        for (int a = 0; sp.Length > a; a++)
        {
            Color color = sp[a].color;
            color = new Color(color.r, color.g, color.b, life);
            sp[a].color = color;
        }
    }
    public void SetPositition(Vector3 pos1, Vector3 pos2)
    {
        after1.transform.position += pos1;
        after2.transform.position += pos2;

    }
    void CheckDead()
    {
        life -= Time.deltaTime;
        if (life < 0)
        {
            Destroy(gameObject);
        }
    }
    public void Left()
    {
        if (sp == null)
        {
            sp = GetComponentsInChildren<SpriteRenderer>();
        } 
        for (int a = 0; sp.Length > a; a++)
        {
            sp[a].flipX = true;
        }
    }
}
