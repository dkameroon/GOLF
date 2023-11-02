using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public static LevelSelector Instance { get; private set; }
    public GameObject buttonPrefab;
    private string activeLevelName;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        int levelCount = SceneManager.sceneCountInBuildSettings;

        for (int i = 0; i < levelCount; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string levelName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            if (levelName.StartsWith("Level "))
            {
                int levelNumber;
                if (int.TryParse(levelName.Substring(6), out levelNumber))
                {
                    GameObject button = Instantiate(buttonPrefab, transform);
                    Button buttonComponent = button.GetComponent<Button>();
                    TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                    
                    buttonText.text = levelNumber.ToString();
                    
                    int levelIndex = i;
                    buttonComponent.onClick.AddListener(() => LoadLevel(levelIndex, levelName));
                }
            }
        }
    }

    public void SceneUnload()
    {
        Scene[] activeScenes = new Scene[SceneManager.sceneCount];
        for (int i = 0; i < activeScenes.Length; i++)
        {
            activeScenes[i] = SceneManager.GetSceneAt(i);
        }
        
        foreach (Scene scene in activeScenes)
        {
            if (scene.name.Contains("Level"))
            {
                SceneManager.UnloadSceneAsync(scene.name);
            }
        }
    }

    public void RestartActiveLevel()
    {
        if (!string.IsNullOrEmpty(activeLevelName))
        {
            SceneUnload(); 
            GameSceneManager.Instance.LoadScene(activeLevelName, LoadSceneMode.Additive);
            Debug.Log(activeLevelName);
        }
    }
    
    void LoadLevel(int levelIndex, string levelName)
    {
        activeLevelName = levelName;
        SceneUnload();
        GameSceneManager.Instance.LoadScene(levelName, LoadSceneMode.Additive);
        
        if (SceneManager.GetSceneByName("MainMenu").isLoaded)
        {
            SceneManager.UnloadSceneAsync("MainMenu");
            MainMenuUI.Instance.gameObject.SetActive(false);
        }
        
    }
}