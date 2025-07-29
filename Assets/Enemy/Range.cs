using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class Range : Enemies
{
    //public Range()
    //{
    //    estadoID = AIState.Range;
    //}

    public Chase chaseState;
    public Attack attackState;
    public Type dmgType;
    public GameObject visualEffectPrefab;
    public VisualEffectAsset[] assets;
    public Vector3 setBack;
    public bool flee;
    //public Jump jumpState;
    //public override void Reason(GameObject player, NavMeshAgent npc)
    //{
    //    if (CheckPlayerInSight() && Vector3.Distance(npc.transform.position, player.transform.position) > rangedAttackRange)
    //    {
    //        npc.GetComponent<FSMSystem>().HacerTransicion(Transicion.EnemyFound);
    //    }
    //    else if (Vector3.Distance(npc.transform.position, player.transform.position) == jumpRange)
    //    {
    //        npc.GetComponent<FSMSystem>().HacerTransicion(Transicion.EnemyInJump);
    //    }
    //    else if (Vector3.Distance(npc.transform.position, player.transform.position) < attackRange)
    //    {
    //        npc.GetComponent<FSMSystem>().HacerTransicion(Transicion.EnemyInRange); //Ataca a 2 metros
    //    }
    //}
    //public override void Behaviour(GameObject player, NavMeshAgent npc)
    //{

    //    if (Time.time >= lastRangedAttackTime + rangedAttackCooldown)
    //    {




    //        agent.transform.LookAt(target);

    //        anim.SetTrigger("Shoot"); // Trigger ranged attack animation
    //        lastRangedAttackTime = Time.time;

    //        // Instantiate the projectile.
    //        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

    //        // Add force or velocity to the projectile (replace with your projectile logic)
    //        projectile.GetComponent<Rigidbody>().AddForce(projectileSpawnPoint.forward * 10f, ForceMode.Impulse);

    //        Debug.Log("Enemy performs ranged attack!");
    //        //currentState = AIState.Chase;
    //    }
    //}

    private new void Update()
    {
        base.Update();

        //if(flee)
        //{

        //}


        setBack = -transform.forward * chaseSpeed; 

        if (isNinja && ranged && Vector3.Distance(this.transform.position, player.transform.position) <= attackRange)
        {
            agent.SetDestination(setBack);
            //flee = true;
            //attackState.Att();
            //rb.velocity = -transform.forward * chaseSpeed;
            //Vector3 retreatPosition = transform.position + retreatDirection * retreatDistance;
            //agent.SetDestination(retreatPosition);
            //return this;
        }
    }



    public void RangeAttack()
    {
       
        agent.ResetPath();
        agent.transform.LookAt(target);
        anim.SetBool("Moving", false);
        if (Time.time >= lastRangedAttackTime + rangedAttackCooldown)
        {

            VisualEffect veAsset = visualEffectPrefab.GetComponent<VisualEffect>();
            if(dmgType == Type.Base)
            {
                veAsset.visualEffectAsset = assets[0];
            }
            else if(dmgType == Type.Electric)
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

            anim.SetTrigger("Shoot"); // Trigger ranged attack animation
            lastRangedAttackTime = Time.time;

            // Instantiate the projectile.
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation, projectileSpawnPoint);
            Vector3 arrowPosition = projectile.transform.position;
            Vector3 effectSpawnPosition = arrowPosition;
            // Add force or velocity to the projectile (replace with your projectile logic)
            projectile.GetComponent<Rigidbody>().AddForce(projectileSpawnPoint.forward * 10f, ForceMode.Impulse);
            GameObject spawnedEffect = Instantiate(visualEffectPrefab, effectSpawnPosition, Quaternion.identity);
            spawnedEffect.transform.SetParent(projectile.transform);

            Debug.Log("Enemy performs ranged attack!");
            //currentState = AIState.Chase;
        }
        
    }

    public override Enemies GetEnemies()
    {

        if (Vector3.Distance(this.transform.position, player.transform.position) > rangedAttackRange)
        {
            return chaseState;
        }
        else if (Vector3.Distance(this.transform.position, player.transform.position) <= attackRange)
        {
            //attackState.Att();
            
            return attackState;
            
        }
        else
        {

            RangeAttack();
            return this;
        }

        //if (Vector3.Distance(this.transform.position, player.transform.position) == jumpRange)
        //{
        //    return jumpState;
        //}



    }
}
