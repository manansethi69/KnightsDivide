using System;
using System.Runtime.InteropServices.WindowsRuntime;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{   
    [Header("Basic Stats")]
    private int playerLevel = 1;
    private int maxHealth;
    private int maxMana;
    private int currMana;
    private float currentEXP;
    private float maxExp;
    private String currentCharacter;


    [Header("Advanced stats")]
    public float playerSpeed;
    public float playerAtkSpeed;
    public int playerAtk;
    public float cooldown;
    

    [Header("Base Stats")]
    [SerializeField] private int baseMaxHp = 100;
    [SerializeField] private int baseMaxMana = 10;
    [SerializeField] private float basePlayerSpeed = 0f;
    [SerializeField] private float basePlayerAtkSpeed = 1f;
    [SerializeField] private int basePlayerAtk = 5;
    [SerializeField] private float baseCooldown = 0;
    [SerializeField] private float baseEXP;


    private int hpStat = 1 , manaStat = 1, atkStat = 1, atkSpdStat = 1, spdStat = 1, cooldownStat = 1;


    private int statPoints = 5;
    private Health health;

    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject player;
    [SerializeField] private StatsUIHelper pointsUI;


    void Awake()
    {
        currentCharacter = gameObject.name;
        FetchStoredData();
        pointsUI.UpdateStoredStatPoints();

        health = GetComponent<Health>();
        UpdateStatValues();
        currMana = maxMana;
        maxExp = baseEXP * ((float)playerLevel/2);
    }

    

    // Update is called once per frame
    void Update()
    {
    }

    private void FetchStoredData() {
        if(currentCharacter.Equals("Lancelot")) {
            playerLevel = LancelotStoredStats.playerLevel;
            hpStat = LancelotStoredStats.hpStat;
            manaStat = LancelotStoredStats.manaStat;
            atkStat = LancelotStoredStats.atkStat;
            atkSpdStat = LancelotStoredStats.atkSpdStat;
            spdStat = LancelotStoredStats.spdStat;
            cooldownStat = LancelotStoredStats.cooldownStat;
            statPoints = LancelotStoredStats.statPoints;
            currentEXP = LancelotStoredStats.currentEXP;
        } else {
            playerLevel = RaevynStoredStats.playerLevel;
            hpStat = RaevynStoredStats.hpStat;
            manaStat = RaevynStoredStats.manaStat;
            atkStat = RaevynStoredStats.atkStat;
            atkSpdStat = RaevynStoredStats.atkSpdStat;
            spdStat = RaevynStoredStats.spdStat;
            cooldownStat = RaevynStoredStats.cooldownStat;
            statPoints = RaevynStoredStats.statPoints;
            currentEXP = RaevynStoredStats.currentEXP;
        }
    }

    public void StoreCharacterData() {
        if(currentCharacter.Equals("Lancelot")) {
            LancelotStoredStats.playerLevel = playerLevel;
            LancelotStoredStats.hpStat = hpStat;
            LancelotStoredStats.manaStat = manaStat;
            LancelotStoredStats.atkStat = atkStat;
            LancelotStoredStats.atkSpdStat = atkSpdStat;
            LancelotStoredStats.spdStat = spdStat;
            LancelotStoredStats.cooldownStat = cooldownStat;
            LancelotStoredStats.statPoints = statPoints;
            LancelotStoredStats.currentEXP = currentEXP;
        } else {
            RaevynStoredStats.playerLevel = playerLevel;
            RaevynStoredStats.hpStat = hpStat;
            RaevynStoredStats.manaStat = manaStat;
            RaevynStoredStats.atkStat = atkStat;
            RaevynStoredStats.atkSpdStat = atkSpdStat;
            RaevynStoredStats.spdStat = spdStat;
            RaevynStoredStats.cooldownStat = cooldownStat;
            RaevynStoredStats.statPoints = statPoints;
            RaevynStoredStats.currentEXP = currentEXP;
        }
    }

    public int getMana() {
        return currMana;
    }

    public int GetMaxMana() {
        return maxMana;
    }

    public void addExp(float exp) {
        currentEXP += exp;
        if(currentEXP >= maxExp) {
            LevelUp(currentEXP - maxExp);
        }
    }

    public int getMaxHealth() {
        return maxHealth;
    }

    public float getMaxExp() {
        return maxExp;
    }

    public float getCurrentExp() {
        return currentEXP;
    }

    public int GetStatPoints() {
        return statPoints;
    }

    public void AddStatPoints() {
        statPoints += 1;
    }

    public void MinusStatPoints() {
        statPoints -= 1;
    }

    private void LevelUp(float overflow) {
        statPoints +=  5;
        playerLevel += 1;
        currentEXP = overflow;
        maxExp =  baseEXP * (playerLevel/2);
        pointsUI.UpdatePointsUI();
    }

    public bool HasRemainingStatPoints() {
        return statPoints > 0 ? true : false;
    }

    public void UpdateStatValues() {
        maxHealth = baseMaxHp + (25 * hpStat);
        maxMana = baseMaxMana + (5 * manaStat);
        playerAtk = basePlayerAtk * atkStat;
        if(GetComponent<playerAiming>()) {
            basePlayerSpeed = 6f;
        }
        playerAtkSpeed = basePlayerAtkSpeed + (atkSpdStat * 0.2f);
        playerSpeed = basePlayerSpeed + ((spdStat - 1) * 0.4f);
        cooldown = baseCooldown - (cooldownStat * 0.5f);
        AssignStats();
    }

    private void AssignStats() {
        if(gameObject.name.Equals("Lancelot")) {
            health.AdjustMaxHealth(maxHealth);
            //uncomment after bug fixed
            // playerAnimator.SetFloat("atkSpd", playerAtkSpeed);
            GetComponent<lancelotController>().AdjustSpeed(playerSpeed);
            GetComponentInChildren<lancelotAttack>().adjustAtkBoost(playerAtk);
            GetComponent<SkillHolder>().AdjustCooldownReduction(cooldown);
        }else{
            health.AdjustMaxHealth(maxHealth);
            GetComponent<PlayerControl>().AdjustSpeed(playerSpeed);
            GetComponent<playerAiming>().AdjustAttackSpeed(playerAtkSpeed);
            GetComponent<playerAiming>().AdjustArrowDmgBoost(playerAtk);
        }       
    }

    public int getHpStat() {
        return hpStat;
    }

    public void setHpStat(int setHp) {
        hpStat += setHp;
    }

    public int getManaStat() {
        return manaStat;
    }

    public void setManaStat(int setMana) {
        manaStat += setMana;
    }

    public int getAtkStat() {
        return atkStat;
    }

    public void setAtkStat(int setAtk) {
        atkStat += setAtk;
    }

    public int getAtkSpdStat() {
        return atkSpdStat;
    }

    public void setAtkSpdStat(int setAtkSpd) {
        atkSpdStat += setAtkSpd;
    }

    public int getSpdStat() {
        return spdStat;
    }

    public void setSpdStat(int setSpd) {
        spdStat += setSpd;
    }

    public int getCooldownStat() {
        return cooldownStat;
    }

    public void setCooldownStat(int setCooldown) {
        cooldownStat += setCooldown;
    }




}
