using UnityEngine;

/// <summary>
/// The EntityMeleeAttack class is responsible for handling the enemy's melee attack.
/// </summary>
public class EntityMeleeAttack : EntityAttack
{
    /// <summary>
    /// The meleeLeft, meleeRight, meleeUp and meleeDown properties are responsible for storing the entity's attack areas.
    /// </summary>
    [SerializeField]
    private GameObject meleeLeft, meleeRight, meleeUp, meleeDown;

    /// <summary>
    /// The attackArea property is responsible for storing the current entity's attack area.
    /// </summary>
    private GameObject attackArea;

    /// <summary>
    /// The DeactivateAttack method is responsible for deactivating the entity's attack areas.
    /// </summary>
    private void DeactivateAttackAreas()
    {
        meleeLeft.SetActive(false);
        meleeRight.SetActive(false);
        meleeUp.SetActive(false);
        meleeDown.SetActive(false);
    }

    /// <summary>
    /// The SetAttackArea method is responsible for setting the entity's attack area based on the entity's attack direction.
    /// The entity's attack direction is the current direction the enemy is facing.
    /// </summary>
    private void SetAttackArea(Vector2 attackDirection)
    {
        if (attackDirection == Vector2.left)
        {
            attackArea = meleeLeft;
        }
        else if (attackDirection == Vector2.right)
        {
            attackArea = meleeRight;
        }
        else if (attackDirection == Vector2.up)
        {
            attackArea = meleeUp;
        }
        else
        {
            attackArea = meleeDown;
        }
    }

    /// <summary>
    /// The HandleAttackCooldown method is responsible for handling the enemy's attack cooldown.
    /// It overrides the HandleAttackCooldown method from the base class (EntityAttack).
    /// If the entity is attacking, a coroutine is started to wait 1.5 seconds and then the entity stops attacking.
    /// After that, the entity's attack areas are deactivated.
    /// </summary>
    protected override void HandleAttackCooldown()
    {
        if (attacking)
        {
            // !!!!!!!!!!!!!!
            // 0.5f PARA O PLAYER, 1f PARA OS ESQUELETOS
            // !!!!!!!!!!!!!!
            StartCoroutine(Utils.Wait(0.5f, () =>
            {
                attacking = false;

                DeactivateAttackAreas();
            }));
        }
    }

    /// <summary>
    /// The Attack method is responsible for activating the entity's attack area and updating the entity's attack status.
    /// It overrides the Attack method from the base class (EntityAttack).
    /// It also desactivates the other attack areas.
    /// </summary>
    public override void Attack(Vector2 attackDirection)
    {
        SetAttackArea(attackDirection);

        //GOTTA FIX THIS!!!!
        DeactivateAttackAreas();

        attackArea.SetActive(true);

        attacking = true;

        HandleAttackCooldown();
    }
}
