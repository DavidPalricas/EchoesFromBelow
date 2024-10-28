using UnityEditor.SearchService;
using UnityEditor.UI;
using UnityEngine;

public class Tourist : MonoBehaviour
{

    [SerializeField] float speed;

    private  Rigidbody2D tourist;


    /// <summary>
    /// The Awake method is called when the script instance is being loaded (Unity Method).
    /// In this method, the tourist variable is initialized.
    /// </summary>
    private void Awake()
    {
        tourist = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// The Update method is called every frame (Unity Method).
    /// In this method, the tourist's walks horizontally.
    /// </summary>
    private void Update()
    {
        tourist.velocity = new Vector2(speed, tourist.velocity.y);
    }


    /// <summary>
    /// The OnCollisionEnter2D method is called when this collider/rigidbody has begun touching another rigidbody/collider (Unity Method).
    /// In this method, if the tourist collides with a checkpoint, the tourist is destroyed and ignores the collisions with another tourists.
    /// </summary>
    /// <param name="collision">The collision parameter stores a RigidBody2D or a collider of a game object which collide with the tourist.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("CheckPoint"))
        {  
            Debug.Log("Checkpoint");
            
        }
        else if (collision.gameObject.CompareTag("Tourist"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
}



}
