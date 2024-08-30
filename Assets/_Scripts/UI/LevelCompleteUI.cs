using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCompleteUI : BaseUI
{
    public static LevelCompleteUI Instance { get; private set; }
    [SerializeField] public Button retryLevelCompleteButton;
    [SerializeField] public Button selectLevelsLevelCompleteButton;
    [SerializeField] public Button selectLevelsLevelCompleteExitButton;
    [SerializeField] public Button menuLevelCompleteButton;
    [SerializeField] public Button nextLvlLevelCompleteButton;
    [SerializeField] public GameObject selectLevelCompleteLevels;
    [SerializeField] public GameObject starsHolderLevelComplete;

    private void Awake()
    {
        Instance = this;
        
    }
}
