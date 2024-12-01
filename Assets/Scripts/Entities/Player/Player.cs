

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// The Player class is responsible for handling the player's attributes.
/// </summary>
public class Player : Entity
{
    /// <summary>
    /// The spawnPoint property is responsible for storing the player's spawn point.
    /// This property is hidden in the inspector, so it can't be modified in the Unity Editor.
    /// </summary>
    [HideInInspector]
    public Vector2 spawnPoint;

    /// <summary>
    /// The movement property is responsible for storing the player's PlayerMovement component.
    /// </summary>
    private PlayerMovement movement;

    /// <summary>
    /// The attack property is responsible for storing the player's PlayerAttack component.
    /// </summary>
    private PlayerAttack attack;

    /// <summary>
    /// The player actions property is responsible for storing the player's PlayerActions component.
    /// </summary>
    private PlayerActions playerActions;

    /// <summary>
    /// The Awake method is called when the script instance is being loaded (Unity Method).
    /// </summary>
    private void Awake()
    {
        spawnPoint = new Vector2(transform.position.x, transform.position.y);
        movement = GetComponent<PlayerMovement>();
        attack = GetComponent<PlayerAttack>();
        playerActions = GetComponent<PlayerActions>();
    }

    /// <summary>
    /// Overrides the EntityDeath method from the parent class (Entity) to add custom behavior.
    /// The base method is called to ensure the standard death behavior is preserved.
    /// When the player dies, it will respawn at the spawn point after the death animation ends.
    /// </summary>
    protected override void EntityDeath()
    {
        base.EntityDeath();

        StartCoroutine(Utils.WaitForAnimationEnd(animator, "Death", Respawn));
    }

    /// <summary>
    /// The Respawn method is responsible for respawning the player at the spawn point and resetting its health.
    /// It waits for 2 seconds before respawning the player.
    /// </summary>
    private void Respawn()
    { 
        DesactivateScripts();

        StartCoroutine(Utils.Wait(1.5f, () =>
        {
            Health = 100;
            transform.position = spawnPoint;
            animator.SetBool("IsDead", false);
            GetComponent<Entity>().isDead = false;

            movement.enabled = true;
            playerActions.enabled = true;

            Dictionary<string, string> playerWeapons = GetComponent<PlayerInventory>().Weapons;

            if (playerWeapons.Values.Any(weapon => weapon != null))
            {
                attack.enabled = true;
            }
        }));
    }

    /// <summary>
    /// The DesactivateScripts method is responsible for deactivating the player's scripts when it dies.
    /// The scripts that are deactivated are the PlayerAttack, PlayerMovement, and PlayerActions.
    /// </summary>
    private void DesactivateScripts()
    {
        attack.enabled = false;
        movement.enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        playerActions.enabled = false;
    }
}
