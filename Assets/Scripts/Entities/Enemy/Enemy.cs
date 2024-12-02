using UnityEngine;

/// <summary>
/// The Enemy class is responsible for handling the game's enemies attributes.
/// </summary>
public class Enemy : Entity
{
    /// <summary>
    /// The IsBoss property is responsible for storing if the enemy is a boss.
    /// </summary>
    [SerializeField]
    private bool isBoss;

    /// <summary>
    /// The bossDropItem property is responsible for storing the item that the boss will drop.
    /// </summary>
    [SerializeField]
    private GameObject bossDropItem;

    /// <summary>
    /// The IsLongRanged property is responsible for storing if the enemy is long ranged.
    /// </summary>
    public bool IsLongRanged;

    /// <summary>
    /// The IsIndependent property is responsible for storing if the enemy is independent (is not in a horde).
    /// </summary>
    public bool IsIndependent;

    /// <summary>
    /// The Awake method is called when the script instance is being loaded (Unity Method).
    /// In this method, we are checking if the enemy is no boss and has a drop item.
    /// If these conditions are met, an error message is displayed.
    /// </summary>
    private void Awake()
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
    }

    /// <summary>
    /// Overrides the EntityDeath method to add custom behavior for boss enemies, to drop an item when they die.
    /// The base method is called to ensure the standard death behavior is preserved.
    /// </summary>
    protected override void EntityDeath()
    {
        // Call the base EntityDeath method to retain the original behavior

        base.EntityDeath();

        GetComponent<EnemyMovement>().enabled = false;

        if (isBoss)
        {
            Instantiate(bossDropItem, transform.position, Quaternion.identity);
        }

        StartCoroutine(Utils.WaitForAnimationEnd(animator, "Skeleton_Death", CreateEnemyDeadBody));
    }

    /// <summary>
    /// The CreateEnemyDeadBody method is responsible for creating the enemy's dead body.
    /// This method creates a new GameObject with the enemy's sprite and position, and destroys the enemy GameObject.
    /// This new game object it has only a sprite renderer as a component.
    /// </summary>
    private void CreateEnemyDeadBody()
    {   
        Debug.Log("Creating dead body");
        var deadBody = new GameObject("DeadBody");
        deadBody.transform.position = transform.position;
        deadBody.transform.localScale = transform.localScale;
        deadBody.layer = transform.gameObject.layer;
      


        SpriteRenderer deadBodySprite = deadBody.AddComponent<SpriteRenderer>();
        deadBodySprite.sprite = GetComponent<SpriteRenderer>().sprite;
        deadBodySprite.sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

        Destroy(gameObject);
    }
}
