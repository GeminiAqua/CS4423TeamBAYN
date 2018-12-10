using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTornadoDamageScript : MonoBehaviour {

	public float lastHitTime;
    public float tickTime = 1f;
    public int damage = 10;
    public float delayTime = 2f;
    private AudioSource hitSound;
    
	void Start () {
        hitSound = GetComponent<AudioSource>();
		lastHitTime = Time.time + delayTime;
	}
	
	void OnTriggerEnter(Collider col){
        if (col.gameObject.tag == "Player"){
            if (Time.time > (lastHitTime + tickTime)){
                lastHitTime = Time.time;
                col.gameObject.GetComponent<Health>().DecrementHealth(damage);
                hitSound.Play();
                Debug.Log("Player hit by TORNADO ENTER");
            }
        }
    }
    
    void OnTriggerStay(Collider col){
        if (col.gameObject.tag == "Player"){
            if (Time.time > (lastHitTime + tickTime)){
                lastHitTime = Time.time;
                col.gameObject.GetComponent<Health>().DecrementHealth(damage);
                hitSound.Play();
                Debug.Log("Player hit by TORNADO STAY");
            }
        }
    }
}
