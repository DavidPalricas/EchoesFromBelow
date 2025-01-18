using UnityEngine;

/// <summary>
/// The Entity class is responsible for handling the game's entities attributes.
/// </summary>
public abstract class Entity : MonoBehaviour
{
    /// <summary>
    /// The maxHealth property  is responsible for storing the entity's max health.
    /// </summary>
    public int maxHealth;

    /// <summary>
    /// The speed, attackDamage, and attackCooldown properties are responsible for storing the entity's speed, attack damage, and attack cooldown.
    /// </summary>
    public float speed, attackDamage, attackCooldown;

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
    /// The entityRigidBody property is responsible for storing the entity's Rigidbody2D component.
    /// </summary>
    [HideInInspector]
    public Rigidbody2D entityRigidBody;

    /// <summary>
    /// The attack property is responsible for storing the entity's EntityMeleeAttack component.
    /// </summary>
    [HideInInspector]
    public EntityAttack attack;

    /// <summary>
    /// The lastMovingDirection property is responsible for storing the entity's last moving direction.
    /// It is initializer with Vector2.down to make the entity face down when the game starts.
    /// </summary>
    [HideInInspector]
    public Vector2 lastMovingDirection = Vector2.down;

}
