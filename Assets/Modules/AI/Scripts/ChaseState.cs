using UnityEngine;

/// <summary>
/// State of NPC that is entered when a target comes too close to the NPC, it will move in the direction of the target until it meets 
/// the condition of entering a AttackState or becomes too far away in which case it will return back to its waypoints to initiate a PatrolState.
/// </summary>
public class ChaseState : AIState
{
    public override void Enter(AIAgent agent)
    {
        agent.navAgent.speed = 4.5f;
    }

    public override void Update(AIAgent agent)
    {
        // Navigate to the target
        agent.navAgent.SetDestination(agent.target.position);

        float dist = Vector3.Distance(agent.transform.position, agent.target.position);
        if (dist > 15f)
        {
            // Go back to patrolling if the target gets too far away
            agent.ChangeState(new PatrolState());
        }
        else if (dist <= 2f)
        {
            // If very close to the target begin attacking
            agent.ChangeState(new AttackState());
        }
    }

    public override void Exit(AIAgent agent) { }
}
