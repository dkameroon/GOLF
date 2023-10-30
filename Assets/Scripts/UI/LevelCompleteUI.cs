using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCompleteUI : MonoBehaviour
{
    [SerializeField] private Button retryButton;
    [SerializeField] private Button selectLevelsButton;
    [SerializeField] private Button selectLevelsExitButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private GameObject selectLevels;

    private void Awake()
    {
        selectLevels.gameObject.SetActive(false);
        retryButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
        selectLevelsButton.onClick.AddListener(() =>
        {
            selectLevels.gameObject.SetActive(true);
        });
        selectLevelsExitButton.onClick.AddListener(() =>
        {
            selectLevels.gameObject.SetActive(false);
        });
        menuButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MainMenu");
        });
    }
}
