using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkElfWeapon : MonoBehaviour {

    /// bool isDamaging;
     public int damageAmount;
	DarkElfAI darkElf;
    

    
	void Start () {
		darkElf = GetComponentInParent<DarkElfAI>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
   private void OnTriggerEnter(Collider other)
    {
        Debug.Log("detected collision");
        if (collision.gameObject.tag.Equals("Player") && darkElf.isDamaging)
        {
            float damageTime = collision.gameObject.GetComponent<GodrickController>().timeLastTookDamage;
            if (Time.timeSinceLevelLoad < (damageTime + collision.gameObject.GetComponent<GodrickController>().takeDamageCooldown))
            {
                Debug.Log("Player recently took damage. Can't deal damage yet");
            }
            else
            {
                collision.gameObject.GetComponent<GodrickController>().timeLastTookDamage = Time.timeSinceLevelLoad;
             
                int playeHealth = collision.gameObject.GetComponent<Health>().GetHealth();

                collision.gameObject.GetComponent<Health>().DecrementHealth(damageAmount);
                Debug.Log(gameObject.name + " did " + damageAmount + " damage");
            }
        }
    }
}
