using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DarkElfAI : MonoBehaviour {

    public Transform target;
    public int damageAmount;
    Rigidbody rBody;
    NavMeshAgent agent;
    Animator animator;
    Health health;
    float damageCooldown = 1f;
    public bool isDamaging;
    bool hasSpecialAttack = false;
    //public EnemyAttack attackType;
    DarkElfAttack daf;
    public GameObject[] throwPoints;
    public int attackType;
    public float range;



    float timer;
    public GameObject dagger;


    // Use this for initialization
    void Start () {
        isDamaging = false;
        animator = gameObject.GetComponent<Animator>();
        rBody = GetComponent<Rigidbody>();
        target = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>(); // the agent component of
        health = GetComponent<Health>();
        damageAmount = 10;


            agent.stoppingDistance = range;

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
            animator.SetFloat("speed", 0f);
            if (attackType == 1){
                //Debug.Log("Should be attacking");


                Debug.Log("attacking in a1");

                agent.isStopped = false;

                animator.SetTrigger("Attack3");
               
            }
            else{
                Debug.Log("attacking in a2");
                foreach(GameObject point in throwPoints)
               {
                    animator.SetTrigger("Attack2");
                    
                    GameObject s = Instantiate(dagger, point.transform.position, point.transform.rotation);
                  //  s.transform.Rotate(Vector3.left * 90);

                    s.GetComponent<Rigidbody>().AddForce(s.transform.forward * 100);
                 //s   Destroy(dagger, 10);
                   // rBody.velocity = s.transform.forward * 9;
                    //Instantiate(dagger);
                    // Instantiate(dagger, child.transform, child.rotation);
                    // GameObject missile1 = Instantiate(skillPrefab.two) as GameObject;
                    // dagger.transform.forward = transform.forward;
                    //  dagger.transform.position = transform.position + new Vector3(0, 1, 0) + (transform.forward * 2.5f);

                }
            }
            isDamaging = true;
            animator.SetBool("isAttacking", false);
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
