using UnityEngine;

public class StrangeManTrigger1 : StrangeManDialogueTrigger
{
    [SerializeField]
    private StrangeManTrigger2 strangeManTrigger2;


    private void Update()
    {
        if (playerEntered)
        {   

            GameObject player = GameObject.FindGameObjectWithTag("Player");

            Player playerProprieties = (Player) player.GetComponent<EntityFSM>().entityProprieties;
            playerProprieties.spawnPoint = transform.position;

            speechTrigger.ChangeSpeech(speechName);
            enabled = false;
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
        strangeManTrigger2.enabled = true;

    }
}
