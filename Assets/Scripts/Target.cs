using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    int health = 50;

    [SerializeField]
    int damage = 10;
    Animator anim;
    [SerializeField]
    GameObject zombi;

    [SerializeField]
    bool dropAmmo;
    [SerializeField]
    GameObject ammo;
    bool collidersOff;

    void Start(){
        anim = gameObject.GetComponentInParent<Animator>();
        if(Random.Range(1, 3) == 1){
            dropAmmo = true;
        }
        dropAmmo = true;
        collidersOff = true;
    }

    public void TargetHealth(){
        health -= damage;
        if(health <= 0){
            anim.SetBool("Dead", true);
            if(collidersOff){ColliderOff();}

            if(dropAmmo){
                GameObject duplicate = Instantiate(ammo, zombi.transform);
                dropAmmo = false;
            }
            Destroy(zombi, 5f);
        }
    }

    void ColliderOff(){
        foreach(Collider col in zombi.GetComponents<Collider>())
        {
          col.enabled = false;
        }
        foreach(Collider col in zombi.GetComponentsInChildren<Collider>()){
            col.enabled = false;
        }
        collidersOff = false;
    }
}
