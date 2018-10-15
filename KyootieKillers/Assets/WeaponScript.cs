using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour {
    
    public GodrickController hero;
    public bool canDamage;
    public int damageAmount;

	// Use this for initialization
	void Start () {
        damageAmount = 50;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		canDamage = hero.isDamaging;
	}
    
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit");
        if (collision.gameObject.tag.Equals("Enemy") && canDamage)
        {
            Health mobHealth = collision.gameObject.GetComponent<Health>();
            int enemyHealth = mobHealth.GetHealth();
           
            mobHealth.DecrementHealth(damageAmount);
            Debug.Log(gameObject.name + " did " + damageAmount + " damage");
            if (enemyHealth <= 0)
            {
                Debug.Log("Player Killed");
               // Destroy(collision.gameObject);
            }
        }
    }
}
