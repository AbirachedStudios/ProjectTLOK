using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class Enemy : MonoBehaviour
{
    public string nameEnemy;
    public FSMSystem fsmSystemRef;

    public NavMeshAgent ag;

    //public AIState currentState;
    
    private float jumpCooldown = 2f; // Cooldown time in seconds.





    public float minDistanceToPlayer; // Add this variable

    //public bool isRanged;

    //public bool samu;

  

    //public bool isNinja = false;
    

    public float speedNinja = -10.0f;
    private Vector3 position;

    public VisualEffect ef;

    //public VisualEffectAsset[] effect;


    //public Dictionary<Transicion, AIState> diccionario = new Dictionary<Transicion, AIState>();

    //protected AIState estadoID;
    //private void CrearFSM()
    //{
    //    Spawn spawn = new Spawn();
    //    spawn.AgregarTransicion(Transicion.Initialize, AIState.Idle);
    //    //spawn.AgregarTransicion(Transicion.LifeZero, Estado.Dead);

    //    Idle idle = new Idle();
    //    idle.AgregarTransicion(Transicion.NoEnemyFound, AIState.Patrol);
    //    idle.AgregarTransicion(Transicion.EnemyFound, AIState.Chase);

    //    Patrol patrol = new Patrol();
    //    patrol.AgregarTransicion(Transicion.EnemyFound, AIState.Chase);
    //    patrol.AgregarTransicion(Transicion.NoInteraction, AIState.Idle);

    //    Chase chase = new Chase();
    //    chase.AgregarTransicion(Transicion.EnemyInShoot, AIState.Range);

    //    Range range = new Range();
    //    range.AgregarTransicion(Transicion.EnemyInRange, AIState.Attack);

    //    Attack attack = new Attack();
    //    attack.AgregarTransicion(Transicion.EnemyInShoot, AIState.Range);

    //    fsmSystemRef.AgregarEstado(spawn);
    //    fsmSystemRef.AgregarEstado(idle);
    //    fsmSystemRef.AgregarEstado(patrol);
    //    fsmSystemRef.AgregarEstado(chase);
    //    fsmSystemRef.AgregarEstado(range);
    //    fsmSystemRef.AgregarEstado(attack);
    //}

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<FSMSystem>();
        //CrearFSM();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
