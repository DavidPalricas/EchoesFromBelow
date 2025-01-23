using UnityEngine;

/// <summary>
/// The PhysicalSpeechTrigger class is responsible for handling the logic of the a trigger that shows a speech box when the player enters it.
/// </summary>
public class PhysicalSpeechTrigger : MonoBehaviour
{
    /// <summary>
    /// The speechTrigger property is responsible for storing the speech trigger.
    /// </summary>
    [SerializeField]
    private SpeechTrigger speechTrigger;

    /// <summary>
    /// The speechTrigger property is responsible for storing the speech name.
    /// </summary>
    [SerializeField]
    private string speechName;

    /// <summary>
    /// The OnTriggerEnter2D method is called when the Collider2D other enters the trigger (Unity Method).
    /// In this method we are checking if the player has entered the trigger ,showing the speech and destroying the trigger.
    /// </summary>
    /// <param name="collision">The collider of the game object that collided.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if (collision.CompareTag("Player"))
        {   
            speechTrigger.ChangeSpeech(speechName);
            Destroy(gameObject);
        }
    }
}
