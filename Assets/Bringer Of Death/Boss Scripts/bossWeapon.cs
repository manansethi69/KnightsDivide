using UnityEngine;

public class bossWeapon : MonoBehaviour
{
    private int damage = 35; // Damage per attack
    private Collider2D weaponCollider; 

    public bool attackingPLayer;
    void Awake()
    {
        
        weaponCollider = GetComponent<Collider2D>();
        weaponCollider.enabled = false; 
        attackingPLayer = false; 
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit " + damage);

            Health health;
            Block block;
            if(health = collider.GetComponent<Health>())
            {
                
                if(block = collider.GetComponentInChildren<Block>()){
                    if(block.perfectBlock){
                        block.perfectBlockAttack();
                        return;
                    }
                    if(block.IsBlocking){
                        health.GetHit(10, transform.parent.gameObject);
                        return;
                    }
                }
                Debug.Log(collider.name + "damage: " + damage);
                health.GetHit(damage, transform.parent.gameObject);
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