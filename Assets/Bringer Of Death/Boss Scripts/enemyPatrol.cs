using UnityEngine;

public class enemyChase : MonoBehaviour
{
    public float speed = 2.0f; 
    public float detectionRangeMultiplier = 3.0f;  

    private Transform player; 
    private enemyAttack attackScript; 
    private float yPosition; 
    private Animator animator;
    private float detectionRange; 

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        attackScript = GetComponent<enemyAttack>();
        animator = GetComponentInChildren<Animator>(); 
        yPosition = transform.position.y;

        if (attackScript != null)
        {
            detectionRange = attackScript.attackRange * detectionRangeMultiplier;
        }
        else
        {
            detectionRange = 3.0f; 
        }
    }

    void Update()
    {
          // Rotate the boss to face the player
            if (player.position.x < transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            
        // If player is null switch to "Idle"
        if (player == null || !player.gameObject.activeInHierarchy)
        {
            if (animator != null)
            {
                animator.SetTrigger("Idle");
            }
            // return;
        }

       
        if (attackScript != null && attackScript.isAttacking)
        {
            // return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Only chase if player is within detection range
        if (distanceToPlayer <= detectionRange)
        {
            Vector3 targetPosition = new Vector3(player.position.x, yPosition, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);


            if (animator != null)
            {
                animator.SetTrigger("Walk");
            }
        }
        else
        {
            // Stop movement and switch to "Idle"
            if (animator != null)
            {
                animator.SetTrigger("Idle");
            }
        }

      
    }

    // public void Flip() {
    //     if(player.position.x > transform.position.x
    // }
}