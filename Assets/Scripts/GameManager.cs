using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event EventHandler OnGameOver;
    [SerializeField] private int count;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1f;
        BallControl.Instance.OnShooting += BallControl_OnShooting;
        SettingsUI.Instance.SliderMusic.onValueChanged.AddListener(HandleMusicVolumeChanged);
        SettingsUI.Instance.SliderSounds.onValueChanged.AddListener(HandleSoundsVolumeChanged);
        GameUI.Instance.countOfShots.text = count.ToString();
        SettingsUI.Instance.gameObject.SetActive(false);
        PauseUI.Instance.gameObject.SetActive(false);
        LevelCompleteUI.Instance.gameObject.SetActive(false);
        GameOverUI.Instance.gameObject.SetActive(false);
        GameOverUI.Instance.selectGameOverLevels.gameObject.SetActive(false);
        LevelCompleteUI.Instance.selectLevelCompleteLevels.gameObject.SetActive(false);
        GameUI.Instance.pauseGameButton.onClick.AddListener(() =>
        {
            Time.timeScale = 0f;
            PauseUI.Instance.gameObject.SetActive(true);
        });
        Debug.Log(PauseUI.Instance);
        PauseUI.Instance.continueButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            PauseUI.Instance.gameObject.SetActive(false);
        });
        PauseUI.Instance.retryPauseButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
        PauseUI.Instance.settingsButton.onClick.AddListener(() =>
        {
            float musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            float soundsVolume = PlayerPrefs.GetFloat("SoundsVolume");

            SettingsUI.Instance.SliderMusic.value = musicVolume;
            SettingsUI.Instance.SliderSounds.value = soundsVolume;
            SettingsUI.Instance.gameObject.SetActive(true);
        });
        PauseUI.Instance.menuPauseButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MainMenu");
        });
        SettingsUI.Instance.closeSettingsButton.onClick.AddListener(() =>
        {
            SettingsUI.Instance.gameObject.SetActive(false);
            PlayerPrefs.Save();
        });
        LevelCompleteUI.Instance.retryLevelCompleteButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
        LevelCompleteUI.Instance.selectLevelsLevelCompleteButton.onClick.AddListener(() =>
        {
            LevelCompleteUI.Instance.selectLevelCompleteLevels.gameObject.SetActive(true);
        });
        LevelCompleteUI.Instance.selectLevelsLevelCompleteExitButton.onClick.AddListener(() =>
        {
            LevelCompleteUI.Instance.selectLevelCompleteLevels.gameObject.SetActive(false);
        });
        LevelCompleteUI.Instance.menuLevelCompleteButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MainMenu");
        });
        GameOverUI.Instance.retryGameOverButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
        GameOverUI.Instance.selectLevelsGameOverButton.onClick.AddListener(() =>
        {
            GameOverUI.Instance.selectGameOverLevels.gameObject.SetActive(true);
        });
        GameOverUI.Instance.selectLevelsGameOverExitButton.onClick.AddListener(() =>
        {
            GameOverUI.Instance.selectGameOverLevels.gameObject.SetActive(false);
        });
        GameOverUI.Instance.menuGameOverButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MainMenu");
        });
    }

    private void BallControl_OnShooting(object sender, EventArgs e)
    {
        count--;
        GameUI.Instance.countOfShots.text = count.ToString();
        if (count <= 0)
        {
            GameOverUI.Instance.gameObject.SetActive(true);
            Time.timeScale = 0f;
            SoundManager.Instance.PlayDefeatSound(Camera.main.transform.position,0.5f);
        }
        
    }

    public int Count()
    {
        return count;
    }
    
    private void OnDestroy()
    {
        BallControl.Instance.OnShooting -= BallControl_OnShooting;
    }
    
    private void HandleMusicVolumeChanged(float value)
    {
        SoundManager.Instance.volumeMusic.volume = value;
        PlayerPrefs.SetFloat("MusicVolume", value);
        PlayerPrefs.Save();
        SettingsUI.Instance.musicProcents.text = (value * 100).ToString("0") + "%";
    }
    
    private void HandleSoundsVolumeChanged(float value)
    {
        SoundManager.Instance.volumeSounds = value;
        PlayerPrefs.SetFloat("SoundsVolume", value);
        PlayerPrefs.Save();
        SettingsUI.Instance.soundsProcents.text = (value * 100).ToString("0") + "%";
    }
}
