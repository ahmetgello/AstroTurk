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
            print(PlayerPrefs.GetInt("Level" + SceneManager.GetActiveScene().buildIndex, 0));
            PlayerPrefs.SetInt("Level" + SceneManager.GetActiveScene().buildIndex, 1);
            SceneManager.LoadScene("main");
        }
    }
}
