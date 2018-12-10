using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfDagger : MonoBehaviour {

    // Use this for initialization
    Rigidbody rBody;
    float speed = 3f;
    DarkElfAI elf;

    Transform elfPos;
	void Start () {
        rBody = gameObject.GetComponent<Rigidbody>();
      //  Invoke("ThrowDagger", 0.5f);
      //  elf = gameObject.GetComponentInParent<DarkElfAI>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public Transform GetElfPosition(){
        return elfPos;
    }
    public void SetElfPosition(Transform elfPos){
        this.elfPos = elfPos;
    }
    private void OnTriggerEnter(Collider other){
        if ( other.tag.Equals("Player") ){
            other.GetComponent<Health>().DecrementHealth(elf.damageAmount);
        }
    }
    
    private void ThrowDagger(){
        //rBody.velocity = elf.transform.forward * speed;
        //rBody.AddForce(elf.transform.forward * speed);
      
    }
}

