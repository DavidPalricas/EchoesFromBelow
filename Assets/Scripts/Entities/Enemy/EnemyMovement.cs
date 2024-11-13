using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// The EnemyMovement class is responsible for handling the player's movement.
/// </summary>
public class EnemyMovement : MonoBehaviour
{
    /// <summary>
    /// The player property is responsible for storing the player's Rigidbody2D component.
    /// </summary>
    private Rigidbody2D player;

    /// <summary>
    /// The enemy property is responsible for storing the enemy's Rigidbody2D component.
    /// </summary>
    private Rigidbody2D enemy;

    /// <summary>
    /// The speed property is responsible for storing the enemy's speed.
    /// </summary>
    private float speed;

    /// <summary>
    /// The willCollide property is responsible for storing whether the enemy will collide with an obstacle.
    /// </summary>
    private bool willCollide;

    /// <summary>
    /// The movetoNotCollide property is responsible for storing the direction the enemy will move to avoid colliding with an obstacle.
    /// </summary>
    private Vector2 moveToNotCollide;

    /// <summary>
    /// The playerDirections property is responsible for storing the possible movement directions of the player.
    /// </summary>
    private static readonly Vector2[] playerDirections = new Vector2[]
    {
        Vector2.left,
        Vector2.right,
        Vector2.down,
        Vector2.up,
        new (-1, -1), //Left Down Diagonal
        new (-1, 1), // Left Up Diagonal
        new (1, -1), // Right Down Diagonal
        new (1, 1), // Right Up Diagonal
    };

    /// <summary>
    /// The attackDirections property is responsible for storing the possible attack directions of the enemy.
    /// </summary>
    private readonly Vector2[] attackDirections = { Vector2.left, Vector2.right, Vector2.up, Vector2.down };

    /// <summary>
    /// The Awake method is called when the script instance is being loaded (Unity Method).
    /// In this method, the enemy,speed,player and willCollide variables are initialized.
    /// </summary>
    private void Awake()
    {
        enemy = GetComponent<Rigidbody2D>();
        speed = GetComponent<Entity>().Speed;
        player = GameObject.Find("Player").GetComponent<Rigidbody2D>();

        willCollide = false;
    }

    /// <summary>
    /// The Update method is called every frame (Unity Method).
    /// In this method, the direction to the player is calculated ,the enemy's movement is handled and check if the enemy is ready to attack.
    /// If the enemy is attacking or the conditions to attack are met, the enemy stops moving, to perform the attack.
    /// If the enemy will collide, the HandleCollision method is called, otherwise the ChasePlayer method is called.
    /// </summary>
    private void Update()
    {
        // Check if the enemy is attacking
        if (GetComponent<EnemyAttack>().Attacking)
        {
            return;
        }

        Vector2 directionToPlayer = GetDirectionToPlayer();

        if (EnemyIsReadyToAttack(directionToPlayer))
        {
            return;
        }

        if (willCollide)
        {   
            HandleCollision(directionToPlayer);   
        }
        else
        {
          ChasePlayer(directionToPlayer);
        }

        enemy.velocity = moveToNotCollide * speed;
    }

    /// <summary>
    /// The GetDirectionToPlayer method is responsible for calculating the normalized direction to the player .
    /// </summary>
    /// <returns>The normalized direction to the player </returns>
    private Vector2 GetDirectionToPlayer()
    {
        // Normalize the direction to the player to obtain vectors of this form: (-1, 0), (0, 1), (1, 0), (0, -1)
        Vector2 directionToPlayer = (player.position - enemy.position).normalized;

        // Round the direction to the player if the normalized direction is not a vector of this form: (-1, 0), (0, 1), (1, 0), (0, -1)
        return new Vector2(Mathf.Round(directionToPlayer.x), Mathf.Round(directionToPlayer.y));
    }

    /// <summary>
    /// The EnemyIsReadyToAttack method is responsible for checking if the enemy is ready to attack.
    /// If it is, the enemy stops moving to attack, and its attack direction is set has is current direction.
    /// </summary>
    /// <param name="directionToPlayer">The direction to player.</param>
    /// <returns>
    ///   <c>true</c> if the enemy is ready to attack; otherwise, <c>false</c>.
    /// </returns>
    private bool EnemyIsReadyToAttack(Vector2 directionToPlayer)
    {
        // Check if the enemy is attacking or the conditions to attack are met
        if (PlayerNear(directionToPlayer) && IsAttackDirection(directionToPlayer))
        {
            // Stop the enemy's movement to attack
            enemy.velocity = Vector2.zero;

            GetComponent<EnemyAttack>().AttackDirection = directionToPlayer;

            return true;
        }

        return false;
    }

    /// <summary>
    /// The PlayerNear method is responsible for checking if the player is near the enemy.
    /// </summary>
    /// <param name="directionToPlayer">The direction to the player.</param>
    /// <returns>
    ///   <c>true</c> if the enemy is near to the player; otherwise, <c>false</c>.
    /// </returns>
    private bool PlayerNear(Vector2 directionToPlayer)
    {
        float rayCastDistance = 1f;

        LayerMask playerLayer = LayerMask.GetMask("Default");

        RaycastHit2D hit = Physics2D.Raycast(enemy.position, directionToPlayer, rayCastDistance, playerLayer);

        // This line is used to draw a ray in the scene view for debugging purposes
        //Debug.DrawRay(enemy.position, directionToPlayer * rayCastDistance, Color.yellow);

        return hit.collider != null && hit.collider.CompareTag("Player");
    }

    /// <summary>
    /// Determines if the current direction is an attack direction.
    /// </summary>
    /// <param name="enemyDirection">The enemy direction.</param>
    /// <returns>
    ///   <c>true</c> if the current enemy direction is an attak direction; otherwise, <c>false</c>.
    /// </returns>
    private bool IsAttackDirection(Vector2 enemyDirection)
    {
        foreach (var attackDirection in attackDirections)
        {
            if (attackDirection == enemyDirection)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// The HandleCollision method is responsible for handling the enemy's movement when it will collide with an obstacle.
    /// If the path is clear, the enemy's velocity is set to the direction it should move.
    /// </summary>
    /// <param name="directionToPlayer">The directionToPlayer parameter stores an vector which represents te distance of the enemy to the player</param>
    private void HandleCollision(Vector2 directionToPlayer)
    {
        if (IsPathClear(directionToPlayer))
        {
            willCollide = false;

            moveToNotCollide = directionToPlayer;
        }

        if (!IsPathClear(moveToNotCollide))
        {
            moveToNotCollide = FindAlternativeDirection(moveToNotCollide);
        }
    }

    /// <summary>
    /// The ChasePlayer method is responsible for handling the enemy's movement when it will not collide with an obstacle.
    /// If the path is clear, the enemy's velocity is set to the direction it should move.
    /// Otherwise, the FindAlternativeDirection method is called, to find an alternative direction to move.
    /// </summary>
    /// <param name="directionToPlayer">The directionToPlayer parameter stores an vector which represents te distance of the enemy to the player</param>
    private void ChasePlayer(Vector2 directionToPlayer)
    {
        if (IsPathClear(directionToPlayer))
        {
            moveToNotCollide = directionToPlayer;
        }
        else
        {
            Vector2 alternativeDirection = FindAlternativeDirection(directionToPlayer);

            willCollide = true;

            moveToNotCollide = alternativeDirection;
        }
    }

    /// <summary>
    /// The IsPathClear method is responsible for checking if the path is clear for the enemy to move.
    /// It uses a raycast to check if there is an obstacle in the enemy's path.
    /// </summary>
    /// <param name="direction">The direction parameter stores an vector which represents the direction the enemy should move</param>
    /// <returns>
    /// <c>true</c> if the path is clear,otherwise <c>false</c>.
    /// </returns>
    private bool IsPathClear(Vector2 direction)
    {
        // Check if the enemy has a Collider2D component
        if (!TryGetComponent<BoxCollider2D>(out var enemyCollider))
        {
            Debug.LogWarning("Enemy does not have a Collider2D component.");
            return true;
        }

        Vector2 boxSize = enemyCollider.bounds.size;
        LayerMask obstacleLayer = LayerMask.GetMask("Default");

        // Create a box in the direction the enemy will moves, with the same size as the enemy's collider
        Vector2 offsetPosition = (Vector2)enemyCollider.bounds.center + direction.normalized * boxSize.x / 2;

        Collider2D[] colliders = Physics2D.OverlapBoxAll(offsetPosition, boxSize, 0f, obstacleLayer);

        // Check if there is an obstacle in the enemy's path
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Object"))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// The CheckPossibleCollision method is responsible for checking if the enemy will collide with an obstacle, in a future movement.
    /// </summary>
    /// <param name="direction">A possible direction of the enemy's future movement</param>
    /// <returns>
    /// <c>true</c> if the gate is near otherwise, <c>false</c>.
    /// </returns>
    private bool CheckPossibleCollision(Vector2 direction)
    {
        // Check if the enemy has a Collider2D component
        if (!TryGetComponent<BoxCollider2D>(out var enemyCollider))
        {
            Debug.LogWarning("Enemy does not have a Collider2D component.");
            return true;
        }

        Vector2 boxSize = enemyCollider.bounds.size;
        LayerMask obstacleLayer = LayerMask.GetMask("Default");

        // Create a box in the direction the enemy will moves, with the same size as the enemy's collider
        Vector2 offsetPosition = (Vector2)enemyCollider.bounds.center + direction.normalized * boxSize.x / 2;

        Collider2D[] colliders = Physics2D.OverlapBoxAll(offsetPosition, boxSize, 0f, obstacleLayer);

        // Check if there is an obstacle in the enemy's path
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Object"))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// The FindAlternativeDirection method is responsible for finding an alternative direction for the enemy to move.
    /// This method tries to find the closest direction to the player that is not blocked by an obstacle.
    /// If there is no alternative direction, the method returns the blocked direction.
    /// </summary>
    /// <param name="blockedDirection"></param>
    /// <returns>It returns a Vector2 which represents the direction in which the enemy should move</returns>
    private Vector2 FindAlternativeDirection(Vector2 blockedDirection)
    {
        List<Vector2> alternativeDirections = playerDirections
            .Where(direction => direction != blockedDirection)
            .OrderBy(direction => Vector2.Distance((Vector2)enemy.position + direction, player.position))
            .ToList();

        foreach (var direction in alternativeDirections)
        {
            if (IsPathClear(direction))
            {; 
                return direction;
            }
        }

        return blockedDirection;
    }

    /// <summary>
    /// The OnCollisionEnter2D method is called when the enemy collides with another object (Unity Method).
    /// In this method, the enemy ignores the collision with the player and other enemies.
    /// </summary>
    /// <param name="collision">The collision parameter stores a RigidBody2D or a collider of a game object which collide with the enemy.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());

        }
    }   
}

