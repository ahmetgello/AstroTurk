using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienStuff : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer sr;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    private void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(Walk());
        anim.Play("AlienMove");
    }

    IEnumerator Walk()
    {

        float t = 0;
        sr.flipX = false;
        while (t < 0.6f)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(pointA.position, pointB.position, t / 0.6f);
            yield return new WaitForSeconds(0.01f);
        }
        sr.flipX = true;
        t = 0;
        while (t < 0.6f)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(pointB.position, pointA.position, t / 0.6f);
            yield return new WaitForSeconds(0.01f);
        }
        StartCoroutine(Walk());

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "explosion")
        {
            StartCoroutine(Lose());
        }
    }

    IEnumerator Lose()
    {
        anim.StopPlayback();
        yield return new WaitForSeconds(0.7f);
        anim.Play("alien_lose");
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
