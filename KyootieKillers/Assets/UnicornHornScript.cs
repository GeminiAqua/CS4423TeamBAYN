using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnicornHornScript : MonoBehaviour {

    public float damage = 10f;
    public float lastHit;
    public float cooldown = 2f;
    public UnicornFastAttack attackScript; // set manually
    
    void Awake(){
        lastHit = Time.timeSinceLevelLoad - cooldown;
    }
    
	void OnTriggerEnter(Collider col){
        if (col.gameObject.tag.Equals("Player")) {
            if (Time.timeSinceLevelLoad > (lastHit + cooldown)){
            // if (!attackScript.isAttacking){
                lastHit = Time.timeSinceLevelLoad;
                col.gameObject.GetComponent<Health>().DecrementHealth(10);
                Debug.Log("Player hit with UNICORN HORN");
            }
        }
    }
}
