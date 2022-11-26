using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveController : MonoBehaviour
{
    GirlController girl;
    [SerializeField]
    Camera camera1;
    [SerializeField]
    Camera camera2;

    [SerializeField]
    Camera camera3;
    bool changeCam;
    GameObject spawner;
    ZombieSpawner zspawner;

    [SerializeField]
    GameObject [] spawners;
    void Start(){
        girl = GameObject.FindGameObjectWithTag("Girl").GetComponent<GirlController>();
        camera1.enabled = true;
        camera2.enabled = false;
        camera3.enabled = false;
        changeCam = true;
        DesactiveSpawns();
    }
    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player" && changeCam){
            girl.Rescue();
            camera1.enabled = false;
            camera2.enabled = true;
            camera3.enabled = false;
            // spawner.SetActive(true);
            ActiveSpawns();
            if(changeCam) StartCoroutine("ChangeCameras1");
        }
    }

    private IEnumerator ChangeCameras1()
    {
        changeCam = false;
        yield return new WaitForSeconds(4f);
        camera1.enabled = false;
        camera2.enabled = false;
        camera3.enabled = true;
        // zspawner.StopSpawn();
        // spawner.SetActive(false);
        DesactiveSpawns();
        StartCoroutine("ChangeCameras2");
    }

    private IEnumerator ChangeCameras2(){
        changeCam = false;
        yield return new WaitForSeconds(4f);
        camera1.enabled = true;
        camera2.enabled = false;
        camera3.enabled = false;
    }

    void ActiveSpawns(){
        foreach(GameObject spawn in spawners){
            spawn.SetActive(true);
        }
    }

    void DesactiveSpawns(){
        foreach(GameObject spawn in spawners){
            spawn.SetActive(false);
            zspawner = spawn.GetComponent<ZombieSpawner>();
            zspawner.StopSpawn();
        }
    }
}
