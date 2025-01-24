
using UnityEngine;

/// <summary>
/// The ActiveHordesTrigger class is responsible for activating the horde spawner.
/// </summary>
public class ActiveHordesTrigger : MonoBehaviour
{
    /// <summary>
    /// The horde property is responsible for storing the script that spawns the horde.
    /// </summary>
    [SerializeField]
    private SpawnHorde horde;

    /// <summary>
    /// The speechTrigger property is responsible for storing the speech trigger.
    /// </summary>
    [SerializeField]
    private SpeechTrigger speechTrigger;

    /// <summary>
    /// The speechName property is responsible for storing the speech name.
    /// </summary>
    [SerializeField]
    private string speechName;

    /// <summary>
    /// The OnTriggerExit2D method is called when the Collider2D other exits the trigger (Unity Method).
    /// In this method, we are activating the horde spawner and destroying the trigger if the player exits the trigger and is above the trigger.
    /// </summary>
    /// <param name="collision">The collider of the game object that collided.</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && speechTrigger != null && collision.transform.position.y > transform.position.y)
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
            return;
        }
    }

    /// <summary>
    /// The OnTriggerExit2D method is called when the Collider2D other exits the trigger (Unity Method).
    /// In this method, we are activating the horde spawner and destroying the trigger if the player enters the trigger.
    /// </summary>
    /// <param name="collision">The collider of the game object that collided.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            horde.enabled = true;

            if (speechTrigger == null)
            {
                Destroy(gameObject);
            }      
        }
    }

    /// <summary>
    /// The OnCollisionEnter2D method is called when the Collider2D other enters the trigger (Unity Method).
    /// In this method, we are showing the speech if the player and enemies collide with the trigger.
    /// 
    /// </summary>
    /// <param name="collision">The collision of a game object.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {     
            speechTrigger.ChangeSpeech(speechName);  
        }else if(collision.collider.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<BoxCollider2D>());
        }
    }
}
