using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public Slider volumeSlider;
    public TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;

    void Start()
    {
        // Çözünürlükleri listele
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        int currentResolutionIndex = 0;
        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        resolutionDropdown.onValueChanged.AddListener(SetResolution);

        // Ses yükle
        float savedVolume = PlayerPrefs.GetFloat("volume", 1f);
        volumeSlider.value = savedVolume;
        AudioListener.volume = savedVolume;
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetResolution(int index)
    {
        Resolution res = resolutions[index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("volume", volume);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
