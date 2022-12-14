using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GirlController : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    [SerializeField]
    bool waitRescue;
    
    NavMeshAgent girl;

    bool follow;
    bool wait;

    [SerializeField]
    float waitRange;

    [SerializeField]
    LayerMask isPlayer;
    
    Vector3 playerPosition;

    AudioSource girlScream;
    void Start()
    {
        girlScream = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        girl = GetComponent<NavMeshAgent>();
        waitRescue = true;

    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        if(waitRescue){
            animator.SetBool("Wary", true);
        } else {
            if(!GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerHealth>().isDead){
                animator.SetBool("Wary", false);
                if(follow){
                    girl.SetDestination(playerPosition);
                    animator.SetBool("Run", true);
                    animator.SetBool("Idle", false);
                } else {
                    animator.SetBool("Run", false);
                    animator.SetBool("Idle", true);
                }
            } else {
                if(!animator.GetBool("Wary")){
                    girlScream.Play();
                }
                animator.SetBool("Wary", true);
                animator.SetBool("Run", false);
                animator.SetBool("Idle", false);
                girl.velocity = new Vector3(0, 0, 0);
            }
        }
        FollowArea();
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, waitRange);
    }

    void FollowArea(){
        wait = Physics.CheckCapsule(transform.position, transform.position,
        waitRange, isPlayer);
        if(wait){
            follow = false;
        } else {
            follow = true;
        }
    }

    public void Rescue(){
        waitRescue = false;
    }
}
