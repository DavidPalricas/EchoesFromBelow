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
    /// Stores the players animator.
    /// </summary>
    private Animator animator;

    /// <summary>
    /// The speed property is responsible for storing the player's speed.
    /// </summary>
    [SerializeField]
    private float speed;

    /// <summary>
    /// The speed property is responsible for storing the player's speed.
    /// </summary>
    private Vector2 speedVector;

    /// <summary>
    /// The forbidennDirection property is responsible for storing the direction that the player can't move to.
    /// </summary>
    private Vector2 forbidennDirection = Vector2.zero;

    /// <summary>
    /// The previousDirection property is responsible for storing the player's previous direction.
    /// </summary>
    public Vector2 LastMovingDirection { get; set; }

    /// <summary>
    /// The Awake method is called when the script instance is being loaded(Unity Method).
    /// In this method, we are getting the player's Rigidbody2D component.  
    /// </summary>
    private void Awake()
    {
        player = GetComponent<Rigidbody2D>();

        speed = GetComponent<Entity>().Speed;

        animator = GetComponent<Entity>().animator;

        //Define a posição inicial para onde o player está a olhar como para baixo
        LastMovingDirection = Vector2.down;
    }

    /// <summary>
    /// The Update method is called every frame(Unity Method).
    /// In this method, we are getting the users's input and moving the player.
    /// If the player stops moving, we are storing the last direction the player was moving.
    /// </summary>
    private void Update()
    {
        float speedX = Input.GetAxis("Horizontal");
        float speedY = Input.GetAxis("Vertical");

        speedVector.x = speedX;
        speedVector.y = speedY;

        if (speedVector != Vector2.zero){

            speedVector = speedVector.normalized;

            LastMovingDirection = speedVector;
        
        }

        if (forbidennDirection != Vector2.zero && Utils.NormalizeDirectionVector(speedVector) == forbidennDirection)
        {
            player.velocity = Vector2.zero;
            return;
        }

        animator.SetFloat("Horizontal", speedX);
        animator.SetFloat("Vertical", speedY);
        animator.SetFloat("Speed", speedVector.sqrMagnitude);

        animator.SetFloat("LastHorizontal", LastMovingDirection.x);
        animator.SetFloat("LastVertical", LastMovingDirection.y);
        

       player.velocity = speedVector * speed;

       //player.velocity = new Vector2(speedX * speed, speedY * speed);  
       
        if (player.velocity != Vector2.zero)
        {
            LastMovingDirection = Utils.NormalizeDirectionVector(player.velocity);
        }    
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
                collision.gameObject.GetComponent<Collectable>().isCollected = true;
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
            forbidennDirection = Vector2.zero;
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
            collision.gameObject.GetComponent<EnemyAttack>().AttackDirection = Utils.NormalizeDirectionVector(collision.transform.position - transform.position);
            collision.gameObject.GetComponent<EnemyMovement>().enabled = false;

            forbidennDirection = Utils.NormalizeDirectionVector(collision.transform.position - transform.position);
        }
    }
}
