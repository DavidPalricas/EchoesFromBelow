using UnityEngine;

/// <summary>
/// The EntityDeadState class is responsible for handling the logic of the dead state of the entity.
/// An entity can be an enemy or the player.+
/// </summary>
public class EntityDeadState : EntityStateBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityDeadState"/> class.
    /// </summary>
    /// <param name="entityFSM">The entity's finite state machine.</param>
    public EntityDeadState(EntityFSM entityFSM) : base(entityFSM) { }

    /// <summary>
    /// The Enter method is responsible for entering the entity's dead state.
    /// It overrides the Enter method from the base class (EntityStateBase).
    /// It is responsible for setting the entity's animator and executing the logic of the entity according to its type (enemy or player).
    /// </summary>
    public override void Enter()
    {
        // Debug.Log("Entering Dead State");

        entityAnimator = entityFSM.entityProprieties.animator;

        entityFSM.entityProprieties.entityRigidBody.velocity = Vector2.zero;

        UpdateAnimator();

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
        // Debug.Log("Executing Dead State");
    }

    /// <summary>
    /// The Exit method is responsible for executing the logic when the entity's dead state is exited.
    /// It overrides the Exit method from the base class (EntityStateBase).
    /// </summary>
    public override void Exit()
    {
        // Debug.Log("Exiting Dead State");
    }

    /// <summary>
    /// The UpdateAnimator method is responsible for updating the animator of the entity.
    /// It overrides the UpdateAnimator method from the base class (EntityStateBase), to play the dead animation of the entity.
    /// </summary>
    protected override void UpdateAnimator()
    {
        entityAnimator.SetBool("IsDead", true);
    }

    /// <summary>
    /// The ExecuteEnemyLogic method is responsible for executing the logic of the enemy entity.
    /// It overrides the ExecuteEnemyLogic method from the base class (EntityStateBase).
    /// It starts to increment the number of skeletons kill and if the enemy is a boss it increments the number of boss killed and drops its item.
    /// After thath a coroutine is started to wait for the end of the death animation and create the enemy's dead body.
    /// </summary>
    protected override void ExecuteEnemyLogic()
    {
        Enemy enemyClass = (Enemy)entityFSM.entityProprieties;

        GameObject.Find("Level1").GetComponent<Rank>().SkeletonsKilled++;

        if (enemyClass.isBoss)
        {
            entityFSM.InstantiateItem(enemyClass.bossDropItem, enemyClass.entityRigidBody.transform.position);
            GameObject.Find("Level1").GetComponent<Rank>().BossKilled = true;
        }

        entityFSM.StartCoroutine(Utils.WaitForAnimationEnd(entityAnimator, "Death", CreateEnemyDeadBody));
    }

    /// <summary>
    /// The ExecutePlayerLogic method is responsible for executing the logic of the player entity.
    /// It overrides the ExecutePlayerLogic method from the base class (EntityStateBase).
    /// It increments the number of deaths of the player and calls the RespawnPlayer  method to respawn the player.
    /// </summary>
    protected override void ExecutePlayerLogic()
    {  
        Player player =  entityFSM.entityProprieties as Player;
        player.playerActions.enabled = false;

        GameObject.Find("Level1").GetComponent<Rank>().DeathsNumber++;
        RespawnPlayer();
    }

    /// <summary>
    /// The RespawnPlayer method is responsible for respawning the player.
    /// It waits for 4 seconds and then sets the player's health to the maximum value, updates the health bar, and sets the player's position to the spawn point.
    /// </summary>
    private void RespawnPlayer()
    {
        entityFSM.StartCoroutine(Utils.Wait(3f, () =>
        {    
            Player player = entityFSM.entityProprieties as Player;

            player.entityFSM.entitycurrentHealth = player.maxHealth;
            player.healthBar.UpdateLabel(player.maxHealth);

            player.GetComponent<SpriteRenderer>().color = Color.white;

            player.entityRigidBody.transform.position = player.spawnPoint;

            entityFSM.ChangeState(new EntityIdleState(entityFSM));
            entityAnimator.SetBool("IsDead", false);

            player.playerActions.enabled = true;
        }));
    }

    /// <summary>
    /// The CreateEnemyDeadBody method is responsible for creating the enemy's dead body.
    /// This method creates a new GameObject with the enemy's sprite and position, and destroys the enemy GameObject.
    /// This new game object it has only a sprite renderer as a component.
    /// </summary>
    private void CreateEnemyDeadBody()
    {
        // Debug.Log("Creating dead body");

        Enemy enemyClass = (Enemy)entityFSM.entityProprieties;

        Rigidbody2D enemy = enemyClass.entityRigidBody;

        var deadBody = new GameObject("DeadBody");
        deadBody.transform.position = enemy.transform.position;
        deadBody.transform.localScale = enemy.transform.localScale;
        deadBody.layer = enemy.transform.gameObject.layer;

        SpriteRenderer enemySprite = enemy.gameObject.GetComponent<SpriteRenderer>();

        SpriteRenderer deadBodySprite = deadBody.AddComponent<SpriteRenderer>();
        deadBodySprite.sprite = enemySprite.sprite;
        deadBodySprite.sortingOrder = enemySprite.sortingOrder;
        deadBodySprite.color = Color.white;

        entityFSM.DestroyGameObject(enemy.gameObject);
    }
}
