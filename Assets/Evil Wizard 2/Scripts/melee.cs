using UnityEngine;

public class melee : MonoBehaviour
{
    private bossWeapon weaponScript;

    void Start()
    {
       
        weaponScript = transform.parent.GetComponentInChildren<bossWeapon>();

        if (weaponScript == null)
        {
            Debug.LogError("No bossWeapon script found on the boss or its children.");
        }
    }

   
    public void EnableCollider()
    {
        if (weaponScript != null)
        {
            weaponScript.EnableCollider();
            Debug.Log("Weapon collider enabled.");
        }
    }

    
    public void DisableCollider()
    {
        if (weaponScript != null)
        {
            weaponScript.DisableCollider();
            Debug.Log("Weapon collider disabled.");
        }
    }
}