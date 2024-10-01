using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpwaner : MonoBehaviour
{
    [SerializeField] private GameObject bombToSpawn;

    public void SpawnBomb()
    {
        Instantiate(bombToSpawn, gameObject.transform.position, Quaternion.identity, gameObject.transform);
    }
}
