using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMover : MonoBehaviour
{
    public String levelName;
    [SerializeField] private PlayerStats playerStats;
    private Health health;
    void Start()
    {
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health.isDead){
            playerStats.StoreCharacterData();
            SceneManager.LoadScene(levelName);
        }
    }

    public void setPlayerStats(GameObject ps) {
        playerStats = ps.GetComponent<PlayerStats>();
    }
}
