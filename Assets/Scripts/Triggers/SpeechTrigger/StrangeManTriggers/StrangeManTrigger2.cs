using UnityEngine;

public class StrangeManTrigger2 : StrangeManDialogueTrigger
{
    [SerializeField]
    PlayerInventory playerInventory;

    private void Update()
    {
        if (playerEntered && playerInventory.Items["FinalKey"] == 1)
        {
            speechTrigger.ChangeSpeech(speechName);
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// The OnTriggerEnter2D method is called when the Collider2D other enters the trigger (Unity Method).
    /// In this method we are calling the behavior of the parent class of this method.
    /// </summary>
    /// <param name="collision">The collider of the game object that collided.</param>
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }


    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
    }
}
