using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : BaseUI
{
    public static SettingsUI Instance { get; private set; }
    
    [SerializeField] public Button closeSettingsButton;
    [SerializeField] public Slider SliderMusic;
    [SerializeField] public TextMeshProUGUI musicProcents;
    [SerializeField] public Slider SliderSounds;
    [SerializeField] public TextMeshProUGUI soundsProcents;

    private void Awake()
    {
        Instance = this;
        
    }

   

}
