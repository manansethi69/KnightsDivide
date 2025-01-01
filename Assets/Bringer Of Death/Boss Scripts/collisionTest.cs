using UnityEngine;

public class collisionTest : MonoBehaviour
{
    // This method is called when another collider enters the trigger collider
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the other object has the "enemy" tag
        if (other.CompareTag("enemy"))
        {
            // Get the Animator component from the "Sprite" child object
            Animator enemyAnimator = other.transform.Find("Sprite").GetComponent<Animator>();

            // Check if the enemy has an Animator component
            if (enemyAnimator != null)
            {
                // Trigger the "Death" animation
                enemyAnimator.SetTrigger("Death");

                // Destroy the enemy after 1 second
                Destroy(other.gameObject, 3f); // Delay of 1 second
            }
        }
    }
}