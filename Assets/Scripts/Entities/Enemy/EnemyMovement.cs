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
    /// The animator property is responsible for storing the enemy's animator.
    /// </summary>
    private Animator animator;

    /// <summary>
    /// The LastMovingDirection property is responsible for storing the last moving direction of the enemy.
    /// </summary>
    /// <value>
    /// The last moving direction of the enemy.
    /// </value>
    private Vector2 LastMovingDirection { get; set; }

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
    /// In this method, the enemy,speed,player, willCollide and IsInHorde variables are initialized.
    /// </summary>
    private void Awake()
    {
        enemy = GetComponent<Rigidbody2D>();
        speed = GetComponent<Entity>().Speed;
        player = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        willCollide = false;
        LastMovingDirection = Vector2.down;
        animator = GetComponent<Entity>().animator;
    }

    /// <summary>
    /// The Update method is called every frame (Unity Method).
    /// In this method, the direction to the player is calculated ,the enemy's movement is handled and check if the enemy is ready to attack.
    /// If the enemy is attacking or the conditions to attack are met, the enemy stops moving, to perform the attack.
    /// If the enemy will collide, the HandleCollision method is called, otherwise the ChasePlayer method is called.
    /// </summary>
    private void Update()
    {
        if (GetComponent<Enemy>().IsIndependent && !PlayerInRange())
        {
            return;
        }

        // Check if the enemy is attacking
        if (GetComponent<EnemyAttack>().Attacking)
        {
            return;
        }

        Vector2 directionToPlayer = Utils.NormalizeDirectionVector(player.position - enemy.position);

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

        Vector2 velocity = enemy.velocity;
        Vector2 direction = velocity.sqrMagnitude > 0.01f ? velocity.normalized : Vector2.zero;

        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", velocity.sqrMagnitude);

        if (velocity.sqrMagnitude > 0.01f)
        {

            LastMovingDirection = direction;

        }

        animator.SetFloat("LastHorizontal", LastMovingDirection.x);
        animator.SetFloat("LastVertical", LastMovingDirection.y);
    }

    /// <summary>
    /// The PlayerInRange method is responsible for checking if the player is in range of the enemy.
    /// </summary>
    /// <returns>
    ///  <c>true</c> if the player is in range; otherwise, <c>false</c>.
    /// </returns>
    private bool PlayerInRange()
    {
        float range = 5f;

        return Vector2.Distance(player.position, enemy.position) <= range;
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
    /// It crates a raycast in the direction of the player, and if the player is hit, the method returns true.
    /// </summary>
    /// <param name="directionToPlayer">The direction to the player.</param>
    /// <returns>
    ///   <c>true</c> if the enemy is near to the player; otherwise, <c>false</c>.
    /// </returns>
    private bool PlayerNear(Vector2 directionToPlayer)
    {
        float rayCastDistance = 0.5f;

        BoxCollider2D enemyCollider = enemy.GetComponent<BoxCollider2D>();

        Vector2 raycastOrigin = (Vector2) enemyCollider.bounds.center + directionToPlayer * (enemyCollider.bounds.extents + new Vector3(rayCastDistance, rayCastDistance)).magnitude;

        LayerMask playerLayer = LayerMask.GetMask("Default");

        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, directionToPlayer, rayCastDistance, playerLayer);

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
    /// The method creates a box in the direction the enemy will move, with the same size as the enemy's collider.
    /// If there is an obstacle in the enemy's path, the method returns false, otherwise it returns true.
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
        Vector2 offsetPosition = (Vector2)enemyCollider.bounds.center + direction.normalized * (boxSize.x / 2);

        Collider2D[] colliders = Physics2D.OverlapBoxAll(offsetPosition, boxSize, 0f, obstacleLayer);

        // Check if there is an obstacle in the enemy's path
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Object") || collider.CompareTag("Enemy") && collider.gameObject != enemy.gameObject)
            {
                return false;
            }
        }

        // These lines are only used for debugging purposes
        /*
        Debug.DrawLine(offsetPosition - new Vector2(boxSize.x / 2, boxSize.y / 2),
                  offsetPosition + new Vector2(boxSize.x / 2, boxSize.y / 2), Color.red);
        */

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
}

