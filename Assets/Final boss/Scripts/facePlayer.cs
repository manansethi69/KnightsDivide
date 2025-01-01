using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    // Reference to the player GameObject
    private GameObject player;
    private bool facingRight = true;

    // Reference to the GameObject named "FinalBoss"
    private GameObject finalBoss;

    // Start is called before the first execution of Update
    void Start()
    {
        // Find the player GameObject by its tag
        player = GameObject.FindGameObjectWithTag("Player");

        // Find the FinalBoss GameObject by its name
        finalBoss = GameObject.Find("FinalBossTransformRaevyn");

        if (finalBoss == null)
        {
            Debug.LogError("FinalBoss GameObject not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Ensure FinalBoss exists before proceeding
        if (finalBoss == null || player == null) return;

        // Compare the player's position to the FinalBoss position
        if (player.transform.position.x < finalBoss.transform.position.x && facingRight)
        {
            // Rotate the boss 180 degrees on the Y-axis to face left
            transform.Rotate(0, 180f, 0);
            facingRight = false;
        }
        else if (player.transform.position.x > finalBoss.transform.position.x && !facingRight)
        {
            // Rotate the boss 180 degrees on the Y-axis to face right
            transform.Rotate(0, 180f, 0);
            facingRight = true;
        }
    }
}