using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobGenerator : MonoBehaviour
{

    //define a list
    public static List<GameObject> myListObjects = new List<GameObject>();
    //I used this to keep track of the number of objects I spawned in the scene.
    public static int numSpawned = 0;
    public int numToSpawn = 10;
    Vector3 startPosition;
    public Transform minExtent;
    public Transform maxExtent;
    public Camera m_Camera;
    private CameraFacingBillboard cfb;
    public string MobType;
    Object[] subListObjects;
    public int enemynum = 0;
    private bool start = false;
    public GameObject boss;
    public bool made = false;


    private void Awake()
    {
        //boss.SetActive(false);
    }
    void Start()
    {

        if (m_Camera == null){
            m_Camera = GetComponent<Camera>();
        }
      
        Random.seed = (int)Time.time;
        //Important note: place your prefabs folder(or levels or whatever) 
        //in a folder called "Resources" like this "Assets/Resources/Prefabs"
        subListObjects = Resources.LoadAll(MobType, typeof(GameObject));
       
        foreach (GameObject subListObject in subListObjects)
        {
            GameObject lo = (GameObject)subListObject;

            myListObjects.Add(lo);
        }
        startPosition = transform.position;

        for (int i = 0; i < numToSpawn; i++)
        {
            SpawnRandomObject();
        }
        start = true;

    }

    void SpawnRandomObject()
    {
        //spawns item in array position between 0 and 100
        int whichItem = Random.Range(0, subListObjects.Length-1);

        
        GameObject myObj;
       


        Vector3 pos = new Vector3(
                Random.Range(minExtent.position.x, maxExtent.position.x),
            minExtent.transform.position.y,
                Random.Range(minExtent.position.z, maxExtent.position.z));

        myObj = Instantiate(myListObjects[whichItem], pos, transform.rotation) as GameObject;
        enemynum++;
       // cfb.SetCamera(m_Camera);

        //myObj.transform.position = pos;
    }

    void Update()
    {
        // if (Time.timeSinceLevelLoad % 10f == 0f){
            // Debug.Log("Spawned mob");
            // SpawnRandomObject();
        // }

        if (made)
        {
            /*if(boss == null)
            {
                    LoadScene sm = GetComponent<LoadScene>();
                    sm.LoadByIndex(2);
            }*/
            return;
        }
        if(enemynum <= 0 && start)
        {
            Debug.Log("SPAWN BOSS");
            boss.SetActive(true);
            boss.GetComponent<Health>().isBoss = true;
            made = true;
        }
    }

}


