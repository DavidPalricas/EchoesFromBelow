using UnityEngine;

/// <summary>
/// The TutorialTriggers class is responsible for handling the logic of invisible walls when the player collides with them it triggers a tutorial speech.
/// </summary>
public class TutorialTriggers : MonoBehaviour
{
    /// <summary>
    /// The speechTrigger property is responsible for storing the speech trigger.
    /// </summary>
    [SerializeField]
    private SpeechTrigger speechTrigger;

    /// <summary>
    /// The tutorialSpeech property is responsible for storing the tutorial speech.
    /// </summary>
    [SerializeField]
    private string tutorialSpeech;

    /// <summary>
    /// The OnCollisionEnter2D method is called when the object enters in collision with another object (Unity Method).
    /// In this method, we are checking if the object that entered in collision is the player, if it is, we are changing the speech.
    /// </summary>
    /// <param name="collision">The gameobject that collided.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            speechTrigger.ChangeSpeech(tutorialSpeech);
        }
    }
}

