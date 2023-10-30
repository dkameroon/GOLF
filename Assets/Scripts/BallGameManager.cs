using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGameManager : MonoBehaviour
{
    public static BallGameManager Instance { get; private set; }
    public event EventHandler OnGameOver;
    [SerializeField] private int count;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1f;
        BallControl.Instance.OnShooting += BallControl_OnShooting;
    }

    private void BallControl_OnShooting(object sender, EventArgs e)
    {
        count--;
        if (count <= 0)
        {
            OnGameOver?.Invoke(this,EventArgs.Empty);
            SoundManager.Instance.PlayDefeatSound(Camera.main.transform.position,0.5f);
        }
        
    }

    public int Count()
    {
        return count;
    }
    
    private void OnDestroy()
    {
        BallControl.Instance.OnShooting -= BallControl_OnShooting;
    }
}
