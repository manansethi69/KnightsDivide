using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class SkillDmg : MonoBehaviour {

	public int damage;

	void OnTriggerEnter2D (Collider2D hitInfo)
	{
        
        
		if(hitInfo.tag == "enemy"){
            Health health;
            if(health = hitInfo.GetComponent<Health>())
            {
                health.GetHit(damage, gameObject);
            }
            
        }
        
	}
	
}