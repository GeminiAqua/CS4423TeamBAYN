using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedDestroy : MonoBehaviour {

    public float delayTime = 2f;

	void Start () {
		Destroy(gameObject, delayTime);
	}
}
