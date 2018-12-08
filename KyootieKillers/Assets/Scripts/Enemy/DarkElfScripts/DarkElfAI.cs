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
    bool isDamaging;
    bool hasSpecialAttack = false;
    public EnemyAttack attackType;
    DarkElfAttack daf;

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
            //Debug.Log("Should be attacking");
            animator.SetFloat("speed", 0f);
            //daf = new DarkElfAttack(attackType);
            //daf.executeAttack();
            Debug.Log("attacking in a1");
            // agent.stoppingDistance = 3f;
            agent.isStopped = false;
            //animator.SetInteger("attack", 3);
            //animator.SetTrigger("stopMovement");
            animator.SetTrigger("Attack3");
            isDamaging = true;
            //animator.SetBool("isAttacking", false);
            Invoke("canDamage", damageCooldown);
           
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

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag.Equals("Player") && isDamaging)
    //    {
    //        float damageTime = collision.gameObject.GetComponent<GodrickController>().timeLastTookDamage;
    //        if (Time.timeSinceLevelLoad < (damageTime + collision.gameObject.GetComponent<GodrickController>().takeDamageCooldown))
    //        {
    //            Debug.Log("Player recently took damage. Can't deal damage yet");
    //        }
    //        else
    //        {
    //            collision.gameObject.GetComponent<GodrickController>().timeLastTookDamage = Time.timeSinceLevelLoad;
             
    //            int playeHealth = collision.gameObject.GetComponent<Health>().GetHealth();

    //            collision.gameObject.GetComponent<Health>().DecrementHealth(damageAmount);
    //            Debug.Log(gameObject.name + " did " + damageAmount + " damage");
    //        }
    //    }
    //}
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
