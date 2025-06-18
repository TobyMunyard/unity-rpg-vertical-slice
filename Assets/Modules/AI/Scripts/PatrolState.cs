using System.Linq;
using UnityEngine;

/// <summary>
/// Default state of NPCs, moves between waypoints defined in the empty game object "Waypoints". Will leave this state if conditions in Update are met.
/// </summary>
public class PatrolState : AIState
{
    private int currentWaypoint = 0;
    private Transform[] waypoints; // Set in Enter, gets waypoints from empty game object "Waypoints"

    public override void Enter(AIAgent agent)
    {
        // Get the waypoint game object and all its children (the actual waypoints)
        waypoints = GameObject.Find("Waypoints")
                    .GetComponentsInChildren<Transform>()
                    .Where(t => t != t.parent).ToArray(); 
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
            agent.ChangeState(new ChaseState());
        }
    }

    public override void Exit(AIAgent agent) { }
}
