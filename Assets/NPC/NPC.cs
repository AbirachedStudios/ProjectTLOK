using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NPC : MonoBehaviour
{
    public NavMeshAgent agent;
    public float speed;
    public Transform[] wps;
    Transform wayPoint;
    public float distance;

    public Animation anim;
    public string walkAnim;

    // Start is called before the first frame update
    void Start()
    {
        wayPoint = wps[Random.Range(0, wps.Length)];
        anim.Play(walkAnim);
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, wayPoint.position);

        if(distance < 3)
        {
            wayPoint = wps[Random.Range(0,wps.Length)];
        }

        agent.destination = wayPoint.position;

        agent.speed = speed;
    }
}
