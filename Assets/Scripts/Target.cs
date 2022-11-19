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
    void Start(){
        anim = gameObject.GetComponentInParent<Animator>();
    }

    public void TargetHealth(){
        health -= damage;
        if(health <= 0){
            anim.SetBool("Dead", true);
            Destroy(zombi, 5f);
        }
    }
}
