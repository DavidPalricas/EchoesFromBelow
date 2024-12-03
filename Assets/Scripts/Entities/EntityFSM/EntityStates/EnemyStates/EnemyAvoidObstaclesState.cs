using UnityEngine;

/// <summary>
/// The EnemyAvoidObstaclesState class is responsible for handling the logic of the avoid obstacles state of the enemy.
/// </summary>
public class EnemyAvoidObstaclesState : EntityStateBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EnemyAvoidObstaclesState"/> class.
    /// </summary>
    /// <param name="entityFSM">The entity's finite state machine.</param>
    public EnemyAvoidObstaclesState(EntityFSM entityFSM) : base(entityFSM) { }

    /// <summary>
    /// The Enter method is responsible for executing the logic when the state is entered.
    /// It is a virtual method that can be overridden by the derived classes.
    /// It is inirrated from the IState interface.
    /// </summary>
    public override void Enter()
    {
        Debug.Log("Entering Attack State");
    }

    /// <summary>
    /// The Execute method is responsible for executing the logic of the state.
    /// It is a virtual method that can be overridden by the derived classes.
    /// It is inirrated from the IState interface.
    /// </summary>
    public override void Execute()
    {
        Debug.Log("Executing Attack State");
    }

    /// <summary>
    /// The Exit method is responsible for executing the logic when the state is exited.
    /// It is a virtual method that can be overridden by the derived classes.
    /// It is inirrated from the IState interface.
    /// </summary>
    public override void Exit()
    {
        Debug.Log("Exiting Attack State");
    }
}
