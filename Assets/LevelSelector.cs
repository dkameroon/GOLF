using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public static LevelSelector Instance { get; private set; }
    
    public static int selectedLevel;
    public int level;
    public TextMeshProUGUI levelText;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        selectedLevel = level;
        levelText.text = level.ToString();
    }
    public void OpenScene()
    {
        SceneManager.LoadScene("Level " + level.ToString(),LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        MainMenuUI.Instance.gameObject.SetActive(false);
    }
}
