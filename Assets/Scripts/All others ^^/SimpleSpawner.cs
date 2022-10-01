using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSpawner : MonoBehaviour
{
    public GameObject[] ObjectToSpawn;
    public Transform[] SpawnPositions;
    public int amount;

    public float minStartTime;
    public float maxStartTime;
    float timeBetween;
    void Update()
    {
        if (timeBetween <= 0 && amount >= 1)
        {
            int randomObject = Random.Range(0, ObjectToSpawn.Length);
            int randomPosition = Random.Range(0, SpawnPositions.Length);

            Instantiate(ObjectToSpawn[randomObject], SpawnPositions[randomPosition].position, transform.rotation);
            timeBetween = Random.Range(minStartTime, maxStartTime);
            amount--;
        }
        else
            timeBetween -= Time.deltaTime;
    }
}
