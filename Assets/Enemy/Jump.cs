using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public class Jump : Enemies
{
    //public Jump()
    //{
    //    estadoID = AIState.Jump;
    //}
    //public override void Reason(GameObject player, NavMeshAgent npc)
    //{
    //    if (Vector3.Distance(npc.transform.position, player.transform.position) < rangedAttackRange)
    //    {
    //        npc.GetComponent<FSMSystem>().HacerTransicion(Transicion.EnemyInShoot);
    //    }
    //    else if(hasJumped)
    //    {
    //        npc.GetComponent<FSMSystem>().HacerTransicion(Transicion.EnemyFound);
    //        if(Vector3.Distance(npc.transform.position, player.transform.position) < attackRange)
    //        {
    //            npc.GetComponent<FSMSystem>().HacerTransicion(Transicion.EnemyInRange); //Ataca a 2 metros
    //        }
    //    }

    //}
    //public override void Behaviour(GameObject player, NavMeshAgent npc)
    //{


    //}


    //public Range rangeState;
    public Attack attackState;
    public Chase chaseState;

    private float jumpProgress;
    public float jumpSpeed = 5f;
    private Vector3 jumpStartPos;
    float distanceToPlayer;
    private float jumpStartTime;

    //private void Update()
    //{
    //    if (distanceToPlayer <= jumpRange && !isJumping)
    //    {
    //        Jumping();

    //    }
    //    else if (isJumping)
    //    {

    //        JumpPerform();
    //    }
    //}


    public void Jumping()
    {




        isJumping = true;
        agent.isStopped = true; // Stop NavMeshAgent during jump
        rb.isKinematic = false; // Allow physics control for jump
        //agent.enabled = false;
        // Calculate the target position behind the player
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        jumpTarget = player.transform.position - directionToPlayer * 2f; // Adjust distance as needed.
        jumpTarget.y = transform.position.y; // Keep y the same for horizontal movement

        // Calculate the jump trajectory
        Vector3 jumpDirection = (jumpTarget - transform.position).normalized;
        jumpDirection.y = 1f; // Add vertical component for the jump

        // Apply initial velocity for the jump
        rb.velocity = jumpDirection * jumpSpeed;

        anim.SetBool("Ground", false); // Trigger jump animation
    }

    public void JumpPerform()
    {
        rb.AddForce(Vector3.down * 9.81f, ForceMode.Acceleration);

        if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(jumpTarget.x, 0, jumpTarget.z)) < 0.5f)
        {
            isJumping = false;
            rb.isKinematic = true; // Return to NavMeshAgent control
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);
        }

    }

    public override Enemies GetEnemies()
    {
        //if (Vector3.Distance(this.transform.position, player.transform.position) > rangedAttackRange)
        //{
        //    return chaseState;
        //}
        //if (Vector3.Distance(this.transform.position, player.transform.position) < rangedAttackRange)
        //{
        //    return rangeState;
        //}
        if (Vector3.Distance(this.transform.position, player.transform.position) < attackRange)
        {
            return attackState;
        }
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);


        //if (Vector3.Distance(this.transform.position, player.transform.position) < jumpRange && isNinja && !isJumping)
        //{
        //    Jumping();
        //}
        //else if (isJumping && isNinja)
        //{
        //    JumpPerform();
        //}


        agent.transform.LookAt(player.transform.position);

        return this;

        
    }
}
