using UnityEngine.AI;
using UnityEngine;

/// <summary>
/// Handles behaviour of agent/NPC, changes the state of the AI based on conditions specified in State classes.
/// </summary>
public class AIAgent : MonoBehaviour
{
    public NavMeshAgent navAgent;
    public Transform target;

    private AIState currentState;

    void Start()
    {
        // Patrols by default
        navAgent = GetComponent<NavMeshAgent>();
        ChangeState(new PatrolState());
    }

    void Update()
    {
        currentState?.Update(this);
    }

    public void ChangeState(AIState newState)
    {
        // Question mark prevents nulls from causing issues, only exiting if not null
        currentState?.Exit(this);
        currentState = newState;
        currentState.Enter(this);
    }
}
