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
        //Debug.Log("detected collision");
        if (other.gameObject.tag.Equals("Player") && darkElf.isDamaging)
        {
            Debug.Log("damage Amount: "+ darkElf.damageAmount);
            float damageTime = other.gameObject.GetComponent<GodrickController>().timeLastTookDamage;
            if (Time.timeSinceLevelLoad < (damageTime + other.gameObject.GetComponent<GodrickController>().takeDamageCooldown))
            {
                Debug.Log("Player recently took damage. Can't deal damage yet");
            }
            else
            {
                other.gameObject.GetComponent<GodrickController>().timeLastTookDamage = Time.timeSinceLevelLoad;
             
                int playeHealth = other.gameObject.GetComponent<Health>().GetHealth();

                other.gameObject.GetComponent<Health>().DecrementHealth(darkElf.damageAmount);
                Debug.Log(gameObject.name + " did " + darkElf.damageAmount + " damage");
            }
        }
    }
}
