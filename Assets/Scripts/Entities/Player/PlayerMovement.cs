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
    /// </summary>
    private void Update()
    {
        float speedX = Input.GetAxis("Horizontal");
        float speedY = Input.GetAxis("Vertical");

        player.velocity = new Vector2(speedX * speed, speedY * speed);  
    }

    /// <summary>
    /// The OnCollisionEnter2D method is called when a collider or a RigidBody2D has begun touching the collider of the player (Unity Method).
    /// In this method, we are checking if the player collided with an item.
    /// If the player collided with an item, the player's HasKey property is set to true amd the item diseapers from the map.
    /// </summary>
    /// <param name="collision">The collision variable stores the collider of the game object that collided with the player.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            // Destroy the key game object.
            Destroy(collision.gameObject);

            GetComponent<PlayerActions>().HasKey = true;
        }
    } 
}
