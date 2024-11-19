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
    /// The GetPlayerDirection method is responsible for getting the player's direction normalized.
    /// </summary>
    /// <returns> The player's direction normalized</returns>
    public Vector2 GetPlayerDirection()
    {
        Vector2 playerDirection = player.velocity.normalized;

        return new Vector2(Mathf.Round(playerDirection.x), Mathf.Round(playerDirection.y));
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "BoneTrigger")
        {
            GameObject spawnHord = GameObject.Find("SpawnHorde");

            spawnHord.GetComponent<SpawnHorde>().enabled = true;
            collider.gameObject.SetActive(false);

        }
    }
}
