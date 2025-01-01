using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsUIHelper : MonoBehaviour
{   
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private TextMeshProUGUI availPoints;
    [SerializeField] private TextMeshProUGUI hpPoints;
    [SerializeField] private TextMeshProUGUI manaPoints;
    [SerializeField] private TextMeshProUGUI atkPoints;
    [SerializeField] private TextMeshProUGUI atkSpdPoints;
    [SerializeField] private TextMeshProUGUI spdPoints;
    [SerializeField] private TextMeshProUGUI cdPoints;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdatePointsUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseStat(int statID) {


        if(playerStats.HasRemainingStatPoints()) {
            switch(statID) {
                case 1:
                    playerStats.setHpStat(1);
                    playerStats.MinusStatPoints();
                    UpdatePointsUI();
                    GameObject.FindGameObjectWithTag("HpPoints").GetComponent<TextMeshProUGUI>().text = playerStats.getHpStat().ToString();
                    break;
                case 2:
                    playerStats.setManaStat(1);
                    playerStats.MinusStatPoints();
                    UpdatePointsUI();
                    GameObject.FindGameObjectWithTag("ManaPoints").GetComponent<TextMeshProUGUI>().text = playerStats.getManaStat().ToString();
                    break;
                case 3:
                    playerStats.setAtkStat(1);
                    playerStats.MinusStatPoints();
                    UpdatePointsUI();
                    GameObject.FindGameObjectWithTag("AtkPoints").GetComponent<TextMeshProUGUI>().text = playerStats.getAtkStat().ToString();
                    break;
                case 4:
                    playerStats.setAtkSpdStat(1);
                    playerStats.MinusStatPoints();
                    UpdatePointsUI();
                    GameObject.FindGameObjectWithTag("AtkSpdPoints").GetComponent<TextMeshProUGUI>().text = playerStats.getAtkSpdStat().ToString();
                    break;
                case 5:
                    playerStats.setSpdStat(1);
                    playerStats.MinusStatPoints();
                    UpdatePointsUI();
                    GameObject.FindGameObjectWithTag("SpdPoints").GetComponent<TextMeshProUGUI>().text = playerStats.getSpdStat().ToString();
                    break;
                case 6:
                    playerStats.setCooldownStat(1);
                    playerStats.MinusStatPoints();
                    UpdatePointsUI();
                    GameObject.FindGameObjectWithTag("CdPoints").GetComponent<TextMeshProUGUI>().text = playerStats.getCooldownStat().ToString();
                    break;
            }

            playerStats.UpdateStatValues();
        }
        
    }

    public void DecreaseStat(int statID) {
        switch(statID) {
            case 1:
                if(playerStats.getHpStat() > 1) {
                    playerStats.setHpStat(-1);
                    playerStats.AddStatPoints();
                    UpdatePointsUI();
                }
                hpPoints.GetComponent<TextMeshProUGUI>().text = playerStats.getHpStat().ToString();
                break;
            case 2:
                if(playerStats.getManaStat() > 1) {
                    playerStats.setManaStat(-1);
                    playerStats.AddStatPoints();
                    UpdatePointsUI();
                }
                manaPoints.GetComponent<TextMeshProUGUI>().text = playerStats.getManaStat().ToString();
                break;
            case 3:
                if(playerStats.getAtkStat() > 1) {
                    playerStats.setAtkStat(-1);
                    playerStats.AddStatPoints();
                    UpdatePointsUI();
                }
                atkPoints.GetComponent<TextMeshProUGUI>().text = playerStats.getAtkStat().ToString();
                break;
            case 4:
                if(playerStats.getAtkSpdStat() > 1) {
                    playerStats.setAtkSpdStat(-1);
                    playerStats.AddStatPoints();
                    UpdatePointsUI();
                }
                atkSpdPoints.GetComponent<TextMeshProUGUI>().text = playerStats.getAtkSpdStat().ToString();
                break;
            case 5:
                if(playerStats.getSpdStat() > 1) {
                    playerStats.setSpdStat(-1);
                    playerStats.AddStatPoints();
                    UpdatePointsUI();
                }
                spdPoints.GetComponent<TextMeshProUGUI>().text = playerStats.getSpdStat().ToString();
                break;
            case 6:
                if(playerStats.getCooldownStat() > 1) {
                    playerStats.setCooldownStat(-1);
                    playerStats.AddStatPoints();
                    UpdatePointsUI();
                }
                cdPoints.GetComponent<TextMeshProUGUI>().text = playerStats.getCooldownStat().ToString();
                break;
        }

        playerStats.UpdateStatValues();
    }

    public void UpdateStoredStatPoints() {
        hpPoints.text =playerStats.getHpStat().ToString();
        manaPoints.text = playerStats.getManaStat().ToString();
        atkPoints.text = playerStats.getAtkStat().ToString();
        atkSpdPoints.text = playerStats.getAtkSpdStat().ToString();
        spdPoints.text = playerStats.getSpdStat().ToString();
        cdPoints.text = playerStats.getCooldownStat().ToString();
    }

    public void UpdatePointsUI() {
        availPoints.text = "Points: " + playerStats.GetStatPoints();
    }
}
