using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnicornAIMovement : MonoBehaviour
{

    public Transform target;
    public int damageAmount;
    public Rigidbody rBody;
    UnityEngine.AI.NavMeshAgent agent;
    Animator animator;
    Health health;
    public float damageCooldown = 1f;
    public bool isDamaging;
    bool hasSpecialAttack = false;
    bool alive = true;
    


    void Start()
    {
        isDamaging = true;
        animator = gameObject.GetComponent<Animator>();
        rBody = GetComponent<Rigidbody>();
        target = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>(); // the agent component of
        health = GetComponent<Health>();
        damageAmount = 10;
    }

    void Update()
    {
        if (health.GetHealth() <= 0)
        {
            alive = false;
            Die();
        }
        else
        {
            FindPlayer();
        }

    }

    private void FindPlayer()
    {
        //get distance 
        float dist = Vector3.Distance(transform.position, target.position);
        agent.SetDestination(target.position);
        if (dist <= agent.stoppingDistance)
        {
            Attack();// move towards the target while avoiding things// move towards the target while avoiding things
        }
        else if (dist > agent.stoppingDistance)
        {
            Chasing();
        }
    }

    void Chasing()
    {


        animator.SetInteger("animation", 5);

    }
    void Attack()
    {


        //animator.SetInteger("animation", 8);
        //animator.SetInteger("animation", 3);
        //animator.SetInteger("animation", 7);

    }
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
        if (!alive) {
            GameObject gen = GameObject.FindGameObjectWithTag("GameController");
            gen.GetComponent<MobGenerator>().enemynum--;
        }
        if (gameObject.tag.Equals("Boss"))
        {
            LoadScene sm = GetComponent<LoadScene>();
            sm.LoadByIndex(2);
        }
        Destroy(gameObject,1f);
    }

    void canDamage()
    {
        isDamaging = true;
    }
}