using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text levelLabel;
    //public GameObject nextLevelLabels;

    public bool gameOver = false;

    private int level;
    public int Level
    {
        get { return level; }
        set
        {
            Debug.Log("should set level");
            level = value;
            if (!gameOver)
            {
                Invoke("ExitLevel", .5f);

            }
            levelLabel.text = "Level: " + (level + 1);
        }
    }
    public Text enemyCountLabel;
   // public GameObject nextEnemyCountLabels;


    private int enemyCount;
    public int EnemyCount
    {
        get { return enemyCount; }
        set
        {
            enemyCount = value;
            if (!gameOver)
            {
                enemyCount = 10;

            }
            enemyCountLabel.text = "Enemies:n " + (enemyCount + 1);
        }
    }
    public void ExitLevel(){
        Debug.Log("Should Exit");
    }
    void Start()
    {
        Level = 0;

        EnemyCount = 0;
    }
}
