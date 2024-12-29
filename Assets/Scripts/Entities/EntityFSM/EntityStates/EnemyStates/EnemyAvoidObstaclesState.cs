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
    /// The alternativeDirection property is responsible for storing the enemy's alternative direction to move.
    /// </summary>
    private Vector2 alternativeDirection;

    /// <summary>
    /// The Enter method is responsible for entering the enemy's avoid obstacle state.
    /// It overrides the Enter method from the base class (EntityStateBase).
    /// </summary>
    public override void Enter()
    {
        Debug.Log("Entering avoid obstacles state");
    }

    /// <summary>
    /// The Execute method is responsible for executing the logic of the entity's idle state.
    /// It overrides the Execute method from the base class (EntityStateBase).
    /// First it checks if the entity's state should be changed.
    /// If the entity's health is less than or equal to 0, if so, it changes the state to the dead state, if the player is dead, it changes the state to the idle state.
    /// If the state is not changed, it gets the enemy direction to the player and checks if the path is clear.
    /// If the path is clear, it changes the state to the chase state, otherwise it finds the alternative direction to move by calling the FindAlternativeDirection method.
    /// If the alternative direction is zero, it changes the state to the idle state, otherwise it moves the enemy to the alternative direction.
    /// </summary>
    public override void Execute()
    {
        if (entityFSM.entitycurrentHealth <= 0)
        {
            entityFSM.ChangeState(new EntityDeadState(entityFSM));
            return;
        }

        if (!Utils.IsPlayerAlive())
        {
            entityFSM.ChangeState(new EntityIdleState(entityFSM));
            return;
        }

        Enemy enemyClass = (Enemy)entityFSM.entityProprieties;
        EnemyMovement enemyMovement = enemyClass.movement;

        Rigidbody2D enemyRigidBody = entityFSM.entityProprieties.entityRigidBody;

        Vector2 directionToPlayer = (enemyMovement.playerRigidBody.position - enemyRigidBody.position).normalized;

        if (enemyMovement.IsPathClear(directionToPlayer))
        {
            entityFSM.ChangeState(new EnemyChaseState(entityFSM));
            return;
        }

        alternativeDirection = enemyMovement.FindAlternativeDirection(directionToPlayer);

        if (alternativeDirection == Vector2.zero)
        {
            entityFSM.entityProprieties.lastMovingDirection = directionToPlayer;
            entityFSM.ChangeState(new EntityIdleState(entityFSM));
            return;
        }

        enemyMovement.MoveEnemy(alternativeDirection);
    }

    /// <summary>
    /// The Exit method is responsible for executing the logic when the enemy's avoid obstacle state is exited.
    /// It overrides the Exit method from the base class (EntityStateBase).
    /// </summary>
    public override void Exit()
    {
        Debug.Log("Exiting avoid obstacles State");
    }

    /// <summary>
    /// The UpdateAnimator method is responsible for updating the animator of the entity.
    /// It overrides the UpdateAnimator method from the base class (EntityStateBase), to play the enemy's movement animation of the entity.
    /// </summary>
    protected override void UpdateAnimator()
    {
        entityAnimator.SetFloat("Horizontal", alternativeDirection.x);
        entityAnimator.SetFloat("Vertical", alternativeDirection.y);
        entityAnimator.SetFloat("Speed", entityFSM.entityProprieties.speed);
    }
}
