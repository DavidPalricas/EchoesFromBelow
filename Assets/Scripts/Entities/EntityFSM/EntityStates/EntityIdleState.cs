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

        entityAnimator = entityFSM.entityProprieties.animator;

        UpdateAnimator();

        entityFSM.entityProprieties.entity.velocity = Vector2.zero;
    }

    public override void Execute()
    {
        if (entityFSM.entityProprieties is Enemy)
        {
            ExecuteEnemyLogic();
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting Idle State");
    }


    protected override void ExecuteEnemyLogic()
    {

        Enemy enemy = (Enemy)entityFSM.entityProprieties;
        
        if (!enemy.IsIndependent || enemy.IsIndependent && enemy.movement.PlayerInRange())
        {
            entityFSM.ChangeState(new EnemyChaseState(entityFSM));
        }
    }


    protected override void UpdateAnimator()
    {   
        Vector2 entityDirection = Utils.NormalizeDirectionVector(entityFSM.entityProprieties.entity.velocity);
        entityAnimator.SetFloat("Horizontal", entityDirection.x);
        entityAnimator.SetFloat("Vertical", entityDirection.y);
        entityAnimator.SetFloat("Speed", 0);
    }
}
