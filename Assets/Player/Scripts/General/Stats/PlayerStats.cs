using System.Collections;
using UnityEngine;


[System.Serializable]
public class PlayerStats //ACA VAN LAS INTERFACES QUE DETECTAN METODOS DEL PLAYER
{
    
    MonoBehaviour _monoBehaviour;
    /*
    Estadisticas del jugador persistentes entre escenas (vida, damage, armor, speed)
    Las variables relacionadas a player van con p_
    Variables encapsuladas {get; private set;}
    ej:

    public int p_health {get; private set;}
    public int p_maxHealth {get; private set;} 
    

    Metodos publicos que modifican las variables directamente
    ej:

    public void HealPlayer(int heal)
    {
        p_actualHealth += heal;
    }
    */

    public PlayerStats(MonoBehaviour monoBehaviour)
    {
        _monoBehaviour = monoBehaviour;
    }

    /***************-VARIABLES-***************/
    public float p_damage { get; private set; }
    public float p_attackSpeed { get; private set; }
    public float p_maxHealth { get; private set; }
    public float p_health { get; private set; }
    public float p_armor { get; private set; }
    public float p_moveSpeed { get; private set; }
    public float p_jumpHeight { get; private set; }


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
    public void PercentageDebuff(float stat, float armor, float timer)
    {
        _monoBehaviour.StartCoroutine(StatPercentageChronometer(stat, armor, timer));
    }
    public void FlatDebuff(float stat, float armor, float timer)
    {
        _monoBehaviour.StartCoroutine(StatFlatChronometer(stat, armor, timer));
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