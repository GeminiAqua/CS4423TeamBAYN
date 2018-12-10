using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIScript : MonoBehaviour {
    public Transform target;
    GameObject player;
    NavMeshAgent agent;
    public float minDistance = 0f;
    public GameObject spell;
    public Transform shootPoint;
    public float shootRate = 4f;
    private float shootTimer = 0f;
    bool canShoot = false;
    bool playerFound = false;
    public bool isShooter = false;

    public float ShootForce = 1;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
	
	// Update is called once per frame
	void Update () {
        agent.SetDestination(target.position);
        float distanceToEnemy = Vector3.Distance(transform.position, target.transform.position);
        agent.stoppingDistance = minDistance;
        if (distanceToEnemy <= minDistance && shootTimer <= 0f)
        {
            canShoot = true;
            Shoot();
            canShoot = false;
            shootTimer = 1f / shootRate;
        }
        else
        {
            canShoot = false;
        }
        shootTimer -= Time.deltaTime;
    }

    private void Shoot()
    {
        playerFound = true;
        if (canShoot) StartCoroutine("SpellCast");
        if (player != null)
        {
            Ray ray = new Ray(shootPoint.position, (target.position - shootPoint.position));
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    //Debug.Log("HIT DETECTED");
                    player = hit.collider.gameObject;
                    playerFound = true;
                }
                else playerFound = false;
            }
        }
        else playerFound = false;

        //GameObject boom = Instantiate(spell, shootPoint.position, shootPoint.rotation);
        //Debug.Log("SPELL CAST");
        //StopCoroutine("CastSpell");
        //DestroyImmediate(boom);
    }

    IEnumerator SpellCast()
    {
        //Debug.Log("SpellCast CALLED");
        canShoot = false;
        //playerFound = true;
        while (playerFound)
        {
            //Debug.Log("PLAYER FOUND");
            GameObject s = Instantiate(spell, shootPoint.position, shootPoint.rotation);
            s.GetComponent<Rigidbody>().AddForce(transform.forward * ShootForce);
            yield return new WaitForSeconds(2);
            Destroy(s);
        }
        yield return null;
    }

}
