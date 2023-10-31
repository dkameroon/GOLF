using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : BaseUI
{
    public static MainMenuUI Instance { get; private set; }
    [SerializeField] private GameObject selectLevels;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        selectLevels.gameObject.SetActive(false);
    }
}
