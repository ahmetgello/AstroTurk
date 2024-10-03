using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource audioSource;

    IEnumerator Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        Destroy(gameObject, 2.8f);
        anim.Play("bomb_idle");
        yield return new WaitForSeconds(1f);
        audioSource.Play();
        transform.tag = "explosion";
        anim.Play("Explode");
    }

}
