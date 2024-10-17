using UnityEngine;
using TMPro;
using System.Collections;

public class TimerCode : MonoBehaviour
{
    private TextMeshProUGUI timerText;
    [SerializeField] float currentTimer = 0;
    float time = 30;
    [SerializeField] private GameObject losePanel;

    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        currentTimer = time;
        StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    {
        while (currentTimer > 0)
        {
            currentTimer -= 1;
            timerText.text = currentTimer.ToString();
            yield return new WaitForSeconds(1);
        }
        Time.timeScale = 0;
        gameObject.SetActive(false);
        losePanel.SetActive(true);
    }
}
