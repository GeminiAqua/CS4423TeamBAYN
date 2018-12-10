using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonSwordScript : MonoBehaviour {

    public GodrickController controller;
	public int damageAmount = 50;
    public float speed = 40;
    private Rigidbody rBody;
    private bool alreadyHitBoss;
    private AudioSource hitSound;
    private float spawnTime;
    public float delayTime = .5f;
    
	void Awake(){
        rBody = gameObject.GetComponent<Rigidbody>();
        hitSound = GetComponent<AudioSource>();
        controller = GameObject.Find("Godrick").GetComponent<GodrickController>();
        Invoke("MoveForward", 0.5f);
        spawnTime = Time.timeSinceLevelLoad;
    }
    
    private void OnTriggerEnter(Collider other){
        if (Time.timeSinceLevelLoad > (spawnTime + delayTime)){
            if (other.tag.Equals("Enemy")){
                other.GetComponent<Health>().DecrementHealth(damageAmount);
                hitSound.Play();
            } else if (other.tag.Equals("Boss")){
                if (!alreadyHitBoss && other.GetComponent<Health>()){
                    alreadyHitBoss = true;
                        other.GetComponent<Health>().DecrementHealth(damageAmount);
                        hitSound.Play();
                }
            }
        }
    }
    
    private void MoveForward(){
        rBody.velocity = controller.transform.forward * speed;
    }
}
