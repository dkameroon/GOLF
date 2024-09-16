using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameUI : BaseUI
{
    public static GameUI Instance { get; private set; }
    
    [SerializeField] public TextMeshProUGUI countOfShots;
    [SerializeField] public Button pauseGameButton;
    [SerializeField] public TextMeshProUGUI textYouCanShoot;
    [SerializeField] public TextMeshProUGUI textOfLevel;
    [SerializeField] public Button rotateLeftButton;
    [SerializeField] public Button rotateRightButton;


    private void Awake()
    {
        Instance = this;
    }
    
}
