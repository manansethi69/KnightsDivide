using UnityEngine;

public class rangedWizard : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float bulletSpeed = 9f; 
    [SerializeField] private float targetOffsetY = 1.5f;
    
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogError("Player object with tag 'Player' not found!");
        }
    }

    
    void Update()
    {
        
    }
    
    
    public void Shoot()
    {
        if (player == null)
        {
            Debug.LogError("Player reference is null! Cannot shoot.");
            return;
        }

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
    }
}