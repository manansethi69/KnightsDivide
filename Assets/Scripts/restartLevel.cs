using UnityEngine;
using UnityEngine.SceneManagement;

public class restartLevel : MonoBehaviour
{
    private Health health;
    void Start()
    {
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health.isDead){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
