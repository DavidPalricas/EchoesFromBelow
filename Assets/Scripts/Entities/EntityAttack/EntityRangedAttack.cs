using UnityEngine;

/// <summary>
/// The EntityRangedAttack class is responsible for handling the enemy's ranged attack.
/// </summary>
public class EntityRangedAttack : EntityAttack
{
    /// <summary>
    /// The projetile property is responsible for storing the projetile prefab.
    /// </summary>
    [SerializeField]
    private GameObject projetile;

    /// <summary>
    /// The HandleAttackCooldown method is responsible for handling the enemy's attack cooldown.
    /// If the entity is attacking, a coroutine is started to wait 1.5 seconds and then the entity stops attacking.
    /// It overrides the HandleAttackCooldown method from the base class (EntityAttack).
    /// <param name="attackCoolDown">The attack cooldown of entity.</param>""
    /// </summary>
    protected override void HandleAttackCooldown(float attackCoolDown)
    {
        if (attacking)
        {
            StartCoroutine(Utils.Wait(attackCoolDown, () =>
            {
                attacking = false;
            }));
        }
    }

    /// <summary>
    /// The Attack method is responsible for handling the enemy's attack.
    ///  It overrides the Attack method from the base class (EntityAttack).
    ///  In this method, the entity's projetile is instantiated and its movement is set.
    /// </summary>
    /// <param name="attackDirection">The vector's attack direction of entity.</param>
    /// <param name="attackCoolDown">The attack cooldown of entity.</param>
    public override void Attack(Vector2 attackDirection, float attackCoolDown)
    {
        if (!attacking)
        {
            attacking = true;

            Rigidbody2D entityRigidBody2D = GetComponent<Rigidbody2D>();

            Vector2 projetilePosition = entityRigidBody2D.position + attackDirection;

            GameObject newProjetile = Instantiate(projetile, projetilePosition, Quaternion.identity);

            ProjetileMovement newProjetileMovement = newProjetile.GetComponent<ProjetileMovement>();

            newProjetileMovement.attackDirection = attackDirection;
            newProjetileMovement.playerThrown = GetComponent<EntityFSM>().entityProprieties is Player;

            HandleAttackCooldown(attackCoolDown);
        }
    }
}

