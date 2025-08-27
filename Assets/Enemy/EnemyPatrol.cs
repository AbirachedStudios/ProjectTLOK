using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    public NavMeshAgent agent;
    public float speed;
    public GameObject[] wps;
    GameObject wayPoint;
    public float distance;
    public float waitTime;
    public Animator anim;
    public string walkAnim;
    public Animation animat;


    void Start()
    {
        wps = GameObject.FindGameObjectsWithTag("Waypoint");

        anim = GetComponent<Animator>();

        wayPoint = wps[Random.Range(0, wps.Length)];

        anim.Play(walkAnim);

        // Optional: Sort waypoints if you want them to be in a specific order
        // This example assumes they are placed correctly in the scene.


    }

    private void Update()
    {

        distance = Vector3.Distance(transform.position, wayPoint.transform.position);

        if (distance < 3)
        {
            wayPoint = wps[Random.Range(0, wps.Length)];
        }

        agent.destination = wayPoint.transform.position;

        agent.speed = speed;
    }

   

}