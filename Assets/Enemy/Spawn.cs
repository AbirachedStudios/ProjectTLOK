using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawn : Enemies
{
    //public Spawn()
    //{
    //    estadoID = AIState.Spawn;
    //}

    public Idle idleState;

    float timer = 0.15f;
    //public NavMeshAgent npc;
    //public override void Reason(GameObject player, NavMeshAgent npc)
    //{

    //    //Al spawnear, hace idle
    //    if (timer <= 0)
    //    {            
    //        npc.GetComponent<FSMSystem>().HacerTransicion(Transicion.Initialize);
    //    }        
    //}

    //public override void Behaviour(GameObject player, NavMeshAgent npc)
    //{
    //    currentState = AIState.Spawn;
    //    //SetSpeedsAnimation
    //    switch (npc.GetComponent<Enemy>().nameEnemy) //PATROL
    //    {
    //        case "Samurai":
    //            sightRange = 15f;
    //            jumpRange = 0f;
    //            jumpHeight = 0f;
    //            patrolSpeed = 6f;
    //            chaseSpeed = 8;
    //            attackRange = 2f;
    //            attackCooldown = 1f;
    //            rangedAttackRange = 8f;
    //            rangedAttackCooldown = 2f;
    //            retreatDistance = 0f;
    //            speedNinja = 0f;
    //            break;
    //        case "Ranged Ninja":

    //            break;
    //        case "Ninja":

    //            break;    
    //    }
    //    Debug.Log("SPAWNING");
    //    timer -= Time.deltaTime;
    //}

    private new void Update()
    {
        base.Update();

    }
    public override Enemies GetEnemies()
    {

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            return idleState;
        }
        else
        {
            return this;
        }


        
    }
}
