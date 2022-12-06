using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cart : MonoBehaviour
{
    public Transform Player;
    public float minDistance;

    private NavMeshAgent agent;
    private NavMeshPath path;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = minDistance;
        path = new NavMeshPath();
    }

    void Update()
    {
        agent.CalculatePath(Player.position, path);
        if (path.status == NavMeshPathStatus.PathComplete)
        {
            if (Vector3.Distance(Player.position, transform.position) > minDistance)
            {
                agent.SetDestination(Player.position);
            }
        }
    }
}