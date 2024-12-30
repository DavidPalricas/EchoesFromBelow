using UnityEngine;

/// <summary>
///  The ProjetileMovement class is responsible for handling the projetile's movement.
/// </summary>
public class ProjetileMovement : MonoBehaviour
{
    /// <summary>
    /// The timeElasped property is responsible for storing the time elapsed since the projetile was thrown.
    /// </summary>
    private float timeElapsed = 0f;

    /// <summary>
    /// The projetileRigidBody property is responsible for storing the projetile's Rigidbody2D component.
    /// </summary>
    private Rigidbody2D projetileRigidBody;

    /// <summary>
    /// The speed property is responsible for storing the projetile's speed.
    /// </summary>
    [SerializeField]
    private float speed;

    /// <summary>
    /// The playerThrown property is responsible for storing whether the player shooted the projetile or not.
    /// </summary>
    [HideInInspector]
    public bool playerThrown;

    /// <summary>
    /// The attackDirection property is responsible for storing the direction in which the projetile was thrown.
    /// </summary>
    [HideInInspector]
    public Vector2 attackDirection;

    /// <summary>
    /// The Awake method is called when the script instance is being loaded (Unity Method).
    /// In this method, the projetileRigidBody variable is initialized.
    /// </summary>
    private void Awake()
    {
        projetileRigidBody = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// The Update method is called once per frame (Unity Method).
    /// This method is used to update the time elapsed since the projetile was thrown.
    /// If the time elapsed is greater than or equal to 2 seconds, the projetile is destroyed otherwise the projetile is moved.
    /// </summary>
    private void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= 2f)
        {
            Destroy(gameObject);
        }
        else
        {
            MoveProjetile();
        }
    }

    /// <summary>
    /// The MoveProjetile method is responsible for moving the projetile.
    /// These movement is horizontal or vertical, depending on the attack direction.
    /// The attack direction can be (0,1), (0,-1), (1,0) or (-1,0).
    /// </summary>
    private void MoveProjetile()
    {   
        projetileRigidBody.position += speed * Time.deltaTime * attackDirection;
    }

    /// <summary>
    /// The OnCollisionEnter2D method is called when the Collider2D collider enters the trigger (Unity Method).
    /// This method is used to check if the projetile is to be destroy on collision or  not.
    /// To check this the PlayerShooted or EnemyShooted methods are called, if the projetile is thrown by the player or the enemy, respectively.
    /// </summary>
    /// <param name="collision">The game object that collided.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool destroyProjetile = playerThrown ? HandlePlayerProjectileCollision(collision) : HandleEnemyProjectileCollision(collision);

        if (destroyProjetile)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// The PlayerShooted method is responsible for checking which game object that the projetile thrown by the player collided and indicate if the projetile is to be destroyed.
    /// If the projetile collided with the player, the projetile is not set to be destroyed otherwise the projetile is set to be  destroyed.
    /// If the projetile collided with an enemy, the enemy's health is decreased by the player's attack damage.
    /// </summary>
    /// <param name="collision">The game object that collided</param>
    /// <returns>
    ///   <c>true</c> if the projetile is to be destroyed; Otherwise <c>false</c>.
    /// </returns>
    private bool HandlePlayerProjectileCollision(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());

            return false;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            int attackDamage = (int)enemy.attackDamage;
            enemy.entityFSM.entitycurrentHealth -= attackDamage;
        }

        return true;
    }

    /// <summary>
    /// The HandleEnemyProjectileCollision method is responsible for checking which game object that the projetile thrown by an enemy collided and indicate if the projetile is to be destroyed.
    /// If the projetile collided with a enemy, the projetile is not set to be destroyed otherwise the projetile is set to be  destroyed.
    /// If the projetile collided with the player, the player's health is decreased by the enemys's attack damage and the player's health bar is updated.
    /// </summary>
    /// <param name="collision">The game object that collided</param>
    /// <returns>
    ///   <c>true</c> if the projetile is to be destroyed; Otherwise <c>false</c>.
    /// </returns>
    private bool HandleEnemyProjectileCollision(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
            return false;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            int attackDamage = (int)player.attackDamage;
            player.entityFSM.entitycurrentHealth -= attackDamage;

            player.healthBar.UpdateLabel(player.entityFSM.entitycurrentHealth);
        }
        return true;
    }
}
