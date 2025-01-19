using UnityEngine;

/// <summary>
/// The StickTrigger class is responsible for triggering the speech when the player interacts sees the stick.
/// </summary>
public class StickTrigger : MonoBehaviour
{
    /// <summary>
    /// The speechTrigger property is responsible for storing the speech trigger.
    /// </summary>
    [SerializeField]
    private SpeechTrigger speechTrigger;

    /// <summary>
    /// The OnTriggerEnter2D method is called when the Collider2D other enters the trigger (Unity Method).
    /// In this method we are checking if the player has entered the trigger and showing the speech.
    /// </summary>
    /// <param name="collision">The collider of the game object that collided.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if (collision.CompareTag("Player"))
        {   
            speechTrigger.ChangeSpeech("stickSpeech");
            Destroy(gameObject);
        }
    }
}
