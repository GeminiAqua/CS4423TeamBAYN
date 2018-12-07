using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnicornBeam : MonoBehaviour
{
    //public Transform startPoint;
    public float attackRange = 15f;
    [SerializeField]
    public int damagePerShot = 20;
    ParticleSystem laserParticles;
    LineRenderer laserLine;
    bool isShooting = false;
    [SerializeField]
    float cooldown = 3f;
    float lastAttackTime = 0;
    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    GameObject player;
    Rigidbody rb;
    Animator animator;


    // Use this for initialization
    void Start () {
        rb = GetComponentInParent<Rigidbody>();
        animator = GetComponentInParent<Animator>();
        laserLine = GetComponentInChildren<LineRenderer>();
        laserParticles = GetComponentInChildren<ParticleSystem>();
        player = GameObject.FindWithTag("Player");
        laserLine.SetWidth(3f, 3f);
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if(!isShooting && Vector3.Distance(rb.transform.position, player.transform.position) <= attackRange)
        {
            ShootBeam();
        }
  
       
       
	}

    private void DisableEffects()
    {
        laserLine.enabled = false;
        laserParticles.Stop();

    }
   
    void ShootBeam()
    {
        animator.SetInteger("animation", 3);
        animator.SetInteger("animation", 7);
        lastAttackTime = Time.timeSinceLevelLoad;
        Debug.Log("IsShooting");
        // timer = 0f;
        isShooting = true;

        // Reset the timer.


        // Play the laser shot audioclip.
  

        // Stop the particles from playing if they were, then start the particles.
       // laserParticles.Stop();
        laserParticles.Play();

        //  Enable the line renderer and set it's first position to be the end of the gun.
        laserLine.enabled = true;
        laserLine.SetPosition(0, new Vector3(0,0,0));
        laserLine.SetPosition(1, new Vector3(0, 15, attackRange));

        // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
        shootRay.origin = laserLine.transform.position;

        //endPointOfRay = Quaternion.AngleAxis(45, Vector3.up) * new Vector3(0, 3, 15);
  
        shootRay.direction = new Vector3(0, 1.5f, 0) + player.transform.position;

        Debug.DrawRay(shootRay.origin, shootRay.direction * 100f, Color.yellow,10);
        // Perform the raycast against gameobjects on the shootable layer and if it hits something...
        if (Physics.Raycast(shootRay, out shootHit, attackRange))
        {

            //    // Try and find an EnemyHealth script on the gameobject hit.
            Health playerHealth = shootHit.collider.GetComponent<Health>();
            Debug.Log("Player Hit!!!: " + shootHit.collider);
            //    // If the EnemyHealth component exist...
            if (playerHealth != null)
            {
                //        Debug.Log("Enemy Health is null");
                //        // ... the enemy should take damage.
                playerHealth.DecrementHealth(damagePerShot);
                //        //shootHit.point
                //        //hitParticles.transform.position = hitPoint;
                //        //hitParticles.Play();


            }
        }

        //    // Set the second position of the line renderer to the point the raycast hit.
        //    gunLine.SetPosition(1, shootHit.point);
        //}
        //// If the raycast didn't hit anything on the shootable layer...
        //else
        //{
        //    // ... set the second position of the line renderer to the fullest extent of the gun's range.
        //    gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        //    // Debug.Log("Missed enemy");
        //}
        Invoke("DoneShooting", 0.5f);

    }

     void DoneShooting()

    {
        Debug.Log("Done Shooting");
        DisableEffects();
        StartCoroutine(WaitBeforeShooting());
        // cooldown = 3f;
    }
    IEnumerator WaitBeforeShooting(){
        Debug.Log("Before wait");
        yield return new WaitForSeconds(cooldown);
        Debug.Log("Done waiting");
        isShooting = false;
    }
}
