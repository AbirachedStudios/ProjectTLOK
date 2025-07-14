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
    Camera cam;

    [Header("Player Stats")]
    [SerializeField] float damage;
    [SerializeField] float attackSpeed;
    [SerializeField] float maxHealth;
    [SerializeField] float health;
    [SerializeField] float armor;
    [SerializeField] float walkSpeed;
    [SerializeField] float sprintSpeed;
    [SerializeField] float speedInterpolation;
    [SerializeField] float jumpHeight;
    [SerializeField] float gravity;

    private void Awake()
    {
        cam = Camera.main;
        cc = GetComponent<CharacterController>();

        pStats = new PlayerStats(this, damage, attackSpeed, maxHealth, health, armor, walkSpeed, sprintSpeed, jumpHeight, gravity);
        pInputs = new PlayerInputs();
        pMovement = new PlayerMovement(pInputs, pStats, cc, cam, speedInterpolation);

    }
    private void Update()
    {
        pInputs.InputsUpdate();
        pMovement.MovementUpdate();
    }
}