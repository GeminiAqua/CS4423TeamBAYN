using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnicornAIMovement : MonoBehaviour
{

    public Transform target;
    public int damageAmount;
    UnityEngine.AI.NavMeshAgent agent;
    Animator animator;
    Health health;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>(); // the agent component of
        health = GetComponent<Health>();
        damageAmount = 10;
    }
    void Update()
    {
        agent.SetDestination(target.position); // move towards the target while avoiding things
        Chasing();
        if (health.GetHealth() < 0)
        {
            Debug.Log("Dead unicorn");
            Die();
        }
    }
    void Chasing()
    {
        //animator.SetBool("isRunning", true);
        animator.SetInteger("animation", 5);
    }
    void Attack()
    {
        //animator.SetBool("isRunning", false);
        animator.SetInteger("animation", 8);
    }
    void Die()
    {
        animator.SetInteger("animation", 10);

        if(AnimationIsPlaying("Die") == false)
        {
            StartCoroutine(WaitForAnimation());

        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Weapon"))
        {
           // Debug.Log("there is a collision with" + collision.gameObject);
            health.DecrementHealth(10);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Attack();
            int playeHealth = collision.gameObject.GetComponent<Health>().GetHealth();
           
            collision.gameObject.GetComponent<Health>().DecrementHealth(damageAmount);
            if (playeHealth < 0)
            {
                Debug.Log("Player Killed");
               // Destroy(collision.gameObject);
            }
        }
    }
    bool AnimationIsPlaying(string animation)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animation);
    }

    private IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}


