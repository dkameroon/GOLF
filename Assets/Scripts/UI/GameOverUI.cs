using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : BaseUI
{
    public static GameOverUI Instance { get; private set; }
    
    [SerializeField] public Button retryGameOverButton;
    [SerializeField] public Button selectLevelsGameOverButton;
    [SerializeField] public Button selectLevelsGameOverExitButton;
    [SerializeField] public Button menuGameOverButton;
    [SerializeField] public GameObject selectGameOverLevels;

    private void Awake()
    {
        Instance = this;
    }
}
