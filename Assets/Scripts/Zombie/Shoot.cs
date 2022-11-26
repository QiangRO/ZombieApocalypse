using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    Camera cam;

    [SerializeField]
    float range;

    [SerializeField]
    GameObject bullet;

    [SerializeField]
    GameObject shootPosition;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            RaycastShoot();
            //PhysicShoot();
        }
    }

    void RaycastShoot(){
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range)){
            Target target = hit.transform.GetComponent<Target>();
            if(target != null){
                target.TargetHealth();
            }
        }
    }

    void PhysicShoot(){
        Instantiate(bullet, shootPosition.transform.position, 
        shootPosition.transform.rotation);
    }
}
