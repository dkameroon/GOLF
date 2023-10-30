using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    
    [SerializeField] private TextMeshProUGUI countOfShots;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject levelCompleteUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private Button pauseButton;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Rigidbody ball;
    [SerializeField] private float standingThreshold = 0.01f;
    
    public TextMeshProUGUI CountOfShots { get { return countOfShots; } } 
    public GameObject PauseUI { get { return pauseUI;  } }

    private int ct;
    private bool isGameOver = false;

    private void Awake()
    {
        pauseUI.gameObject.SetActive(false);
        pauseButton.onClick.AddListener(() =>
        {
            Time.timeScale = 0f;
            pauseUI.gameObject.SetActive(true);
        });
        Instance = this;
    }
    
    private void Start()
    {
        gameOverUI.gameObject.SetActive(false);
        levelCompleteUI.gameObject.SetActive(false);
        countOfShots.text = BallGameManager.Instance.Count().ToString();
        BallControl.Instance.OnShooting += BallControl_OnShooting;
        BallControl.Instance.OnLevelComplete += BallControl_OnLevelComplete;
        BallGameManager.Instance.OnGameOver += GameManager_OnGameOver;
    }

    private void BallControl_OnLevelComplete(object sender, EventArgs e)
    {
        Time.timeScale = 0f;
        countOfShots.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
        levelCompleteUI.gameObject.SetActive(true);
        isGameOver = true;
    }

    private void Update()
    {
        if (ball.velocity.magnitude < standingThreshold && !isGameOver)
        {
            text.gameObject.SetActive(true);
            text.text = "Shot";
        }
        else
        {
            text.gameObject.SetActive(false);
        }
    }

    private void GameManager_OnGameOver(object sender, EventArgs e)
    {
        Time.timeScale = 0f;
        countOfShots.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
        gameOverUI.gameObject.SetActive(true);
        isGameOver = true;
    }


    private void BallControl_OnShooting(object sender, EventArgs e)
    {
        StartCoroutine(UpdateCountOfShotsDelayed());
    }
    
    private IEnumerator UpdateCountOfShotsDelayed()
    {
        yield return new WaitForEndOfFrame(); // Wait for the end of the current frame
        ct = BallGameManager.Instance.Count();
        countOfShots.text = ct.ToString();
    }
}