using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnicornAIMovement : MonoBehaviour {

    public Transform target;
    UnityEngine.AI.NavMeshAgent agent;
    Animator animator;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>(); // the agent component of
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
}
