using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyForceForward : MonoBehaviour {

    public float speed = 1f;
    private Rigidbody rBody;


	void Start () {
		rBody = GetComponent<Rigidbody>();
	}
	
	void Update () {
		rBody.MovePosition(transform.position + transform.forward * Time.deltaTime * speed);
	}
}
