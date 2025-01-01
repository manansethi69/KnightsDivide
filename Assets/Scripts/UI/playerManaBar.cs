using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class playerManaBar : MonoBehaviour
{
    public Slider manaSlider;
    public Slider easeManaSlider;
    public float mana;
    public float easeSpeed = 0.02f;
    [SerializeField] private PlayerStats playerStats;
    public TextMeshProUGUI manaDisplay;

    // Start is called before the first frame update
    void Start()
    {
        mana = playerStats.getMana();
        manaSlider.maxValue = mana;
        easeManaSlider.maxValue = mana;
    }


    // //player health is updated via slider that shows damage taken temporarily
    void Update()
    {   

        if(manaSlider.value != mana) {
            manaSlider.value = mana;
        }

        if(manaSlider.value != easeManaSlider.value) {
            easeManaSlider.value = Mathf.MoveTowards(easeManaSlider.value, manaSlider.value, easeSpeed);
        }

        updateHealthUI();

    }

    public void updateHealthUI() {
        mana = playerStats.getMana();
        manaSlider.maxValue = playerStats.GetMaxMana();
        easeManaSlider.maxValue = playerStats.GetMaxMana();
        manaDisplay.text = mana + " / " + manaSlider.maxValue;
    }

    
}
