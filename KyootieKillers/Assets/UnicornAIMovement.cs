using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnicornAIMovement : MonoBehaviour {

    public Transform target;
    UnityEngine.AI.NavMeshAgent agent;
    Animator animator;
    Health health;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>(); // the agent component of
        health = GetComponent<Health>();
    }
    void Update()
    {
        agent.SetDestination(target.position); // move towards the target while avoiding things
        Chasing();
   }
    void Chasing(){
        animator.SetBool("isRunning", true);
        animator.SetInteger("animation", 5);
    }
    void Attack(){
        animator.SetBool("isRunning", false);
        animator.SetInteger("animation", 8);
    }
    void Die(){
        animator.SetInteger("animation", 10);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Weapon")){
            health.DecrementHealth(10);
        }
    }
}
