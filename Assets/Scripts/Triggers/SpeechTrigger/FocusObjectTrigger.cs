
using UnityEngine;

/// <summary>
///  The FocusObjectTrigger class is responsible for handling the logic of the a trigger that shows a speech box and focus the camera on an object.
/// </summary>
public class FocusObjectTrigger : MonoBehaviour
{
    /// <summary>
    /// The speechTrigger property is responsible for storing the speech trigger.
    /// </summary>
    [SerializeField]
    private SpeechTrigger speechTrigger;

    /// <summary>
    /// The mainCameraMovement property is responsible for storing the main camera movement component.
    /// </summary>
    [SerializeField]
    private CameraMovement mainCameraMovement;

    /// <summary>
    /// The object to focus
    /// </summary>
    [SerializeField]
    private GameObject objectToFocus;

    /// <summary>
    /// The speechTrigger property is responsible for storing the speech name.
    /// </summary>
    [SerializeField]
    private string speechName;

    /// <summary>
    /// The OnTriggerEnter2D method is called when the Collider2D other enters the trigger (Unity Method).
    /// In this method we are checking if the player has entered the trigger and showing the speech.
    /// Before showing the speech, we are changing the camera target to the object to focus, and after that the trigger is destroyed.
    /// </summary>
    /// <param name="collision">The collider of the game object that collided.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {   
            mainCameraMovement.ChangeTarget(objectToFocus, 1f);
            speechTrigger.ChangeSpeech(speechName);
            Destroy(gameObject);
        }
    }
}
