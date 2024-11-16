using System.Collections.Generic;
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
    /// The Items property is responsible for storing the items that the player has.
    /// </summary>
    public Dictionary<string, int> Items { get; private set; }

    /// <summary>
    /// The Weapons property is responsible for storing the weapons that the player has.
    /// </summary>
    public Dictionary <string,string> Weapons { get; private set; }

    /// <summary>
    /// The Awake method is called when the script instance is being loaded (Unity Method).
    /// In this method, we are initializing the player's inventory (Items and Weapons).
    /// </summary>
    private void Awake()
    {
        Items = new Dictionary<string, int>
        {
            { "HealItems", 0 },
            { "Key", 0 }
        };

        Weapons = new Dictionary<string, string>
        {
            { "Melee", null },
            { "Long", null }
        };

    }

    /// <summary>
    /// The resetItems method is responsible for resetting the player's items.
    /// This method is called when the player dies.
    /// </summary>
    public void ResetItems()
    {
        foreach (var item in Items)
        {
            Items[item.Key] = 0;
        }
    } 
}
