using UnityEngine;

/// <summary>
/// The GraveyardCheckPoint class is responsible for triggering a checkpoint when the player enters in the "checkpoint" area of the graveyard.
/// </summary>
public class GraveyardCheckPoint : MonoBehaviour
{
    /// <summary>
    /// The fakeLever property is responsible for storing the fake lever game object.
    /// </summary>
    [SerializeField]
    private GameObject Lever;

    /// <summary>
    /// The level2Logic property is responsible for storing the Level2Logic component.
    /// </summary>
    [SerializeField]
    private Level2Logic level2Logic;

    /// <summary>
    /// The OnTriggerExit2D method is called when the Collider2D other exits the trigger (Unity Method).
    /// In this method, the player's spawn point is changed when the player enters the checkpoint area and the checkpoint is destroyed.
    /// </summary>
    /// <param name="collision">The collider of the game object that collided.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ChangeLeverPosition();

            Player playerProprieties = collision.GetComponent<EntityFSM>().entityProprieties as Player;

            playerProprieties.spawnPoint = new Vector2(transform.position.x, transform.position.y);

            Destroy(gameObject);
        }
    }

    /// <summary>
    /// The ChangeLeverPosition method is responsible for changing the lever position.
    /// </summary>
    private void ChangeLeverPosition()
    {
        Lever.transform.position = level2Logic.GetNewLeverPosition();
    }
}
