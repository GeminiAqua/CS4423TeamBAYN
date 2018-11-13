using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodrickCameraController : MonoBehaviour {

    public Transform target;
    public float lookSmooth = 0.09f;
    public Vector3 offsetFromTarget = new Vector3(0, 6, -8);
    public float xTilt = 10;
    public float yTilt = 30;
    public bool ToggleRotate;
    
    Vector3 destination = Vector3.zero;
    public GodrickController godrickController;
    float rotateVel = 0;
    
	// Use this for initialization
	void Start () {
		SetCameraTarget(target);
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (godrickController.isAlive){
            MoveToTarget();
            if (ToggleRotate == true){
                LookAtTarget();
            }
        }
	}
    
    // Can set new target if needed
    public void SetCameraTarget(Transform t){
        target = t;
        
        if (target != null){
            if (target.GetComponent<GodrickController>()){
                godrickController = target.GetComponent<GodrickController>();
            } else {
                Debug.LogError("Godrick doesn't have controller");
            }
        } else {
            Debug.LogError("Camera has no target");
        }
    }
    
    void MoveToTarget(){
        destination = godrickController.TargetRotation * offsetFromTarget;
        destination += target.position;
        transform.position = destination;
    }
    
    void LookAtTarget(){
        float eulerYAngel = Mathf.SmoothDampAngle(transform.eulerAngles.y, target.eulerAngles.y, ref rotateVel, lookSmooth);
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, eulerYAngel, 0);
    }
}
