using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    private Image foregroundImage;
    Health health;

    /// <summary>
    /// The value we want to smoothly move to
    /// </summary>
    private int targetValue;

    /// <summary>
    /// The value used by the bar image
    /// </summary>
    private int actualValue;

    public void OnHurt(int damage){

        targetValue -= damage;
        if (targetValue < 0)
        {
            targetValue = 0;
        }
        else if (targetValue > 100)
        {
            targetValue = 100;
        }

    }


    // Update is called once per frame
    void Update()
    {
        targetValue = health.GetHealth();
        // Move health bar to its target
        if (actualValue < targetValue)
        {
            actualValue++;
        }
        else if (actualValue > targetValue)
        {
            actualValue--;
        }

        if (foregroundImage != null)
        {
            foregroundImage.fillAmount = actualValue / 100f;
        }
    }

    void Awake()
    {
        foregroundImage = gameObject.GetComponent<Image>();
    }

    private void Start()
    {
        actualValue = 100;
        targetValue = 100;
        health = GetComponentInParent<Health>();
    }

}
