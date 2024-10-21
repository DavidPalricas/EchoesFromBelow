using UnityEngine;

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
    /// The isEnemyRight variable is responsible for storing the enemy's spawn direction.
    /// </summary>
    private bool isEnemyRight;


    /// <summary>
    /// The Awake method is called when the script instance is being loaded(Unity Method).
    /// In this method, we are getting the player's and the enemy Rigidbody2D components and checking the enemy's spawn direction.
    /// </summary>
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        enemy = GetComponent<Rigidbody2D>();
        isEnemyRight = enemy.position.x > player.position.x;
    }

    /// <summary>
    /// The Update method is called every frame(Unity Method).
    /// In this method, we are moving the enemy to chase the player.
    /// </summary>
    void Update()
    {
        ChasePlayerHorizontaly();

        ChasePlayerVertically();
    }

    /// <summary>
    /// The ChasePlayerHorizontaly method is responsible for moving the enemy to chase the player horizontaly.
    /// </summary>
    private void ChasePlayerHorizontaly()
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

        // If the player is not moving horizontally, the enemy will stop moving horizontally.
        if (player.velocity.x == 0)
        {
            enemy.velocity = new Vector2(0, enemy.velocity.y);
        }
    }

    /// <summary>
    /// The ChasePlayerVertically method is responsible for moving the enemy to chase the player vertically.
    /// </summary>
    private void ChasePlayerVertically()
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
        // If the player is not moving vertically, the enemy will stop moving vertically.
        else
        {
            enemy.velocity = new Vector2(enemy.velocity.x, 0);
        }

    }
}
