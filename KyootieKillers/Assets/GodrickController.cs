using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodrickController : MonoBehaviour {

    [System.Serializable]
    public class MoveSettings{
        public float forwardVel = 15;
        public float rotateVel = 100;
        public float jumpVel = 25;
        public float distToGround = 0.1f;
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
        public string TURN_AXIS = "Horizontal";
        public string JUMP_AXIS = "Jump";
        public string AUTO_ATTACK = "Fire1";
        public string CLICK_MOVE = "Fire2";
    }
    
    public Health HP;
    public bool isAlive = true;
    public bool isDamaging = true;
    public int damageAmount = 50;
    public float takeDamageCooldown = 3f;
    public bool isInvincible;
    public float timeLastTookDamage = 0.0f;

    Animator anim;
    // UnityEngine.AI.NavMeshAgent nmAgent;
    bool isMoving = false;
    bool isJumping = false;
    Quaternion targetRotation;
    Rigidbody rBody;
    float moveInput;
    public float jumpInput;
    float autoAttackInput;
    Vector3 velocity = Vector3.zero;
    public Vector3 moveLocation;
    public Vector3 jumpLocation;
    bool userFirstClick = false;
    
    
    public MoveSettings moveSetting = new MoveSettings();
    public PhysSettings physSetting = new PhysSettings();
    public InputSettings inputSetting = new InputSettings();
    
    public Quaternion TargetRotation{
        get {return targetRotation;}
    }

    // Use this for initialization
	void Start () {
        moveLocation = transform.position;
		targetRotation = transform.rotation;
        HP = GetComponent<Health>();
        anim = GetComponent<Animator>();
        anim.SetFloat("Velocity", 0);
        // nmAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (GetComponent<Rigidbody>()){
            rBody = GetComponent<Rigidbody>();
        } else {
            Debug.LogError("Add rigidbody to object");
        }
        moveInput = jumpInput = autoAttackInput = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (isAlive){
            GetInput();
            LookAtMouse();
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 2, Color.red);
            checkAlive();
        } else {
            anim.SetTrigger("Dead");
            velocity.z = 0f;
        }
	}
    
    void FixedUpdate(){
        if (isAlive){
            Move();
            Jump();
            AutoAttack();
            
            rBody.velocity = transform.TransformDirection(velocity);
        }
    }
    
    void GetInput(){
        moveInput = Input.GetAxis(inputSetting.CLICK_MOVE);
        jumpInput = Input.GetAxis(inputSetting.JUMP_AXIS);
        autoAttackInput = Input.GetAxisRaw(inputSetting.AUTO_ATTACK);
    }
    
	void Move(){
        if( (Mathf.Abs(moveInput) > inputSetting.inputDelay)){
            userFirstClick = true;
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            int layerMask = 1 << 12;
            
            if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)){
                moveLocation = new Vector3(hit.point.x, 0.1f, hit.point.z);
                
            }
        }
        
        
        if (Vector3.Distance(moveLocation, transform.position) <= 0.03f){
            isMoving = false;
            anim.SetFloat("Velocity", 0f);
        } else {
            if (!isJumping){
                transform.position = Vector3.MoveTowards(transform.position, moveLocation, Time.deltaTime * moveSetting.forwardVel);
            } else {
                jumpLocation = new Vector3(moveLocation.x, transform.position.y, moveLocation.z);
                transform.position = Vector3.MoveTowards(transform.position, jumpLocation, Time.deltaTime * moveSetting.forwardVel);
                // transform.position.x = Mathf.MoveTowards(transform.position.x, moveLocation.x, Time.deltaTime * moveSetting.forwardVel);
                // transform.position.z = Mathf.MoveTowards(transform.position.z, moveLocation.z, Time.deltaTime * moveSetting.forwardVel);
            }
            isMoving = true;
            anim.SetFloat("Velocity", moveSetting.forwardVel);
        }
        
    }
    
    void LookAtMouse() {
        if (userFirstClick){            
            Vector3 relativePos = moveLocation - transform.position;
            Vector3 adjustPos = new Vector3(relativePos.x, 0, relativePos.z);
            
            // float mouseX = Input.mousePosition.x;
            // float mouseZ = Input.mousePosition.z;
            // Vector3 adjustPos = new Vector3(mouseX, 0, mouseZ);
            
            Quaternion rotation = Quaternion.LookRotation(adjustPos, Vector3.up);
            // if (Vector3.Distance(moveLocation, transform.position) >= 0.05f){
            if ( (Mathf.Abs(moveLocation.x - transform.position.x) >= 0.05f) || (Mathf.Abs(moveLocation.z - transform.position.z) >= 0.05f)){
                transform.rotation = rotation;
            }
        }
    }
    
    bool Grounded(){
        physSetting.isGrounded = Physics.Raycast(transform.position, Vector3.down, moveSetting.distToGround, moveSetting.ground);
        if (physSetting.isGrounded){
            anim.SetBool("Grounded", true);
        } else {
            anim.SetBool("Grounded", false);
        }
        return physSetting.isGrounded;
    }
    
    void Jump(){
        if ( (jumpInput > 0.05) && Grounded() ){
            velocity.y = moveSetting.jumpVel;
            isJumping = true;
        } else if ( (jumpInput == 0)  && Grounded() ){
            velocity.y = 0;
            isJumping = false;
        } else {
            velocity.y -= physSetting.downAccel;
            isJumping = true;
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
