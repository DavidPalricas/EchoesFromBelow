using UnityEngine;

/// <summary>
/// The EntityIdleState class is responsible for handling the entity's idle state.
/// </summary>
public class EntityIdleState : EntityStateBase
{   /// <summary>
    ///  The EntityIdleState constructor is responsible for initializing the entity's idle state.
    /// </summary>
    /// <param name="entityFSM"></param>
    public EntityIdleState(EntityFSM entityFSM) : base(entityFSM) { }

    /// <summary>
    /// The Enter method is responsible for entering the idle state.
    /// </summary>
    public override void Enter()
    {
        Debug.Log("Entering Idle State");
    }

    public override void Execute()
    {
        Debug.Log("Executing Idle State");
    }

    public override void Exit()
    {
        Debug.Log("Exiting Idle State");
    }
}
