using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int currentHealth, maxHealth;

    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference, OnBlockedWithReference;

    [SerializeField]
    private bool isDead = false;

    public void InitializeHealth(int healthValue)
    {
        if(GetComponent<PlayerStats>()) {
            healthValue = (int)GetComponent<PlayerStats>().getMaxHealth();
        }
        currentHealth = healthValue;
        maxHealth = healthValue;
        isDead = false;
    }

    public void GetHit(int amount, GameObject sender)
    {
        if (isDead)
            return;
        if (sender.layer == gameObject.layer)
            return;

        currentHealth -= amount;

        if (currentHealth > 0)
        {
            OnHitWithReference?.Invoke(sender);
            Debug.Log(amount + " " + sender);
        }
        else
        {
            
            OnDeathWithReference?.Invoke(sender);
            isDead = true;
            PlayerHealth health = sender.GetComponentInChildren<PlayerHealth>();
            Debug.Log(health.currentHealth + " " + sender);
            if(health.currentHealth < maxHealth){
                health.currentHealth += 25;
            }
            if(health.currentHealth > maxHealth){
                health.currentHealth = maxHealth;
            }
            Debug.Log(health.currentHealth + " " + sender);
            Destroy(gameObject, 1.5f); 
        }
    }

    public void OnBlocked(GameObject sender){
        OnBlockedWithReference?.Invoke(sender);
    }

    public int GetCurrentHealth() {
        return currentHealth;
    }
}