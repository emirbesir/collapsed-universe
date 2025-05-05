using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    [Header("Sound Settings")]
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TextMeshProUGUI volumeValue;
    [SerializeField] private AudioMixer audioMixer;

    [Header("Main Menu")]
    [SerializeField] private GameObject mainMenuPanel;

    public GameObject settingsPanel;
    public TMP_Dropdown resolutionDropdown;
    private bool isFullScreen = true;
    private float currentVolume = 1f;

    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;

    void Start()
    {
        // Çözünürlükleri listele
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();

        resolutionDropdown.ClearOptions();

        int currentResolutionIndex = 0;
        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;

            bool isDuplicate = false;
            foreach (Resolution existingRes in filteredResolutions)
            {
                if (existingRes.width == resolutions[i].width && existingRes.height == resolutions[i].height)
                {
                    isDuplicate = true;
                    break;
                }
            }

            if (!isDuplicate)
            {
                options.Add(option);
                filteredResolutions.Add(resolutions[i]);

                // Check if this is the current resolution
                if (resolutions[i].width == Screen.currentResolution.width &&
                    resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = filteredResolutions.Count - 1;
                }
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        resolutionDropdown.onValueChanged.AddListener(SetResolution);
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetResolution(int index)
    {
        Resolution res = resolutions[index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        currentVolume = volume;
        volumeValue.text = (volume * 100).ToString("0") + "%";

        // Convert to logarithmic scale for audio mixer
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        isFullScreen = !isFullScreen;
        Screen.fullScreen = isFullScreen;
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
    public void OpenMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
