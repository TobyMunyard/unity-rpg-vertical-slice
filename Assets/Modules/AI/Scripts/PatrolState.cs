using System.Linq;
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
        if (Vector3.Distance(agent.transform.position, agent.target.position) < 10f)
        {
            agent.ChangeState(new ChaseState(waypoints));
        }
    }

    public override void Exit(AIAgent agent) { }
}
