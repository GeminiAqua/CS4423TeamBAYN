using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DarkElfAI : MonoBehaviour {

    public Transform target;
    public int damageAmount;
    public Rigidbody rBody;
    NavMeshAgent agent;
    Animator animator;
    Health health;
    float damageCooldown = 1f;
    public bool isDamaging;
    bool hasSpecialAttack = false;
    //public EnemyAttack attackType;
    DarkElfAttack daf;

    public int attackType;



    float timer;


    // Use this for initialization
    void Start () {
        isDamaging = false;
        animator = gameObject.GetComponent<Animator>();
        rBody = GetComponent<Rigidbody>();
        target = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>(); // the agent component of
        health = GetComponent<Health>();
        damageAmount = 10;
    }
	
	// Update is called once per frame
	void Update () {
        timer = Time.time;
        if (health.GetHealth() <= 0)
        {
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

       // Debug.Log("Should be chasing");
        animator.SetFloat("speed", agent.speed);

    }
    void Attack()
    {
        if(isDamaging == false){

            if(attackType == 1){
                //Debug.Log("Should be attacking");
                animator.SetFloat("speed", 0f);

                Debug.Log("attacking in a1");

                agent.isStopped = false;

                animator.SetTrigger("Attack3");
                isDamaging = true;
                //animator.SetBool("isAttacking", false);
                Invoke("canDamage", damageCooldown);
            }
            else{

            }

           
        }
    }

    void Die()
    {
        agent.Stop();
        Destroy(rBody);
        agent.SetDestination(transform.position);
        animator.SetBool("dead",true);

        if (AnimationIsPlaying("Die") == false)
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


    bool AnimationIsPlaying(string animation)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animation);
    }

    private IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    void canDamage()
    {
        isDamaging = false;
    }
 
}
