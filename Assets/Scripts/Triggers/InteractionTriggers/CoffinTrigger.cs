using UnityEngine;

/// <summary>
/// The CoffinTrigger class is responsible for triggering the coffin interaction.
/// </summary>
public class CoffinTrigger : BaseInteractionTrigger
{
    /// <summary>
    /// The hasHorse property is responsible for storing the horse status.
    /// </summary>
    [HideInInspector]
    public bool hasKey = false;

    /// <summary>
    /// The keyPrefab property is responsible for storing the key prefab.
    /// </summary>
    [SerializeField]
    private GameObject keyPrefab;

    /// <summary>
    /// The level2Logic property is responsible for storing the Level2Logic component.
    /// </summary>
    [SerializeField]
    private Level2Logic level2Logic;

    /// <summary>
    /// The Update method is called every frame (Unity Method).
    /// In this method, we are checking if the player is detected, the interact input is triggered, the graveyard enemies are dead and the player has the key
    /// These are the conditions to open the coffin.
    /// </summary>
    private void Update()
    {
        if (playerDetected && InteractInputTriggered() && level2Logic.GraveYardEnemiesDead() && hasKey)
        {
            OpenCoffin();
        }
    }

    /// <summary>
    /// The OpenCoffin method is responsible for opening the coffin and spawning the key.
    /// </summary>
    private void OpenCoffin()
    {   
       var keySpawn = new Vector3(transform.position.x, transform.position.y - 2, transform.position.z);

       Instantiate(keyPrefab,keySpawn, Quaternion.identity);

        Destroy(gameObject);
    }
}

