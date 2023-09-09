using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CountDownTimer : MonoBehaviour
{
    public float fullTime;

    public float minutes;
    public float seconds;

    public TextMeshProUGUI minuteTexts;
    public TextMeshProUGUI secondTexts;

    void Update()
    {
        CountdownTimer();
    }

    void CountdownTimer()
    {
        if (fullTime > 0)
        {
            fullTime -= Time.deltaTime;
            minutes = Mathf.FloorToInt(fullTime / 60);
            seconds = Mathf.FloorToInt(fullTime % 60);

            minuteTexts.text = minutes.ToString();
            secondTexts.text = seconds.ToString();
        }
    }
}
