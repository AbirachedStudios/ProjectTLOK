using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : Enemies
{
    //public Chase()
    //{
    //    estadoID = AIState.Chase;
    //}

    public Range rangeState;
    public Attack attackState;
    public Jump jumpState;
    public Collider[] enemyInRange;
    public float detectionRange = 10f;

    //public override void Reason(GameObject player, NavMeshAgent npc)
    //{
    //        if (Vector3.Distance(npc.transform.position, player.transform.position) < rangedAttackRange)
    //        {
    //            npc.GetComponent<FSMSystem>().HacerTransicion(Transicion.EnemyInShoot);
    //}
    //        else if (Vector3.Distance(npc.transform.position, player.transform.position) == jumpRange)
    //{
    //    npc.GetComponent<FSMSystem>().HacerTransicion(Transicion.EnemyInJump);
    //}
    //else if (Vector3.Distance(npc.transform.position, player.transform.position) < attackRange)
    //{
    //    npc.GetComponent<FSMSystem>().HacerTransicion(Transicion.EnemyInRange); //Ataca a 2 metros
    //}
    //}
    //public override void Behaviour(GameObject player, NavMeshAgent npc)
    //{
    //    anim.SetBool("Moving", true);
    //    agent.SetDestination(player.transform.position);
    //    agent.speed = chaseSpeed;
    //    agent.transform.LookAt(target);
    //}


    private new void Update()
    {
        base.Update();

        enemyInRange = Physics.OverlapSphere(transform.position, detectionRange);
    }

    

    public void Perseguir()
    {
        anim.SetBool("Moving", true);
        agent.SetDestination(player.transform.position);
        agent.speed = chaseSpeed;
        FaceTarget(target);
    }

    public override Enemies GetEnemies()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) < rangedAttackRange && ranged)
        {
            FaceTarget(target);
            return rangeState;
        }
        else if (Vector3.Distance(this.transform.position, player.transform.position) < attackRange)
        {
            attackState.Att();

            if (isNinja && ranged) 
            {
                //attackState.Att();
                rb.velocity = -transform.forward * chaseSpeed;
                //Vector3 retreatDirection = (transform.position - player.position).normalized;
                //Vector3 retreatPosition = transform.position + retreatDirection * retreatDistance;
                //agent.SetDestination(retreatPosition);
                return rangeState;
            }
            return attackState; //Ataca a 2 metros
        }
        else
        {
            Perseguir();
            return this;
        }
        
        
        
    }
}
