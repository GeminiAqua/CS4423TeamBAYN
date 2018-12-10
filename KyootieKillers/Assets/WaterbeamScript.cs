using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterbeamScript : MonoBehaviour {

	public float lastHitTime;
    public float tickTime = 1f;
    public int damage = 50;
    private AudioSource hitSound;
    
	void Start () {
		lastHitTime = Time.timeSinceLevelLoad;
        hitSound = GetComponent<AudioSource>();
	}
	
	void OnTriggerEnter(Collider col){
        if (col.gameObject.tag == "Player"){
            if (Time.timeSinceLevelLoad > (lastHitTime + tickTime)){
                lastHitTime = Time.time;
                col.gameObject.GetComponent<Health>().DecrementHealth(damage);
                Debug.Log("Hit PLAYER with WATERBEAM - ENTER");
                hitSound.Play();
            }
        }
    }
    
    void OnTriggerStay(Collider col){
        if (col.gameObject.tag == "Player"){
            if (Time.timeSinceLevelLoad > (lastHitTime + tickTime)){
                lastHitTime = Time.time;
                col.gameObject.GetComponent<Health>().DecrementHealth(damage);
                Debug.Log("Hit PLAYER with WATERBEAM - STAY");
                hitSound.Play();
            }
        }
    }
}
