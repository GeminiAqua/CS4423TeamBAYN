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
    
    public Health HP;
    public bool isAlive = true;
    public bool isDamaging = true;
    public int damageAmount = 50;
    public float takeDamageCooldown = 3f;
    public bool isInvincible;
    public float timeLastTookDamage = 0.0f;

    Animator anim;
    Quaternion targetRotation;
    Rigidbody rBody;
    float forwardInput;
    float turnInput;
    float jumpInput;
    float autoAttackInput;
    Vector3 velocity = Vector3.zero;
    Bounds bounds;
    
    
    public MoveSettings moveSetting = new MoveSettings();
    public PhysSettings physSetting = new PhysSettings();
    public InputSettings inputSetting = new InputSettings();
    
    public Quaternion TargetRotation{
        get {return targetRotation;}
    }

    // Use this for initialization
	void Start () {
        bounds = new Bounds(new Vector3(0, 0, 0), new Vector3(1f, 1f, 1f));
		targetRotation = transform.rotation;
        HP = GetComponent<Health>();
        anim = GetComponent<Animator>();
        anim.SetFloat("VelocityForward", 0);
        if (GetComponent<Rigidbody>()){
            rBody = GetComponent<Rigidbody>();
        } else {
            Debug.LogError("Add rigidbody to object");
        }
        forwardInput = turnInput = jumpInput = autoAttackInput = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (isAlive){
            GetInput();
            Turn();
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 2, Color.red);
            checkAlive();
        } else {
            anim.SetTrigger("Dead");
            velocity.z = 0f;
        }
	}
    
    void FixedUpdate(){
        if (isAlive){
            Run();
            Jump();
            AutoAttack();
            
            rBody.velocity = transform.TransformDirection(velocity);
        }
    }
    
    void GetInput(){
        forwardInput = Input.GetAxis(inputSetting.FORWARD_AXIS);
        turnInput = Input.GetAxis(inputSetting.TURN_AXIS);
        jumpInput = Input.GetAxisRaw(inputSetting.JUMP_AXIS);
        autoAttackInput = Input.GetAxisRaw(inputSetting.AUTO_ATTACK);
    }
    
	void Run(){
        if ((transform.position.x > 0.5f) && (transform.position.x < 199.5f) && (transform.position.z > 0.5f) && (transform.position.z < 199.5f)){
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
    
    void AutoAttack(){
        if ( autoAttackInput > 0 && isDamaging){
            int layerMask = 1 << 10;
            anim.SetTrigger("AutoAttack");
            isDamaging = false;
            Invoke("canDamage", 0.25f);
            
            RaycastHit hit;
            if (Physics.Raycast (transform.position, transform.TransformDirection(Vector3.forward), out hit, 2f, layerMask)) {
                if (hit.transform.gameObject.tag.Equals("Enemy")){
                    Debug.Log("Hit " + hit.transform.gameObject.name + "for " + damageAmount);
                    hit.transform.gameObject.GetComponent<Health>().DecrementHealth(damageAmount);
                }
            }
        }
    }
    
    void canDamage(){
        isDamaging = true;
    }
    
    void recentlyTookDamage(){
        isInvincible = true;
    }
    
    void checkAlive(){
        if (HP.currentHealth <= 0){
            isAlive = false;
        }
    }
}
