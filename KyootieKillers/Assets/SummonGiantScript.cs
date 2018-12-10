using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonGiantScript : MonoBehaviour {

    public float dropCD = 1.5f;
    public float dropVelocity = 5f;
    public int damage = 50;
    private float startTime;
    public bool isPostDone = false;
    public GameObject postObj;
    public float effectRadius = 10f;
    public Collider[] nearbyObjects;
    private AudioSource hitSound;
    
	void Start () {
        postObj = Resources.Load("SummonGiantPost") as GameObject;
        hitSound = GetComponent<AudioSource>();
		Invoke("FallToZero", dropCD);
        startTime = Time.time;
        Invoke("Post", 1f);
	}
    
    void Update(){
        if ( Time.time > startTime + dropCD){
            FallToZero();
        }
    }
    
    private void FallToZero(){
        if (transform.position.y > 0){
            transform.position -= new Vector3(0, dropVelocity * Time.deltaTime, 0);
        } else {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            if (!isPostDone){
                Post();
            }
        }
    }
    
    private void Post(){
        GameObject rocks = Instantiate(postObj) as GameObject;
        rocks.transform.position = transform.position;
        rocks.transform.position = new Vector3(transform.position.x, -0.3f, transform.position.z);
        isPostDone = true;
        hitSound.Play();
        GiantKnockback();
    }
    
    private void GiantKnockback(){
        nearbyObjects = Physics.OverlapSphere(gameObject.transform.position, effectRadius);
        foreach (Collider thing in nearbyObjects){
            if ((thing.tag == "Enemy") || (thing.tag == "Boss")){
                thing.gameObject.GetComponent<Health>().DecrementHealth(damage);
            }
            if (thing.tag == "Enemy"){
                thing.gameObject.GetComponent<KnockbackScript>().KnockbackEnemy(gameObject, 500f);
            }
        }
    }
	
}
