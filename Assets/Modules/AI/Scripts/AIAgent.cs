using UnityEngine.AI;
using UnityEngine;

/// <summary>
/// Handles behaviour of agent/NPC, changes the state of the AI based on conditions specified in State classes.
/// </summary>
public class AIAgent : MonoBehaviour
{
    public NavMeshAgent navAgent;
    public Transform target;
    public PatrolPath patrolPath;
    public AIStats stats;

    private AIState currentState;

    public Animator animator;

    void Start()
    {
        // Patrols by default
        navAgent = GetComponent<NavMeshAgent>();
        if (patrolPath != null)
        {
            Transform[] waypoints = patrolPath.GetWaypoints();
            ChangeState(new PatrolState(waypoints));
        }
    }

    void Update()
    {
        currentState?.Update(this);
        PerformDetectionCheck();
    }

    public void ChangeState(AIState newState)
    {
        // Question mark prevents nulls from causing issues, only exiting if not null
        currentState?.Exit(this);
        currentState = newState;
        currentState.Enter(this);
    }

    private void OnDrawGizmosSelected()
    {
        if (stats == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, stats.detectionRange);

        Vector3 left = Quaternion.Euler(0, -stats.fieldOfView / 2, 0) * transform.forward;
        Vector3 right = Quaternion.Euler(0, stats.fieldOfView / 2, 0) * transform.forward;

        Gizmos.DrawLine(transform.position, transform.position + left * stats.detectionRange);
        Gizmos.DrawLine(transform.position, transform.position + right * stats.detectionRange);
    }

    public void PerformDetectionCheck()
    {
        Vector3 origin = transform.position + Vector3.up * 1.5f;
        Vector3 directionToTarget = (target.position - origin).normalized;
        float distanceToTarget = Vector3.Distance(origin, target.position);
        float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);

        Debug.DrawRay(origin, directionToTarget * stats.detectionRange, Color.yellow);

        if (distanceToTarget <= stats.detectionRange && angleToTarget <= stats.fieldOfView / 2f)
        {
            if (Physics.Raycast(origin, directionToTarget, out RaycastHit hit, stats.detectionRange))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    Debug.Log("FOV DETECTED PLAYER");
                    ChangeState(new ChaseState(patrolPath.GetWaypoints()));
                }
            }
        }
    }
}
