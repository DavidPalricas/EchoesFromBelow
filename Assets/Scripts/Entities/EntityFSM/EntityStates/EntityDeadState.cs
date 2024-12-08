using UnityEngine;


/// <summary>
/// The EntityDeadState class is responsible for handling the logic of the dead state of the entity.
/// </summary>
public class EntityDeadState : EntityStateBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityDeadState"/> class.
    /// </summary>
    /// <param name="entityFSM">The entity's finite state machine.</param>
    public EntityDeadState(EntityFSM entityFSM) : base(entityFSM) { }

    /// <summary>
    /// The Enter method is responsible for executing the logic when the state is entered.
    /// It is a virtual method that can be overridden by the derived classes.
    /// It is inirrated from the IState interface.
    /// </summary>
    public override void Enter()
    {
        Debug.Log("Entering Dead State");

        entityAnimator = entityFSM.entityProprieties.animator;

        entityFSM.entityProprieties.entity.velocity = Vector2.zero;

        UpdateAnimator();


        if (entityFSM.entityProprieties is Enemy)
        {
            ExecuteEnemyLogic();

        }
    }

    /// <summary>
    /// The Execute method is responsible for executing the logic of the state.
    /// It is a virtual method that can be overridden by the derived classes.
    /// It is inirrated from the IState interface.
    /// </summary>
    public override void Execute()
    {
        Debug.Log("Executing Dead State");
    }

    /// <summary>
    /// The Exit method is responsible for executing the logic when the state is exited.
    /// It is a virtual method that can be overridden by the derived classes.
    /// It is inirrated from the IState interface.
    /// </summary>
    public override void Exit()
    {
        Debug.Log("Exiting Dead State");
    }


    protected override void UpdateAnimator()
    {
        entityAnimator.SetBool("IsDead", true);
    }


    protected override void ExecuteEnemyLogic()
    {
        Enemy enemyClass = (Enemy)entityFSM.entityProprieties;


        GameObject.Find("Level1").GetComponent<Rank>().SkeletonsKilled++;

        if (enemyClass.isBoss)
        {

            entityFSM.InstantiateItem(enemyClass.bossDropItem, enemyClass.entity.transform.position);
            GameObject.Find("Level1").GetComponent<Rank>().BossKilled = true;
        }

        entityFSM.StartCoroutine(Utils.WaitForAnimationEnd(entityAnimator, "Death", CreateEnemyDeadBody));
    }


    /// <summary>
    /// The CreateEnemyDeadBody method is responsible for creating the enemy's dead body.
    /// This method creates a new GameObject with the enemy's sprite and position, and destroys the enemy GameObject.
    /// This new game object it has only a sprite renderer as a component.
    /// </summary>
    private void CreateEnemyDeadBody()
    {
        Debug.Log("Creating dead body");

        Enemy enemyClass = (Enemy)entityFSM.entityProprieties;

        Rigidbody2D enemy = enemyClass.entity;

        var deadBody = new GameObject("DeadBody");
        deadBody.transform.position = enemy.transform.position;
        deadBody.transform.localScale = enemy.transform.localScale;
        deadBody.layer = enemy.transform.gameObject.layer;

        SpriteRenderer enemySprite = enemy.gameObject.GetComponent<SpriteRenderer>();

        SpriteRenderer deadBodySprite = deadBody.AddComponent<SpriteRenderer>();
        deadBodySprite.sprite = enemySprite.sprite;
        deadBodySprite.sortingOrder = enemySprite.sortingOrder;
        deadBodySprite.color = enemySprite.color;
        

        entityFSM.DestroyGameObject(enemy.gameObject);
    }
}
