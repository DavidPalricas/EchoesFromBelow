using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

/// <summary>
/// The PlayerInventory class is responsible for representing the player's inventory.
/// </summary>
public class PlayerInventory : MonoBehaviour
{
    /// <summary>
    /// The stickIcon, flaskIcon, slingShotIcon and keyIcon properties are responsible for storing the icons of the stick, sling shot, flask, and key items respectively.
    /// Its Serialized so that it can be set in the Unity Editor.
    /// </summary>
    [SerializeField]
    private GameObject stickIcon, slingShotIcon, flaskIcon, keyIcon;

    /// <summary>
    /// The flaskQuantity property is responsible for storing the quantity of heal items in the player's inventory.
    /// Its Serialized so that it can be set in the Unity Editor.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI flaskQuantity;

    /// <summary>
    /// The MaxHealItems property is responsible for storing the maximum number of heal items that the player can have.
    /// </summary>
    public const int MaxHealItems = 3;

    /// <summary>
    /// The Items property is responsible for storing the items that the player has.
    /// </summary>player
    public Dictionary<string, int> Items { get; private set; }

    /// <summary>
    /// The weapons property is responsible for storing the weapons that the player has.
    /// </summary>
    public Dictionary<string, string> Weapons { get; set; }

    /// <summary>
    /// The currentWeapon property is responsible for storing the current weapon that the player is using.
    /// </summary>
    [HideInInspector]
    public string currentWeapon;

    /// <summary>
    /// The Awake method is called when the script instance is being loaded (Unity Method).
    /// In this method, we are initializing the player's inventory (Items and weapons).
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
            { "Ranged", null }
        };
    }

    /// <summary>
    /// The UpdateRangedWeapon method is responsible for updating the ranged weapon in the player's inventory.
    /// </summary>
    private void UpdateRangedWeapon()
    {
        Weapons["Ranged"] = "SlingShot";
        slingShotIcon.SetActive(true);
    }

    /// <summary>
    /// The UpdateMeleeWeapon method is responsible for updating the melee weapon in the player's inventory.
    /// And its icon on the HUD.
    /// </summary>
    /// <param name="meleeWeapon">The melee weapon that the player grabbed.</param>
    private void UpdateMeleeWeapon(Utils.CollectableType meleeWeapon)
    {
        Weapons["Melee"] = meleeWeapon == Utils.CollectableType.Stick ? "Stick" : "Sword";

        if (meleeWeapon == Utils.CollectableType.Stick)
        {
            stickIcon.SetActive(true);
        }
    }

    /// <summary>
    /// The UpdateKeyItem method is responsible for updating the key item in the player's inventory and its icon on the HUD.
    /// This method also checks if the player grabbed the right key to open the gate by calling the RightKeyGrabbed method.
    /// If the player grabbed the right key, its value is set to 2, otherwise it is set to 1.
    /// </summary>
    private void UpdateKeyItem()
    {
        Items["Key"] = RightKeyGrabbed() ? 2 : 1;
        keyIcon.SetActive(true);
    }

    /// <summary>
    /// The UpdateHealItem method is responsible for updating the heal item in the player's inventory and its icon on the HUD.
    /// If the player has less than the maximum number of heal items, the quantity is incremented.
    /// If the player has the maximum number of heal items, the flask icon color is changed to yellow to indicate that the player has the maximum number of heal items.
    /// </summary>
    private void UpdateHealItem()
    {
        if (Items["HealItems"] < MaxHealItems)
        {
            Items["HealItems"]++;
            flaskQuantity.text = Items["HealItems"].ToString();
            flaskIcon.SetActive(true);
        }

        if (Items["HealItems"] == MaxHealItems)
        {
            flaskQuantity.color = new Color32(255, 178, 0, 255);
        }
        else
        {

            flaskQuantity.color = Color.white;

        }
    }

    /// <summary>
    /// The RightKeyGrabbed method is responsible for checking if the player has grabbed the right key, to open the gate.
    /// If the are no keys with a true value in the level, it means that the player has grabbed the right key.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if the right key was grabbed; otherwise, <c>false</c>.
    /// </returns>

    private bool RightKeyGrabbed()
    {
        // Gets the key and its values (true or false)
        Dictionary<GameObject, bool> keys = GameObject.Find("Level1").GetComponent<Level1Logic>().Keys;

        Debug.Log("Keys Left : " + GameObject.Find("Level1").GetComponent<Level1Logic>().Keys.Count);

        return (!keys.Values.Any(value => value));
    }

    /// <summary>
    /// The UpdateInventory method is responsible for updating the player's inventory and HUD when a collectable item is grabbed.
    /// Base on the type of the collectable a proper method is called to update the inventory.
    /// </summary>
    /// <param name="collectable">The collectable that player grabbed.</param>
    public void UpdateInventory(Utils.CollectableType collectable)
    {
        switch (collectable)
        {
            case Utils.CollectableType.Stick:
            case Utils.CollectableType.Sword:
                UpdateMeleeWeapon(collectable);

                return;

            case Utils.CollectableType.SlingShot:
                UpdateRangedWeapon();
                return;

            case Utils.CollectableType.Key:
                UpdateKeyItem();

                return;

            case Utils.CollectableType.HealItem:
                UpdateHealItem();
                return;
        }
    }

    /// <summary>
    /// The HealItemUsed method is responsible for updating the player's inventory and HUD when a heal item is used.
    /// If the player has no heal items left its icon is desatived in the HUD.
    /// </summary>
    public void HealItemUsed()
    {
        Items["HealItems"]--;

        if (Items["HealItems"] == 0)
        {
            flaskIcon.SetActive(false);
        }

        if (Items["HealItems"] < MaxHealItems)
        {

            flaskQuantity.color = Color.white;

        }

        flaskQuantity.text = Items["HealItems"].ToString();
    }

    /// <summary>
    /// The UpdateCurrentWeapon method is responsible for updating the current weapon that the player is using in the HUD.
    /// </summary>
    /// <param name="newCurrentWeapon">The new current weapon.</param>
    public void UpdateCurrentWeapon(string newCurrentWeapon)
    {
        currentWeapon = newCurrentWeapon;

        if (newCurrentWeapon == "Ranged")
        {
            slingShotIcon.SetActive(true);
            stickIcon.SetActive(false);
        }
        else
        {
            stickIcon.SetActive(true);
            slingShotIcon.SetActive(false);
        }
    }
}
