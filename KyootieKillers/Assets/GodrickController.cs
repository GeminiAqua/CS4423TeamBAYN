using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodrickController : MonoBehaviour {

    [System.Serializable]
    public class MoveSettings{
        public float forwardVel = 15;
        public float rotateVel = 100;
        public float jumpVel = 25;
        public float distToGround = 0.02f;
        public LayerMask ground;
    }
    
    [System.Serializable]
    public class PhysSettings{
        public bool isGrounded;
        public float downAccel = 2f;
    }
    
    [System.Serializable]
    public class InputSettings{
        public float inputDelay = 0.1f;
        public string FORWARD_AXIS = "Vertical";
        public string TURN_AXIS = "Horizontal";
        public string JUMP_AXIS = "Jump";
        public string AUTO_ATTACK = "Fire1";
    }
    
    Animator anim;
    Quaternion targetRotation;
    Rigidbody rBody;
    float forwardInput;
    float turnInput;
    float jumpInput;
    Vector3 velocity = Vector3.zero;
    
    public MoveSettings moveSetting = new MoveSettings();
    public PhysSettings physSetting = new PhysSettings();
    public InputSettings inputSetting = new InputSettings();
    
    public Quaternion TargetRotation{
        get {return targetRotation;}
    }

    // Use this for initialization
	void Start () {
		targetRotation = transform.rotation;
        anim = GetComponent<Animator>();
        anim.SetFloat("VelocityForward", 0);
        if (GetComponent<Rigidbody>()){
            rBody = GetComponent<Rigidbody>();
        } else {
            Debug.LogError("Add rigidbody to object");
        }
        forwardInput = turnInput = jumpInput = 0;
	}
	
	// Update is called once per frame
	void Update () {
		GetInput();
        Turn();
	}
    
    void FixedUpdate(){
        Run();
        Jump();
        
        rBody.velocity = transform.TransformDirection(velocity);
    }
    
    void GetInput(){
        forwardInput = Input.GetAxis(inputSetting.FORWARD_AXIS);
        turnInput = Input.GetAxis(inputSetting.TURN_AXIS);
        jumpInput = Input.GetAxisRaw(inputSetting.JUMP_AXIS);
    }
    
	void Run(){
        if (Mathf.Abs(forwardInput) > inputSetting.inputDelay){
            // rBody.velocity = transform.forward * forwardInput * moveSetting.forwardVel;
            velocity.z = moveSetting.forwardVel * forwardInput;
            anim.SetFloat("VelocityForward", velocity.z);
        } else {
            // rBody.velocity = Vector3.zero;
            velocity.z = 0;
            anim.SetFloat("VelocityForward", 0);
        }
    }
    
    void Turn() {
        if (Mathf.Abs(turnInput) > inputSetting.inputDelay){
            targetRotation *= Quaternion.AngleAxis(moveSetting.rotateVel * turnInput * Time.deltaTime, Vector3.up);
        }
        
        transform.rotation = targetRotation;
    }
    
    bool Grounded(){
        physSetting.isGrounded = Physics.Raycast(transform.position, Vector3.down, moveSetting.distToGround, moveSetting.ground);
        if (physSetting.isGrounded){
            anim.SetBool("isGrounded", true);
        } else {
            anim.SetBool("isGrounded", false);
        }
        return physSetting.isGrounded;
    }
    
    void Jump(){
        if ( (jumpInput > 0) && Grounded() ){
            velocity.y = moveSetting.jumpVel;
        } else if ( (jumpInput == 0)  && Grounded() ){
            velocity.y = 0;
        } else {
            velocity.y -= physSetting.downAccel;
        }
    }
}
