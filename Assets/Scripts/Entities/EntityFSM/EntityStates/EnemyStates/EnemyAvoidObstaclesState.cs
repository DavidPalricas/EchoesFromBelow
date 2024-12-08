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
        Debug.Log("Entering avoid obstacles state");
    }

    /// <summary>
    /// The Execute method is responsible for executing the logic of the state.
    /// It is a virtual method that can be overridden by the derived classes.
    /// It is inirrated from the IState interface.
    /// </summary>
    public override void Execute()
    {   
        Enemy enemyClass = (Enemy)entityFSM.entityProprieties;
        EnemyMovement enemyMovement = enemyClass.movement;

        Rigidbody2D enemy = entityFSM.entityProprieties.entity;
        float speed = entityFSM.entityProprieties.Speed;

        Vector2 directionToPlayer = Utils.NormalizeDirectionVector(enemyMovement.player.position -  enemy.position);

        if (enemyMovement.IsPathClear(directionToPlayer))
        {
            entityFSM.ChangeState(new EnemyChaseState(entityFSM));
            return;
        }

        Vector2 alternativeDirection = enemyMovement.FindAlternativeDirection(directionToPlayer);
        enemy.velocity = alternativeDirection * speed;
    }

    /// <summary>
    /// The Exit method is responsible for executing the logic when the state is exited.
    /// It is a virtual method that can be overridden by the derived classes.
    /// It is inirrated from the IState interface.
    /// </summary>
    public override void Exit()
    {
        Debug.Log("Exiting avoid obstacles State");
    }

    protected override void UpdateAnimator()
    {
        Vector2 enemyDirection = Utils.NormalizeDirectionVector(entityFSM.entityProprieties.entity.velocity);

        entityAnimator.SetFloat("Horizontal", enemyDirection.x);
        entityAnimator.SetFloat("Vertical", enemyDirection.y);
        entityAnimator.SetFloat("Speed", entityFSM.entityProprieties.Speed);
    }
}
