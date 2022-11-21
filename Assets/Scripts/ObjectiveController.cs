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
    bool changeCam;
    [SerializeField]
    GameObject spawner;
    ZombieSpawner zspawner;
    void Start(){
        girl = GameObject.FindGameObjectWithTag("Girl").GetComponent<GirlController>();
        camera1.enabled = true;
        camera2.enabled = false;
        changeCam = true;
        zspawner = spawner.GetComponent<ZombieSpawner>();
        spawner.SetActive(false);
    }
    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player" && changeCam){
            girl.Rescue();
            camera1.enabled = false;
            camera2.enabled = true;
            spawner.SetActive(true);
            if(changeCam) StartCoroutine("ChangeCameras");
        }
    }

    private IEnumerator ChangeCameras()
    {
        changeCam = false;
        yield return new WaitForSeconds(2f);
        camera1.enabled = true;
        camera2.enabled = false;
        zspawner.StopSpawn();
        spawner.SetActive(false);
    }
}
