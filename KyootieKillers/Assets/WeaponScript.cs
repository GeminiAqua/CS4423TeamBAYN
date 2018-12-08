using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour {
    
    public GodrickController hero;
    public bool canDamage;
    public int damageAmount = 50;

	// Use this for initialization
	void Start () {
        // damageAmount = 50;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		canDamage = hero.getCanAttackBool();
	}
    
    private void OnTriggerEnter(Collider other){
        if (other.tag.Equals("Enemy")){
            other.GetComponent<Health>().DecrementHealth(damageAmount);
        }
            
    }
}
