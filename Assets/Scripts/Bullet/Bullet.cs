using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float velocity = 15;

    [SerializeField]
    float timeDestroy;


    void Start()
    {
        StartCoroutine("TimeToDestroy");
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other) {
        Target target = other.gameObject.GetComponent<Target>();
        if(target != null){
            target.TargetHealth();
            Destroy(gameObject);
        }
    }


    void Update()
    {
        transform.Translate(Vector3.up * velocity * Time.deltaTime);

    }

    IEnumerator TimeToDestroy(){
        yield return new WaitForSeconds(timeDestroy);
        Destroy(gameObject);
    }
}
