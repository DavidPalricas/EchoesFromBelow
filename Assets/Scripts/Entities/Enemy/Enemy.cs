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
    public bool IsLongRanged;

    /// <summary>
    /// The IsIndependent property is responsible for storing if the enemy is independent (is not in a horde).
    /// </summary>
    public bool IsIndependent;

    /// <summary>
    /// The movement of the enemy.
    /// </summary>
    [HideInInspector]
    public EnemyMovement movement;

    [HideInInspector]
    public EnemyAttack attack;

    /// <summary>
    /// The Start method is called before the first frame update (Unity Method).
    /// In this method, we are checking if the enemy is no boss and has a drop item.
    /// If these conditions are met, an error message is displayed.
    /// </summary>
    private void Start()
    {
        if (!isBoss && bossDropItem != null)
        {
            Debug.LogError("Only Bosses can drop items!");
        }

        // When we don't have a sprite for the boss, we make the enemy bigger, to be different from the others enemies
        if (isBoss)
        {
            transform.localScale = new Vector2(transform.localScale.x * 1.5f, transform.localScale.y * 1.5f);
        }

        movement = GetComponent<EnemyMovement>();
        attack = GetComponent<EnemyAttack>();

        entity = GetComponent<Rigidbody2D>();

        entityFSM.entityProprieties = this;
    }

 
    /// <summary>
    /// The Initialize method is responsible for initializing the enemy's attributes.
    /// This method is used to replace the Start method in the Entity class, which is ignore when we instiantie a enemy during runtime.
    /// Example of this type of initalization: Instantiate(enemyPrefab, spawnPoint, Quaternion.identity)
    /// </summary>
    public void Initialize()
    {
        movement = GetComponent<EnemyMovement>();
        attack = GetComponent<EnemyAttack>();
        entity = GetComponent<Rigidbody2D>();
        entityFSM.entityProprieties = this;

        if (!isBoss && bossDropItem != null)
        {
            Debug.LogError("Only Bosses can drop items!");
        }

        if (isBoss)
        {
            transform.localScale = new Vector2(transform.localScale.x * 1.5f, transform.localScale.y * 1.5f);
        }
    }

}
