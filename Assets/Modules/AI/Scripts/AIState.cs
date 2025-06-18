using UnityEngine;

/// <summary>
/// Abstract class containing the three methods all states must have, implemented by all state classes.
/// </summary>
public abstract class AIState
{
    /// <summary>
    /// Enter provided agent into a new state.
    /// </summary>
    public abstract void Enter(AIAgent agent);

    /// <summary>
    /// Called every frame to check if the conditions for state change are met or if we just carry on.
    /// </summary>
    public abstract void Update(AIAgent agent);

    /// <summary>
    /// Exits the current state in preperation for moving to another.
    /// </summary>
    public abstract void Exit(AIAgent agent);
}
