using UnityEngine;


/// <summary>
/// The EnemyAttack class is responsible for handling the enemy's attack.
/// </summary>
public class EntityMeleeAttack : MonoBehaviour
{
    /// <summary>
    /// The meleeLeft, meleeRight, meleeUp and meleeDown properties are responsible for storing the enemy's attack areas.
    /// </summary>
    public GameObject meleeLeft, meleeRight, meleeUp, meleeDown;

    /// <summary>
    /// The attackArea property is responsible for storing the current enemy's attack area.
    /// </summary>
    private GameObject attackArea;

    /// <summary>
    /// The Attacking property gets or sets a value indicating whether the enemy is attacking.
    /// </summary>
    /// <value>
    ///   <c>true</c> if attacking; otherwise, <c>false</c>.
    /// </value>
    public bool attacking = false;

    /// <summary>
    /// The DeactivateAttack method is responsible for deactivating the enemy's attack areas.
    /// </summary>
    private void DeactivateAttackAreas()
    {
        meleeLeft.SetActive(false);
        meleeRight.SetActive(false);
        meleeUp.SetActive(false);
        meleeDown.SetActive(false);
    }


    /// <summary>
    /// The SetAttackArea method is responsible for setting the enemy's attack area based on the enemy's attack direction.
    /// The enemy's attack direction is the current direction the enemy is facing.
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
    /// If the enemy is attacking, the timer will increase, if the timer is greater than or equal to the timeToAttack, the enemy will stop attacking.
    /// </summary>
    private void HandleAttackCooldown()
    {
        if (attacking)
        {
            StartCoroutine(Utils.Wait(0.5f, () =>
            {
                attacking = false;

                DeactivateAttackAreas();
            }));
        }
    }

    /// <summary>
    /// The Attack method is responsible for activating the enemy's attack area and updating the enemy's attack status.
    /// It also desactivates the other attack areas.
    /// </summary>
    public void Attack(Vector2 attackDirection)
    {

        SetAttackArea(attackDirection);
        //GOTTA FIX THIS!!!!
        DeactivateAttackAreas();

        attackArea.SetActive(true);

        attacking = true;

        HandleAttackCooldown();
    }
}
