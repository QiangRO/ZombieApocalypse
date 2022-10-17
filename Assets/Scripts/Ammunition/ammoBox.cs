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
        if(other.tag == "Player" && !boxUsed){
            ProjectileGun gun = GameObject.Find("BulletGun").GetComponent<ProjectileGun>();
            gun.addBullets();
            boxUsed = true;
            rotSpeed *= 4;
            Destroy(gameObject, 1f);
        }
    }
}
