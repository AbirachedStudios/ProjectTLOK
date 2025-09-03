using UnityEngine;

public class PedestrianAI : MonoBehaviour
{
    public float runSpeed = 6f;
    public Animator animator;
    public NPC npc;

    private Transform player;
    private bool isThreatDetected = false;
    public float rotationSpeed = 5f; // New variable for rotation speed

    private bool isCrouching = false;
    private Vector3 startPosition;
    public float safeDistance = 10f; // The distance at which the pedestrian will stop and crouch

    public EnemyAI enemy;
    public float reactionRadius = 20f;
    private bool isFleeing = false;

    private void Start()
    {
        npc = GetComponent<NPC>();
        enemy = FindObjectOfType<EnemyAI>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        EnemyAI.OnPlayerDetected += CheckDistanceAndFlee;
    }

    void OnDestroy()
    {
        // Unsubscribe from the event
        EnemyAI.OnPlayerDetected -= CheckDistanceAndFlee;
    }

    void Update()
    {

        if (isFleeing && !isCrouching)
        {
            npc.StopAllCoroutines();

            float distanceCovered = Vector3.Distance(transform.position, startPosition);

            if (distanceCovered < safeDistance)
            {
                // Flee from the player
                Vector3 fleeDirection = transform.position - player.position;
                transform.position += fleeDirection.normalized * runSpeed * Time.deltaTime;
                transform.LookAt(fleeDirection);
            }
            else
            {
                // Reached a safe distance, stop and crouch
                isCrouching = true;
                isFleeing = false;
                animator.SetBool("IsRunning", false);
                animator.SetBool("IsCrouching", true);
            }
        }
    }

    

    private void CheckDistanceAndFlee(Vector3 enemyPosition)
    {
        // Check if the pedestrian is close enough to the event
        float distanceToThreat = Vector3.Distance(transform.position, enemyPosition);

        if (distanceToThreat < reactionRadius && !isFleeing && !isCrouching)
        {
            // If they are close, start the fleeing behavior
            startPosition = transform.position;
            isFleeing = true;
            animator.SetBool("IsRunning", true);

        }
    }

}