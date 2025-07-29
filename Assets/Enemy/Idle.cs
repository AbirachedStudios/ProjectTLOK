using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Idle : Enemies
{
    //public Idle()
    //{
    //    estadoID = AIState.Idle;
    //}


    public Patrol patrolState;

    public Chase chaseState;
    //Neutral State
    //public override void Reason(GameObject player, NavMeshAgent npc)
    //{

    //    if (CheckPlayerInSight())
    //    {
    //        npc.GetComponent<FSMSystem>().HacerTransicion(Transicion.EnemyFound);
    //    }


    //    if (waypoints.Length > 0)
    //    {

    //        npc.GetComponent<FSMSystem>().HacerTransicion(Transicion.NoEnemyFound);
    //    }

    //}
    //public override void Behaviour(GameObject player, NavMeshAgent npc)
    //{
    //    anim.SetBool("Moving", false);

    //    agent.ResetPath();
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
        
        if(waypoints.Length > 0)
        {
            return patrolState;
        }
        else
        {
            return this;
        }
        
    }
}
