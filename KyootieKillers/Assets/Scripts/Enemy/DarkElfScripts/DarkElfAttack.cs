using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkElfAttack:MonoBehaviour  {
    public EnemyAttack enemyAttack;
    public GameObject gameObject;
    void Awake(){
     //   enemyAttack = typeof(GameObject));
    }
 //   private EnemyAttack enemyAttack;

    //public DarkElfAttack(EnemyAttack enemyAttack){
    //    this.enemyAttack = enemyAttack;
    //}
    public void executeAttack()
    {
        enemyAttack.attack();
    }

}
