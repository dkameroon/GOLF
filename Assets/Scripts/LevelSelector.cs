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
        // Get the number of levels in the Build Settings
        int levelCount = SceneManager.sceneCountInBuildSettings;

        Debug.Log("Total scenes in Build Settings: " + levelCount);

        for (int i = 0; i < levelCount; i++)
        {
            // Get the level name from the Build Settings
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string levelName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

            // Check if the level name starts with "Level "
            if (levelName.StartsWith("Level "))
            {
                int levelNumber;
                if (int.TryParse(levelName.Substring(6), out levelNumber))
                {
                    Debug.Log("Level number: " + levelNumber);

                    GameObject button = Instantiate(buttonPrefab, transform);
                    Button buttonComponent = button.GetComponent<Button>();
                    TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();

                    // Set the button's label (level number)
                    buttonText.text = levelNumber.ToString();

                    // Attach a click event handler to the button (optional)
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

        // Find and unload scenes that contain "Level" in their name
        foreach (Scene scene in activeScenes)
        {
            if (scene.name.Contains("Level"))
            {
                SceneManager.UnloadSceneAsync(scene.name);
                Debug.Log("Deleting :" + scene.name);
            }
        }
    }

    public void RestartActiveLevel()
    {
        if (!string.IsNullOrEmpty(activeLevelName))
        {
            SceneUnload(); // Unload the active level
            GameSceneManager.Instance.LoadScene(activeLevelName, LoadSceneMode.Additive);
            Debug.Log(activeLevelName);// Load it again
        }
    }
    
    // Example level loading function
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