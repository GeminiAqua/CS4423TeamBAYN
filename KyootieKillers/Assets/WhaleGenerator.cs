using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleGenerator : MonoBehaviour {
    public static GameObject[] enemies;
    public int numOfEnemies = 0;
    public int enemiesSpawned = 0;
    public string MobType;
    public float startDelay;
    public float spawnCooldown = 7f;
    Vector3 position;
    Quaternion rotation;
    // Use this for initialization
    void Awake () {
        enemies = Resources.LoadAll<GameObject>(MobType);
        startDelay = Random.Range(0, 11);
        InvokeRepeating("SpawnEnemies", startDelay, spawnCooldown);
    }
	
    void SpawnEnemies()
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

}
