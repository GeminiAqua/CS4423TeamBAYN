using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTwoController : MonoBehaviour {

	[System.Serializable]
    public class SkillOne{
        public GameObject skillObj; // set manually
        public bool isCasting;
        public float lastCast;
        public float cooldown = 11f;
        public bool alreadyInvoked;
    }
    
    [System.Serializable]
    public class SkillTwo{
        public GameObject skillObj; // set manually
        public bool isPrepping;
        public bool isDonePrepping;
        public bool isCasting = false;
        public int variation;
        public bool hasPickedVar = false;
        public float lastCast;
        public float cooldown = 19f;
        public bool alreadyInvoked;
    }
    
    [System.Serializable]
    public class SkillThree{
        public GameObject skillObj1;
        public GameObject skillObj2;
        public bool alreadySummoned1;
        public bool alreadySummoned2;
        public bool isCasting;
        public float lastCast;
        public float cooldown = 13f;
        public bool hasPickedVar = false;
        public int variation;
        public Vector3 pos1 = new Vector3(50f, 0f, -82f);
        public Vector3 pos2 = new Vector3(-30f, 1f, -87f);
        public bool alreadyInvoked;
    }
    
    public float attackRange = 78f;
    public GameObject target; 
    public SkillOne skillOne = new SkillOne();
    public SkillTwo skillTwo = new SkillTwo();
    public SkillThree skillThree = new SkillThree();
    public float healthPercent;
    private FaceChanger fc;
    public string currentFace = "idle";
    public Animator anim;
    
    private Health HP;
    
    
    public bool isAlive = true;
    
	void Start () {
        skillOne.lastCast = Time.timeSinceLevelLoad - skillOne.cooldown;
        skillTwo.lastCast = Time.timeSinceLevelLoad - skillTwo.cooldown;
        skillTwo.skillObj.SetActive(false);
		fc = GetComponent<FaceChanger>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player");
        HP = GetComponent<Health>();
	}
    
	void Update () {
        CheckAlive();
        if (!skillTwo.isCasting && !skillTwo.isPrepping && isAlive){
            transform.LookAt(target.transform);
        }
        healthPercent = ( (float) HP.currentHealth / (float) HP.startingHealth) * 100;
        float distance = Mathf.Abs(Vector3.Distance(transform.position, target.transform.position));
        if ( (distance <= attackRange )) {
            if (healthPercent > 75){
                UseSkillOne();
            } else if ( (healthPercent <= 75f) && (healthPercent > 50f)){
                currentFace = "surprised";
                UseSkillOne();
                UseSkillTwo();
            } else if ( (healthPercent <= 50f) && (healthPercent > 0f)){
                currentFace = "crying";
                UseSkillOne();
                UseSkillTwo();
                UseSkillThree();
            }
        }
	}
    
    private void UseSkillOne(){
        if (!skillTwo.isCasting && !skillTwo.isPrepping && !skillThree.isCasting){
            if (Time.timeSinceLevelLoad > skillOne.lastCast + skillOne.cooldown){
                anim.SetTrigger("SkillOne");
                skillOne.lastCast = Time.timeSinceLevelLoad;
                skillOne.isCasting = true;
                fc.ChangeEmotion("angry");
                if (!skillOne.alreadyInvoked){
                    skillOne.alreadyInvoked = true;
                    Invoke("EndSkillOne", 2f);
                }
                GameObject attackObj = Instantiate(skillOne.skillObj) as GameObject;
                attackObj.transform.position = new Vector3(target.transform.position.x, 0, target.transform.position.z);
            }
        }
    }
    private void EndSkillOne(){
        ResetIdleFace();
        skillOne.alreadyInvoked = false;
        skillOne.isCasting = false;
    }
    
    private void UseSkillTwo(){
        if(!skillOne.isCasting && !skillThree.isCasting){
            if (Time.timeSinceLevelLoad >= (skillTwo.lastCast + skillTwo.cooldown)){
                if (!skillTwo.isDonePrepping){
                    skillTwo.isPrepping = true;
                } else {
                    skillTwo.isPrepping = false;
                }
                if (skillTwo.isPrepping){
                    fc.ChangeEmotion("ecksdee");
                    float tempFloat1 = 5;
                    if (!skillTwo.hasPickedVar){
                        skillTwo.hasPickedVar = true;
                        skillTwo.variation = Random.Range(0, 2);
                    }
                    if ((transform.rotation.eulerAngles.y >= 0f) && (transform.rotation.eulerAngles.y < 42f)){
                        transform.Rotate(0, tempFloat1 * Time.deltaTime, 0);
                    } else if ( (transform.rotation.eulerAngles.y > 48f) && (transform.rotation.eulerAngles.y <= 90f)){
                        transform.Rotate(0, -tempFloat1 * Time.deltaTime, 0);
                    } else if ( (transform.rotation.eulerAngles.y > 90f) && (transform.rotation.eulerAngles.y < 132f) ){
                        transform.Rotate(0, tempFloat1 * Time.deltaTime, 0);
                    } else if ( (transform.rotation.eulerAngles.y > 138f) && (transform.rotation.eulerAngles.y <= 180f) ){
                        transform.Rotate(0, -tempFloat1 * Time.deltaTime, 0);
                    } else if ( (transform.rotation.eulerAngles.y > 180f) && (transform.rotation.eulerAngles.y < 222f) ){
                        transform.Rotate(0, tempFloat1 * Time.deltaTime, 0);
                    } else if ( (transform.rotation.eulerAngles.y > 228f) && (transform.rotation.eulerAngles.y <= 270f)){
                        transform.Rotate(0, -tempFloat1 * Time.deltaTime, 0);
                    } else if ((transform.rotation.eulerAngles.y > 270f) && (transform.rotation.eulerAngles.y < 312f)){
                        transform.Rotate(0, tempFloat1 * Time.deltaTime, 0);
                    } else if ( (transform.rotation.eulerAngles.y > 318f) && (transform.rotation.eulerAngles.y < 360f) ) {
                        transform.Rotate(0, -tempFloat1 * Time.deltaTime, 0);
                    } else {
                        skillTwo.isCasting = true;
                        skillTwo.isDonePrepping = true;
                    }
                }
                if (skillTwo.isCasting){
                    fc.ChangeEmotion("kiss");
                    skillTwo.skillObj.SetActive(true);
                    if (skillTwo.variation == 0){
                        transform.Rotate(0, 20 * Time.deltaTime, 0);
                        if (!skillTwo.alreadyInvoked){
                            skillTwo.alreadyInvoked = true;
                            Invoke("EndSkillTwo", 6f);
                        }
                    } else if (skillTwo.variation == 1){
                        transform.Rotate(0, -20 * Time.deltaTime, 0);
                        if (!skillTwo.alreadyInvoked){
                            skillTwo.alreadyInvoked = true;
                            Invoke("EndSkillTwo", 6f);
                        }
                    }
                }
            }
        }
    }
    
    private void EndSkillTwo(){
        ResetIdleFace();
        skillTwo.alreadyInvoked = false;
        skillTwo.hasPickedVar = false;
        skillTwo.isCasting = false;
        skillTwo.isPrepping = false;
        skillTwo.isDonePrepping = false;
        skillTwo.skillObj.SetActive(false);
        skillTwo.lastCast = Time.timeSinceLevelLoad;
    }
    
    private void UseSkillThree(){
        if (!skillOne.isCasting && !skillTwo.isCasting && !skillTwo.isPrepping){
            if (Time.timeSinceLevelLoad > (skillThree.lastCast + skillThree.cooldown)){
                fc.ChangeEmotion("hungry");
                if (!skillThree.alreadyInvoked){
                    skillThree.alreadyInvoked = true;
                    Invoke("EndSkillThree", 2f);
                }
                skillThree.isCasting = true;
                if (!skillThree.hasPickedVar){
                    skillThree.hasPickedVar = true;
                    skillThree.variation = Random.Range(0, 2);
                }
                if (!skillThree.alreadySummoned1){
                    anim.SetTrigger("SkillThree");
                    skillThree.alreadySummoned1 = true;
                    GameObject healer = Instantiate(skillThree.skillObj1) as GameObject;
                    if (skillThree.variation == 0){
                        healer.transform.position = skillThree.pos1;
                    } else if (skillThree.variation == 1){
                        healer.transform.position = skillThree.pos2;
                    }
                    if (!skillThree.alreadySummoned2){
                        skillThree.alreadySummoned2 = true;
                        GameObject ad = Instantiate(skillThree.skillObj2) as GameObject;
                        ad.transform.position = transform.position + new Vector3(0, 1f, 10f);
                        ad.GetComponent<Boss2Skill3AdController>().SetTarget(healer);
                    }
                }
            }
        }
        
        
    }
    
    private void EndSkillThree(){
        skillThree.alreadyInvoked = false;
        skillThree.isCasting = false;
        skillThree.hasPickedVar = false;
        skillThree.alreadySummoned1 = false;
        skillThree.alreadySummoned2 = false;
        skillThree.lastCast = Time.timeSinceLevelLoad;
        ResetIdleFace();
    }
    
    private void CheckAlive(){
        if (HP.currentHealth <= 0){
            isAlive = false;
            fc.ChangeEmotion("dead");
            anim.SetTrigger("Dead");
            Destroy(gameObject, 5f);
        }
    }
    
    private void ResetIdleFace(){
        fc.ChangeEmotion(currentFace);
    }
}
