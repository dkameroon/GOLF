using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : BaseUI
{
    public static MainMenuUI Instance { get; private set; }
    public GameObject selectLevelsMenu;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        selectLevelsMenu.gameObject.SetActive(false);
    }
}
