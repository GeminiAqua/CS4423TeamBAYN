using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpecialAttack : MonoBehaviour, EnemyAttack {
    Animator anim;
    NavMeshAgent agent;

    public void attack()
    {
        Debug.Log("attacking");
        agent.stoppingDistance = 0.1f;
        anim.SetInteger("attack",3);

    }

    // Use this for initialization
    void Start () {
        anim = gameObject.GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
