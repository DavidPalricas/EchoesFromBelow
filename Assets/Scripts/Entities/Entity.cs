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
    /// Stores the animator.
    /// </summary>
    private Animator animator;
    private bool isDead = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

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
        if (isDead) return;

        isDead = true;
        animator.SetBool("IsDead", true);

        if (TryGetComponent<PlayerMovement>(out var movement))
        {

            movement.enabled = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        }

        if (TryGetComponent<PlayerAttack>(out var attack))
        {

            attack.enabled = false;

        }

        if (gameObject.CompareTag("Enemy")){

            animator.SetBool("isDead", true);

        }

        if (TryGetComponent<EnemyMovement>(out var enemyMovement))
        {

            enemyMovement.enabled = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        }

        if (TryGetComponent<EnemyAttack>(out var enemyAttack))
        {

            enemyAttack.DeactivateAttackAreas();
            enemyAttack.enabled = false;

        }

        Debug.Log("Player has died.");
    }
}
