using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class Attack : Enemies
{
    //public Attack()
    //{
    //    estadoID = AIState.Attack;
    //}

    public Range rangeState;
    public Jump jumpState;
    public Type dmgType;
    public GameObject visualEffectPrefab;
    public VisualEffectAsset[] assets;
    public Transform attack;
    public Vector3 setBack;

    public Chase chaseState;

    //public override void Reason(GameObject player, NavMeshAgent npc)
    //{
    //    if (Vector3.Distance(npc.transform.position, player.transform.position) < rangedAttackRange)
    //    {
    //        npc.GetComponent<FSMSystem>().HacerTransicion(Transicion.EnemyInShoot);
    //    }
    //    else if (Vector3.Distance(npc.transform.position, player.transform.position) == jumpRange)
    //    {
    //        npc.GetComponent<FSMSystem>().HacerTransicion(Transicion.EnemyInJump);
    //    }

    //}
    //public override void Behaviour(GameObject player, NavMeshAgent npc)
    //{


    //}
    private new void Update()
    {
        base.Update();

       

        target = player.transform.position;

        if (Vector3.Distance(this.transform.position, player.transform.position) <= attackRange)
        {
            Att();
        }
        else
        {
            chaseState.Perseguir();
        }

        if (isNinja && ranged)
        {
            setBack = -transform.forward * chaseSpeed;
            agent.SetDestination(setBack);

            //attackState.Att();
            //rb.velocity = -transform.forward * chaseSpeed;
            //Vector3 retreatPosition = transform.position + retreatDirection * retreatDistance;
            //agent.SetDestination(retreatPosition);
            //return this;
        }
    }
    public void Att()
    {
        //agent.ResetPath();
        FaceTarget(target);
        anim.SetBool("Moving", false);
        if (Time.time >= lastAttackTime + attackCooldown)
        {

            VisualEffect veAsset = visualEffectPrefab.GetComponent<VisualEffect>();
            if (dmgType == Type.Base)
            {
                veAsset.visualEffectAsset = assets[0];
            }
            else if (dmgType == Type.Electric)
            {
                veAsset.visualEffectAsset = assets[1];

            }
            else if (dmgType == Type.Magical)
            {
                veAsset.visualEffectAsset = assets[2];

            }
            else if (dmgType == Type.Radioactive)
            {
                veAsset.visualEffectAsset = assets[3];

            }

            agent.transform.LookAt(target);

            anim.SetTrigger("Attack"); // Trigger attack animation.
                                       //StartCoroutine(Slash());

            Vector3 effectSpawnPosition = attack.position;
            GameObject spawnedEffect = Instantiate(visualEffectPrefab, effectSpawnPosition, Quaternion.identity);

            lastAttackTime = Time.time;
            agent.ResetPath();
        }
    }

    public override Enemies GetEnemies()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) > rangedAttackRange && ranged)
        {
            return chaseState;
        }

        if(Vector3.Distance(this.transform.position, player.transform.position) > rangedAttackRange && !ranged)
        {
            return chaseState;
        }
        
        //if (Vector3.Distance(this.transform.position, player.transform.position) < jumpRange && isNinja)
        //{
        //    return jumpState;
        //}

        if (Vector3.Distance(this.transform.position, player.transform.position) > attackRange && Vector3.Distance(this.transform.position, player.transform.position) <= rangedAttackRange && ranged)
        {
            
            return rangeState;
        }
        //else if(Vector3.Distance(this.transform.position, player.transform.position) <= attackRange &&isNinja && ranged)
        //{
        //    Att();
        //    Vector3 retreatDirection = (transform.position - player.position).normalized;
        //    Vector3 retreatPosition = transform.position + retreatDirection * retreatDistance;
        //    agent.SetDestination(retreatPosition);
        //    return rangeState;
        //}

        
            Att();
            return this;
        
        

        
    }
}
