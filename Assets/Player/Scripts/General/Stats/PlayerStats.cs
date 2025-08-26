using System.Collections;
using UnityEngine;


[System.Serializable]
public class PlayerStats : IDamageable
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

    public IEnumerator StatPercentageChronometer(float stat, float debuf, float timer)
    {
        float reset = stat;
        stat = (debuf * stat) / 100;
        Debug.Log("Aumentado de " + reset + " a " + stat);
        yield return new WaitForSeconds(timer);
        stat = reset;
    }
    public IEnumerator StatFlatChronometer(float stat, float debuf, float timer)
    {
        float reset = stat;
        stat -= debuf;
        Debug.Log("Aumentado de " + reset + " a " + stat);
        yield return new WaitForSeconds(timer);
        Debug.Log("Estado reseteado");
        stat = reset;
    }

    public IEnumerator BoostStat(int category, float boost)
    {
        switch (category)
        {
            case 0:
                p_damage += boost;

                Debug.Log("Daño aumentado a " + p_damage);
                break;

            case 1:
                p_attackSpeed += boost;

                Debug.Log("Velocidad de ataque aumentada a " + p_attackSpeed);
                break;

            case 2:
                p_armor += boost;

                Debug.Log("Armadura aumentada a " + p_armor);
                break;

            case 3:
                p_walkSpeed += boost;
                p_sprintSpeed += boost;

                Debug.Log("Velocidad aumentada a " + p_walkSpeed);
                break;
        }

        yield return new WaitForSeconds(5);

        switch (category)
        {
            case 0:
                p_damage -= boost;
                break;

            case 1:
                p_attackSpeed -= boost;
                break;

            case 2:
                p_armor -= boost;
                break;

            case 3:
                p_walkSpeed -= boost;
                p_sprintSpeed -= boost;
                break;
        }

        Debug.Log("reiniciado los estados");
    }
}