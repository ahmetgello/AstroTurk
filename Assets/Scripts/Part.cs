using System.Collections;
using UnityEngine;

public class Part : MonoBehaviour
{
    private AudioSource audioSource;
    private int partCount = 0;
    [SerializeField] private AudioClip[] grabSounds;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Part"))
        {
            StartCoroutine(Grabbbit(collision.transform)); 
        }
    }

    private IEnumerator Grabbbit(Transform col)
    {
        partCount++;
        col.GetComponent<Collider2D>().enabled = false;
        col.GetComponent<Animator>().StopPlayback();
        col.GetComponent<Animator>().enabled = false;
        int randomAud = Random.Range(0, grabSounds.Length);
        audioSource.clip = grabSounds[randomAud];
        audioSource.Play();

        float t = 0;
        while (col.position != transform.position && t != 1 && col.localScale.x > 0)
        {
            t += Time.deltaTime * .5f;
            col.position = Vector2.Lerp(col.position, transform.position, t);
            col.localScale -= new Vector3(0.002f, 0.002f, 0);
            yield return null;
        }
        Destroy(col.gameObject);
    }
}
