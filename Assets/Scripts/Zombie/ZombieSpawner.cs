using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ZombieSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject zombie;
    int seconds;
    bool spawner;
    Transform posZombie;
    
    [SerializeField]
    float spawnTime;
    void Start()
    {

        spawner = true;
        /*1. Funcion que spawnea zombies
        2. tiempo que tarda en iniciar el spawn
        3. el intervalo de tiempo entre spawn*/
        InvokeRepeating("SpawnZombie", 0f, spawnTime);
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(seconds);
        seconds = DateTime.Now.Second;
        //StartCoroutine(ExampleCoroutine());
    }

    void SpawnZombie(){
        posZombie = zombie.transform;
        posZombie.transform.position = new Vector3(transform.position.x, zombie.transform.position.y, gameObject.transform.position.z);
        GameObject duplicate = Instantiate(zombie, posZombie);
    }
    
    public void StopSpawn(){
        CancelInvoke();
    }
}
