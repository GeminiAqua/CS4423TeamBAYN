using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonSwordScript : MonoBehaviour {

    public GodrickController controller;
	public int damageAmount = 50;
    public float speed = 40;
    private Rigidbody rBody;
    
	void Start(){
        rBody = gameObject.GetComponent<Rigidbody>();
        controller = GameObject.Find("Godrick").GetComponent<GodrickController>();
        Invoke("MoveForward", 0.5f);
    }
    
    private void OnTriggerEnter(Collider other){
        if ( (other.tag.Equals("Enemy")) || (other.tag.Equals("Boss")) ){
            other.GetComponent<Health>().DecrementHealth(damageAmount);
        }
    }
    
    private void MoveForward(){
        rBody.velocity = controller.transform.forward * speed;
    }
}
