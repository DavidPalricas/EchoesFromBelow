using UnityEngine;

/// <summary>
/// The EntityIdleState class is responsible for handling the entity's idle state.
/// An entity can be an enemy or the player
/// </summary>
public class EntityIdleState : EntityStateBase
{   /// <summary>
    ///  The EntityIdleState constructor is responsible for initializing the entity's idle state.
    /// </summary>
    /// <param name="entityFSM"></param>
    public EntityIdleState(EntityFSM entityFSM) : base(entityFSM) { }

    /// <summary>
    /// The Enter method is responsible for entering the entity's idle state.
    /// It overrides the Enter method from the base class (EntityStateBase).
    /// </summary>
    public override void Enter()
    {
        // Debug.Log("Entering Idle State");
        entityAnimator = entityFSM.entityProprieties.animator;

        UpdateAnimator();

        entityFSM.entityProprieties.entityRigidBody.velocity = Vector2.zero;
    }

    /// <summary>
    /// The Execute method is responsible for executing the logic of the entity's idle state.
    /// It overrides the Execute method from the base class (EntityStateBase).
    /// If the entity's health is less than or equal to 0, it changes the state to the dead state.
    /// Otherwise it executes the logic of the entity according to its type (enemy or player).
    /// </summary>
    public override void Execute()
    {   
        // Debug.Log("Executing Idle State");

        if (entityFSM.entitycurrentHealth <= 0)
        {
            entityFSM.ChangeState(new EntityDeadState(entityFSM));
            return;
        }

        if (entityFSM.entityProprieties is Enemy)
        {
            ExecuteEnemyLogic();
        }
        else
        {
            ExecutePlayerLogic();
        }
    }

    /// <summary>
    /// The Exit method is responsible for executing the logic when the entity's idle state is exited.
    /// It overrides the Exit method from the base class (EntityStateBase).
    /// </summary>
    public override void Exit()
    {
        // Debug.Log("Exiting Idle State");
    }

    /// <summary>
    /// The ExecuteEnemyLogic method is responsible for executing the logic of the enemy entity.
    /// It overrides the ExecuteEnemyLogic method from the base class (EntityStateBase).
    /// It checks if the condition to change the enemy chase state are met.
    /// These conditions are: if the player is alive and if the enemy is not independent or not (if it is in a horde or not).
    /// If the enemy is not independent, it automatically changes the state to the chase state otherwise it checks if the player is in range.
    /// Otherwise, it maintains the enemy in the idle state.
    /// </summary>
    protected override void ExecuteEnemyLogic()
    {
        Enemy enemy = (Enemy)entityFSM.entityProprieties;

        if (Utils.IsPlayerAlive())
        {
            if (!enemy.IsIndependent || enemy.IsIndependent && enemy.movement.PlayerInRange())
            {
                entityFSM.ChangeState(new EnemyChaseState(entityFSM));
            }
        }
    }

    /// <summary>
    /// The ExecutePlayerLogic method is responsible for executing the logic of the player entity.
    /// It overrides the ExecutePlayerLogic method from the base class (EntityStateBase).
    /// It checks if the player is moving and if the player is attacking (pressing the space key).
    /// If the player is moving, it changes the state to the movement state, if the player is attacking, it changes the state to the attack state.
    /// Otherwise, it maintains the player in the idle state.
    /// </summary>
    protected override void ExecutePlayerLogic()
    {
        Player player = (Player)entityFSM.entityProprieties;
  
        player.movement.MovePlayer();

        if (player.movement.speedVector != Vector2.zero)
        {
            entityFSM.ChangeState(new EntityMovementState(entityFSM));

            return;
        }

        if (player.GetComponent<PlayerActions>().InputTriggered("Attack") && player.attack.enabled)
        {
            entityFSM.ChangeState(new EntityAttackState(entityFSM));
        }
    }

    /// <summary>
    /// The UpdateAnimator method is responsible for updating the animator of the entity.
    /// It overrides the UpdateAnimator method from the base class (EntityStateBase), to play the idle animation of the entity.
    /// </summary>
    protected override void UpdateAnimator()
    {   
        Vector2 entityDirection = Utils.GetUnitaryVector(entityFSM.entityProprieties.lastMovingDirection);

        entityAnimator.SetFloat("Horizontal", entityDirection.x);
        entityAnimator.SetFloat("Vertical", entityDirection.y);
        entityAnimator.SetFloat("Speed", 0);
    }
}
