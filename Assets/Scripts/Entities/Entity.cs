using UnityEngine;

/// <summary>
/// The Entity class is responsible for handling the game's entities attributes.
/// </summary>
public abstract class Entity : MonoBehaviour
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
    /// Stores the animator.
    /// </summary>
    [HideInInspector]
    public Animator animator;

    /// <summary>
    /// The entityFSM property is responsible for storing the entity's finite state machine.
    /// </summary>
    public EntityFSM entityFSM;

    /// <summary>
    /// The isDead property is responsible for storing whether the entity is dead or not.
    /// </summary>
    [HideInInspector]
    public bool isDead = false;

    [HideInInspector]
    public Rigidbody2D entity;

   
    /// <summary>
    /// The Update method is called every frame (Unity Method).
    /// In this method, the EntityDeath method is called, when the entity is dead.
    /// </summary>
    private void Update()
    {
        if (!isDead && Health <= 0)
        {
           EntityDeath();
        }
    }

    /// <summary>
    /// The EntityDeath method is responsible for handling the entity's death.
    /// </summary>
    protected virtual void EntityDeath()
    {
        isDead = true;
        // THIS IF STATEMENT IS USESSELESS the bool "isDead" in the enemy animator must be called "IsDead" with a capital "I"
        if (gameObject.CompareTag("Player"))
        {
            animator.SetBool("IsDead", true);
            Debug.Log("Player has died.");
        }
        else
        {
            entityFSM.ChangeState(new EntityDeadState(entityFSM));
        }   
    }
}
