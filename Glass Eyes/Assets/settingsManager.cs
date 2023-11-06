using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class settingsManager : MonoBehaviour
{
    public Slider volumeSlider;
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown fullscreenDropdown; 
    public Button applyButton;

    private Resolution[] resolutions;
    private int currentResolutionIndex;
    private bool isFullscreen;

    public Slider sensitivitySlider;
    private float sensitivityValue = 200f;

    public TMP_Text senseText;
    public static settingsManager instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        var resolutionOptions = new List<string>();
        currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            resolutionOptions.Add($"{resolutions[i].width}x{resolutions[i].height}");

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        volumeSlider.value = 100;
        volumeSlider.onValueChanged.AddListener(ChangeVolume);

        fullscreenDropdown.ClearOptions();
        fullscreenDropdown.AddOptions(new List<string> { "Fullscreen", "Windowed" });
        fullscreenDropdown.value = Screen.fullScreen ? 0 : 1;
        fullscreenDropdown.RefreshShownValue();

        fullscreenDropdown.onValueChanged.AddListener(ToggleFullscreen);

        applyButton.onClick.AddListener(ApplySettings);

        sensitivitySlider.minValue = 0f;
        sensitivitySlider.maxValue = 400f;
        sensitivitySlider.value = sensitivityValue;
        sensitivitySlider.onValueChanged.AddListener(ChangeSensitivity);

        UpdateSensitivityText();
    }

    public void ChangeVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void SetResolution(int resolutionIndex)
    {
        if (resolutionIndex >= 0 && resolutionIndex < resolutions.Length)
        {
            currentResolutionIndex = resolutionIndex;
        }
    }

    public void ApplySettings()
    {
        ChangeVolume(volumeSlider.value);
        SetResolution(resolutionDropdown.value);

        sensitivityValue = sensitivitySlider.value;
        UpdateSensitivityText();

        timeVar.playerSens = sensitivityValue;

        if (PhotonNetwork.IsConnectedAndReady)
        {
            if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("sensitivity"))
            {
                ExitGames.Client.Photon.Hashtable playerProps = new ExitGames.Client.Photon.Hashtable();
                playerProps["sensitivity"] = sensitivityValue;
                PhotonNetwork.LocalPlayer.SetCustomProperties(playerProps);
            }
        }

        if (currentResolutionIndex >= 0 && currentResolutionIndex < resolutions.Length)
        {
            Resolution resolution = resolutions[currentResolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, isFullscreen);
        }
    }


    public void ToggleFullscreen(int index)
    {
        isFullscreen = index == 0;
        Screen.fullScreen = isFullscreen;
    }

    public void ChangeSensitivity(float sensitivity)
    {
        sensitivityValue = sensitivity;
        UpdateSensitivityText();
    }

    private void UpdateSensitivityText()
    {
        senseText.text = sensitivityValue.ToString("F2");
    }
}
