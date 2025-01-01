using UnityEngine;
using System.Collections;

public class rangedAttack : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float bulletSpeed = 9f; 
    [SerializeField] private float shootInterval = 5f; 
    [SerializeField] private float targetOffsetY = 1.5f; 
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float attackRange = 2f;

    private Animator animator;
    private Transform player;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        InvokeRepeating(nameof(Shoot), shootInterval, shootInterval);
    }

    void Shoot()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange && distanceToPlayer > attackRange)
        {
            StartCoroutine(RangedAttackCoroutine(player));
        }
    }

    private IEnumerator RangedAttackCoroutine(Transform player)
    {
        animator.SetTrigger("Ranged");  // Start the ranged attack animation

        
        yield return new WaitForSeconds(1.5f);

        Vector3 targetPosition = player.position + new Vector3(0, targetOffsetY, 0);
        Vector3 directionToPlayer = (targetPosition - bulletSpawnPoint.position).normalized;

        GameObject bulletInst = Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);

        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        bulletInst.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        Rigidbody2D rb = bulletInst.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = directionToPlayer * bulletSpeed;
        }

        // Check if the boss is moving towards the player after the attack
        if (Vector2.Distance(transform.position, player.position) > attackRange)
        {
            
            animator.SetTrigger("Walk");
        }
        else
        {
            // Boss is not chasing player
            animator.SetTrigger("Idle");
        }
    }
}