using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] levelDoneObjects;

    private void Start()
    {
        int levelCount = levelDoneObjects.Length;

        for (int i = 1; i <= levelCount; i++)
        {
            if (PlayerPrefs.GetInt("Level" + i, 0) == 1)
            {
                levelDoneObjects[i-1].SetActive(true);
            }
        }
        
    }
}
