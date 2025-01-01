using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class HealthScript : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeHealthSlider;
    public float health;
    public float easeSpeed = 0.02f;
    [SerializeField] private Health currHealth;
    public TextMeshProUGUI healthDisplay;

    // Start is called before the first frame update
    void Start()
    {
        health = currHealth.GetCurrentHealth();
        healthSlider.maxValue = health;
        easeHealthSlider.maxValue = health;
        healthSlider.value = health;
        easeHealthSlider.value = health;
    }


    // //player health is updated via slider that shows damage taken temporarily
    void Update()
    {   
        if(currHealth.GetCurrentHealth() < currHealth.GetMaxHealth()){
            gameObject.transform.parent.gameObject.transform.localScale = new Vector3(0.006f, 0.006f, 0.006f);
        }
        else{
            gameObject.transform.parent.gameObject.transform.localScale = new Vector3(0, 0, 0);
        }
        if(healthSlider.value != health) {
            healthSlider.value = health;
        }

        if(healthSlider.value != easeHealthSlider.value) {
            easeHealthSlider.value = Mathf.MoveTowards(easeHealthSlider.value, healthSlider.value, easeSpeed);
        }

        updateHealthUI();

    }

    public void updateHealthUI() {
        health = currHealth.GetCurrentHealth();
        healthSlider.maxValue = currHealth.GetMaxHealth();
        easeHealthSlider.maxValue = currHealth.GetMaxHealth();
        healthDisplay.text = health + " / " + healthSlider.maxValue;
    }

    public void SetHealth(Health health){
        currHealth = health;
    }
}
