using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class timeChange : MonoBehaviour
{
    public TMP_Text timeDisplay;
    public Button forwardButton;
    public Button backButton;

    public int currentMinutes = 60;

    private void Start()
    {
        UpdateTimeDisplay();
        forwardButton.onClick.AddListener(IncreaseTime);
        backButton.onClick.AddListener(DecreaseTime);
    }

    public void IncreaseTime()
    {
        currentMinutes += 60; 
        if (currentMinutes >= 1440) 
        {
            currentMinutes = 0;
        }
        UpdateTimeDisplay();
    }

    public void DecreaseTime()
    {
        currentMinutes -= 60; // Decrease by 1 minute (60 seconds)
        if (currentMinutes < 0)
        {
            currentMinutes = 1439; // Set to 23:59
        }
        UpdateTimeDisplay();
    }

    private void UpdateTimeDisplay()
    {
        int hours = currentMinutes / 60;
        int minutes = currentMinutes % 60;

        string formattedTime = string.Format("{0:D2}:{1:D2}", hours, minutes);
        timeDisplay.text = formattedTime;
        timeVar.setTime = currentMinutes;
        currentMinutes = timeVar.setTime;
    }
}
