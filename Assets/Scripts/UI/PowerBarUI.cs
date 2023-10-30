using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    
    public class PowerBarUI : MonoBehaviour
    {
        public static PowerBarUI Instance { get; set; }
        [SerializeField] private Image barImage;

        public Image PowerBar { get => barImage; }

        private void Awake()
        {
            Instance = this;
            barImage.fillAmount = 0;
        }
    }

