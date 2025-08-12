using System.Collections;
using UnityEngine;


[System.Serializable]
public class PlayerStats : IDamageable
{
    
    MonoBehaviour _monoBehaviour;

    public PlayerStats(MonoBehaviour monoBehaviour, float dmg, float atkSpd, float mxHlth, float hlth, float armr, float wS, float spS, float jpHght, float gravity)
    {
        _monoBehaviour = monoBehaviour;
        playerDamage = dmg;
        playerAttackSpeed = atkSpd;
        playerMaxHealth = mxHlth;
        playerHealth = hlth;
        playerArmor = armr;
        playerWalkSpeed = wS;
        playerSprintSpeed = spS;
        playerJumpHeight = jpHght;
        playerGravity = gravity;
    }

    /***************-VARIABLES-***************/
    public float playerDamage { get; private set; }
    public float playerAttackSpeed { get; private set; }
    public float playerMaxHealth { get; private set; }
    public float playerHealth { get; private set; }
    public float playerArmor { get; private set; }
    public float playerWalkSpeed { get; private set; }
    public float playerSprintSpeed { get; private set; }
    public float playerJumpHeight { get; private set; }
    public float playerGravity { get; private set; }


    /***************-METODOS-***************/
    public void TakeDamage(float damage)
    {
        playerHealth -= damage;
        Die();
    }
    private void Die()
    {
        if(playerHealth <= 0)
        {
            //Player Muere
        }
    }
    public void Heal(float healing)
    {
        if (playerHealth < playerMaxHealth)
        {
            playerHealth += healing;

            if(playerHealth >= playerMaxHealth)
            {
                playerHealth = playerMaxHealth;
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