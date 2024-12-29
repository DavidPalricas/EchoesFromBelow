using UnityEngine;

/// <summary>
/// The BoneTrigger class is responsible for triggering a horde spawn when the player enters in the "bone" area.
/// </summary>
public class BoneTrigger : MonoBehaviour
{

    /// <summary>
    /// The OnTriggerExit2D method is called when the Collider2D other exits the trigger (Unity Method).
    /// In this method, we are setting the playerDetected variable to true (player enter the "bone" area).
    /// </summary>
    /// <param name="collision">The collider of the game object that collided.</param>
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            GameObject spawnHorde = GameObject.Find("SpawnHorde");

            spawnHorde.GetComponent<SpawnHorde>().enabled = true;
            gameObject.SetActive(false);

            Player playerProprieties = collider.gameObject.GetComponent<EntityFSM>().entityProprieties as Player;
            playerProprieties.spawnPoint = new Vector2(transform.position.x, transform.position.y);
        }
    }
}
