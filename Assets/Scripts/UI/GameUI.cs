using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : BaseUI
{
    public static GameUI Instance { get; private set; }
    
    [SerializeField] public TextMeshProUGUI countOfShots;
    [SerializeField] public Button pauseGameButton;
    [SerializeField] public TextMeshProUGUI text;


    private void Awake()
    {
        Instance = this;
    }
    


    /*private void BallControl_OnShooting(object sender, EventArgs e)
    {
        StartCoroutine(UpdateCountOfShotsDelayed());
    }
    
    private IEnumerator UpdateCountOfShotsDelayed()
    {
        yield return new WaitForEndOfFrame(); // Wait for the end of the current frame
        ct = GameManager.Instance.Count();
        countOfShots.text = ct.ToString();
    }*/
}
