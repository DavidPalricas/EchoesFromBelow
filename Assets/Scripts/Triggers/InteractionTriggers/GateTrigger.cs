using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The GateTrigger class is responsible for setting the gate trigger to open it.
/// </summary>
public class GateTrigger : BaseInteractionTrigger
{
    /// <summary>
    /// The gate property is responsible for storing the gate game object.
    ///  It is serialized to be set in the Unity Editor.
    /// </summary>
    [SerializeField]
    private GameObject gate, player,keyIcon;

    /// <summary>
    /// The playerRank property is responsible for storing the player rank.
    /// It is serialized to be set in the Unity Editor.
    /// </summary>
    [SerializeField]
    private Rank playerRank;

    /// <summary>
    /// The speechTrigger property is responsible for storing the speech trigger.
    /// </summary>
    [SerializeField]
    private SpeechTrigger speechTrigger;

    /// <summary>
    /// The gateKey property is responsible for storing the value of the key that the player has.
    /// 0 - No key, 1 - False key, 2 - Correct key to open the gate
    /// </summary>
    private int gateKey;

    /// <summary>
    /// The attempts property is responsible for storing the number of attempts to open the gate.
    /// </summary>
    private int attempts = 0;

    /// <summary>
    /// The Update method is called every frame (Unity Method).
    /// In this method, we are checking if the player is detected and if the player interact input was triggred and if the conditions to open the gate are met.
    /// If these conditions are met, the OpenGate method is called to open the gate.
    /// </summary>
    private void Update()
    {
        if (playerDetected && InteractInputTriggered() && CheckConditionsToOpenGate())
        {
            OpenGate();
        }
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

    private bool CheckConditionsToOpenGate()
    {
        Vector2 lastMovingDirection = player.GetComponent<EntityFSM>().entityProprieties.lastMovingDirection;
        Vector2 playerVelocity = player.GetComponent<Rigidbody2D>().velocity;

        Vector2 playerDirection = Utils.GetUnitaryVector(playerVelocity);

        return (playerDirection == Vector2.down || lastMovingDirection == Vector2.down) && PlayerHasKey();
    }

    /// <summary>
    /// The PlayerHasKey method is responsible for checking if the player has the key to open the gate.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if the player has key; otherwise, <c>false</c>.
    /// </returns>
    private bool PlayerHasKey()
    {
        Dictionary<string, int> playerItems = player.GetComponent<PlayerInventory>().Items;

        gateKey = playerItems["Key"];

        return gateKey > 0;
    }

    /// <summary>
    /// The OpenGate method is responsible for opening the gate, if the player has the key.
    /// If the player has the corect key, the gate is opened, otherwise the key is removed from the player's inventory.
    /// </summary>
    private void OpenGate()
    {
        if (gateKey == 2)
        {   
            if (!playerRank.BossKilled)
            {
                speechTrigger.ChangeSpeech("bossNotKilledSpeech");
                return;
            }

            gate.SetActive(false);
        }
        else
        {
            gateKey = 0;
            player.GetComponent<PlayerInventory>().Items["Key"] = gateKey;

            string[] attemptSpeech = { "firstWrongKeySpeech", "secondWrongKeySpeech", "thirdWrongKeySpeech" };
            speechTrigger.ChangeSpeech(attemptSpeech[attempts]);

            attempts++;
        }

        keyIcon.SetActive(false);
    }
}
