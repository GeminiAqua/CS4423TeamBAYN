using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodrickController : MonoBehaviour {

    [System.Serializable]
    public class MoveSetting{
        public float forwardVel = 15;
        public float rotateVel = 100;
        public float jumpVel = 50;
        public float dashDist = 3f;
        public float dashCD = 1f;
        public float dashDuration = 0.9f;
        public float distToGround = 0.1f;
        public LayerMask ground;
    }
    
    [System.Serializable]
    public class SkillCD{
        public bool castingSpell = false;
        public float one = 1f;
        public float two = 1f;
        public float three = 1f;
        public float four = 1f;
        public bool canCastOne = true;
        public bool canCastTwo = true;
        public bool canCastThree = true;
        public bool canCastFour = false;
        public bool oneRotate = false;
        public float oneDuration = 2f;
        public int fourDamage = 100;
        public float fourKBForce = 1000f;
    }
    
    [System.Serializable]
    public class SkillPrefab{
        public GameObject two;
        public GameObject three;
        public GameObject four;
    }
    
    [System.Serializable]
    public class SkillOne{
        public float XdegreesPerSecond = 0;
        public float YdegreesPerSecond = 540;
        public float ZdegreesPerSecond = 0;
    }
    
    [System.Serializable]
    public class PhysSetting{
        public bool isGrounded;
        public float downAccel = 15f;
    }
    
    [System.Serializable]
    public class InputSetting{
        public float inputDelay = 0.1f;
        public string TURN_AXIS = "Horizontal";
        public string JUMP_AXIS = "Jump";
        public string AUTO_ATTACK = "Fire1";
        public string CLICK_MOVE = "Fire2";
        public string DASH = "Fire3";
    }
    
    [System.Serializable]
    public class ActionBool{
        public bool isAlive = true;
        public bool isInvincible;
        public bool isMoving = false;
        public bool isDashing = false;
        public bool isJumping = false;
        public bool canAttack = true;
        public bool canDash = true;
    }
    
    public Health HP;
    public Mana SP;
    public int damageAmount = 50;
    public float takeDamageCooldown = 2f;
    public float timeLastTookDamage = 0.0f;

    Animator anim;
    
    Quaternion targetRotation;
    Rigidbody rBody;
    Vector3 velocity = Vector3.zero;
    public Vector3 moveLocation;
    bool userFirstClick = false;
    
    
    public MoveSetting moveSetting = new MoveSetting();
    public SkillCD skillCD = new SkillCD();
    public SkillOne skillOne = new SkillOne();
    public SkillPrefab skillPrefab = new SkillPrefab();
    public PhysSetting physSetting = new PhysSetting();
    public InputSetting inputSetting = new InputSetting();
    public ActionBool actionBool = new ActionBool();
    
    private float moveInput;
    private float jumpInput;
    private float autoAttackInput;
    private float dashInput;
    
    public Quaternion TargetRotation{
        get {return targetRotation;}
    }

    // Use this for initialization
	void Start () {
        moveLocation = transform.position;
		targetRotation = transform.rotation;
        HP = GetComponent<Health>();
        SP = GetComponent<Mana>();
        anim = GetComponent<Animator>();
        anim.SetFloat("Velocity", 0);
        skillPrefab.two = Resources.Load("SummonSword") as GameObject;
        skillPrefab.three = Resources.Load("SummonGiant") as GameObject;
        skillPrefab.four = Resources.Load("SummonUlt") as GameObject;
        if (GetComponent<Rigidbody>()){
            rBody = GetComponent<Rigidbody>();
        } else {
            Debug.LogError("Add rigidbody to object");
        }
        moveInput = jumpInput = autoAttackInput = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (actionBool.isAlive){
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
        if (actionBool.isAlive){
            if (skillCD.oneRotate){
                SkillOneRotate();
            }
            if (!skillCD.castingSpell){
                Move();
                // Jump();
                AutoAttack();
                SkillAttacks();
                Dash();
            }
            Grounded();
            
            // rBody.velocity = transform.TransformDirection(velocity);
        }
    }
    
    void GetInput(){
        moveInput = Input.GetAxis(inputSetting.CLICK_MOVE);
        autoAttackInput = Input.GetAxisRaw(inputSetting.AUTO_ATTACK);
        // jumpInput = Input.GetAxisRaw(inputSetting.JUMP_AXIS);
        dashInput = Input.GetAxisRaw(inputSetting.DASH);
    }
    
    void Dash(){
        if ( Input.GetKey("space") ) {
            if (actionBool.canDash){
                clearCanDash();
                Invoke("setCanDash", moveSetting.dashCD);
                
                setIsDashing();
                Invoke("clearIsDashing", moveSetting.dashDuration);
            }
        }
        if (actionBool.isDashing){
            anim.SetBool("Dash", true);
            rBody.MovePosition(transform.position + transform.forward * Time.deltaTime * moveSetting.forwardVel * 2);
            moveLocation = transform.position;
        } else {
            anim.SetBool("Dash", false);
        }
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
            
            if (!actionBool.isDashing){
                // if (!actionBool.isDashing){
                    // transform.position = Vector3.MoveTowards(transform.position, moveLocation, Time.deltaTime * moveSetting.forwardVel);
            rBody.MovePosition(transform.position + transform.forward * Time.deltaTime * moveSetting.forwardVel);
            // rBody.AddForce(transform.forward * moveSetting.forwardVel);
                // } else {
                    // float xzMagnitude = (Mathf.Sqrt(Mathf.Abs(moveLocation.x) * Mathf.Abs(moveLocation.z)));
                    // float xScaled = ( moveLocation.x / xzMagnitude ) * moveSetting.dashDist;
                    // float zScaled = ( moveLocation.z / xzMagnitude ) * moveSetting.dashDist;
                    // Debug.Log(hit.point.x);
                    // Debug.Log(hit.point.z);
                    // moveLocation = new Vector3( xScaled, 0.1f, zScaled);
                // }
            // } else {
                // jumpLocation = new Vector3(moveLocation.x, transform.position.y, moveLocation.z);
                // transform.position = Vector3.MoveTowards(transform.position, jumpLocation, Time.deltaTime * moveSetting.forwardVel);
                /* // transform.position.x = Mathf.MoveTowards(transform.position.x, moveLocation.x, Time.deltaTime * moveSetting.forwardVel);
                // // transform.position.z = Mathf.MoveTowards(transform.position.z, moveLocation.z, Time.deltaTime * moveSetting.forwardVel);
                */
            }
            actionBool.isMoving = true;
            anim.SetFloat("Velocity", moveSetting.forwardVel);
        } else {
            actionBool.isMoving = false;
            anim.SetFloat("Velocity", 0f);
            // velocity = Vector3.zero;
        }  
    }
    
    void LookAtMouse() {
        if (userFirstClick){            
            Vector3 relativePos = moveLocation - transform.position;
            Vector3 adjustPos = new Vector3(relativePos.x, 0, relativePos.z);
            
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            if ( (Mathf.Abs(moveLocation.x - transform.position.x) >= 0.3f) || (Mathf.Abs(moveLocation.z - transform.position.z) >= 0.3f)){
                if (!actionBool.isDashing && !skillCD.oneRotate){
                    transform.rotation = rotation;
                }
            }
        }
    }
    
    void SkillAttacks(){
        if (Input.GetKey(KeyCode.A)){
            if (skillCD.canCastOne){
                skillCD.castingSpell = true;
                UseSkillOne();
            }
        }
        if (Input.GetKey(KeyCode.S)){
            if (skillCD.canCastTwo){
                skillCD.castingSpell = true;
                UseSkillTwo();
            }
        }
        if (Input.GetKey(KeyCode.D)){
            if (skillCD.canCastThree){
                skillCD.castingSpell = true;
                UseSkillThree();
            }
        }
        if (Input.GetKey(KeyCode.F)){
            if (skillCD.canCastFour){
                skillCD.castingSpell = true;
                UseSkillFour();
            }
        }
    }
    
    private void UseSkillOne(){
        anim.SetTrigger("Skill1");
        skillCD.oneRotate = true;
        Invoke("endSkill1", skillCD.oneDuration);
    }
    
    private void SkillOneRotate(){
        transform.Rotate(Random.Range((skillOne.XdegreesPerSecond * Time.deltaTime)/2, skillOne.XdegreesPerSecond * Time.deltaTime), Random.Range((skillOne.YdegreesPerSecond * Time.deltaTime)/2, skillOne.YdegreesPerSecond * Time.deltaTime), Random.Range((skillOne.ZdegreesPerSecond * Time.deltaTime)/2, skillOne.ZdegreesPerSecond * Time.deltaTime));
    }
    
    private void UseSkillTwo(){
        skillCD.canCastTwo = false;
        Invoke("setSkillTwoCD", skillCD.two);
        anim.SetTrigger("Skill2");
        GameObject missile1 = Instantiate(skillPrefab.two) as GameObject;
        missile1.transform.forward = transform.forward;
        missile1.transform.position = transform.position + new Vector3(0, 1, 0) + (transform.forward * 2.5f);
    }
    
    private void UseSkillThree(){
        skillCD.canCastThree = false;
        Invoke("setSkillThreeCD", skillCD.three);
        anim.SetTrigger("Skill3");
        GameObject giant = Instantiate(skillPrefab.three) as GameObject;
        giant.transform.forward = -transform.forward;
        giant.transform.rotation = Quaternion.Euler(165, 90, 90);
        giant.transform.position = transform.position + new Vector3(0, 30, 0) + (transform.forward * 15f);
        
    }
    
    private void UseSkillFour(){
        skillCD.canCastFour = false;
        SP.ConsumeMana();
        anim.SetTrigger("Skill4");
        GameObject ultimate = Instantiate(skillPrefab.four) as GameObject;
        ultimate.transform.position = transform.position;
        SkillFourDoDamage();
    }
    
    private void SkillFourDoDamage(){
        Collider[] nearbyObjects;
        nearbyObjects = Physics.OverlapSphere(gameObject.transform.position, 22);
        foreach (Collider thing in nearbyObjects){
            if ((thing.tag == "Enemy") || (thing.tag == "Boss")){
                thing.gameObject.GetComponent<Health>().DecrementHealth(skillCD.fourDamage);
            }
            if (thing.tag == "Enemy"){
                thing.gameObject.GetComponent<KnockbackScript>().KnockbackEnemy(gameObject, skillCD.fourKBForce);
            }
        }
    }
    
    bool Grounded(){
        if (Physics.Raycast(transform.position, Vector3.down, moveSetting.distToGround, moveSetting.ground) || transform.position.y <= 0){
            physSetting.isGrounded = true;
        } else {
            physSetting.isGrounded = false;
        }
        
        if (physSetting.isGrounded){
            anim.SetBool("Grounded", true);
        } else {
            anim.SetBool("Grounded", false);
            if (transform.position.y > 0.2){
                rBody.AddForce(0, -50, 0, ForceMode.VelocityChange);
            }
        }
        return physSetting.isGrounded;
    }
    
    // void Jump(){
        // if ( Grounded() ){
            // if (Input.GetKey("space")){
                // velocity.y = moveSetting.jumpVel;
                // actionBool.isJumping = true;
            // } else {
                // velocity.y = 0;
                // actionBool.isJumping = false;
            // }
        // } else {
            // velocity.y -= physSetting.downAccel;
            // actionBool.isJumping = true;
        // }
        
        // if ( (jumpInput > 0) && Grounded()){
            // setIsJumping();
            // Invoke("clearIsJumping", 0.25f);
        // }
        
        // if (actionBool.isJumping){
            // velocity.y = moveSetting.jumpVel;
        // } else {
            // if (!Grounded()){
                // velocity.y -= physSetting.downAccel;
            // }
        // }
    // }
    
    void AutoAttack(){
        if ( autoAttackInput > 0 && actionBool.canAttack){
            int layerMask = 1 << 10;
            anim.SetTrigger("AutoAttack");
            actionBool.canAttack = false;
            Invoke("setCanAttack", 0.25f);
        }
    }
    
    void setCanAttack(){
        actionBool.canAttack = true;
    }
    
    void clearCanAttack(){
        actionBool.canAttack = false;
    }
    
    void setCanDash(){
        actionBool.canDash = true;
    }
    
    void clearCanDash(){
        actionBool.canDash = false;
    }
    
    void setIsDashing(){
        actionBool.isDashing = true;
    }
    
    void clearIsDashing(){
        actionBool.isDashing = false;
    }
    
    void setIsJumping(){
        actionBool.isJumping = true;
    }
    
    void clearIsJumping(){
        actionBool.isJumping = false;
        jumpInput = 0f;
    }
    
    void recentlyTookDamage(){
        actionBool.isInvincible = true;
    }
    
    void checkAlive(){
        if (HP.currentHealth <= 0){
            actionBool.isAlive = false;
        }
    }
    
    public void clearCastingSpell(){
        skillCD.castingSpell = false;
    }
    
    public void endSkill1(){
        anim.SetTrigger("Skill1End");
        skillCD.oneRotate = false;
        clearCastingSpell();
        skillCD.canCastOne = false;
        Invoke("setSkillOneCD", skillCD.one);
    }
    
    private void setSkillOneCD(){
        skillCD.canCastOne = true;
    }
    
    private void setSkillTwoCD(){
        skillCD.canCastTwo = true;
    }
    
    private void setSkillThreeCD(){
        skillCD.canCastThree = true;
    }
    
    public void setSkillFourCD(){
        skillCD.canCastFour = true;
    }
    
    public bool getAliveBool(){
        return actionBool.isAlive;
    }
    
    public bool getCanAttackBool(){
        return actionBool.canAttack;
    }
}
