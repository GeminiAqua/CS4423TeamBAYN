using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnicornFastAttack : MonoBehaviour
{

    public float velocity = 30f;
    public float attackRange = 10f;
    public float cooldown = 4f;
    public bool isAttacking = false;
    public GameObject player;

    private Animator animator;
    private UnityEngine.AI.NavMeshAgent agent;
    private Rigidbody rBody;
    private Health HP;
    private float baseSpeed;
    private bool alreadyInvoked = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        rBody = GetComponent<Rigidbody>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        HP = GetComponent<Health>();
        baseSpeed = agent.speed;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDead();
        transform.LookAt(player.transform);
        float dist = Vector3.Distance(transform.position, player.transform.position);
        // if( dist < agent.stoppingDistance){
        if (!isAttacking)
        {
            HornAttack();
            // }
        }
        else
        {
            LookForPlayer();
        }
    }

    private void LookForPlayer()
    {
        agent.speed = baseSpeed;

        agent.SetDestination(player.transform.position);
        animator.SetInteger("animation", 5);
    }

    private void HornAttack()
    {
        agent.speed = velocity;
        isAttacking = true;
        if (!alreadyInvoked)
        {
            alreadyInvoked = true;
            Invoke("clearIsAttacking", cooldown);
        }
        // rBody.AddForce(0, 0, 50, ForceMode.VelocityChange);
        // rBody.transform.Translate(player.transform.forward * velocity * Time.deltaTime);
        // if(originalPosition= null){
        // originalPosition = rBody.transform;
        // }

        //PLAY AUDIO SOURCE
        //GetComponentInParent<AudioSource>().Play();

        animator.SetInteger("animation", 3);
        animator.SetInteger("animation", 7);
        //rBody.MovePosition(player.transform.position * speed);
        //rBody.transform.position = Vector3.Lerp(rBody.transform, player.transform.position, i);
        // rBody.velocity = player.transform.forward * speed;


        //if (rBody.transform.position != player.transform.position)
        //{
        //   // isAttacking = true;
        //    transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        //    Debug.Log("Attacking");
        //}else{
        //    playerHit = true;
        //    Debug.Log("Player is hit with fast attack");
        //}
        // if(playerHit){
        // Invoke("GoBackToOrginalPosition", 1f);
        //  }

    }

    private void clearIsAttacking()
    {
        isAttacking = false;
        alreadyInvoked = false;
    }
    // private void GoBackToOrginalPosition()
    // {
    // isAttacking = false;
    // rBody.velocity = player.transform.position * (-1) * speed;
    // rBody.transform.Translate(originalPosition.position*1000);
    //rBody.velocity = new Vector3(0, 0, 0);
    //rBody.velocity = Vector3.zero;

    // rBody.MovePosition(originalPosition.position + transform.forward * Time.deltaTime);
    //  Debug.Log("Go back");
    // transform.Translate(-transform.forward * speed * Time.deltaTime);
    // }

    // private void OnCollisionEnter(Collision collision)
    // {
    //Decrement Health
    // Debug.Log("Decrement Health");
    // }

    void Die()
    {
        agent.Stop();
        agent.SetDestination(transform.position);
        animator.SetInteger("animation", 10);

        if (AnimationIsPlaying("Die") == false)
        {
            StartCoroutine(WaitForAnimation());
        }
    }

    bool AnimationIsPlaying(string animation)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animation);
    }

    private IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    private void CheckDead()
    {
        if (HP.GetHealth() <= 0)
        {
            Die();
        }
    }
}