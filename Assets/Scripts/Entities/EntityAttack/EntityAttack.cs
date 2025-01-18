using UnityEngine;

/// <summary>
/// The EntityAttack class is responsible for creating the base methods and properties for the enemy's attack.
/// The enemy's attack is divided into two types: melee and ranged, which are represented by another classes that inherits from this class.
/// </summary>
public class EntityAttack : MonoBehaviour
{
    /// <summary>
    /// The Attacking property gets or sets a value indicating whether the enemy is attacking.
    /// </summary>
    /// <value>
    ///   <c>true</c> if attacking; otherwise, <c>false</c>.
    /// </value>
    [HideInInspector]
    public bool attacking = false;

    /// <summary>
    /// The HandleAttackCooldown method is responsible for handling the enemy's attack cooldown.
    /// It is a virtual method, so it can be overridden by the classes that inherit from this class.
    /// <param name="attackCoolDown">The attack cooldown of entity.</param>"
    /// </summary>
    protected virtual void HandleAttackCooldown(float attackCoolDown) { }

    /// <summary>
    /// The Attack method is responsible for handling the enemy's attack.
    /// It is a virtual method, so it can be overridden by the classes that inherit from this class.
    /// </summary>
    /// <param name="attackDirection">The vector's attack direction of entity.</param>
    /// <param name="attackCoolDown">The attack cooldown of entity.</param>
    public virtual void Attack(Vector2 attackDirection, float attackCoolDown) { }

}
