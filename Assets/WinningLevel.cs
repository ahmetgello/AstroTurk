using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinningLevel : MonoBehaviour
{
   void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "Astronaut")
        {
            SceneManager.LoadScene("main");
        }
    }
}
