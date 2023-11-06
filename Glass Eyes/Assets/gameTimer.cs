using UnityEngine;
using TMPro;

public class gameTimer : MonoBehaviour
{
    private float currentTime;
    private bool isTimerRunning = false;

    public TMP_Text timerText;

    private void Start()
    {
        LoadTime();
        UpdateTimerText();
        StartTimer();
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                UpdateTimerText();
            }
            else
            {
                isTimerRunning = false;
                Debug.Log("Time's up!");
                ServerConnect.instance.endMatch();
            }
        }
    }

    public void StartTimer()
    {
        isTimerRunning = true;
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime - (minutes * 60));

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void SaveTime()
    {
        PlayerPrefs.SetFloat("CurrentTime", currentTime);
    }

    private void LoadTime()
    {
        currentTime = timeVar.setTime;
    }
}
