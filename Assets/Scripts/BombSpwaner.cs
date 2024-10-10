using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BombSpwaner : MonoBehaviour
{
    [SerializeField] private GameObject bombToSpawn;
    [SerializeField] private TextMeshProUGUI bombCountDownText;

    public void SpawnBomb()
    {
        Instantiate(bombToSpawn, gameObject.transform.position, Quaternion.identity, gameObject.transform);
    }

    public IEnumerator BombCountDown()
    {
        bombCountDownText.text = "3";
        yield return new WaitForSeconds(1f);
        bombCountDownText.text = "2";
        yield return new WaitForSeconds(1f);
        bombCountDownText.text = "1";
        yield return new WaitForSeconds(1f);
        bombCountDownText.text = "";
        SpawnBomb();
    }


}
