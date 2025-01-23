using UnityEngine;

/// <summary>
/// The CheckPontTrigger class is responsible for triggering a checkpoint when the player enters in the "checkpoint" area.
/// </summary>
public class CheckPontTrigger : MonoBehaviour
{
    /// <summary>
    /// The OnTriggerExit2D method is called when the Collider2D other exits the trigger (Unity Method).
    /// In this method, the player's spawn point is changed when the player enters the checkpoint area and the checkpoint is destroyed.
    /// </summary>
    /// <param name="collision">The collider of the game object that collided.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player playerProprieties = collision.GetComponent<EntityFSM>().entityProprieties as Player;

            playerProprieties.spawnPoint = new Vector2(transform.position.x, transform.position.y);

            Destroy(gameObject);
        }
    }
}
