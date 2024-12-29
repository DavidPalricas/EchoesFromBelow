
using UnityEngine;

/// <summary>
/// The EntityMovementState class is responsible for handling the logic of the movement state of the entity.
/// </summary>
public class EntityMovementState : EntityStateBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityMovementState"/> class.
    /// </summary>
    /// <param name="entityFSM">The entity's finite state machine.</param>
    public EntityMovementState(EntityFSM entityFSM) : base(entityFSM) { }

    /// <summary>
    /// The player property is responsible for storing the a Player class instance.
    /// </summary>
    private Player player;

    /// <summary>
    /// The Enter method is responsible for entering the players's movement state.
    /// It overrides the Enter method from the base class (EntityStateBase).
    /// </summary>
    public override void Enter() 
    {    
        entityAnimator = entityFSM.entityProprieties.animator;

        player = (Player)entityFSM.entityProprieties;

        UpdateAnimator();

        Debug.Log("Entering Move State");
    }

    /// <summary>
    /// The Execute method is responsible for executing the logic of the state.
    /// It is a virtual method that can be overridden by the derived classes.
    /// It is inirrated from the IState interface.
    /// </summary>
    public override void Execute()
    {
        if (entityFSM.entitycurrentHealth <= 0)
        {
            entityFSM.ChangeState(new EntityDeadState(entityFSM));
            return;
        }

        player.movement.MovePlayer();

        if (player.movement.speedVector == Vector2.zero)
        {
            entityFSM.ChangeState(new EntityIdleState(entityFSM));

            return;
        }

        player.lastMovingDirection = player.movement.speedVector;

        if (Input.GetKeyDown(KeyCode.Space) && player.attack.enabled)
        {
            entityFSM.entityProprieties.lastMovingDirection = player.movement.speedVector;
            player.movement.StopPlayer();
            entityFSM.ChangeState(new EntityAttackState(entityFSM));

            return;
        }

        UpdateAnimator();
    }

    /// <summary>
    /// The Exit method is responsible for executing the logic when the state is exited.
    /// It is a virtual method that can be overridden by the derived classes.
    /// It is inirrated from the IState interface.
    /// </summary>
    public override void Exit()
    {
        Debug.Log("Exiting Move State");
    }

    /// <summary>
    /// The UpdateAnimator method is responsible for updating the animator of the entity.
    /// It overrides the UpdateAnimator method from the base class (EntityStateBase), to play the player's movement animation of the entity.
    /// </summary>
    protected override void UpdateAnimator()
    {   
        Vector2 speedVector = player.movement.speedVector;

        entityAnimator.SetFloat("Horizontal", speedVector.x);
        entityAnimator.SetFloat("Vertical", speedVector.y);
        entityAnimator.SetFloat("Speed", speedVector.sqrMagnitude);

        entityAnimator.SetFloat("LastHorizontal", player.lastMovingDirection.x);
        entityAnimator.SetFloat("LastVertical", player.lastMovingDirection.y);
    }
}
