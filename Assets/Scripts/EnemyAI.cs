using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent agent;

    Animator animZombie;

    int patrolPosition;

    [SerializeField]
    GameObject[] patrolZones;

    bool patrol = true;

    [SerializeField]
    float persecutionRange;

    [SerializeField]
    float attackRange;

    bool persecution = false;

    bool attack = false;

    [SerializeField]
    LayerMask isPlayer;

    [SerializeField]
    bool ifPatrol;

    [SerializeField]
    bool run;
    float speed;

    Vector3 playerPosition;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animZombie = GetComponent<Animator>();
        if(ifPatrol){
            patrolPosition = Random.Range(0, patrolZones.Length - 1);
        }
        if(run){
            speed = 7.0f;
        } else {
            speed = 2.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        // animZombie.SetBool("Run", true);
        // if(animZombie.GetBool("Dead")){
        //    gameObject.GetComponent<EnemyAI>().enabled = false;
        // }
        // agent.SetDestination(player.transform.position);
        if(!animZombie.GetBool("Dead")){
            if(patrol && ifPatrol){
                animZombie.SetBool("Walk", true);
                animZombie.SetBool("Run", false);
                agent.speed = 1.5f;
                agent.SetDestination(patrolZones[patrolPosition].transform.position);
            } else {
                attack = persecution = Physics.CheckCapsule(transform.position, transform.position,
                attackRange, isPlayer);
                if(attack){
                    animZombie.SetBool("Walk", false);
                    animZombie.SetBool("Run", false);
                    animZombie.SetBool("Attack", true);
                    agent.speed = speed;
                    agent.SetDestination(playerPosition);
                } else {
                    animZombie.SetBool("Walk", false);
                    animZombie.SetBool("Run", true);
                    animZombie.SetBool("Attack", false);
                    agent.speed = speed;
                    agent.SetDestination(playerPosition);
                }
            }
        } else {
            agent.speed = 0f;
        }
        if(ifPatrol){
            PersecutionArea();
        } else {
            patrol = false;
        }
    }
    private void OnTriggerStay(Collider other) {
        Debug.Log("hi");
        if(other.gameObject.tag.Equals("PatrolZone")){
            patrolPosition = Random.Range(0, patrolZones.Length - 1);
        }    
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, persecutionRange); 

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);   
    }

    void PersecutionArea(){
        persecution = Physics.CheckCapsule(transform.position, transform.position,
        persecutionRange, isPlayer);
        if(persecution){
            patrol = false;
        } else{
            patrol = true;
        }
    }
}
