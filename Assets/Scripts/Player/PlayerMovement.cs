using UnityEngine;

/// <summary>
/// The PlayerMovement class is responsible for handling the player's movement.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// The plyers variable is responsible for storing the player's Rigidbody2D component.
    /// </summary>
    private Rigidbody2D player;

    /// <summary>
    /// The speed variable is responsible for storing the player's speed.
    /// The SerializeField attribute is used to show the variable in the Unity Inspector.
    /// </summary>
    [SerializeField] float speed;

    /// <summary>
    /// The Awake method is called when the script instance is being loaded(Unity Method).
    /// In this method, we are getting the player's Rigidbody2D component.  
    /// </summary>
    private void Awake()
    {
        player = GetComponent<Rigidbody2D>();
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
}
