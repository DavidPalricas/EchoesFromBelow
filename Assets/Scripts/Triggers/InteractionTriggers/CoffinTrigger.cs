using Microsoft.Unity.VisualStudio.Editor;
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

    [SerializeField]
    private GameObject coffinObject;
    
    [SerializeField]
    private bool originalCoffin = true;

    [SerializeField]
    private Sprite coffin01, coffin02;


    private void OnEnable()
    {
        if (playerActions == null)
        {
            playerActions = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActions>();
        }
    }

    /// <summary>
    /// The Update method is called every frame (Unity Method).
    /// In this method, we are checking if the player is detected, the interact input is triggered, the graveyard enemies are dead and the player has the key
    /// These are the conditions to open the coffin.
    /// </summary>
    private void Update()
    {
        if (playerDetected && InteractInputTriggered() && level2Logic.GraveYardEnemiesDead())
        {   
            Utils.PlaySoundEffect("coffin");

            if (originalCoffin)
            {
                coffinObject.GetComponent<SpriteRenderer>().sprite = coffin01;
            } else
            {
                coffinObject.GetComponent<SpriteRenderer>().sprite = coffin02;
            }

            if (hasKey)
            {
                OpenRightCoffin();
            }
        }
    }

    /// <summary>
    /// The OpenRightCoffin method is responsible for opening the coffin and spawning the key.
    /// </summary>
    private void OpenRightCoffin()
    {   
       var keySpawn = new Vector3(transform.position.x, transform.position.y - 4.5f, transform.position.z);

       Instantiate(keyPrefab,keySpawn, Quaternion.identity);

        Destroy(gameObject);
    }
}

