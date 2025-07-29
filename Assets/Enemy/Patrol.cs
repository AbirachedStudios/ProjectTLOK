using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : Enemies
{
    //public Patrol()
    //{
    //    estadoID = AIState.Patrol;
    //}


    public Chase chaseState;



    ////Neutral State
    //public override void Reason(GameObject player, NavMeshAgent npc)
    //{
    //    if (CheckPlayerInSight())
    //    {
    //        npc.GetComponent<FSMSystem>().HacerTransicion(Transicion.EnemyFound); //CAMBIA A CHASE AL VERLO        

    //    }

    //}
    //public override void Behaviour(GameObject player, NavMeshAgent npc)
    //{
    //    if (!agent.pathPending && agent.remainingDistance < 0.5f)
    //    {
    //        anim.SetBool("Moving", true);

    //        GotoNextWaypoint();
    //        agent.speed = patrolSpeed;

    //    }
    //}

    private new void Update()
    {
        base.Update();
    }

    public override Enemies GetEnemies()
    {
        if(CheckPlayerInSight())
        {
            return chaseState;
        }
        else
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                anim.SetBool("Moving", true);

                GotoNextWaypoint();
                agent.speed = patrolSpeed;
            }
            return this;

        }

    }
}
