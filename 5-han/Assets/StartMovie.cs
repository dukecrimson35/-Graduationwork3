using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMovie : MonoBehaviour
{
    Animator anim;
    bool endMovie;
    GameObject parent;
    SpriteRenderer parentSp;
    SpriteRenderer sp;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        endMovie = false;
        parent = transform.parent.gameObject;
        parentSp = parent.GetComponent<SpriteRenderer>();
        sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vec = (parent.transform.position - transform.position).normalized;
        transform.position += vec * Time.deltaTime * 7;
        anim.SetBool("Walk", true);
        if ((parent.transform.position - transform.position).magnitude > 0.1f)
        {
            endMovie = false;
           // parent.transform.rotation = Quaternion.Euler(0, 180, 0);
            parentSp.enabled = false;
            sp.enabled = true;
        }
        else
        {
            parentSp.enabled = true;
            sp.enabled = false;
            endMovie = true;
        }
    }
    public bool GetMovieEnd()
    {
        return endMovie;
    }
}
