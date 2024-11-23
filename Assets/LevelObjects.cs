using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelObjects : MonoBehaviour
{
    public static LevelObjects Instance;
    
    public Camera MainCamera;

    private void Awake()
    {
        Instance = this;
    }


}