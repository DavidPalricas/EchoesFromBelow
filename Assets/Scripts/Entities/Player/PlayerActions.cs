using UnityEngine;

/// <summary>
/// The PlayerActions class is responsible for handling the player's actions.
/// </summary>
public class PlayerActions : MonoBehaviour
{
    /// <summary>
    /// The player property is responsible for storing the player's Rigidbody2D component.
    /// </summary>
    private Rigidbody2D player;

    /// <summary>
    /// The gate property is responsible for storing the gate GameObject.
    /// </summary>
    private GameObject gate;

    /// <summary>
    /// The rayCastDistance property is responsible for storing the distance of the ray cast.
    /// This ray cast will be used to check items or objects near the player.
    /// </summary>
    private readonly float rayCastDistance = 1.5f;

    /// <summary>
    /// The layer property is responsible for storing the layer mask.
    /// This layer mask will be used to check the layer of the objects and items near the player.
    /// </summary>
    private LayerMask layer;

    /// <summary>
    /// The itemNear property is responsible for storing the item near the player.
    /// </summary>
    private GameObject itemNear;

    /// <summary>
    /// The playerMaxHealth property is responsible for storing the player's maximum health.
    /// </summary>
    private int playerMaxHealth;

    /// <summary>
    /// The Awake method is called when the script instance is being loaded (Unity Method).
    /// In this method, we are initializing the player and gate variables and setting the HasKey property to false.
    /// </summary>
    private void Awake()
    {
        player = GetComponent<Rigidbody2D>();
        gate = GameObject.Find("Gate");
        layer = LayerMask.GetMask("Default");
        playerMaxHealth = GetComponent<Entity>().Health;
    }

    /// <summary>
    /// The Update method is called every frame (Unity Method).
    /// In this method, we are checking if the are gates and items in the level.
    /// If there are gates, the IsGateNear() and OpenGate() methods are called.
    /// If there are items, the ItemsLeft() and IsItemNear() methods are called.
    /// </summary>
    private void Update()
    {
        if (gate != null)
        {
            if (IsGateNear())
            {
                OpenGate();
            }
        }

        if (ItemsLeft() && IsItemNear())
        {
            GrabItem();
        }

        if (GetComponent<PlayerInventory>().HealItems > 0 && Input.GetKeyDown(KeyCode.H))
        {

            HealPlayer();
        }
    }

    /// <summary>
    /// The isGateNear method is responsible for checking if the player is near the gate.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the gate is near otherwise, <c>false</c>.
    /// </returns>
    private bool IsGateNear()
    {
        RaycastHit2D hit = Physics2D.Raycast(player.position, Vector2.down, rayCastDistance, layer);

        return hit.collider != null || hit.collider.gameObject.name != "Gate";
    }

    /// <summary>
    /// The IsItemNear method is responsible for checking if the player is near an item.
    /// To check if the player is near an item, a box collider is created around the player.
    /// If the player is near an item, the itemNear variable is set to the item game object.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if the item is  near; otherwise, <c>false</c>.
    /// </returns>
    private bool IsItemNear()
    {   
        BoxCollider2D playerCollider = GetComponent<BoxCollider2D>();

        // The box size is 1.5 times the player's collider size.
        Vector2 boxSize = playerCollider.bounds.size * 1.5f;

        Vector2 offsetPosition = (Vector2)playerCollider.bounds.center;

        Collider2D[] colliders = Physics2D.OverlapBoxAll(offsetPosition, boxSize, 0, layer);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Item"))
            {
                itemNear = collider.gameObject;
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// The OpenGate method is responsible for opening the gate, if the player has the key and presses the E key.
    /// </summary>
    private void OpenGate()
    {
        if (Input.GetKeyDown(KeyCode.E) && GetComponent<PlayerInventory>().HasKey)
        {
            gate.SetActive(false);
        }
    }

    /// <summary>
    /// The ItemsLeft method is responsible for checking if there are items left in the level.
    /// </summary>
    /// <returns></returns>
    private bool ItemsLeft()
    {
        return GameObject.FindGameObjectsWithTag("Item").Length > 0;
    }

    /// <summary>
    /// The GrabItem method is responsible for grabbing the item near the player.
    /// It grabs the item if the player presses the E key, and calls specific methods for each type of item.
    /// </summary>
    private void GrabItem()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (itemNear.name == "Key")
            {
                GrabKey();
            }
            else
            {
                GrabHealItem();
            }
        }
    }

    /// <summary>
    /// The GrabKey method is responsible for grabbing the key.
    /// The player's iventory is updated, and the key is destroyed.
    /// </summary>
    private void GrabKey()
    {
        GetComponent<PlayerInventory>().HasKey = true;

        GameObject itemToDestroy = itemNear;
        itemNear = null;
        Destroy(itemToDestroy);
    }

    /// <summary>
    /// The GrabHealItem method is responsible for grabbing the heal item, if the player has space in the inventory.
    /// If the player has space in the inventory, the player's inventory is updated, and the item is destroyed.
    /// </summary>
    private void GrabHealItem()
    {
        if (GetComponent<PlayerInventory>().HealItems < PlayerInventory.MaxHealItems)
        {
            GetComponent<PlayerInventory>().HealItems++;

            GameObject itemToDestroy = itemNear;
            itemNear = null;
            Destroy(itemToDestroy);
        }
    }

    /// <summary>
    /// The HealPlayer method is responsible for healing the player, if the player's health is less than its maximum health.
    /// </summary>
    private void HealPlayer()
    {   
        if (GetComponent<Entity>().Health < playerMaxHealth)
        {
            GetComponent<Entity>().Health++;
            GetComponent<PlayerInventory>().HealItems--;
        }
    }
}
