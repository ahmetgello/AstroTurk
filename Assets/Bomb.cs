using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private Animator anim;

    IEnumerator Start()
    {
        Destroy(gameObject, 3f);
        anim = GetComponent<Animator>();
        anim.Play("bomb_idle");
        yield return new WaitForSeconds(1f);
        transform.tag = "explosion";
        anim.Play("Explode");
    }

}
