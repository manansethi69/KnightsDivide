using SupanthaPaul;
using Unity.Cinemachine;
using UnityEngine;

public class playerChoice : MonoBehaviour
{
    public GameObject lancelot;

    public GameObject raevyn;
    public CinemachineCamera cameraLancelot;
    public HealthScript healthScript;
    public GameObject raevynExp;
    public GameObject lancelotExp;
    public GameObject raevynMana;
    public GameObject lancelotMana;
    public GameObject lancelotSkillbar;
    public GameObject raevynSkillbar;
    public CinemachineCamera cameraRaevyn;
    public GameObject gm;
    public GameObject raevynMenu;
    public GameObject lancelotMenu;
    public GameObject finalBoss;
    public void Lancelot(){
        Destroy(raevyn);
        lancelot.SetActive(true);
        healthScript.SetHealth(lancelot.GetComponent<Health>());
        cameraLancelot.gameObject.SetActive(true);
        gameObject.SetActive(false);
        lancelotExp.SetActive(true);
        lancelotMana.SetActive(true);
        lancelotSkillbar.SetActive(true);
        gm.GetComponent<UIInput>().SetStatsMenu(lancelotMenu);
        finalBoss.GetComponent<LevelMover>().setPlayerStats(lancelot);
    }
    public void Raevyn(){
        Destroy(lancelot);
        raevyn.SetActive(true);
        healthScript.SetHealth(raevyn.GetComponent<Health>());
        cameraRaevyn.gameObject.SetActive(true);
        gameObject.SetActive(false);
        raevynExp.SetActive(true);
        raevynMana.SetActive(true);
        raevynSkillbar.SetActive(true);
        gm.GetComponent<UIInput>().SetStatsMenu(raevynMenu);
        finalBoss.GetComponent<LevelMover>().setPlayerStats(raevyn);
    }
}
