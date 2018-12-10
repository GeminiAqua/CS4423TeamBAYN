using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public bool immune;


    // Use this for initialization
    void Start()
    {
        resetHealthToStart();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public int GetHealth()
    {
        return this.currentHealth;
    }
    public void AddHealth(int increaseValue)
    {
        if (!isHealthfull())
        {
            this.currentHealth += increaseValue;
        }
        //make sure not to go over
        if (currentHealth > startingHealth)
            resetHealthToStart();
    }
    public void DecrementHealth(int decrementValue)
    {   
        if(immune == false)
            this.currentHealth -= decrementValue;
        else
        {
            return;
        }
    }

    public bool isHealthfull()
    {
        if (currentHealth < startingHealth)
        {
            return false;
        }
        return true;
    }
    
    public void resetHealthToStart()
    {
        this.currentHealth = startingHealth;
    }

    public void beImmune(){
        immune = true;
    }

    public void beNotImmune(){
        immune = false;
    }

}
