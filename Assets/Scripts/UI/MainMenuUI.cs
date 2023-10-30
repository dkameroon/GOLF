using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject selectLevels;
    private void Start()
    {
        selectLevels.gameObject.SetActive(false);
    }
}
