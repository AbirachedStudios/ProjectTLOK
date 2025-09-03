using System;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Public variables for tuning in the Inspector
    public float attackRange = 2f;
    public float moveSpeed = 4f;
    public Animator animator;

    // Private variables for logic
    private Transform player;
    public bool isPlayerDetected = false;

    public NPC npc;
    public static event Action<Vector3> OnPlayerDetected;

    void Start()
    {
        npc = GetComponent<NPC>();
        // Find the player by tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {

        if (isPlayerDetected)
        {
            npc.StopAllCoroutines();
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer > attackRange)
            {
                // Chase the player
                animator.SetBool("IsWalking", true);
                transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
                transform.LookAt(player);
            }
            else
            {
                // Player is in attack range, stand and prepare to fight
                animator.SetBool("IsWalking", false);
                animator.SetTrigger("EnterCombat");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isPlayerDetected)
            {
                isPlayerDetected = true;
                // Fire the event, passing the enemy's position as the event's source
                OnPlayerDetected?.Invoke(transform.position);
            }
        }
    }

}