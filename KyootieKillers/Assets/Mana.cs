using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour {

    public int startingMana = 0;
    public int currentMana;
    public int maxMana = 100;
    private GodrickController player;
    
    public bool RegenManaForTest = false;
    

	void Start () {
		player = gameObject.GetComponent<GodrickController>();
        if (!RegenManaForTest){
            ConsumeMana();
        } else {
            currentMana = maxMana;
        }
	}
	
	void Update () {
		CheckManaFull();
        if (RegenManaForTest){
            InfiniteRegen();
        }
	}
    
    private void CheckManaFull(){
        if (currentMana >= maxMana){
            player.setSkillFourCD();
        }
    }
    
    public void IncrementMana(int value){
        currentMana += value;
    }
    
    public void ConsumeMana(){
        currentMana = startingMana;
    }
    
    private void InfiniteRegen(){
        if (currentMana < maxMana){
            currentMana += 1;
        }
    }
}
