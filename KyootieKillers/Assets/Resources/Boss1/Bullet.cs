using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	private Transform target;

	public float speed = 70f;

	public int damage = 50;

	public double bulletDuration = 2.0;

	public GameObject impactEffect;

	public GameObject bulletEffect;

	public bool seek = false;
	private double startTime = 0.0;


	public void Seek(Transform _target) {
		seek = true;
		if(seek == true) {
			target = _target;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(target == null){
			Destroy(gameObject);
			return;
		}
		if (bulletDuration >= startTime && seek == true) {
			Debug.Log(startTime);
			startTime += Time.deltaTime;
		} 

		if(bulletDuration <= startTime){
			//Debug.Log("destroyed in func");
			Destroy(gameObject);
		}

		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distanceThisFrame+1) {
			HitTarget();
			return;
		}

		transform.Translate(dir.normalized * distanceThisFrame, Space.World);

	}

	void HitTarget() {
		//GameObject effectInstance = Instantiate(impactEffect, transform.position, transform.rotation);
		//Destroy(effectInstance, 2f);


		//Damage(target);
		Debug.Log("hit");
		seek = false;
		Destroy(gameObject);
	}

}
