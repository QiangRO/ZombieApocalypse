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

    [SerializeField]
    bool dropAmmo;
    [SerializeField]
    GameObject ammo;
    
    AudioSource [] deadSounds;
    AudioSource deadSound;
    bool playDeadSound;
    string anim;
    void Start()
    {   
        playDeadSound = true;
        deadSounds = GetComponents<AudioSource>();
        deadSound = deadSounds[Random.Range(0, 2)];
        agent = GetComponent<NavMeshAgent>();
        animZombie = GetComponentInChildren<Animator>();
        Debug.Log(animZombie.gameObject.name + "Aqui");
        if(ifPatrol){
            patrolPosition = Random.Range(0, patrolZones.Length - 1);
        }
        if(run){
            speed = 6.0f;
            anim = "Run";
        } else {
            speed = 1.5f;
            anim = "Walk";
        }
        if(Random.Range(1, 3) == 1){
            dropAmmo = true;
        }
        dropAmmo = true;
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
                    animZombie.SetBool(anim, false);
                    animZombie.SetBool("Attack", true);
                    agent.speed = speed;
                    agent.SetDestination(playerPosition);
                } else {
                    animZombie.SetBool("Walk", false);
                    animZombie.SetBool(anim, true);
                    animZombie.SetBool("Attack", false);
                    agent.speed = speed;
                    agent.SetDestination(playerPosition);
                }
            }
        } else {
            if(dropAmmo){
                GameObject duplicate = Instantiate(ammo, transform);
                dropAmmo = false;
            }
            if(playDeadSound){
                Debug.Log(deadSound);
                deadSound.Play();
                playDeadSound = false;
            }
            agent.speed = 0f;
        }
        if(ifPatrol){
            PersecutionArea();
        } else {
            patrol = false;
        }
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
