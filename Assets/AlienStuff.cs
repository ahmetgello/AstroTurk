using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienStuff : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "explosion")
            /*anim.Play("alien_lose");*/
            Destroy(gameObject,1.5f);
    }
}
