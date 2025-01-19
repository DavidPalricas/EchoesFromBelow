using UnityEngine;

/// <summary>
/// The BoneTrigger class is responsible for triggering a horde spawn when the player enters in the "bone" area.
/// </summary>
public class BoneTrigger : MonoBehaviour
{
    /// <summary>
    /// The mainCameraMovement property is responsible for storing the main camera movement component.
    /// </summary>
    [SerializeField]
    private CameraMovement mainCameraMovement;

    /// <summary>
    /// The gate property is responsible for storing the gate game object.
    /// </summary>
    [SerializeField]
    private GameObject gate;

    /// <summary>
    /// The spawnHorde property is responsible for storing the spawn horde component.
    /// </summary>
    [SerializeField]
    SpawnHorde SpawnHorde;

    /// <summary>
    /// The speechTrigger property is responsible for storing the speech trigger.
    /// </summary>
    [SerializeField]
    private SpeechTrigger speechTrigger;

    /// <summary>
    /// The OnTriggerExit2D method is called when the Collider2D other exits the trigger (Unity Method).
    /// In this method, we are enabling the spawn of hordes in the game, changing the player's spawn point, and calling the GateSpotted method to handle the logic when the gate is spotted.
    /// </summary>
    /// <param name="collision">The collider of the game object that collided.</param>
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            SpawnHorde.enabled = true;

            ChangePlayerSpawnPoint(collider.gameObject);

            mainCameraMovement.ChangeTarget(gate, 1.6f);

            speechTrigger.ChangeSpeech("gateSpottedSpeech");

            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// The ChangePlayerSpawnPoint method is responsible for changing the player's spawn point.
    /// </summary>
    /// <param name="player">The player's game object.</param>
    private void ChangePlayerSpawnPoint(GameObject player)
    {
        Player playerProprieties = player.GetComponent<EntityFSM>().entityProprieties as Player;
        playerProprieties.spawnPoint = new Vector2(transform.position.x, transform.position.y);
    }
}
