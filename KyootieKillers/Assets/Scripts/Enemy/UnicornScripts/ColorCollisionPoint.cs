using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ColorCollisionPoint : MonoBehaviour {
	public GameObject unicornBoss;
	public string color = "green";
	private float timeLeft = 2f;

	private double startTime = 0.0;
	private double startTimeCooldown = 0.0;
	public double timeToGetOff;
	public double timeToStay;
	float currCountdownValue;

	bool onFlower = false;
	bool cooldown = false;

    public float lastHitTime;
    public float tickTime = 1f;
    public int damage = 10;
    public float delayTime = 2f;

    void Start () {
		unicornBoss = GameObject.Find("Boss1");
	}


	// Update is called once per frame
	void Update () {
		if(onFlower == true){
			startTime += Time.deltaTime;
			//Debug.Log(startTime);
		}if (cooldown == true) {
			startTimeCooldown += Time.deltaTime;
			Debug.Log(startTimeCooldown);
			if (startTimeCooldown > timeToGetOff)
				cooldown=false;
		}
	}
	 
	void OnTriggerStay(Collider c) {
		string bossColor = unicornBoss.GetComponent<UnicornBossAI>().getColor();
		//unicornBoss.getColor();
		
		if (c.tag == "Player") {
			if(color == bossColor) {
				onFlower = true;
				if(startTime >= timeToStay) {

				unicornBoss.GetComponent<UnicornBossAI>().incrementColorIndex();
				unicornBoss.GetComponent<UnicornBossAI>().setColor();
				unicornBoss.GetComponent<UnicornBossAI>().PickColor();
				unicornBoss.GetComponent<UnicornBossAI>().setActiveColor();
				cooldown = true;
				Debug.Log("UR STANDING IN "+color+" BOSS COLOR "+bossColor);
				startTimeCooldown = 0.0;
				}
			} else if (cooldown == false)
			{
                if (Time.time > (lastHitTime + tickTime))
                {
                    Debug.Log("WRONG COLOR, TAKE DAMAGE");
                    lastHitTime = Time.time;
                    c.gameObject.GetComponent<Health>().DecrementHealth(damage);
                }
                    
			}
		}
	}

	void OnTriggerExit(Collider other)
 	{
   		if(other.tag == "Player") 
     	{
			cooldown = false; 
			onFlower = false;
      		Debug.Log("Player OnTriggerExit"); 
     		ResetTimer();
     	}
 	}

 	public void ResetTimer()
 	{
   		startTime = 0.0;
 	}
}
