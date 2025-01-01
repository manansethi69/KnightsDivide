using UnityEngine;

public class ranged : MonoBehaviour
{
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public Transform spawnPoint;   // Reference to the spawn point
    public float fireInterval = 2f; // Time between shots
    public float bulletSpeed = 10f; // Speed of the bullet

    private Transform player; // Reference to the player's transform
    private float nextFireTime = 0f; // Tracks when the boss can fire next

    void Start()
    {
        // Find the player in the scene
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null) return; // Exit if no player is found

        // Fire bullets at the player every `fireInterval` seconds
        if (Time.time >= nextFireTime)
        {
            FireAtPlayer();
            nextFireTime = Time.time + fireInterval;
        }
    }

    void FireAtPlayer()
    {
        if (spawnPoint == null || bulletPrefab == null || player == null) return;

        // Instantiate the bullet at the spawn point
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);

        // Calculate direction to the player
        Vector2 directionToPlayer = (player.position - spawnPoint.position).normalized;

        // Assign velocity to the bullet's Rigidbody2D
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        if (bulletRb != null)
        {
            bulletRb.linearVelocity = directionToPlayer * bulletSpeed;
        }
    }
}
