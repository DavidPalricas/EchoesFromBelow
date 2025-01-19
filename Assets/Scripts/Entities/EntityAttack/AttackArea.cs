using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// The AttackArea class is responsible for handling the entities's attack area.
/// </summary>  
public class AttackArea : MonoBehaviour
{
    /// <summary>
    /// The meleeDamage property is responsible for storing the entity's attack damage in close range.
    /// </summary>
    private float meleeDamage;

    /// <summary>
    /// The isPlayer property is responsible for storing whether the entity is a player or not.
    /// </summary>
    private bool attackerIsPlayer;

    /// <summary>
    /// Variable to hold the Sprite Renderer of the attacked entity.
    /// </summary>
    private SpriteRenderer targetSpriteRenderer;
    private void Awake()
    {
        meleeDamage = GetComponentInParent<Entity>().attackDamage;

        attackerIsPlayer =  transform.parent.CompareTag("Player");
    }

    /// <summary>
    /// The OnTriggerEnter2D method is called when the Collider2D collider enters the trigger (Unity Method).
    /// In this method, we check if a player's attack area collides with an enemy or if an enemy's attack area collides with a player.
    /// If this conditions are met, the entity which collided with the attack area will lose health.
    /// </summary>
    /// <param name="collider">The collider or RigidBody2D of a game object.</param>
    private void OnTriggerEnter2D (Collider2D collider){
        // Player attacked an enemy
        if (collider.gameObject.CompareTag("Enemy") && attackerIsPlayer)
        {
            collider.GetComponent<Enemy>().entityFSM.entitycurrentHealth -= (int)meleeDamage;

            targetSpriteRenderer = collider.GetComponent<SpriteRenderer>();
            targetSpriteRenderer.color = new Color32(207, 115, 115, 255);

            // Start the coroutine to reset the color
            StartCoroutine(ResetColorAfterHit(targetSpriteRenderer, 0.3f));
        }
        // Enemy attacked the player
        else if (collider.gameObject.CompareTag("Player") && !attackerIsPlayer) //Enemy attacked the player
        {
            Player player = collider.GetComponent<Player>();

            player.entityFSM.entitycurrentHealth -= (int)meleeDamage;
            player.healthBar.UpdateLabel(player.entityFSM.entitycurrentHealth);
  
            targetSpriteRenderer = collider.GetComponent<SpriteRenderer>();
            targetSpriteRenderer.color = new Color32(207, 115, 115, 255);

            // Start the coroutine to reset the color
            StartCoroutine(ResetColorAfterHit(targetSpriteRenderer, 0.3f));
        }
    }

    /// <summary>
    /// The ResetColorAfterHit resets the entity sprite's color to white after being hit.
    /// </summary>
    /// <param name="spriteRenderer">The SpriteRenderer component whose color will be reset.</param>
    /// <param name="delay">The time in seconds to wait before resetting the color.</param>
    /// <returns>
    /// An IEnumerator, allowing this method to be used in a coroutine for delayed execution.
    /// </returns>
    private IEnumerator ResetColorAfterHit(SpriteRenderer spriteRenderer, float delay)
    {
        yield return new WaitForSeconds(delay);

        // To avoid bugs, we check if the spriteRenderer is destroyed before changing its color
        if (!spriteRenderer.IsDestroyed())
        {
            spriteRenderer.color = Color.white;
        } 
    }
}
