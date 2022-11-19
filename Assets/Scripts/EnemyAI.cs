using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent agent;

    [SerializeField]
    GameObject player;

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

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animZombie = GetComponent<Animator>();
        patrolPosition = Random.Range(0, patrolZones.Length - 1);
    }

    // Update is called once per frame
    void Update()
    {
        // animZombie.SetBool("Run", true);
        // if(animZombie.GetBool("Dead")){
        //    gameObject.GetComponent<EnemyAI>().enabled = false;
        // }
        // agent.SetDestination(player.transform.position);
        if(!animZombie.GetBool("Dead")){
            if(patrol){
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
                    agent.speed = 7.0f;
                    agent.SetDestination(player.transform.position);
                } else {
                    animZombie.SetBool("Walk", false);
                    animZombie.SetBool("Run", true);
                    animZombie.SetBool("Attack", false);
                    agent.speed = 7.0f;
                    agent.SetDestination(player.transform.position);
                }
            }
        } else {
            agent.speed = 0f;
        }
        PersecutionArea();
    }
    private void OnTriggerStay(Collider other) {
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
