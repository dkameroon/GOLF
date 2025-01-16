using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public static LevelSelector Instance { get; private set; }
    public GameObject buttonPrefab;
    private string activeLevelName;
    private bool isLoaded = false;
    [SerializeField] private TextMeshProUGUI levelText;

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
            if (scene.name.StartsWith("Level "))
            {
                SceneManager.UnloadSceneAsync(scene.name);
            }
        }
    }

    public void LoadNextLevel()
    {
        if (!string.IsNullOrEmpty(activeLevelName))
        {
            int currentLevelIndex = SceneManager.GetSceneByName(activeLevelName).buildIndex;
            int nextLevelIndex = currentLevelIndex + 1;
            LevelCompleteUI.Instance.gameObject.SetActive(false);

            if (nextLevelIndex < SceneManager.sceneCountInBuildSettings)
            {
                string nextLevelName = SceneUtility.GetScenePathByBuildIndex(nextLevelIndex);
                nextLevelName = System.IO.Path.GetFileNameWithoutExtension(nextLevelName);

                LoadLevel(nextLevelIndex, nextLevelName);
                GameManager.Instance.LoadLevelData();
                Time.timeScale = 1f;
            }
            else
            {
                Debug.Log("No more levels available.");
                LoadMenu();
            }
        }
    }

    public void RestartActiveLevel()
    {
        if (!string.IsNullOrEmpty(activeLevelName))
        {
            LoadLevel(SceneManager.GetSceneByName(activeLevelName).buildIndex, activeLevelName);
            GameOverUI.Instance.gameObject.SetActive(false);
            LevelCompleteUI.Instance.gameObject.SetActive(false);
            PauseUI.Instance.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void LoadMenu()
    {
        SceneUnload();
    
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(PlayerPrefsNames.MAIN_MENU_SCENE, LoadSceneMode.Additive);
        asyncLoad.completed += (asyncOperation) =>
        {
            Scene loadedScene = SceneManager.GetSceneByName(PlayerPrefsNames.MAIN_MENU_SCENE);
            SceneManager.SetActiveScene(loadedScene);

            MainMenuUI.Instance.gameObject.SetActive(true);
            GameUI.Instance.gameObject.SetActive(false);
            MainMenuUI.Instance.selectLevelsMenu.SetActive(false);
            LevelCompleteUI.Instance.gameObject.SetActive(false);
            PauseUI.Instance.gameObject.SetActive(false);

            isLoaded = false;
        };
    }
    
    private void LoadLevel(int levelIndex, string levelName)
    {
        SceneUnload();

        if (SceneManager.GetSceneByName(PlayerPrefsNames.MAIN_MENU_SCENE).isLoaded)
        {
            SceneManager.UnloadSceneAsync(PlayerPrefsNames.MAIN_MENU_SCENE);
        }

        activeLevelName = levelName;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        asyncLoad.completed += (asyncOperation) =>
        {
            Scene loadedScene = SceneManager.GetSceneByName(levelName);
            SceneManager.SetActiveScene(loadedScene);

            GameUI.Instance.textOfLevel.text = "0" + (levelIndex - 3).ToString();

            MainMenuUI.Instance.gameObject.SetActive(levelName.Contains(PlayerPrefsNames.MAIN_MENU_SCENE));
            GameOverUI.Instance.gameObject.SetActive(false);

            GameManager.Instance.LoadLevelData();

            if (!isLoaded)
            {
                StarsHandler.Instance.RestartAnimations();
                GameUI.Instance.gameObject.SetActive(true);
                isLoaded = true;
                LevelCompleteUI.Instance.gameObject.SetActive(false);
                PauseUI.Instance.gameObject.SetActive(false);
                Time.timeScale = 1f;
            }
        };
    }
}
