using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Skill3HealScript : MonoBehaviour {
	
    public int healAmount = 2500;
    private bool hasHealedBoss = false;
    
	void OnTriggerEnter(Collider other){
        if (other.name.Equals("Boss2")){
            if (!hasHealedBoss){
                hasHealedBoss = true;
                other.gameObject.GetComponent<Health>().DecrementHealth(-healAmount);
                Destroy(gameObject, 3f);
                Debug.Log("HEALED BOSS2");
            }
        }
    }
    
    public void DestroyViaAd(){
        Destroy(gameObject, 4.5f);
    }
}
