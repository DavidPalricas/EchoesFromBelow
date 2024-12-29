using UnityEngine;

/// <summary>
/// The PlayerMovement class is responsible for handling the player's movement.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// The player property is responsible for storing the player's Rigidbody2D component.
    /// </summary>
    private Rigidbody2D player;

    /// <summary>
    /// The speed property is responsible for storing the player's speed.
    /// </summary>
    private float speed;

    /// <summary>
    /// The speed property is responsible for storing the player's speed.
    /// </summary>
    public Vector2 speedVector;

    /// <summary>
    /// The Start method is called before the first frame update (Unity Method).
    /// </summary>
    private void Start()
    {
        player = GetComponent<Rigidbody2D>();

        speed = GetComponent<Entity>().speed;

        EntityFSM entityFSM = GetComponent<Player>().entityFSM;

        entityFSM.ChangeState(new EntityIdleState(entityFSM));
    }

    public void MovePlayer()
    {
        float speedX = Input.GetAxis("Horizontal");
        float speedY = Input.GetAxis("Vertical");

        speedVector  = new Vector2(speedX, speedY).normalized;

        player.position += speed * Time.deltaTime * speedVector;     
    }


    public void StopPlayer()
    {
        player.velocity = Vector2.zero;
    }

    /// <summary>
    /// The OnTriggerEnter2D method is called when the player enters a trigger collider (Unity Method).
    /// In this method, we are checking if the player has collided with an item or weapon.
    /// After that, we are calling the GrabCollectable method from the PlayerActions component, to grab the collectable.
    /// </summary>
    /// <param name="collision">The collision (collider of a game object).</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item") || collision.gameObject.CompareTag("Weapon"))
        {   
            if (collision.gameObject.GetComponent<Collectable>().isCollected == false)
            {   
                GetComponent<PlayerActions>().GrabCollectable(collision.gameObject);     
            }        
        }
    }


    /// <summary>
    /// The OnCollisionExit2D method is called when the player exits a collision (Unity Method).
    /// In this method, we are checking if the player has exited a collision with an enemy, if it has, we are enabling the movement of the entities.
    /// </summary>
    /// <param name="collision">   collision.gameObject.GetComponent<EnemyMovement>().enabled = false;.</param>
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyMovement>().enabled = true;
        }
    }

    /// <summary>
    /// The OnCollisionStay2D method is called when the player is colliding with another game object (Unity Method).
    /// In this method, we are checking if the player is colliding with an enemy, if it is, we are stopping the enemy's and player's pushing each other.
    /// </summary>
    /// <param name="collision">collision.gameObject.GetComponent<EnemyMovement>().enabled = false;</param>
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collision.gameObject.GetComponent<EnemyMovement>().enabled = false;
        }
    }
}
