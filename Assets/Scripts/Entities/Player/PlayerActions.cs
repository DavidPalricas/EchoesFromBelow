using System.Collections.Generic;
using System.Linq;
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


    /// <summary>
    /// The layer property is responsible for storing the layer mask.
    /// This layer mask will be used to check the layer of the objects and items near the player.
    /// </summary>
    private LayerMask layer;

    /// <summary>
    /// The gate and itemNear properties are responsible for storing the gate and item near the player.
    /// </summary>
    private GameObject gate, itemNear;

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
            if (Input.GetKeyDown(KeyCode.E) && IsGateNear())
            {   
                OpenGate();
            }
        }

        if (ItemsLeft() && IsItemNear())
        {
            GrabItem();
        }

        if (Input.GetKeyDown(KeyCode.H) && GetComponent<PlayerInventory>().Items["HealItems"] > 0)
        {
            HealPlayer();
        }
    }

    /// <summary>
    /// The isGateNear method is responsible for checking if the player is near the gate.
    /// A raycast is create to check if the player is near the gate and its facing down the gate.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the gate is near otherwise, <c>false</c>.
    /// </returns>
    private bool IsGateNear()
    {        
        float rayCastDistance = 1.5f;

        Vector2 raycastOrigin = player.position + new Vector2(0, -1f); 

        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, Vector2.down, rayCastDistance, layer);

        // This line is used to visualize the raycast in the scene view, for debugging purposes.
        // Debug.DrawRay(raycastOrigin, Vector2.down * rayCastDistance, Color.red);

        return hit.collider != null && hit.collider.gameObject.name == "Gate";
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
    /// The OpenGate method is responsible for opening the gate, if the player has the key.
    /// If the player has the corect key, the gate is opened, otherwise the key is removed from the player's inventory.
    /// To check if the player has the correct key, it checks the keys dictionary of the first level.
    /// If the keys dictionary has no key with a true value, it means that the player has grabbed the correct key.
    /// </summary>
    private void OpenGate()
    {
        if (GetComponent<PlayerInventory>().Items["Key"] == 1)
        {
            // Gets the key and its value
            Dictionary<GameObject, bool> keys = GameObject.Find("Level1").GetComponent<Level1Logic>().Keys;

            if (!keys.Values.Any(value => value))
            {    
                // Opens the gate
                 gate.SetActive(false);
            }
            else
            {
                // Removes the key from the player's inventory
                GetComponent<PlayerInventory>().Items["Key"] = 0;
            }
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
          
            if (itemNear.name.Contains("Key"))
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
    /// It adds a key to the player's inventory, and removes the key from the level.
    /// </summary>
    private void GrabKey()
    {   
        if (GetComponent<PlayerInventory>().Items["Key"] == 0)
        {  
            GetComponent<PlayerInventory>().Items["Key"] = 1;

            GameObject itemToDestroy = DestroyItem();

            // Removes the key from the dictionary which stores the keys and their values
            GameObject.Find("Level1").GetComponent<Level1Logic>().Keys.Remove(itemToDestroy); ;
        }   
    }

    /// <summary>
    /// The GrabHealItem method is responsible for grabbing the heal item, if the player has space in the inventory.
    /// If the player has space in the inventory, the player's inventory is updated, and the item is destroyed.
    /// </summary>
    private void GrabHealItem()
    {
        if (GetComponent<PlayerInventory>().Items["HealItems"] < PlayerInventory.MaxHealItems)
        {
            GetComponent<PlayerInventory>().Items["HealItems"]++;

            DestroyItem();
        }
    }

    /// <summary>
    /// The DestroyItem method is responsible for destroying the item near the player.
    /// </summary>
    /// <returns>The item to destroy</returns>
    private GameObject DestroyItem()
    {
        GameObject itemToDestroy = itemNear;
        itemNear = null;
        Destroy(itemToDestroy);

        return itemToDestroy;
    }

    /// <summary>
    /// The HealPlayer method is responsible for healing the player, if the player's health is less than its maximum health.
    /// </summary>
    private void HealPlayer()
    {   
        if (GetComponent<Entity>().Health < playerMaxHealth)
        {
            GetComponent<Entity>().Health++;
            GetComponent<PlayerInventory>().Items["HealItems"]--;
        }
    }
}
