using Unity.VisualScripting;
using UnityEngine;

public class FinalGateTrigger : BaseInteractionTrigger
{
    [SerializeField]
    private GameObject Finalgate, secondDialogueWithStrangeMan;

    [SerializeField]
    private PlayerInventory playerInventory;

    [SerializeField]
     private SpeechTrigger speechTrigger;


    private void OnEnable()
    {   
        if (playerInventory == null)
        {
            playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        }

        if (playerActions == null)
        {
            playerActions = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActions>();
        }
    }

    /// <summary>
    /// The Update method is called every frame (Unity Method).
    /// In this method, we are checking if the player is detected and if the player interact input was triggred and if the conditions to open the gate are met.
    /// If these conditions are met, the OpenGate method is called to open the gate.
    /// </summary>
    private void Update()
    {
        if (playerDetected && InteractInputTriggered() && PlayerHasFinalKey())
        {   
            if (!secondDialogueWithStrangeMan.IsDestroyed())
            {
                speechTrigger.ChangeSpeech("mustTalkWithNPC");

                return;
            }
            OpenFinalGate();
        }
    }

    private bool PlayerHasFinalKey()
    {
        return playerInventory.Items["FinalKey"] == 1;
    }


    private void OpenFinalGate()
    {   
        playerInventory.FinalKeyUsed();
        Destroy(Finalgate);
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

    /// <summary>
    /// The OnTriggerExit2D method is called when the Collider2D other exits the trigger (Unity Method).
    /// In this method we are calling the behavior of the parent class of this method.
    /// </summary>
    /// <param name="collision">The collider of the game object that collided.</param>
    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
    }
}
