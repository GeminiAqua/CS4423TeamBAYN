using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackScript : MonoBehaviour {

    public bool isAbleToBeKnockbacked = false;
    public Rigidbody rBody;
	
	void Start () {
		rBody = gameObject.GetComponent<Rigidbody>();
	}
	
	public void KnockbackEnemy(GameObject knockbacker, float force) {
        if (isAbleToBeKnockbacked){
            Vector3 moveDirection = gameObject.transform.position - knockbacker.transform.position;
            rBody.AddForce( moveDirection.normalized * force);
        }
	}
}
