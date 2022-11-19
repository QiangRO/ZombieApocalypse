using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ammoBox : MonoBehaviour
{
    bool boxUsed;
    int rotSpeed;
    // Start is called before the first frame update
    void Start()
    {
        boxUsed = false;
        rotSpeed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,rotSpeed,0));
    }

    void OnTriggerEnter(Collider other){
        ProjectileGun gun = GameObject.Find("GunContainer").GetComponentInChildren<ProjectileGun>();
        if(other.tag == "Player" && !boxUsed && gun != null){
            gun.addBullets();
            boxUsed = true;
            rotSpeed *= 4;
            Destroy(gameObject, 1f);
        }
    }
}
