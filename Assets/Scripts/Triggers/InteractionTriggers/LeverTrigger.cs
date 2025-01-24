using UnityEngine;

public class LeverTrigger : BaseInteractionTrigger
{
    [SerializeField]
    private PlayerInventory playerInventory;

    [SerializeField]
    private GameObject bridge;


    // Update is called once per frame
    private void Update()
    {
        if (playerDetected && InteractInputTriggered() && playerInventory.Items["Lever"] == 1){
            ActivateBridge();
        }
    }

    private void ActivateBridge()
    {
        playerInventory.KeyOrLeverUsed(false);

        bridge.SetActive(true);

        Destroy(gameObject);
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
