using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] float health;
    [SerializeField] float moveSpeed;
    [SerializeField] float armor;
    [SerializeField] float attackSpeed;
    [SerializeField] float jumpHeight;

    private void Awake()
    {
        pStats = new PlayerStats(this);
        pMovement = new PlayerMovement(pInputs, pStats);
        pInputs = new PlayerInputs();
    }

    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }
    private void Update()
    {
        pInputs.InputsUpdate();
    }
}