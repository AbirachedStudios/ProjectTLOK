using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float waitTime = 1f;
    public float rotationSpeed = 5f; // New variable for rotation speed


    private Transform[] waypoints;
    private int currentWaypointIndex = 0;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        GameObject[] waypointObjects = GameObject.FindGameObjectsWithTag("Waypoint");
        waypoints = new Transform[waypointObjects.Length];
        for (int i = 0; i < waypointObjects.Length; i++)
        {
            waypoints[i] = waypointObjects[i].transform;
        }

        // Select a random starting waypoint
        if (waypoints.Length > 0)
        {
            currentWaypointIndex = Random.Range(0, waypoints.Length);
            // Move the enemy to the starting waypoint's position
            transform.position = waypoints[currentWaypointIndex].position;
        }


        // Start the patrol coroutine
        StartCoroutine(Patrol());
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            // Move towards the current waypoint
            Vector3 targetPosition = waypoints[currentWaypointIndex].position;
            animator.SetBool("IsWalking", true);
            animator.SetFloat("Speed", moveSpeed); // Set Speed parameter to start walking

            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                // Calculate the direction to the target
                Vector3 direction = (targetPosition - transform.position).normalized;

                // Only rotate if there's a direction to face
                if (direction != Vector3.zero)
                {
                    // Calculate the target rotation
                    Quaternion targetRotation = Quaternion.LookRotation(direction);

                    // Smoothly rotate the NPC towards the target rotation
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                }
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null; // Wait for the next frame
            }

            // Waypoint reached, stop and idle
            animator.SetBool("IsWalking", false);
            animator.SetFloat("Speed", 0f); // Set Speed parameter to stop walking
            yield return new WaitForSeconds(waitTime); // Wait for the specified time

            // Move to the next waypoint
            currentWaypointIndex = Random.Range(0, waypoints.Length);
        }
    }
}