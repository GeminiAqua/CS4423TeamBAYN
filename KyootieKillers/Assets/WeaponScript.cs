using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour {
    
    public GodrickController hero;
    public bool canDamage;
    public int damageAmount = 50;
    private float lastHit;
    private float cooldown = 0.1f;
    private AudioSource hitSound;

	void Start () {
        hitSound = GetComponent<AudioSource>();
        lastHit = Time.timeSinceLevelLoad - cooldown;
	}
	
	void FixedUpdate () {
		canDamage = hero.getCanAttackBool();
	}
    
    private void OnTriggerEnter(Collider other){
        if (other.tag.Equals("Enemy") || other.tag.Equals("Boss")){
            if (Time.timeSinceLevelLoad > (lastHit + cooldown)){
                hitSound.Play();
                other.GetComponent<Health>().DecrementHealth(damageAmount);
            }
        }
            
    }
}
