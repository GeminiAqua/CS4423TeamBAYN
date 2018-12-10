using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnicornBossAI : MonoBehaviour
{

    //public GameObject flower;

    //public GameObject colorIndicator;
    
    public GameObject[] possibleColors;
    public string[] colorNames = {"green", "yellow", "blue", "red", "purple", "stop", "stop", "stop"};
    public string currentColor = "green";
    public static int colorIndex;
    //public GameObject z = null;
    
    //public GameObject[] nodes;


    public Transform target;
    public int damageAmount;
    public Rigidbody rBody;
    UnityEngine.AI.NavMeshAgent agent;
    Animator animator;
    Health health;
    float damageCooldown = 1f;
    bool isDamaging;
    bool hasSpecialAttack = false;

    bool immune = false;
    int healthVar;

    void Start()
    {
        colorIndex = 0;

        isDamaging = true;
        animator = gameObject.GetComponent<Animator>();
        rBody = GetComponent<Rigidbody>();
        target = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>(); // the agent component of
        health = GetComponent<Health>();
        damageAmount = 10;
        PickColor();
        Debug.Log("GAMEOBJECT"+gameObject.transform.position);
    }

    void Update()
    {
        if (health.GetHealth() <= 0)
        {
            Die();
        }else if(health.GetHealth() <=900 && health.GetHealth() > 700) {
            //Debug.Log(health.GetHealth());
            health.beImmune();
            
        } 
        else
        {
            FindPlayer();
            //Debug.Log("NOT IMMUNE "+health.GetHealth());
        }

    }

    private void FindPlayer()
    {
        //get distance 
        float dist = Vector3.Distance(transform.position, target.position);
        agent.SetDestination(target.position);
        //agent.Warp(target.position);
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
        Destroy(rBody);
        agent.SetDestination(transform.position);
        animator.SetInteger("animation", 10);

        if (AnimationIsPlaying("Die") == false)
        {
            StartCoroutine(WaitForAnimation());

        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Weapon"))
        {
            Debug.Log("there is a collision with" + collision.gameObject);
            health.DecrementHealth(10);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player") && isDamaging)
        {
            float damageTime = collision.gameObject.GetComponent<GodrickController>().timeLastTookDamage;
            if (Time.timeSinceLevelLoad < (damageTime + collision.gameObject.GetComponent<GodrickController>().takeDamageCooldown))
            {
                Debug.Log("Player recently took damage. Can't deal damage yet");
            }
            else
            {
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



    public void PickColor() {
        GameObject z = Instantiate(possibleColors[colorIndex], gameObject.transform);
        Debug.Log(currentColor);

    }

    public string getColor() {
        return currentColor;
    }

    public void setColor() {
        currentColor = colorNames[colorIndex];
        Debug.Log(colorIndex);
    }

    public void incrementColorIndex(){
        colorIndex++;
    }

    public void setActiveColor(){
        if(colorIndex == 1) {
            GameObject x = GameObject.Find("GreenIndicator(Clone)");
            x.SetActive(false);
        }
        if(colorIndex == 2) {
            GameObject x = GameObject.Find("YellowIndicator(Clone)");
            x.SetActive(false);
        }
        if(colorIndex == 3) {
            GameObject x = GameObject.Find("BlueIndicator(Clone)");
            x.SetActive(false);
        }
        if(colorIndex == 4) {
            GameObject x = GameObject.Find("RedIndicator(Clone)");
            x.SetActive(false);
        }
        if(colorIndex == 5) {
            GameObject x = GameObject.Find("PurpleIndicator(Clone)");
            x.SetActive(false);
            health.beNotImmune();
            health.DecrementHealth(300);
            GameObject flowerPoint = GameObject.Find("FlowerPoint");
            flowerPoint.SetActive(false);
            GameObject flowerPoint1 = GameObject.Find("FlowerPoint (1)");
            flowerPoint1.SetActive(false);
            GameObject flowerPoint2 = GameObject.Find("FlowerPoint (2)");
            flowerPoint2.SetActive(false);
            GameObject flowerPoint3 = GameObject.Find("FlowerPoint (3)");
            flowerPoint3.SetActive(false);
            GameObject flowerPoint4 = GameObject.Find("FlowerPoint (4)");
            flowerPoint4.SetActive(false);
        }
    }
    
}


