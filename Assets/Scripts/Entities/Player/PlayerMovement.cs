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

        player.velocity = new Vector2(speedX * speed, speedY * speed);  

        if (player.velocity != Vector2.zero)
        {
            LastMovingDirection = Utils.NormalizeDirectionVector(player.velocity);
        }    
    }

    /// <summary>
    /// The OnTriggerEnter2D method is called when the Collider2D enters the trigger.
    /// In this method, we are checking if the player has collided with the BoneTrigger.
    /// In this case, we are enabling the SpawnHorde script.
    /// </summary>
    /// <param name="collider">The collider.</param>
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "BoneTrigger")
        {
            GameObject spawnHord = GameObject.Find("SpawnHorde");

            spawnHord.GetComponent<SpawnHorde>().enabled = true;
            collider.gameObject.SetActive(false);

        }
    }

    /* METHOD NOT WORKING
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {   
            Rigidbody2D enemy = collision.gameObject.GetComponent<Rigidbody2D>();

            Vector2 collisionDirection = Utils.NormalizeDirectionVector(collision.transform.position - transform.position);

            float relativeVelocity = Vector2.Dot(player.velocity - enemy.velocity, collisionDirection);

            float impactForce = Mathf.Abs(relativeVelocity) * player.mass;

            player.AddForce(-collisionDirection * impactForce, ForceMode2D.Impulse);

            enemy.AddForce(collisionDirection * impactForce, ForceMode2D.Impulse);
        }
    }

    */
}
