using UnityEngine;

public class lightningAttack : MonoBehaviour
{
    

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    
    
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

    
        GameObject bulletInst = Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);
        
    }
}
