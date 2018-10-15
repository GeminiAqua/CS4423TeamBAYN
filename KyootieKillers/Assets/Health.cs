using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;


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
        this.currentHealth -= decrementValue;
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

}
