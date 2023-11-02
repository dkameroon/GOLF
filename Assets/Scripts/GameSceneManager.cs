using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    
    public void LoadScene(string sceneName, LoadSceneMode loadMode = LoadSceneMode.Single)
    {
        SceneManager.LoadScene(sceneName, loadMode);
    }
    
    public void LoadSceneAdditive(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }
    
    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }
}
