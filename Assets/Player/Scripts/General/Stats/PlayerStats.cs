using System.Collections;
using UnityEngine;
using PlayerData;


[System.Serializable]
public class PlayerStats //ACA VAN LAS INTERFACES QUE DETECTAN METODOS DEL PLAYER
{
    
    MonoBehaviour _monoBehaviour;

    public PlayerStats(MonoBehaviour monoBehaviour, float dmg, float atkSpd, float mxHlth, float hlth, float armr, float wS, float spS, float jpHght, float gravity)
    {
        _monoBehaviour = monoBehaviour;
        p_damage = dmg;
        p_attackSpeed = atkSpd;
        p_maxHealth = mxHlth;
        p_health = hlth;
        p_armor = armr;
        p_walkSpeed = wS;
        p_sprintSpeed = spS;
        p_jumpHeight = jpHght;
        p_gravity = gravity;
    }

    /***************-VARIABLES-***************/
    public float p_damage { get; private set; }
    public float p_attackSpeed { get; private set; }
    public float p_maxHealth { get; private set; }
    public float p_health { get; private set; }
    public float p_armor { get; private set; }
    public float p_walkSpeed { get; private set; }
    public float p_sprintSpeed { get; private set; }
    public float p_jumpHeight { get; private set; }
    public float p_gravity { get; private set; }


    /***************-METODOS-***************/
    public void TakeDamage(float damage)
    {
        p_health -= damage;
        Die();
    }
    private void Die()
    {
        if(p_health <= 0)
        {
            //Player Muere
        }
    }
    public void Heal(float healing)
    {
        if (p_health < p_maxHealth)
        {
            p_health += healing;

            if(p_health >= p_maxHealth)
            {
                p_health = p_maxHealth;
            }
        }
    }
    public void PercentageDebuff(float stat, float debuffPercentage, float timer)
    {
        _monoBehaviour.StartCoroutine(StatPercentageChronometer(stat, debuffPercentage, timer));
    }
    public void FlatDebuff(float stat, float debuffQuantity, float timer)
    {
        _monoBehaviour.StartCoroutine(StatFlatChronometer(stat, debuffQuantity, timer));
    }

    /***************-CORRUTINAS-***************/

    private IEnumerator StatPercentageChronometer(float stat, float debuf, float timer)
    {
        float reset = stat;
        stat = (debuf * stat) / 100;
        yield return new WaitForSeconds(timer);
        stat = reset;
    }
    private IEnumerator StatFlatChronometer(float stat, float debuf, float timer)
    {
        float reset = stat;
        stat -= debuf;
        yield return new WaitForSeconds(timer);
        stat = reset;
    }
}