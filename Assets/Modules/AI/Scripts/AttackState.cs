using UnityEngine;

/// <summary>
/// State where NPC is attacking a target, must be very close to the target to enter and stay in this state. Will return to 
/// ChaseState if target moves away.
/// </summary>
public class AttackState : AIState
{
    [SerializeField ]private float attackCooldown = 1.5f;
    private float nextAttackTime = 0f;

    private Transform[] patrolStateWaypoints;

    public AttackState(Transform[] patrolStateWaypoints)
    {
        this.patrolStateWaypoints = patrolStateWaypoints;
    }

    public override void Enter(AIAgent agent)
    {
        // Stop moving so you can begin attacking
        agent.navAgent.isStopped = true;
    }

    public override void Update(AIAgent agent)
    {
        if (Time.time >= nextAttackTime)
        {
            // If cooldown has passed attack the target
            Debug.Log("Enemy attack");
            nextAttackTime = Time.time + attackCooldown;
            // PLAY SOME ANIMATION
            agent.target.GetComponent<PlayerStats>().health -= agent.stats.damagePerHit;
            Debug.Log("Player health: " + agent.target.GetComponent<PlayerStats>().health);
        }

        float dist = Vector3.Distance(agent.transform.position, agent.target.position);
        if (dist > 2.5f)
        {
            // If target is too far away to attack then go back to chasing
            agent.navAgent.isStopped = false;
            agent.ChangeState(new ChaseState(patrolStateWaypoints));
        }
    }

    public override void Exit(AIAgent agent)
    {
        agent.navAgent.isStopped = false;
    }
}
