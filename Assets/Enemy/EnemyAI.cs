using UnityEngine;

public class EnemyAI : MonoBehaviour, IDestroyable
{
    // Public variables for tuning in the Inspector
    public float attackRange = 2f;
    public float moveSpeed = 4f;
    public Animator animator;

    // Private variables for logic
    private Transform player;
    public bool isPlayerDetected = false;

    public NPC npc;

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
                animator.SetFloat("Speed", moveSpeed);
                animator.SetBool("IsWalking", true);
                transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

                // Look at the player
                transform.LookAt(player);
            }
            else
            {
                // Player is in attack range, stand and prepare to fight
                animator.SetFloat("Speed", 0f);
                animator.SetBool("IsWalking", false);
                animator.SetTrigger("EnterCombat"); // Trigger a combat stance animation
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerDetected = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerDetected = false;
            // You can add logic here to return to patrol mode
        }
    }

    public void TakeDamage(float num)
    {
        
    }

    public Transform damageableTransform { get; set; }
    public void DestroyByInterface()
    {
        
    }
}