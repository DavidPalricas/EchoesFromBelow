using UnityEngine;

/// <summary>
/// The StrangeManDialogueTrigger class is responsible for handling the logic of the a trigger that shows a speech box with the dialogue bewteen the player and the strange man.
/// </summary>
public class StrangeManDialogueTrigger : MonoBehaviour
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
    /// The strange man property is responsible for storing the strange man game object.
    /// </summary>
    [SerializeField]
    private GameObject strangeMan;


    /// <summary>
    /// The OnTriggerEnter2D method is called when the Collider2D other enters the trigger (Unity Method).
    /// In this method we are checking if the player has entered the trigger and showing the speech.
    /// </summary>
    /// <param name="collision">The collider of the game object that collided.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ChangePlayerDirection(collision.gameObject);
            speechTrigger.ChangeSpeech(speechName);   
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// The ChangePlayerDirection method is responsible for changing the player's sprite direction by updating its animator.
    /// The new sprite direction is based on the strange position.
    /// </summary>
    /// <param name="player">The player game object.</param>
    private void ChangePlayerDirection(GameObject player)
    {
        Animator playerAnimator = player.GetComponent<Animator>();

        Vector2 strangeManDirection = strangeMan.transform.position - player.transform.position;

        playerAnimator.SetFloat("Horizontal", strangeManDirection.x);
        playerAnimator.SetFloat("Vertical", strangeManDirection.y);
        playerAnimator.SetFloat("Speed", 0);
        playerAnimator.SetFloat("LastHorizontal", strangeManDirection.x);
        playerAnimator.SetFloat("LastVertical", strangeManDirection.y);
    }
}
