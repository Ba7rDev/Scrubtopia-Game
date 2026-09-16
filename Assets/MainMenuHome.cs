using UnityEngine;
using UnityEngine.UI;

public class MainMenuHome : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject mainButtonsGroup;
    public GameObject settingsPanel;
    public GameObject creditsPanel;

    [Header("Settings Controls")]
    public Slider volumeSlider;
    public Slider brightnessSlider;
    public Image brightnessOverlay; // Black UI Image covering the screen

    private void Start()
    {
        // Load saved settings or defaults
        if (volumeSlider != null)
        {
            volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
            SetVolume(volumeSlider.value);
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }

        if (brightnessSlider != null)
        {
            brightnessSlider.value = PlayerPrefs.GetFloat("Brightness", 1f);
            SetBrightness(brightnessSlider.value);
            brightnessSlider.onValueChanged.AddListener(SetBrightness);
        }
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("Volume", value);
    }

    public void SetBrightness(float value)
    {
        if (brightnessOverlay != null)
        {
            // Lower slider value = darker screen (higher opacity)
            Color color = brightnessOverlay.color;
            color.a = 1f - value;
            brightnessOverlay.color = color;
        }
        PlayerPrefs.SetFloat("Brightness", value);
    }

    // --- Panel Navigation ---
    public void OpenSettings()
    {
        if (mainButtonsGroup != null) mainButtonsGroup.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(true);
        if (creditsPanel != null) creditsPanel.SetActive(false);
    }

    public void OpenCredits()
    {
        if (mainButtonsGroup != null) mainButtonsGroup.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(true);
    }


    public void BackToSettings()
    {
        if (creditsPanel != null) creditsPanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(false);
        if (mainButtonsGroup != null) mainButtonsGroup.SetActive(true);
    }

    public void PlayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}