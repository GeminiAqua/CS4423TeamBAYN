using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedActive : MonoBehaviour {

    public GameObject warningObj;
    public GameObject delayedObj;
    public float delayTime = 2f;

	void Start () {
		Invoke("Appear", delayTime);
        Invoke("Disappear", delayTime + 1f);
	}
	
    private void Appear(){
        delayedObj.SetActive(true);
    }
    
    private void Disappear(){
        warningObj.SetActive(false);
    }
}
