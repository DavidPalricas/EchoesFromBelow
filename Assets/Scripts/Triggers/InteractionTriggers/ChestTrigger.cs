using UnityEngine;

public class ChestTrigger : BaseInteractionTrigger
{
    [SerializeField]
    private PlayerInventory playerInventory;

    [SerializeField]
    private GameObject finalKeyPrefab;

    [SerializeField]
    private GameObject chestObject;


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
    private void Update()
    {
        if (playerDetected && InteractInputTriggered() && playerInventory.Items["Key"] == 1)
        {
            OpenChest();
            chestObject.GetComponent<Animator>().Play("openChest");
        }
    }

    private void OpenChest()
    {   
        Utils.PlaySoundEffect("chest");
        playerInventory.KeyOrLeverUsed(true);

        var finalKeyPosition = new Vector3(transform.position.x, transform.position.y - 2, transform.position.z);

        Instantiate(finalKeyPrefab, finalKeyPosition, Quaternion.identity);

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
