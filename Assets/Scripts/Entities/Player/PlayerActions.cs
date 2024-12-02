using System.Collections.Generic;
using System.Linq;
using TMPro;
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
    /// The layer property is responsible for storing the layer mask.
    /// This layer mask will be used to check the layer of the objects and items near the player.
    /// </summary>
    private LayerMask layer;

    /// <summary>
    /// The gate and objectNear properties are responsible for storing the gate and object near the player.
    /// </summary>
    private GameObject gate,collectableItemGrabed;

    /// <summary>
    /// The playerMaxHealth property is responsible for storing the player's maximum health.
    /// </summary>
    private int playerMaxHealth;

    /// <summary>
    /// The playerArmed property is responsible for verifying if the player has a weapon equiped.
    /// The ...Iconm properties are responsible for identifying the proper UI images for the equiped weapons.
    /// </summary>
    private bool playerArmed = false;

    /// <summary>
    /// The animator property is responsible for storing the player's animator.
    /// </summary>
    private Animator animator;

    /// <summary>
    /// The stickIcon property is responsible for storing the stick icon.
    /// Its Serialized so that it can be set in the Unity Editor.
    /// </summary>
    [SerializeField]
    private GameObject stickIcon;

    /// <summary>
    /// Icone e texto do icone dos frascos de vida no HUD.
    /// Its Serialized so that it can be set in the Unity Editor.
    /// </summary>
    [SerializeField]
    private GameObject flaskIcon;

    /// <summary>
    /// The flaskQuantity property is responsible for storing the quantity of heal items in the player's inventory.
    /// Its Serialized so that it can be set in the Unity Editor.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI flaskQuantity;

    /// <summary>
    /// The healthBar property is responsible for storing the player's health bar.
    /// Its Serialized so that it can be set in the Unity Editor.
    /// </summary>
    [SerializeField]
    private HealthBar healthBar;

    /// <summary>
    /// The keyIcon property is responsible for storing the key icon.
    /// Its Serialized so that it can be set in the Unity Editor.
    /// </summary>
    [SerializeField]
    private GameObject keyIcon;

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
        animator = GetComponent<Entity>().animator;
    }

    /// <summary>
    /// The Update method is called every frame (Unity Method).
    /// In this method, we are checking if the are gates, if the player pressed the E key, and if the heal conditions are met.
    /// If there are gates, the IsGateNear() and OpenGate() methods are called.
    /// If the player pressed the E key, the CheckGrabObjectsConditions() method is called.
    /// And if the player pressed the H key, the HealPlayer() method is called.
    /// </summary>
    /// 
    private void Update()
    {

        if (gate != null)
        {
            if (Input.GetKeyDown(KeyCode.E) && CheckConditionsToOpenGate())
            {   
                OpenGate();
            }
        }

        // Heal Conditions
        if (Input.GetKeyDown(KeyCode.H) && GetComponent<PlayerInventory>().Items["HealItems"] > 0)
        {
            HealPlayer();
        }

        if (playerArmed == true){
            // FALTA FAZER CÓDIGO PARA VERIIFCAR SE O JOGADOR JÁ TEM AS OUTRAS ARMAS NO INVENTÁRIO.
            // SE NÃO, CONSEGUE TROCAR PARA QUALQUER IMAGEM DE ARMA SEM AS TER EQUIPADAS

            if (Input.GetKeyDown(KeyCode.Q)){

                //INSERIR CÓDIGO PARA TROCAR PARA AS FUTURAS ARMAS

            }
        }
    }

    /// <summary>
    /// The CheckGateConditionsToOpen method is responsible for checking if the gate conditions are met to open the gate.
    /// First , it checks if the player is facing down, or if the player is idle and the last direction was down.
    /// After that, it checks if the player is near the gate, calling the IsGateNear method.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the gate is near otherwise, <c>false</c>.
    /// </returns>
    private bool CheckConditionsToOpenGate()
    {   
        Vector2 LastMovingDirection = GameObject.Find("Player").GetComponent<PlayerMovement>().LastMovingDirection;

        Vector2 playerDirection = Utils.NormalizeDirectionVector(player.velocity);

        return (playerDirection == Vector2.down || LastMovingDirection == Vector2.down) && IsGateNear();
    }

    /// <summary>
    /// The IsGateNear method is responsible for checking if the player is near a gate, the object can be an item or a weapon.
    /// To check if the player is near an object, a box collider is created around the player.
    /// If the player is near an  object, the objectNear property is updated with the object.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if the object is  near; otherwise, <c>false</c>.
    /// </returns>
    private bool IsGateNear()
    {   
        BoxCollider2D playerCollider = GetComponent<BoxCollider2D>();

        // The box size is 1.5 times the player's collider size.
        Vector2 boxSize = playerCollider.bounds.size * 1.5f;

        Vector2 offsetPosition = (Vector2)playerCollider.bounds.center;

        Collider2D[] colliders = Physics2D.OverlapBoxAll(offsetPosition, boxSize, 0, layer);

        foreach (Collider2D collider in colliders)
        {   
            if (collider.gameObject.CompareTag("Object") && collider.gameObject.name == "Gate")
            {            
               Debug.Log("Gate Near");
               Debug.Log(GetComponent<PlayerInventory>().Items["Key"]);
               return true;
            }
        }

        return false;
    }

    /// <summary>
    /// The OpenGate method is responsible for opening the gate, if the player has the key.
    /// If the player has the corect key, the gate is opened, otherwise the key is removed from the player's inventory.
    /// </summary>
    private void OpenGate()
    {
        if (GetComponent<PlayerInventory>().Items["Key"] > 0)
        {   
            Debug.Log("Open Gate");
            // Player has the right key
            if (GetComponent<PlayerInventory>().Items["Key"] == 2)
            {
                // Opens the gate and not removes the key from the player's inventory, to not grab any more keys
                gate.SetActive(false);
            }
            else
            {
                // Removes the key from the player's inventory
                GetComponent<PlayerInventory>().Items["Key"] = 0;
            }

            keyIcon.SetActive(false);
        }  
    }

    /// <summary>
    /// The GrabMeleeWeapon method is responsible for grabbing the melee weapon, updating the player's inventory, and removing the weapon from the level.
    /// If the player grabs the stick, PlayerAttack component is enable, because the stick is the player's first weapon.
    /// </summary>
    private void GrabMeleeWeapon(Utils.CollectableType meleeWeapon)
    {   
        if (meleeWeapon ==  Utils.CollectableType.Stick)
        {
            GetComponent<PlayerAttack>().enabled = true;

            playerArmed = true;

            stickIcon.SetActive(true);

            animator.SetBool("HasWeapon", true);

            GetComponent<PlayerInventory>().Weapons["Melee"] = "Stick";
        }
        else
        {
            GetComponent<PlayerInventory>().Weapons["Melee"] = "Sword";
        }

        DestroyCollectable();
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

        return (!keys.Values.Any(value => value));
    }

    /// <summary>
    /// The GrabKey method is responsible for grabbing the key.
    /// It adds a key to the player's inventory, removes the key from the level and spawns an horde of enemies.
    /// If the player has the right key, it will have the value 2 in the inventory, otherwise it will have the value 1.
    /// </summary>
    private void GrabKey()
    {   
        if (GetComponent<PlayerInventory>().Items["Key"] == 0)
        {   
            GetComponent<PlayerInventory>().Items["Key"] = RightKeyGrabbed() ? 2 : 1;

            keyIcon.SetActive(true);

            GameObject itemToDestroy = DestroyCollectable();

            // Removes the key from the dictionary which stores the keys and their values
            GameObject.Find("Level1").GetComponent<Level1Logic>().Keys.Remove(itemToDestroy);

            SpawnHordes(GetComponent<PlayerInventory>().Items["Key"] == 2);
        }   
    }

    /// <summary>
    /// The GrabHealItem method is responsible for grabbing the heal item, if the player has space in the inventory.
    /// If the player has space in the inventory, the player's inventory is updated, and the item is destroyed.
    /// </summary>
    private void GrabHealItem()
    {   
        PlayerInventory playerInventory = GetComponent<PlayerInventory>();

        if (playerInventory.Items["HealItems"] < PlayerInventory.MaxHealItems)
        {
            playerInventory.Items["HealItems"]++;

            DestroyCollectable();

            flaskIcon.SetActive(true);

            flaskQuantity.text = playerInventory.Items["HealItems"].ToString();

        }
    }

    /// <summary>
    /// The DestroyCollectable method is responsible for destroying the collectable item grabbed by the player.
    /// </summary>
    /// <returns>The object to destroy</returns>
    private GameObject DestroyCollectable()
    {
        GameObject collectableToDestroy = collectableItemGrabed;
        collectableItemGrabed = null;
        Destroy(collectableToDestroy);

        return collectableToDestroy;
    }

    /// <summary>
    /// The HealPlayer method is responsible for healing the player, if the player's health is less than its maximum health.
    /// </summary>
    private void HealPlayer()
    {   
        if (GetComponent<Entity>().Health < playerMaxHealth)
        {
            GetComponent<Entity>().Health++;
            GetComponent<Entity>().Health += (playerMaxHealth / 2);
            GetComponent<PlayerInventory>().Items["HealItems"]--;

            GameObject.Find("Level1").GetComponent<Rank>().HealItemsUsed++;

            if (GetComponent<PlayerInventory>().Items["HealItems"] == 0)
            {
                flaskIcon.SetActive(false);
            }

            flaskQuantity.text = GetComponent<PlayerInventory>().Items["HealItems"].ToString();

            healthBar.UpdateLabel(GetComponent<Entity>().Health);
        }
    }

    /// <summary>
    /// The SpawnHordes method is responsible for spawning hordes of enemies.
    /// It gets the SpawnHorde component and resets the number of enemies spawned and increases the horde size.
    /// By reseting the number of enemies spawned in a horde, we ensure that a new horde will be spawned.
    /// Because the horde stops spawning when the number of enemies reaches a certain value.
    /// </summary>
    private void SpawnHordes(bool hasRightKey){

        SpawnHorde spawnHord = GameObject.Find("SpawnHorde").GetComponent<SpawnHorde>();

        spawnHord.EnemiesSpawned = 0;

        spawnHord.HordeSize += Mathf.RoundToInt(spawnHord.HordeSize /= 2);

        if (hasRightKey){
            spawnHord.SpawnBoss();
        }
    }

    /// <summary>
    /// The GrabObject method is responsible for grabbing the object near the player.
    /// It removes the (Clone) string from the object's name, if it exists, to get the original object name.
    /// It calls the correct method to grab the object, depending on the object's name.
    /// </summary>
    public void GrabCollectable(GameObject collectableItem)
    {   
        collectableItemGrabed = collectableItem;

        Utils.CollectableType ColleccollectableType = collectableItem.GetComponent<Collectable>().type;

        switch (ColleccollectableType)
        {
            case Utils.CollectableType.Stick:
            case Utils.CollectableType.Sword:
                GrabMeleeWeapon(ColleccollectableType);

                return;

            case Utils.CollectableType.Key:
                Debug.Log("Grab Key");
                GrabKey();

                return;

            case Utils.CollectableType.HealItem:
                GrabHealItem();
                Debug.Log(GetComponent<PlayerInventory>().Items["HealItems"]);

                return;

            default:
                Debug.LogError("Invalid collectable type");
                return;
        }
    }
}
