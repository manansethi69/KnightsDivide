using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class TornadoScrit : MonoBehaviour {

	public int damage;
    private float timer;

	void OnTriggerStay2D (Collider2D hitInfo)
	{
        
        // if(hitInfo.tag != "Player"){
        //     Debug.Log("yuy");
        //     Destroy(gameObject);
        // }
        
		if(hitInfo.tag == "enemy"){
            Health health;
            if(health = hitInfo.GetComponent<Health>())
            {
                health.GetHit(damage, gameObject);
            }
            
        }
        
	}

    void Update() {
        timer += Time.deltaTime;
        if(timer > 3f) {
            Destroy(gameObject);
        }
    }
	
}