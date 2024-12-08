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
    /// The Enter method is responsible for executing the logic when the state is entered.
    /// It is a virtual method that can be overridden by the derived classes.
    /// It is inirrated from the IState interface.
    /// </summary>
    public override void Enter()
    {
        Debug.Log("Entering Chase Player State");

        entityAnimator = entityFSM.entityProprieties.animator;
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

        Rigidbody2D player = enemyMovement.player;

        float speed = entityFSM.entityProprieties.Speed;

        Vector2 directionToPlayer = Utils.NormalizeDirectionVector(player.position - enemy.position);


        if (ChangeState(enemyClass.IsIndependent,enemyMovement,directionToPlayer))
        {
            return;
        } 

        enemy.velocity = directionToPlayer * speed;    

        UpdateAnimator();
    }


    private bool ChangeState(bool independet,EnemyMovement enemyMovement, Vector2 directionToPlayer)
    {
        if (enemyMovement.EnemyIsReadyToAttack(Utils.NormalizeDirectionVector(directionToPlayer)))
        {
            entityFSM.ChangeState(new EntityAttackState(entityFSM));
            return true;
        }

        if (!enemyMovement.IsPathClear(directionToPlayer))
        {
            entityFSM.ChangeState(new EnemyAvoidObstaclesState(entityFSM));
            return true;
        }

        if (independet && !enemyMovement.PlayerInRange())
        {
            entityFSM.ChangeState(new EntityIdleState(entityFSM));
            return true;
        }


        return false;
    }


    /// <summary>
    /// The Exit method is responsible for executing the logic when the state is exited.
    /// It is a virtual method that can be overridden by the derived classes.
    /// It is inirrated from the IState interface.
    /// </summary>
    public override void Exit()
    {
        Debug.Log("Exiting Chase State");
    }


    protected override void UpdateAnimator()
    {
        Vector2 enemyDirection = Utils.NormalizeDirectionVector(entityFSM.entityProprieties.entity.velocity);

        entityFSM.entityProprieties.animator.SetFloat("Horizontal", enemyDirection.x);
        entityFSM.entityProprieties.animator.SetFloat("Vertical", enemyDirection.y);
        entityFSM.entityProprieties.animator.SetFloat("Speed", enemyDirection.sqrMagnitude);
    }
}
