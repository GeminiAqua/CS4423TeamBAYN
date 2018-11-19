using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleGenerator : MonoBehaviour {
    public static GameObject[] enemies;
    public int numOfEnemies = 0;
    public int enemiesSpawned = 0;
    public string MobType;
    Vector3 position;
    Quaternion rotation;
    // Use this for initialization
    void Start () {
        enemies = Resources.LoadAll<GameObject>(MobType);
        SpawnEnemies(numOfEnemies);
    }
	
    void SpawnEnemies(int numOfEnemies)
    {
        position = transform.position;
        rotation = transform.rotation;

        int length = enemies.Length;
        for (int i = 0; i < numOfEnemies; i++)
        {
            int enemyIndex = Random.Range(0, length);
            GameObject enemy = Instantiate(enemies[enemyIndex], position, rotation);
            enemiesSpawned++;
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
