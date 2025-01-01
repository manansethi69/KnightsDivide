using UnityEngine;

public class lightningWeapon : MonoBehaviour
{
    private int damage = 35; // Damage per attack
    private Collider2D weaponCollider;
    private Animator animator; // Reference to the Animator

    public bool attackingPLayer;

    void Awake()
    {
        weaponCollider = GetComponent<Collider2D>();
        weaponCollider.enabled = false;
        attackingPLayer = false;

        animator = GetComponent<Animator>(); 
    }

    void Start()
    {
        

        
        Destroy(gameObject, 1f);
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Health health;
            if (health = collider.GetComponent<Health>())
            {
                health.GetHit(damage, transform.parent.gameObject);
                Debug.Log("ZAP " + damage);
            }
        }
    }

    // Enable and disable the collider
    public void EnableCollider()
    {
        weaponCollider.enabled = true;
        attackingPLayer = true;
    }

    public void DisableCollider()
    {
        weaponCollider.enabled = false;
        attackingPLayer = false;
    }
}