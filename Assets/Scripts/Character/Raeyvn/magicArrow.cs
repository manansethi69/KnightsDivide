using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;

public class magicArrow : MonoBehaviour {

	public int damage;
    public GameObject impactVFX;

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
                
                Instantiate(impactVFX, hitInfo.transform.position, Quaternion.identity);
                
                health.GetHit(damage, gameObject);
            }
            
        }
        
	}
	
}