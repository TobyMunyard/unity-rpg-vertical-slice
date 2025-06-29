using System.Collections;
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
    private bool isWaiting = false;
    private float waitDuration = 0f;
    private float waitTimer = 0f;

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
        bool isMoving = agent.navAgent.velocity.magnitude > 0.1f;
        agent.animator.SetBool("IsWalking", isMoving);

        if (isWaiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitDuration)
            {
                isWaiting = false;
                agent.navAgent.SetDestination(waypoints[currentWaypoint].position);
            }
        }
        else if (!agent.navAgent.pathPending && agent.navAgent.remainingDistance < 0.5f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            isWaiting = true;
            waitDuration = Random.Range(3f, 10f);
            waitTimer = 0f;
            agent.navAgent.velocity = Vector3.zero;
        }

    }

    public override void Exit(AIAgent agent)
    {
    }
}
