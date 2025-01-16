using UnityEngine;

/// <summary>
/// The EntityAttackState class is responsible for handling the logic of the attack state of the entity.
/// The entity can be an enemy or the player.
/// </summary>
public class EntityAttackState : EntityStateBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityAttackState"/> class.
    /// </summary>
    /// <param name="entityFSM">The entity's finite state machine.</param>
    public EntityAttackState(EntityFSM entityFSM) : base(entityFSM) { }

    /// <summary>
    /// The Enter method is responsible for entering the entity's idle state.
    /// It overrides the Enter method from the base class (EntityStateBase).
    /// It is responsible for setting the entity's animator and executing the logic of the entity according to its type (enemy or player).
    /// </summary>
    public override void Enter()
    {
        // Debug.Log("Entering Attack State");

        entityAnimator = entityFSM.entityProprieties.animator;

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
    /// The Execute method is responsible for executing the logic of the entity's dead state.
    /// It overrides the Execute method from the base class (EntityStateBase).
    /// </summary>
    public override void Execute()
    {
        if (entityFSM.entitycurrentHealth <= 0)
        {
            entityFSM.ChangeState(new EntityDeadState(entityFSM));
            return;
        }

        EntityAttack entityAttack = entityFSM.entityProprieties.attack;

        if (!entityAttack.attacking)
        {
            entityFSM.ChangeState(new EntityIdleState(entityFSM));
        }
    }

    /// <summary>
    /// The Exit method is responsible for executing the logic when the entity's dead state is exited.
    /// It overrides the Exit method from the base class (EntityStateBase).
    /// </summary>
    public override void Exit()
    {
        // Debug.Log("Exiting Attack State");
        entityAnimator.SetBool("IsAttacking", false);
    }

    /// <summary>
    /// The ExecuteEnemyLogic method is responsible for executing the logic of the enemy entity.
    /// It overrides the ExecuteEnemyLogic method from the base class (EntityStateBase).
    /// It checks if the enemy's health is less than or equal to 0 if it is, it changes the state to the dead state.
    /// It also checks if the player is dead, if it is, it changes the state to the idle state.
    /// If any of these conditions are not met, it executes the enemy's attack logic, by findiing the its attack direction.
    /// If the enemy attack direction is invalid (represented by Vector2.zero) it changes the state to the idle state.
    /// Otherwise the enemy attacks the player and updates the animator to play the attack animation.
    /// </summary>
    protected override void ExecuteEnemyLogic()
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

        EntityAttack enemyAttack = enemyClass.attack;

        Vector2 enemyAttackDirection = Utils.GetUnitaryVector(enemyClass.lastMovingDirection);

        if (enemyAttackDirection == Vector2.zero)
        {
            entityFSM.ChangeState(new EntityIdleState(entityFSM));
            return;
        }

        UpdateAnimator();
        enemyAttack.Attack(enemyAttackDirection);
    }

    /// <summary>
    /// The ExecutePlayerLogic method is responsible for executing the logic of the player entity.
    /// It overrides the ExecutePlayerLogic method from the base class (EntityStateBase).
    /// The method starts for getting the player's attack direction (last moving direction).
    /// If its last moving direction is invalid (represented by Vector2.zero) it changes the state to the idle state.
    /// Otherwise the player attacks the enemy and updates the animator to play the attack animation.
    /// </summary>
    protected override void ExecutePlayerLogic()
    {
        Player playerClass = (Player)entityFSM.entityProprieties;

        EntityAttack playerAttack = playerClass.attack;

        Vector2 playerAttackDirection = playerClass.lastMovingDirection;

        if (playerAttackDirection == Vector2.zero)
        {   
            entityFSM.ChangeState(new EntityIdleState(entityFSM));
            return;
        }

        UpdateAnimator();

        playerAttack.Attack(playerAttackDirection);
    }

    /// <summary>
    /// The UpdateAnimator method is responsible for updating the animator of the entity.
    /// It overrides the UpdateAnimator method from the base class (EntityStateBase), to play the attack animation of the entity.
    /// </summary>
    protected override void UpdateAnimator()
    {
        Entity entity  = entityFSM.entityProprieties;

        Vector2 attackDirection = entity.lastMovingDirection;

        entityAnimator.SetBool("IsAttacking", true);
        entityAnimator.SetFloat("LastHorizontal", attackDirection.x);
        entityAnimator.SetFloat("LastVertical", attackDirection.y);
    }
}
