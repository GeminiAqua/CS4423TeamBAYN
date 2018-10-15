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
    float damageCooldown = 1f;
    bool isDamaging;
    
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
            Die();
        } else {
			agent.SetDestination(target.position); // move towards the target while avoiding things// move towards the target while avoiding things
            Chasing();
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
		agent.Stop();
        Destroy(rBody);
        agent.SetDestination(transform.position);
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
        if (collision.gameObject.tag.Equals("Player") && isDamaging)
        {
            float damageTime = collision.gameObject.GetComponent<GodrickController>().timeLastTookDamage;
            if (Time.timeSinceLevelLoad < (damageTime + collision.gameObject.GetComponent<GodrickController>().takeDamageCooldown)){
                Debug.Log("Player recently took damage. Can't deal damage yet");
            } else {
                collision.gameObject.GetComponent<GodrickController>().timeLastTookDamage = Time.timeSinceLevelLoad;
                isDamaging = false;
                Invoke("canDamage", damageCooldown);
                Attack();
                int playeHealth = collision.gameObject.GetComponent<Health>().GetHealth();
               
                collision.gameObject.GetComponent<Health>().DecrementHealth(damageAmount);
                Debug.Log(gameObject.name + " did " + damageAmount + " damage");
            }
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
    
    void canDamage(){
        isDamaging = true;
    }
}


