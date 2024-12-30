using UnityEngine;

/// <summary>
/// The Enemy class is responsible for handling the game's enemies attributes.
/// </summary>
public class Enemy : Entity
{
    /// <summary>
    /// The IsBoss property is responsible for storing if the enemy is a boss.
    /// </summary>
    public bool isBoss;

    /// <summary>
    /// The bossDropItem property is responsible for storing the item that the boss will drop.
    /// </summary>
    public GameObject bossDropItem;

    /// <summary>
    /// The IsLongRanged property is responsible for storing if the enemy is long ranged.
    /// </summary>
    public bool IsRanged;

    /// <summary>
    /// The IsIndependent property is responsible for storing if the enemy is independent (is not in a horde).
    /// </summary>
    public bool IsIndependent;

    /// <summary>
    /// The movement of the enemy.
    /// </summary>
    [HideInInspector]
    public EnemyMovement movement;

    /// <summary>
    /// The Start method is called before the first frame update (Unity Method).
    /// In this method, the Initialize method is called to initialize the enemy's attributes.
    /// </summary>
    private void Start()
    {
       Initialize();
    }

    /// <summary>
    /// The Initialize method is responsible for initializing the enemy's attributes.
    /// This method is used to replace the Start method in the Entity class, which is ignore when we instiantie a enemy during runtime.
    /// Example of this type of initalization: Instantiate(enemyPrefab, spawnPoint, Quaternion.identity)
    /// </summary>
    public void Initialize()
    {
        movement = GetComponent<EnemyMovement>();

        attack = IsRanged ? GetComponent<EntityRangedAttack>() : GetComponent<EntityMeleeAttack>();

        entityFSM.entityProprieties = this;
        entityFSM.entitycurrentHealth = maxHealth;
        entityFSM.entityProprieties.entityRigidBody = GetComponent<Rigidbody2D>();


        if (!isBoss && bossDropItem != null)
        {
            Debug.LogError("Only Bosses can drop items!");
        }
    }
}
