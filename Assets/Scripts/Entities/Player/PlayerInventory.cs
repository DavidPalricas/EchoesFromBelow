using UnityEngine;

/// <summary>
/// The PlayerInventory class is responsible for representing the player's inventory.
/// </summary>
public class PlayerInventory : MonoBehaviour
{
    /// <summary>
    /// The MaxHealItems property is responsible for storing the maximum number of heal items that the player can have.
    /// </summary>
    public const int MaxHealItems = 3;

    /// <summary>
    /// The HasKey property is responsible for indicating if the player has the key.
    /// </summary>
    public bool HasKey { get; set; }

    /// <summary>
    /// The HealItems property is responsible for storing the number of heal items that the player has.
    /// </summary>
    public int HealItems { get; set; }

    /// <summary>
    /// The Awake method is called when the script instance is being loaded (Unity Method).
    /// In this method, we are initializing the player's inventory.
    /// </summary>
    private void Awake()
    {
        HealItems = 0;
        HasKey = false;
    }

    /// <summary>
    /// The resetItems method is responsible for resetting the player's items.
    /// This method is called when the player dies.
    /// </summary>
    public void ResetItems()
    {
        HealItems = 0;
        HasKey = false;
    } 
}
