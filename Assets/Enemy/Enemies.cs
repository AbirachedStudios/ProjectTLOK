using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using UnityEngine.VFX;


//public enum AIState
//{
//    NullState= 0,
//    Spawn,
//    Idle,
//    Patrol,
//    Chase,
//    Jump,
//    Attack,
//    Range
//}


//public enum Transicion
//{
//    NullTransition = 0,
//    Initialize, //spawn
//    NoEnemyFound, //Patrol
//    NoInteraction, //Idle
//    EnemyFound, //Start Chase
//    EnemyInRange, //Start Attack
//    EnemyInShoot, //Shoot
//    EnemyInJump //Jump
//}


public enum Type
{
    Base,
    Electric,
    Magical,
    Radioactive
}


public abstract class Enemies : MonoBehaviour
{
    [SerializeReference]
    public NavMeshAgent agent;
    public GameObject[] waypoints;
    public GameObject player;
    public Rigidbody rb;
    protected int currentWaypointIndex = 0;
    public Animator anim;
    public float sightRange;
    public float jumpRange = 5f;
    protected float lastAttackTime = -Mathf.Infinity;
    public float attackCooldown;
    public float rangedAttackRange;
    public float attackRange;
    public float chaseSpeed;
    public Vector3 target;
    public float jumpHeight = 5f;
    public bool isJumping = false;
    protected Vector3 jumpTarget;
    protected float lastJumpTime = -Mathf.Infinity; // Initialize to allow the first jump.
    public bool hasJumped = false; // Add this flag
    public GameObject projectilePrefab; // Prefab of the projectile to spawn
    public Transform projectileSpawnPoint; // Point where the projectile should spawn
    
    public float rangedAttackCooldown;
    public float patrolSpeed;
    protected float lastRangedAttackTime = -Mathf.Infinity;
    public bool isNinja = false;
    public float retreatDistance = 5f; // Distance to retreat after melee attack
    public bool ranged = false;

    //public AIState ID { get { return estadoID; } }

    private void Start()
    {
        waypoints = GameObject.FindGameObjectsWithTag("Waypoints");
        agent = GetComponentInParent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("YBot");
        rb = GetComponentInParent<Rigidbody>();
        anim = GetComponentInParent<Animator>();
    }

    public void Update()
    {
        target = player.transform.position;

        FaceTarget(target);
    }

    public void GotoNextWaypoint()
    {
        if (waypoints.Length == 0) return;

        agent.SetDestination(waypoints[currentWaypointIndex].transform.position);
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }

    

    public void Jump()
    {

        if (player == null) return;

       
       
    }

    public Vector3 CalculateJumpVelocity(Vector3 target, float gravityScale)
    {
        float gravity = Physics.gravity.y * gravityScale;
        float heightDifference = target.y - rb.position.y;
        float distance = Vector3.Distance(new Vector3(rb.position.x, 0, rb.position.z), new Vector3(target.x, 0, target.z));

        float time = Mathf.Sqrt(-2 * heightDifference / gravity) + Mathf.Sqrt(2 * (distance - 0) / -gravity);

        Vector3 velocity = new Vector3(
            (target.x - rb.position.x) / time,
            (target.y - rb.position.y) / time - (0.5f * gravity * time),
            (target.z - rb.position.z) / time
        );

        return velocity;
    }

    public bool IsGrounded()
    {
        RaycastHit hit;
        return Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, out hit, 0.2f);
    }
    

    public bool CheckPlayerInSight()
    {
        //if (player == null) return false;

        Vector3 direction = player.transform.position - agent.transform.position;
        float distance = direction.magnitude;

        if (distance <= sightRange)
        {
            RaycastHit hit;
            if (Physics.Raycast(agent.transform.position + Vector3.up, direction.normalized, out hit, sightRange))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void FaceTarget(Vector3 destination)
    {
        Vector3 lookPos = destination - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5f);
    }

    //IEnumerator Slash()
    //{
    //    ef.enabled = true;
    //    //ef.visualEffectAsset = effect[Random.Range(0, effect.Length)];

    //    yield return new WaitForSeconds(1);

    //    ef.enabled = false;
    //}

    //Esto se utiliza para agregar transiciones y estados
    //public void AgregarTransicion(Transicion key, AIState value) //key transiciones - value estado
    //{
    //    if (!diccionario.ContainsKey(key) && !diccionario.ContainsValue(value))
    //    {
    //        if (key != Transicion.NullTransition && value != AIState.NullState)
    //        {
    //            diccionario.Add(key, value);
    //        }
    //    }
    //}

    //public void RemoverTransicion(Transicion key) //No se usa
    //{
    //    if (!diccionario.ContainsKey(key))
    //    {
    //        if (key != Transicion.NullTransition)
    //        {
    //            diccionario.Remove(key);
    //        }
    //    }
    //}

    ////Se utiliza para obtener el estado mediante una transicion.
    //public AIState GetOutput(Transicion key)
    //{
    //    if (diccionario.ContainsKey(key) && key != Transicion.NullTransition)
    //    {
    //        return diccionario[key];
    //    }
    //    return AIState.NullState;
    //}

    //public abstract void Reason(GameObject player, NavMeshAgent npc);

    //public abstract void Behaviour(GameObject player, NavMeshAgent npc);

    public abstract Enemies GetEnemies();
}
