using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnicornFastAttack : MonoBehaviour {
    public float speed = 30f;
    public int damage = 15;
    public float attackRange = 10f;
    public float cooldown = 3f;
    float lastAttackTime = 0;
    GameObject player;
    Rigidbody rb;
    Transform originalPosition = null;
    bool withinRange = false;
    bool isAttacking = false;
    private bool playerHit = false;



    // Use this for initialization
    void Start () {
        player = GameObject.FindWithTag("Player");
        rb = GetComponentInParent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if(Vector3.Distance(rb.transform.position, player.transform.position)<= attackRange){
            withinRange = true;
        }else{
            withinRange = false;
        }

        if (lastAttackTime <= Time.timeSinceLevelLoad - cooldown && withinRange)
        {
            HornAttack();
        }

    }

    private void HornAttack()

    {
        if(originalPosition= null){
            originalPosition = rb.transform;
        }

        //rb.MovePosition(player.transform.position * speed);
        //rb.transform.position = Vector3.Lerp(rb.transform, player.transform.position, i);
        //rb.transform.Translate(player.transform.forward*speed);
        rb.velocity = player.transform.forward * speed;

      
        //if (rb.transform.position != player.transform.position)
        //{
        //   // isAttacking = true;
        //    transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        //    Debug.Log("Attacking");
        //}else{
        //    playerHit = true;
        //    Debug.Log("Player is hit with fast attack");
        //}
       // if(playerHit){
            Invoke("GoBackToOrginalPosition", 2f);
      //  }
        lastAttackTime = Time.timeSinceLevelLoad;


    }

    private void GoBackToOrginalPosition()
    {
        // isAttacking = false;
        // rb.velocity = player.transform.position * (-1) * speed;
        // rb.transform.Translate(originalPosition.position*1000);
        //rb.velocity = new Vector3(0, 0, 0);
        rb.velocity = Vector3.zero;

       // rb.MovePosition(originalPosition.position + transform.forward * Time.deltaTime);
        Debug.Log("Go back");


    }

    private void OnCollisionEnter(Collision collision)
    {
        //Decrement Health
        Debug.Log("Decrement Health");
    }
}
