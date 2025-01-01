using UnityEngine;

public class bossHealth : MonoBehaviour
{
    private int health = 40; // Boss health
    

    public void TakeDamage(int damage)
    {
        
        health -= damage; // Apply damage

        Debug.Log("Boss health: " + health + "Damage: " + damage);

        if (health <= 0)
        {
            Die();
        }
        
    }

    

    private void Die()
    {
        Animator animator = GetComponentInChildren<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Death");
        }

        Destroy(gameObject, 1.5f); // Destroy after 2 seconds
    }
}