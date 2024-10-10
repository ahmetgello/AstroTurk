using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private ParticleSystem explosionEffect;
    [SerializeField] private GameObject explosingEffectOnGround;

    IEnumerator Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        anim.Play("bomb_idle");
        yield return new WaitForSeconds(1f);
        try
        {
//            Handheld.Vibrate();
        }
        catch (System.Exception)
        {
            // ignored
        }
        Object.FindAnyObjectByType<CinemachineVirtualCamera>().GetComponent<Animator>().Play("CameraShake");
        explosionEffect.Play(); 
        audioSource.Play();
        anim.Play("Explode");
        Instantiate(explosingEffectOnGround, new Vector3(transform.position.x, transform.position.y - 1, 0), Quaternion.identity);
        gameObject.tag = "explosion";
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

}
