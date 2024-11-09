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
    private bool isPlayer;

    /// <summary>
    /// The Awake method is called when the script instance is being loaded (Unity Method).
    /// In this method, the meleeDamage and isPlayer variables are initialized.
    /// </summary>
    private void Awake()
    {
        meleeDamage = GetComponentInParent<Entity>().AttackDamage;

        isPlayer =  transform.parent.CompareTag("Player");
    }

    /// <summary>
    /// The OnTriggerEnter2D method is called when the Collider2D collider enters the trigger (Unity Method).
    /// In this method, we check if a player's attack area collides with an enemy or if an enemy's attack area collides with a player.
    /// If this conditions are met, the entity which collided with the attack area will lose health.
    /// </summary>
    /// <param name="collider">The collider or RigidBody2D of a game object.</param>
    private void OnTriggerEnter2D (Collider2D collider){
        if (collider.gameObject.CompareTag("Enemy") && isPlayer || collider.gameObject.CompareTag("Player") && !isPlayer)
        {
            collider.GetComponent<Entity>().Health -= (int) meleeDamage;
        }
    }
}
