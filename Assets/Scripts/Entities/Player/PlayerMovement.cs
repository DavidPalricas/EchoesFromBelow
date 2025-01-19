using UnityEngine;
using UnityEngine.InputSystem;

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
    [HideInInspector]
    public Vector2 speedVector;

    /// <summary>
    /// The movementInput property is responsible for storing the player's movement input.
    /// It is serialized so it can be modified in the Unity Editor.
    /// </summary>
    [SerializeField]
    private InputActionReference movementInput;

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

    /// <summary>
    /// The MovePlayer method is responsible for moving the player.
    /// </summary>
    public void MovePlayer()
    {   
        // Create a movement vector and normalize it
        speedVector = movementInput.action.ReadValue<Vector2>().normalized;

        // Apply velocity directly to the Rigidbody2D
        player.velocity = speed * speedVector;
    }

    /// <summary>
    /// The StopPlayer method is responsible for stopping the player.
    /// </summary>
    public void StopPlayer()
    {
        if (speedVector == Vector2.zero)
        {
            player.velocity = Vector2.zero;
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
                GetComponent<PlayerActions>().GrabCollectable(collision.gameObject);     
            }        
        }
    }
}
