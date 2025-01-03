using UnityEngine;

/// <summary>
/// The BaseInteractionTrigger class is responsible for creating a base class for the interaction triggers.
/// </summary>
public class BaseInteractionTrigger : MonoBehaviour
{
    /// <summary>
    /// The playerDetected property is responsible for storing the player detection status.
    /// </summary>
    protected bool playerDetected;

    /// <summary>
    /// The playerActions property is responsible for storing the player actions component.
    /// It is serialized to be set in the Unity Editor.
    /// </summary>
    [SerializeField]
    private PlayerActions playerActions;


    /// <summary>
    /// The OnTriggerEnter2D method is called when the Collider2D other enters the trigger (Unity Method).
    /// In this method, we are setting the playerDetected variable to true (player enter in the trigger area).
    /// The OnTriggerEnter2D method is virtual so it can be overriden by the child classes.
    /// </summary>
    /// <param name="collision">The collider of the game object that collided.</param>
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerDetected = true;
        }
    }

    /// <summary>
    /// The OnTriggerExit2D method is called when the Collider2D other exits the trigger (Unity Method).
    /// In this method, we are setting the playerDetected variable to false (player left the torch trigger area).
    /// The OnTriggerExit2D method is virtual so it can be overriden by the child classes.
    /// </summary>
    /// <param name="collision">The collider of the game object that collided.</param>
    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerDetected = false;
        }
    }

    /// <summary>
    /// The InteractInputTriggered method is responsible for checking if the player pressed the interact input.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if the interact input is triggered; otherwise, <c>false</c>.
    /// </returns>
    protected bool InteractInputTriggered()
    {
        return playerActions.InputTriggered("Interact");
    }
}
