using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    public static PauseUI Instance { get; private set; }
    
    [SerializeField] private Button continueButton;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private GameObject settingsUI;
    [SerializeField] private Button menuButton;

    private void Awake()
    {
        Instance = this;
        continueButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            UIManager.Instance.PauseUI.gameObject.SetActive(false);
        });
        retryButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
        settingsButton.onClick.AddListener(() =>
        {
            settingsUI.gameObject.SetActive(true);
        });
        menuButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MainMenu");
        });
    }

    private void Start()
    {
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
