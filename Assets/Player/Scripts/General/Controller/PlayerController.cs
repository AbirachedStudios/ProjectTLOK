using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerData;

public class PlayerController : Entity
{
    [Header("References")]
    PlayerMovement pMovement;
    PlayerInputs pInputs;
    PlayerStats pStats;

    [Header("Dependencies")]
    CharacterController cc;

    [Header("Player Stats")]
    [SerializeField] float damage;
    [SerializeField] float attackSpeed;
    [SerializeField] float maxHealth;
    [SerializeField] float health;
    [SerializeField] float armor;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpHeight;
    [SerializeField] float gravity;

    private void Awake()
    {
        pStats = new PlayerStats(this, damage, attackSpeed, maxHealth, health, armor, moveSpeed, jumpHeight, gravity);
        pInputs = new PlayerInputs();
        pMovement = new PlayerMovement(pInputs, pStats, cc);
    }

    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }
    private void Update()
    {
        pInputs.InputsUpdate();
        pMovement.MovementUpdate();
    }
}