using UnityEngine;

/// <summary>
/// The EnemyChaseState class is responsible for handling the logic of the chase state of the enemy.
/// </summary>
public class EnemyChaseState : EntityStateBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EnemyChaseState"/> class.
    /// </summary>
    /// <param name="entityFSM">The entity's finite state machine.</param>
    public EnemyChaseState(EntityFSM entityFSM) : base(entityFSM) { }

    /// <summary>
    /// The directionToPlayer property is responsible for storing the direction to the player.
    /// </summary>
    private Vector2 directionToPlayer;

    /// <summary>
    /// The Enter method is responsible for entering the enemy's chase state.
    /// It overrides the Enter method from the base class (EntityStateBase).
    /// </summary>
    public override void Enter()
    {
        // Debug.Log("Entering Chase State");

        entityAnimator = entityFSM.entityProprieties.animator;
    }

    /// <summary>
    /// The Execute method is responsible for executing the logic of the enemy's chase state.
    /// It gets the enemy direction to the player and calls the ChangeState method, to check if the state should be changed.
    /// If the state should not be changed, the enemy moves towards the player and the animator is updated.
    /// </summary>
    public override void Execute()
    {   
        // Debug.Log("Executing Chase State");

        Enemy enemyClass = (Enemy)entityFSM.entityProprieties;

        EnemyMovement enemyMovement = enemyClass.movement;

        Rigidbody2D enemy = entityFSM.entityProprieties.entityRigidBody;

        Rigidbody2D playerRigidBody = enemyMovement.playerRigidBody;

        directionToPlayer = (playerRigidBody.position - enemy.position).normalized;

        if (ChangeState(enemyClass.IsIndependent,enemyMovement,directionToPlayer))
        {
            return;
        }

        enemyMovement.MoveEnemy(directionToPlayer);

        UpdateAnimator();
    }

    /// <summary>
    /// The ChangeState method is responsible for checking if the enemy's state should be changed.
    /// If the enemy's health is less than or equal to 0, the state is changed to the dead state.
    /// If the enemy is independent and the player is not in range or the player is not alive, the state is changed to the idle state.
    /// If the enemy attack conditions are met, the state is changed to the attack state.
    /// If the enemy's path is not clear, the state is changed to the avoid obstacles state.
    /// </summary>
    /// <param name="independet">if the enemy is not in a horde <c>true</c> [independet].</param>
    /// <param name="enemyMovement">The enemy movement class.</param>
    /// <param name="directionToPlayer">The direction of the enemy towards to the player.</param>
    /// <returns></returns>
    private bool ChangeState(bool independet,EnemyMovement enemyMovement, Vector2 directionToPlayer)
    {   
        if (entityFSM.entitycurrentHealth <= 0)
        {
            entityFSM.ChangeState(new EntityDeadState(entityFSM));
            return true;
        }

        if (independet && !enemyMovement.PlayerInRange() || !Utils.IsPlayerAlive())
        {
            entityFSM.ChangeState(new EntityIdleState(entityFSM));
            return true;
        }

        if (enemyMovement.EnemyIsReadyToAttack(directionToPlayer))
        {   
            entityFSM.entityProprieties.lastMovingDirection = directionToPlayer;
            entityFSM.ChangeState(new EntityAttackState(entityFSM));
            return true;
        }

        if (!enemyMovement.IsPathClear(directionToPlayer))
        {
            entityFSM.ChangeState(new EnemyAvoidObstaclesState(entityFSM));
            return true;
        }

        return false;
    }

    /// <summary>
    /// The Exit method is responsible for executing the logic when the enemy's chase state is exited.
    /// It overrides the Exit method from the base class (EntityStateBase).
    /// </summary>
    public override void Exit()
    {
        // Debug.Log("Exiting Chase State");
    }

    /// <summary>
    /// The UpdateAnimator method is responsible for updating the animator of the entity.
    /// It overrides the UpdateAnimator method from the base class (EntityStateBase), to play the enemy's movement animation of the entity.
    /// </summary>
    protected override void UpdateAnimator()
    {   
        entityFSM.entityProprieties.animator.SetFloat("Horizontal", directionToPlayer.x);
        entityFSM.entityProprieties.animator.SetFloat("Vertical", directionToPlayer.y);
        entityFSM.entityProprieties.animator.SetFloat("Speed", directionToPlayer.sqrMagnitude);
    }
}
