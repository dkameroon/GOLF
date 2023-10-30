using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class AudioClipRefsSO : ScriptableObject
{
    public AudioClip[] hit;
    public AudioClip[] defeat;
    public AudioClip[] win;
    public AudioClip[] pickUpCoin;
    public AudioClip[] upgrade;
    public AudioClip[] buySomething;
}