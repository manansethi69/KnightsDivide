using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;

public class arrowDamage : MonoBehaviour {

	public int damage;
    public int dmgBoost;

	void OnTriggerEnter2D (Collider2D hitInfo)
	{
        
        if(hitInfo.tag != "Player"){
            Debug.Log("yuy");
            Destroy(gameObject);
        }
        
		if(hitInfo.tag == "enemy"){
            Health health;
            if(health = hitInfo.GetComponent<Health>())
            {
                health.GetHit(damage + dmgBoost, gameObject);
            }
            
        }
        
	}
	
}