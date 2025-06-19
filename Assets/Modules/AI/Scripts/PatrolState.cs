using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Default state of NPCs, moves between waypoints defined in the empty game object "Waypoints". Will leave this state if conditions in Update are met.
/// </summary>
public class PatrolState : AIState
{
    private int currentWaypoint = 0;
    private Transform[] waypoints;

    public PatrolState(Transform[] waypoints)
    {
        this.waypoints = waypoints;
    }

    public override void Enter(AIAgent agent)
    { 
        // Then navigate to the first waypoint
        agent.navAgent.SetDestination(waypoints[currentWaypoint].position);
    }

    public override void Update(AIAgent agent)
    {
        if (!agent.navAgent.pathPending && agent.navAgent.remainingDistance < 0.5f)
        {
            // Move to the next waypoint, uses the modulo operator to loop infinitely between the waypoints
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            agent.navAgent.SetDestination(waypoints[currentWaypoint].position);
        }

        // Detect target if they are within range
        Vector3 origin = agent.transform.position + Vector3.up * 1.5f;
        Vector3 directionToTarget = (agent.target.position - origin).normalized;
        float distanceToTarget = Vector3.Distance(origin, agent.target.position);
        float angleToTarget = Vector3.Angle(agent.transform.forward, directionToTarget);

        Debug.DrawRay(origin, directionToTarget * agent.stats.detectionRange, Color.yellow);
        // Casts a angled raycast that is fieldOfView wide and detectionRange long, these are set in AIStats ScriptableObjects
        if (distanceToTarget <= agent.stats.detectionRange && angleToTarget <= agent.stats.fieldOfView / 2f)
        {
            if (Physics.Raycast(origin, directionToTarget, out RaycastHit hit, agent.stats.detectionRange))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    Debug.Log("FOV DETECTED PLAYER");
                    agent.ChangeState(new ChaseState(waypoints));
                }
            }
        }

    }

    public override void Exit(AIAgent agent) { }
}
