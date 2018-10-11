using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuClick : MonoBehaviour {
    public AudioSource audioData;
    public AudioClip mySound;
    // Use this for initialization
    void Start () {
        audioData = GetComponent<AudioSource>();
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseEnter()
    {
        audioData.PlayOneShot(mySound);
    }
}
