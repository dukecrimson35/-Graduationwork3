using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Textmove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector3(0, 300, 0));
        StartCoroutine(DestroyObject());
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(DestroyObject());
    }

    private IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(0.6f);
        Destroy(this.gameObject);
    }
}
