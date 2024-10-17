using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }   
        else
        {
            Destroy(gameObject);
        }
    }
}
