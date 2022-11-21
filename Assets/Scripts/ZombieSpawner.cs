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
    void Start()
    {
        posZombie = zombie.transform;
        posZombie.transform.position = new Vector3(transform.position.x, zombie.transform.position.y, transform.position.z);
        spawner = true;
        InvokeRepeating("SpawnZombie", 0f, 0.5f);
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(seconds);
        seconds = DateTime.Now.Second;
        //StartCoroutine(ExampleCoroutine());
    }

    void SpawnZombie(){
        GameObject duplicate = Instantiate(zombie, posZombie);
    }
    
    public void StopSpawn(){
        CancelInvoke();
    }
}
