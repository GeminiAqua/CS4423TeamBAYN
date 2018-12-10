using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2ManagerScript : MonoBehaviour {

    public GameObject boss; // set manually
    public int currKills;
    public int reqKills = 100;
    private bool alreadySpawned = false;
    public Vector3 spawnLocation = new Vector3(0, -1, 150);
	// Use this for initialization
	void Start () {
		currKills = 0;
        boss = Resources.Load("Boss2") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (currKills >= reqKills){
            if (!alreadySpawned){
                alreadySpawned = true;
                SpawnBoss();
            }
        }
	}
    
    private void SpawnBoss(){
        GameObject temp = Instantiate (boss) as GameObject;
        boss.transform.position = spawnLocation;
    }
    
    public void IncrementCount(){
        currKills++;
    }
}
