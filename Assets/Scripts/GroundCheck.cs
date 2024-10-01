using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool isGrounded = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "ground")
        {
            isGrounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.tag == "ground")
        {
            isGrounded = false;
        }
    }
}
