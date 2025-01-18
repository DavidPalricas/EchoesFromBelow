using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// The PlayerActions class is responsible for handling the player's actions.
/// These actions include grabbing collecatables, healing the player, switching weapons and check the player's input.
/// </summary>
public class PlayerActions : MonoBehaviour
{
    /// <summary>
    /// The objectNear properties is responsible for storing the object near the player.
    /// </summary>
    private GameObject collectableGrabed;

    /// <summary>
    /// The playerMaxHealth property is responsible for storing the player's maximum health.
    /// </summary>
    private int playerMaxHealth;

    /// <summary>
    /// The playerArmed property is responsible for verifying if the player has a weapon equiped.
    /// </summary>
    private bool playerArmed = false;

    /// <summary>
    /// The playerInventory property is responsible for storing the player's inventory.
    /// </summary>
    private PlayerInventory playerInventory;

    /// <summary>
    /// The healInput, interactInput, switchWeaponsInput, attackInput and pauseUnpauseInput, skipDialogueInput  properties are responsible for storing the player's input actions.
    /// These properties 
    /// These properties are serialized to be set in the Unity Editor.
    /// </summary>
    [SerializeField]
    private InputActionReference healInput, interactInput, switchWeaponsInput, attackInput, pauseUnpauseInput ,skipDialogueInput;

    /// <summary>
    /// The Awake method is called when the script instance is being loaded (Unity Method).
    /// In this method, we are initializing the player and gate variables and setting the HasKey property to false.
    /// </summary>
    private void Awake()
    {
        playerMaxHealth = GetComponent<Entity>().maxHealth;
        playerInventory = GetComponent<PlayerInventory>();
    }

    /// <summary>
    /// The Update method is called every frame (Unity Method).
    /// In this method, we are checking  whether the player pressed the H key to heal the player or the Q key to switch weapons.
    /// The SwitchWeapons method  if the player can switch weapons (check with the SwitchHeaponsConditions method).
    /// </summary>
    private void Update()
    {
        if (healInput.action.triggered)
        {
            HealPlayer();
        }

        if (switchWeaponsInput.action.triggered && SwitchHeaponsConditions()){
            SwitchWeapons();   
        }
    }

    /// <summary>
    /// The GrabWeapons method is responsible for grabbing the weapons.
    /// It adds a weapon to the player's inventory, removes the weapon from the level.
    /// If the player is not armed, it enables the player's attack component and updates the animator.
    /// </summary>
    private void GrabWeapons(Utils.CollectableType weapon)
    {   
        if (!playerArmed)
        {
            GetComponent<Player>().attack.enabled = true;

            // Its not necessary to check if the weapon is as sword or a stick, because the player default attack component is a melee attack
            if (weapon == Utils.CollectableType.SlingShot)
            {
                GetComponent<EntityFSM>().entityProprieties.attack = GetComponent<EntityRangedAttack>();
            }

            playerArmed = true;

            GetComponent<Player>().animator.SetBool("HasWeapon", true);
        }

        DestroyCollectable();

        playerInventory.UpdateInventory(weapon);
    }

    /// <summary>
    /// The GrabKey method is responsible for grabbing the key if the player doesn't have a key in the inventory.
    /// It adds a key to the player's inventory, removes the key from the level.
    /// </summary>
    private void GrabKey()
    {   
        if (playerInventory.Items["Key"] == 0)
        {   
            GameObject keyToDestroy = DestroyCollectable();

            // Removes the key from the dictionary which stores the keys and their values
            GameObject.Find("Level1").GetComponent<Level1Logic>().Keys.Remove(keyToDestroy);

            playerInventory.UpdateInventory(Utils.CollectableType.Key);
        } 
    }

    /// <summary>
    /// The GrabHealItem method is responsible for grabbing the heal item, if the player has space in the inventory.
    /// If the player has space in the inventory, the player's inventory is updated, and the item is destroyed.
    /// </summary>
    private void GrabHealItem()
    {   
        Dictionary<string, int> Items = playerInventory.Items;

        if (Items["HealItems"] < PlayerInventory.MaxHealItems)
        {    
            DestroyCollectable();
        }

        playerInventory.UpdateInventory(Utils.CollectableType.HealItem);
    }

    /// <summary>
    /// The DestroyCollectable method is responsible for destroying the collectable item grabbed by the player.
    /// </summary>
    /// <returns>The destroyed object </returns>
    private GameObject DestroyCollectable()
    {
        collectableGrabed.GetComponent<Collectable>().isCollected = true;

        GameObject collectableToDestroy = collectableGrabed;
        collectableGrabed = null;
        Destroy(collectableToDestroy);

        return collectableToDestroy;
    }

    /// <summary>
    /// The HealPlayer method is responsible for healing the player, if the heal conditions are met.
    /// </summary>
    private void HealPlayer()
    {
        EntityFSM entityFSM = GetComponent<EntityFSM>();

        if (HealConditions(entityFSM.entitycurrentHealth, entityFSM.currentState))
        {   
            entityFSM.entitycurrentHealth = Math.Min(entityFSM.entitycurrentHealth + playerMaxHealth / 2, playerMaxHealth);

            playerInventory.HealItemUsed();

            GameObject.Find("Level1").GetComponent<Rank>().HealItemsUsed++;

            HealthBar healthBar = (entityFSM.entityProprieties as Player).healthBar;

            healthBar.UpdateLabel(entityFSM.entitycurrentHealth);
        }
    }

    /// <summary>
    /// The HealConditions method is responsible for checking if the heal conditions are met.
    /// These conditions are: the player has heal items, the player is not attacking and the player's health is less than the maximum health.
    /// </summary>
    /// <param name="playerCurrentHealth">The player's current health.</param>
    /// <param name="playerCurrentState">The player's current state.</param>
    /// <returns>
    ///   <c>true</c> if the heal conditions are mete; otherwise, <c>false</c>.
    /// </returns>
    private bool HealConditions(int playerCurrentHealth,IState playerCurrentState)
    {
        return playerInventory.Items["HealItems"] > 0 && playerCurrentState is not EntityAttackState && playerCurrentHealth < playerMaxHealth;
    }

    /// <summary>
    /// The SwitchWeapons method is responsible for switching the player's weapons type (melee or ranged).
    /// After switching the weapon, the player's attack component is updated to ensure the logic of the new weapon type
    /// </summary>
    private void SwitchWeapons()
    {   
        string currentWeapon = playerInventory.currentWeapon == "Melee" ? "Ranged" : "Melee";

        EntityAttack attackType = currentWeapon == "Melee" ? GetComponent<EntityMeleeAttack>() : GetComponent<EntityRangedAttack>();

        GetComponent<EntityFSM>().entityProprieties.attack = attackType;

        playerInventory.UpdateCurrentWeapon(currentWeapon);
    }

    /// <summary>
    /// The SwitchHeaponsConditions method is responsible for checking if the player can switch weapons.
    /// These conditions are: the player is armed and the player has 2 different types of weapons (melee and ranged).
    /// To check if the player has 2 different types of weapons its dictionary cannot have any null value has the weapon name.
    /// </summary>
    /// <returns></returns>
    private bool SwitchHeaponsConditions()
    {   
        return playerArmed  && !playerInventory.Weapons.Values.Any(weaponName => weaponName == null);
    }

    /// <summary>
    /// The GrabObject method is responsible for grabbing the object near the player.
    /// It calls the correct method to grab the object, depending on the object's type.
    /// </summary>
    public void GrabCollectable(GameObject collectable)
    {   
        collectableGrabed = collectable;

        Utils.CollectableType CollectableType = collectable.GetComponent<Collectable>().type;

        switch (CollectableType)
        {
            case Utils.CollectableType.Stick:
            case Utils.CollectableType.Sword:
            case Utils.CollectableType.SlingShot:
                GrabWeapons(CollectableType);

                return;

            case Utils.CollectableType.Key:
                GrabKey();

                return;

            case Utils.CollectableType.HealItem:
                GrabHealItem();
 
                return;
        }
    }

    /// <summary>
    /// The InputTriggered method is responsible for checking if the input was triggered.
    /// If the inputName is invalid, an error message is displayed.
    /// </summary>
    /// <param name="inputName"> The name of the input.</param>
    /// <returns>
    ///   <c>true</c> if the selected input is triggered; otherwise, <c>false</c>.
    /// </returns>
    public bool InputTriggered(string inputName)
    {
        switch (inputName)
        {
            case "Pause/Unpause":
                return pauseUnpauseInput.action.triggered;

            case "Interact":
                return interactInput.action.triggered;

            case "Attack":
                return attackInput.action.triggered;

            case "SkipDialogue":
                return skipDialogueInput.action.triggered;

            default:
                Debug.LogError("Invalid Input Name");
                return false;
        }
    }
}
