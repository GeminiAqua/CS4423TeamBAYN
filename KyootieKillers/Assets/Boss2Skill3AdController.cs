using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss2Skill3AdController : MonoBehaviour {
    
    public GameObject target;
    public GameObject boss;
    public GameObject fish;
    public bool hasObject = false;
    private NavMeshAgent agent;
    private float summonTime;
    public Health HP;
    private bool isAlive = true;
    private Animator anim;
    

    void Start () {
        
        agent = GetComponent<NavMeshAgent>();
        HP = GetComponent<Health>();
        anim = GetComponent<Animator>();
        //target = GameObject.FindWithTag("Boss2Heal");
        boss = GameObject.Find("Boss2");
        summonTime = Time.timeSinceLevelLoad;
    }
	
	void Update () {
        CheckAlive();
        if (isAlive){
            if (Time.timeSinceLevelLoad > (summonTime + 1.5f)){
                if (!hasObject){
                    agent.SetDestination(target.transform.position);
                } else {
                    agent.SetDestination(boss.transform.position);
                    fish.transform.position = transform.position + new Vector3(0, 3f, 0);
                }
            }
        }
    }

    void OnTriggerEnter(Collider other){
        if (!hasObject){
            if (other.tag.Equals("Boss2Heal")){
                fish = other.gameObject;
                hasObject = true;
            }
        }
        if (hasObject){
            if (other.name.Equals("Boss2")){
                Destroy(gameObject, 1f);
                Debug.Log("Delivered HEAL to BOSS2");
            }
        }
    }
    
    public void SetTarget(GameObject tar){
        target = tar;
        fish = tar;
    }
    
    private void CheckAlive(){
        if (HP.currentHealth <= 0){
            agent.speed = 0;
            isAlive = false;
            anim.SetTrigger("Dead");
            fish.GetComponent<Boss2Skill3HealScript>().DestroyViaAd();
            Destroy(gameObject, 4f);
        }
    }

}
