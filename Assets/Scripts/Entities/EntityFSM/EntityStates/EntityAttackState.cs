using UnityEngine;

/// <summary>
/// The EntityAttackState class is responsible for handling the logic of the attack state of the entity.
/// </summary>
public class EntityAttackState : EntityStateBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityAttackState"/> class.
    /// </summary>
    /// <param name="entityFSM">The entity's finite state machine.</param>
    public EntityAttackState(EntityFSM entityFSM) : base(entityFSM) { }

    /// <summary>
    /// The Enter method is responsible for executing the logic when the state is entered.
    /// It is a virtual method that can be overridden by the derived classes.
    /// It is inirrated from the IState interface.
    /// </summary>
    public override void Enter()
    {
        Debug.Log("Entering Attack State");

        entityAnimator = entityFSM.entityProprieties.animator;

        if (entityFSM.entityProprieties is Enemy enemyClass)
        {
            enemyClass.attack.Attacking = true;
        }
    }

    /// <summary>
    /// The Execute method is responsible for executing the logic of the state.
    /// It is a virtual method that can be overridden by the derived classes.
    /// It is inirrated from the IState interface.
    /// </summary>
    public override void Execute()
    {
        if (entityFSM.entityProprieties is Enemy)
        {   
            ExecuteEnemyLogic();
        }
    }

    /// <summary>
    /// The Exit method is responsible for executing the logic when the state is exited.
    /// It is a virtual method that can be overridden by the derived classes.
    /// It is inirrated from the IState interface.
    /// </summary>
    public override void Exit()
    {
        Debug.Log("Exiting Attack State");
        entityAnimator.SetBool("IsAttacking", false);
    }


    protected override void ExecuteEnemyLogic()
    {   
        Enemy enemyClass = (Enemy)entityFSM.entityProprieties;

        EnemyAttack enemyAttack = enemyClass.attack;

        if (!enemyAttack.Attacking)
        {
            entityFSM.ChangeState(new EntityIdleState(entityFSM));

            return;
        }

        EnemyMovement enemyMovement = enemyClass.movement;

        Rigidbody2D enemy = enemyClass.entity;

        Vector2 enemyAttackDirection = enemyMovement.attackDirection;

        if (enemyAttackDirection == Vector2.zero)
        {
            return;
        }

        UpdateAnimator();
        enemyAttack.SetAttackArea(enemyAttackDirection);
        enemyAttack.Attack();
        enemyAttack.HandleAttackCooldown();
    }


    protected override void UpdateAnimator()
    {
        Enemy enemyClass = (Enemy)entityFSM.entityProprieties;
        Vector2 attackDirection = enemyClass.movement.attackDirection;

        entityAnimator.SetBool("IsAttacking", true);
        entityAnimator.SetFloat("LastHorizontal", attackDirection.x);
        entityAnimator.SetFloat("LastVertical", attackDirection.y);

    }
}
