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
    /// In this method, the EntityDeath method is called, when the entity is dead.
    /// </summary>
    private void Update()
    {
        if (Health <= 0)
        {
           EntityDeath();
        }   
    }

    /// <summary>
    /// The EntityDeath method is responsible for handling the entity's death.
    /// </summary>
    protected virtual void EntityDeath()
    {
        gameObject.SetActive(false);          
    }
}
