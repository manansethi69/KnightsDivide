using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int currentHealth, maxHealth;

    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference, OnBlockedWithReference;

    [SerializeField]
    public bool isDead = false;

    // public void InitializeHealth(int healthValue)
    // {
    //     currentHealth = healthValue;
    //     maxHealth = healthValue;
    //     isDead = false;
    // }

    void Start() {
        currentHealth = maxHealth;
    }

    public void GetHit(int amount, GameObject sender)
    {
        if (isDead)
            return;
        if (sender.layer == gameObject.layer)
            return;

        Debug.Log(amount + " " + sender);
        currentHealth -= amount;

        if (currentHealth > 0)
        {
            OnHitWithReference?.Invoke(sender);
        }
        else
        {
            
            OnDeathWithReference?.Invoke(sender);
            isDead = true;
            Health health;
            if(health = sender.GetComponentInChildren<Health>()){
                if(health.currentHealth < maxHealth){
                    health.currentHealth += 25;
                }
                if(health.currentHealth > maxHealth){
                    health.currentHealth = maxHealth;
                }
            }
            Destroy(gameObject, 1.5f); 
        }
    }

    public void OnBlocked(GameObject sender){
        OnBlockedWithReference?.Invoke(sender);
    }

    public int GetCurrentHealth() {
        return currentHealth;
    }

    public int GetMaxHealth() {
        return maxHealth;
    }

    public void AdjustCurrentHealth(int hpRec) {
        if(currentHealth + hpRec >= maxHealth){
            currentHealth = maxHealth;
        } else {
            currentHealth += hpRec;
        }
    }

    public void AdjustMaxHealth(int newHealth) {
        maxHealth = newHealth;
    }
}