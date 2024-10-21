using UnityEngine;

/// <summary>
/// The EnemyMovement class is responsible for handling the player's movement.
/// </summary>
public class EnemyMovement : MonoBehaviour
{
    /// <summary>
    /// The plyers variable is responsible for storing the player's Rigidbody2D component.
    /// </summary>
    private Rigidbody2D player;

    /// <summary>
    /// The enemy variable is responsible for storing the enemy's Rigidbody2D component.
    /// </summary>
    private Rigidbody2D enemy;

    /// <summary>
    /// The speed variable is responsible for storing the enemy's speed.
    /// The SerializeField attribute is used to show the variable in the Unity Inspector
    /// </summary>
    [SerializeField] float speed;

    /// <summary>
    /// The Awake method is called when the script instance is being loaded(Unity Method).
    /// In this method, we are getting the player's and the enemy Rigidbody2D components and checking the enemy's spawn direction.
    /// </summary>
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        enemy = GetComponent<Rigidbody2D>();


    }

    /// <summary>
    /// The Update method is called every frame(Unity Method).
    /// In this method, we are moving the enemy to chase the player.
    /// It also checks if the enemy is on the right or left side of the player,and sets the enemy's default movement.
    /// </summary>
    private void Update()
    {
        bool isEnemyRight = enemy.position.x > player.position.x;
        var defaultMove = isEnemyRight ? new Vector2(-speed, enemy.velocity.y) : new Vector2(speed, enemy.velocity.y);

        ChasePlayerHorizontaly(isEnemyRight,defaultMove);

        ChasePlayerVertically(defaultMove);
    }

    /// <summary>
    /// The ChasePlayerHorizontaly method is responsible for moving the enemy to chase the player horizontaly.
    /// </summary>
    /// <param name="isEnemyRight">The isEnemyRight variable indicates if the enemy is on the right side of the player .</param>
    /// <param name="defaultMove">The defaultMove variable stores the default move of the enemy.</param>
    private void ChasePlayerHorizontaly(bool isEnemyRight,Vector2 defaultMove)
    {
        // If the player is moving left and the enemy is on the right side of the player, the enemy will move left.
        // If the player is moving right the enemy wont chase the player.
        if (player.velocity.x < 0 && isEnemyRight)
        {
            enemy.velocity = new Vector2(-speed, enemy.velocity.y);
        }
        // If the player is moving right and the enemy is on the left side of the player, the enemy will move right.
        // If the player is moving left the enemy wont chase the player.
        else if (player.velocity.x > 0 && !isEnemyRight)
        {
            enemy.velocity = new Vector2(speed, enemy.velocity.y);
        }

        // If the player is not moving horizontally, the enemy will move in its default direction.
       
        if (player.velocity.x == 0)
        {
            enemy.velocity = defaultMove;
        }
    }

    /// <summary>
    /// The ChasePlayerVertically method is responsible for moving the enemy to chase the player vertically.
    /// </summary>
    /// <param name="defaultMove">The defaultMove variable stores the default move of the enemy.</param>
    private void ChasePlayerVertically(Vector2 defaultMove)
    {
        // If the player is moving up, the enemy will move up.
        if (player.velocity.y < 0)
        {
            enemy.velocity = new Vector2(enemy.velocity.x, -speed);
        }
        // If the player is moving down, the enemy will move down.
        else if (player.velocity.y > 0)
        {
            enemy.velocity = new Vector2(enemy.velocity.x, speed);
        }
        // If the player is not moving vertically, the enemy will move in its default direction.
        else
        {
            enemy.velocity = defaultMove;
        }
    }

    /// <summary>
    /// The OnCollisionEnter2D method is called when a collider  has begun touching the collider of the enemy (Unity Method).
    /// In this method, we are ignoring the collision between the enemy and the player. 
    /// </summary>
    /// <param name="collision">The collision variable stores the collider of the game object that collided with the enemy.</param>

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }

    }   
}

