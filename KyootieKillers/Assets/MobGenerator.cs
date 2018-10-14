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


    void Start()
    {
        if (m_Camera = null){
            m_Camera = GetComponent<Camera>();
        }
      
        Random.seed = (int)Time.time;
        //Important note: place your prefabs folder(or levels or whatever) 
        //in a folder called "Resources" like this "Assets/Resources/Prefabs"
        Object[] subListObjects = Resources.LoadAll("Unicorn Mob", typeof(GameObject));
        //This may be sloppy (I've only been programing for a short time) 
        //It works though :) 
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

    }

    void SpawnRandomObject()
    {
        //spawns item in array position between 0 and 100
        int whichItem = Random.Range(0, 10);


        GameObject myObj;
       


        Vector3 pos = new Vector3(
                Random.Range(minExtent.position.x, maxExtent.position.x),
            minExtent.transform.position.y,
                Random.Range(minExtent.position.z, maxExtent.position.z));

        myObj = Instantiate(myListObjects[whichItem], pos, transform.rotation) as GameObject;
       
       // cfb.SetCamera(m_Camera);

        //myObj.transform.position = pos;
    }

    void Update()
    {
   
    }

}


