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
    private bool isLoaded = false;

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

        if (isLoaded)
        {
            foreach (Scene scene in activeScenes)
            {
                if (scene.name.Contains("Level"))
                {
                    SceneManager.UnloadSceneAsync(scene.name);
                }
            }

            SceneManager.GetActiveScene();
            isLoaded = false;
        }
    }

    public void RestartActiveLevel()
    {
        if (isLoaded)
        {
            if (!string.IsNullOrEmpty(activeLevelName))
            {
                SceneUnload();
                GameSceneManager.Instance.LoadScene(activeLevelName, LoadSceneMode.Additive);
                StarsHandler.Instance.RestartAnimations();
                /*SceneManager.SetActiveScene(SceneManager.GetSceneByName(activeLevelName));*/
                Debug.Log(activeLevelName);
                isLoaded = false;
            }
        }

        isLoaded = true;

    }

    public void LoadMenu()
    {
        SceneUnload();
        SceneManager.LoadSceneAsync(PlayerPrefsNames.MAIN_MENU_SCENE, LoadSceneMode.Additive);
        MainMenuUI.Instance.gameObject.SetActive(true);
        GameUI.Instance.gameObject.SetActive(false);
        MainMenuUI.Instance.selectLevelsMenu.SetActive(false);
    }
    
    void LoadLevel(int levelIndex, string levelName)
    {
        activeLevelName = levelName;
        SceneManager.UnloadSceneAsync(PlayerPrefsNames.MAIN_MENU_SCENE);
        MainMenuUI.Instance.gameObject.SetActive(levelName.Contains(PlayerPrefsNames.MAIN_MENU_SCENE));
        if (!isLoaded)
        {
            SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
            /*SceneManager.SetActiveScene(SceneManager.GetSceneByName(levelName));*/
            isLoaded = true;
            GameUI.Instance.gameObject.SetActive(true);
        }
    }
}