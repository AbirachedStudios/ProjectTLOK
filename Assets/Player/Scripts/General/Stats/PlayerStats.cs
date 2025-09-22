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

    public Transform damageableTransform { get; set; }

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
        Debug.Log("Aumentado de " + reset + " a " + stat);
        yield return new WaitForSeconds(timer);
        stat = reset;
    }
    private IEnumerator StatFlatChronometer(float stat, float debuf, float timer)
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
                playerDamage += boost;

                Debug.Log("Dano aumentado a " + playerDamage);
                break;

            case 1:
                playerAttackSpeed += boost;

                Debug.Log("Velocidad de ataque aumentada a " + playerAttackSpeed);
                break;

            case 2:
                playerArmor += boost;

                Debug.Log("Armadura aumentada a " + playerArmor);
                break;

            case 3:
                playerWalkSpeed += boost;
                playerSprintSpeed += boost;

                Debug.Log("Velocidad aumentada a " + playerWalkSpeed);
                break;
        }

        yield return new WaitForSeconds(5);

        switch (category)
        {
            case 0:
                playerDamage -= boost;
                break;

            case 1:
                playerAttackSpeed -= boost;
                break;

            case 2:
                playerArmor -= boost;
                break;

            case 3:
                playerWalkSpeed -= boost;
                playerSprintSpeed -= boost;
                break;
        }

        Debug.Log("reiniciado los estados");
    }
}