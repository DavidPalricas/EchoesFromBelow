

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
    /// The Awake method is called when the script instance is being loaded (Unity Method).
    /// </summary>
    private void Awake()
    {
        spawnPoint = new Vector2(transform.position.x, transform.position.y);
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
    /// </summary>
    private void Respawn()
    {
        Health = 100;
        transform.position = spawnPoint;
        animator.SetBool("IsDead", false);
        GetComponent<Entity>().isDead = false;
    }
}
