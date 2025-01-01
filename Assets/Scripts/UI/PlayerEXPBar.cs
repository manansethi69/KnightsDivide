using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using System;

public class playerEXPBar : MonoBehaviour
{
    public Slider expSlider;
    public Slider easeExpSlider;
    public float exp;
    public float easeSpeed = 0.02f;
    [SerializeField] private PlayerStats playerStats;
    public TextMeshProUGUI expDisplay;

    // Start is called before the first frame update
    void Start()
    {
        exp = playerStats.getCurrentExp();
        Debug.Log(playerStats.getMaxExp());
        expSlider.maxValue = playerStats.getMaxExp();
        easeExpSlider.maxValue = exp;
    }


    // //player health is updated via slider that shows damage taken temporarily
    void Update()
    {   

        if(expSlider.value != exp) {
            expSlider.value = exp;
        }

        if(expSlider.value != easeExpSlider.value) {
            easeExpSlider.value = Mathf.MoveTowards(easeExpSlider.value, expSlider.value, easeSpeed);
        }

        updateHealthUI();

    }

    public void updateHealthUI() {
        exp = playerStats.getCurrentExp();
        expSlider.maxValue = playerStats.getMaxExp();
        easeExpSlider.maxValue = playerStats.getMaxExp();
        expDisplay.text = (exp /expSlider.maxValue) * 100 + "%";
        
        
    }

    
}
