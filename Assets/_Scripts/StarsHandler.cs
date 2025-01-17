using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsHandler : MonoBehaviour
{
    public static StarsHandler Instance { get; private set; }
    
    public Animator[] starAnimators;
    public Image[] starImages;
    private Color defaultColor = new Color(51f / 255f, 51f / 255f, 51f / 255f);
    
    public bool star1Condition = false;
    public bool star2Condition = false;
    public bool star3Condition = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (star1Condition)
        {
            StartCoroutine(PlayStarAnimation(0, true, 0f));
        }
        else if (star2Condition)
        {
            StartCoroutine(PlayStarAnimation(0, true, 0f));
            StartCoroutine(PlayStarAnimation(1, true, 0.5f));
        }
        else if (star3Condition)
        {
            StartCoroutine(PlayStarAnimation(0, true, 0f));
            StartCoroutine(PlayStarAnimation(1, true, 0.5f));
            StartCoroutine(PlayStarAnimation(2, true, 1.0f));
        }
    }

    private IEnumerator PlayStarAnimation(int starIndex, bool isEarned, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        starAnimators[starIndex].SetBool("isEarned", isEarned);
    }

    public void ResetStarColors()
    {
        for (int i = 0; i < starImages.Length; i++)
        {
            starImages[i].color = defaultColor;
        }
    }
    
    public void RestartAnimations()
    {
        for (int i = 0; i < starAnimators.Length; i++)
        {
            starAnimators[i].SetBool("isEarned", false);
            starAnimators[i].Play("StarIdle");
        }
    }
    
    
}
