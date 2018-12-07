using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnicornFastAttack : MonoBehaviour {
    public float speed = 30f;
    public int damage = 15;
    public float attackRange = 10f;
    public float cooldown = 4f;

    GameObject player;
    Rigidbody rb;
    Transform originalPosition = null;
    bool withinRange = false;
    bool isAttacking = false;
    private bool playerHit = false;
    Animator animator;



    // Use this for initialization
    void Start () {
        animator = GetComponentInParent<Animator>();
        player = GameObject.FindWithTag("Player");
        rb = GetComponentInParent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if(Vector3.Distance(rb.transform.position, player.transform.position)<= attackRange){
            Invoke("HornAttack", cooldown);
        }
    }

    private void HornAttack()
    {
        if(originalPosition= null){
            originalPosition = rb.transform;
        }

        animator.SetInteger("animation", 3);
        animator.SetInteger("animation", 7);
        //rb.MovePosition(player.transform.position * speed);
        //rb.transform.position = Vector3.Lerp(rb.transform, player.transform.position, i);
        rb.transform.Translate(player.transform.forward*speed *Time.deltaTime);
       // rb.velocity = player.transform.forward * speed;

      
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
            Invoke("GoBackToOrginalPosition", 1f);
      //  }
   


    }

    private void GoBackToOrginalPosition()
    {
        // isAttacking = false;
        // rb.velocity = player.transform.position * (-1) * speed;
        // rb.transform.Translate(originalPosition.position*1000);
        //rb.velocity = new Vector3(0, 0, 0);
        //rb.velocity = Vector3.zero;

       // rb.MovePosition(originalPosition.position + transform.forward * Time.deltaTime);
      //  Debug.Log("Go back");
        transform.Translate(-transform.forward * speed * Time.deltaTime);


    }

    private void OnCollisionEnter(Collision collision)
    {
        //Decrement Health
        Debug.Log("Decrement Health");
    }
}
