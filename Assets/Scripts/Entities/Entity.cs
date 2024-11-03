using UnityEngine;

/// <summary>
/// The Entity class is responsible for handling the game's entities attributes.
/// </summary>
public class Entity : MonoBehaviour
{
    /// <summary>
    /// The Health property  is responsible for storing the entity's health.
    /// </summary>
    public int Health;

    /// <summary>
    /// The Speed property  is responsible for storing the entity's speed.
    /// </summary>
    public float Speed;

    /// <summary>
    /// The AttackDamage property  is responsible for storing the entity's attack damage.
    /// </summary>
    public float AttackDamage;

    /// <summary>
    /// The Update method is called every frame (Unity Method).
    /// In this method, the EntityDeath method is called.
    /// </summary>
    private void Update()
    {
        EntityDeath();          
    }

    /// <summary>
    /// The EntityDeath method is responsible for handling the entity's death.
    /// If the entity's health is less than or equal to 0(the entity is dead), the entity is disabled.
    /// </summary>
    private void EntityDeath()
    {
        if (Health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
