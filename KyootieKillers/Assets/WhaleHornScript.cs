using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleHornScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("COLIDE WITH", other.gameObject);
        if (other.gameObject.tag.Equals("Player"))
        {
            Debug.Log("HORN DAMAGE");
            
        }
    }
}
