﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour {

	public EventSystem eventSystem;
	public GameObject selectedObj;
	private bool buttonSelected;
	// Use this for initializa

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetAxisRaw("Vertical") != 0 && buttonSelected == false )
		{
			eventSystem.SetSelectedGameObject(selectedObj);
			buttonSelected = true;
		}
	}
    private void OnDisable()
    {
        buttonSelected = false;
    }
}