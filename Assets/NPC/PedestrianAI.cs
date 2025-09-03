using UnityEngine;

public class PedestrianAI : MonoBehaviour
{
    public float runSpeed = 6f;
    public Animator animator;
    public NPC npc;

    private Transform threat;
    private bool isThreatDetected = false;
    public float rotationSpeed = 5f; // New variable for rotation speed

    private bool isCrouching = false;
    private Vector3 startPosition;
    public float safeDistance = 10f; // The distance at which the pedestrian will stop and crouch

    public EnemyAI enemy;
    private void Start()
    {
        npc = GetComponent<NPC>();
        enemy = FindObjectOfType<EnemyAI>();
    }

    void Update()
    {
        

        if (!isCrouching && isThreatDetected && threat != null)
        {
            float distanceCovered = Vector3.Distance(transform.position, startPosition);


            if (distanceCovered < safeDistance)
            {


                // Flee from the threat
                Vector3 fleeDirection = transform.position - threat.position;
                transform.position += fleeDirection.normalized * runSpeed * Time.deltaTime;

                // Calculate the direction to the target
                Vector3 direction = fleeDirection.normalized;

                // Only rotate if there's a direction to face
                if (direction != Vector3.zero)
                {
                    // Calculate the target rotation
                    Quaternion targetRotation = Quaternion.LookRotation(direction);

                    // Smoothly rotate the NPC towards the target rotation
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                }
            }
            else
            {
                // Reached a safe distance, stop and crouch
                isCrouching = true;
                animator.SetBool("IsRunning", false);
                animator.SetBool("IsCrouching", true); // Trigger the crouching animation
            }
        }
    }

    private void LateUpdate()
    {
        CheckIfFight();
    }

    void CheckIfFight()
    {
        if (enemy.isPlayerDetected == true)
        {

            npc.StopAllCoroutines();
            isThreatDetected = true;
            threat = enemy.transform;

            // Start the fleeing animation
            animator.SetBool("IsRunning", true);
        }
    }

   
}