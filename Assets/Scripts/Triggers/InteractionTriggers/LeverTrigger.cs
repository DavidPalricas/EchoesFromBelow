using UnityEngine;

public class LeverTrigger : BaseInteractionTrigger
{
    [SerializeField]
    private PlayerInventory playerInventory;

    [SerializeField]
    private GameObject bridge_left, bridge_right;

    [SerializeField]
    private GameObject leverObject;


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

    // Update is called once per frame
    private void Update()
    {
        if (playerDetected && InteractInputTriggered() && playerInventory.Items["Lever"] == 1){
            ActivateBridge();
        }
    }

    private void ActivateBridge()
    {
        leverObject.GetComponent<Animator>().Play("leverActivation");
        
        playerInventory.KeyOrLeverUsed(false);

        bridge_left.GetComponent<BoxCollider2D>().enabled = false;
        bridge_left.GetComponent<Animator>().Play("Bridge_Left_open");
        bridge_right.GetComponent<BoxCollider2D>().enabled = false;
        bridge_right.GetComponent<Animator>().Play("Bridge_Right_open");

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
