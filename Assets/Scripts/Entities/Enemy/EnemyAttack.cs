using UnityEngine;


/// <summary>
/// The EnemyAttack class is responsible for handling the enemy's attack.
/// </summary>
public class EnemyAttack : MonoBehaviour
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
    public bool Attacking { get; set; }

    /// <summary>
    /// The AttackDirection property gets or sets the enemy's attack direction.
    /// </summary>
    /// <value>
    /// The attack direction of the enemy.
    /// </value>
    public Vector2 AttackDirection { get; set; }

    /// <summary>
    /// The timer property is responsible for storing the time the enemy has been attacking.
    /// </summary>
    private float timer = 0f;

    /// <summary>
    /// The timetoAttack property is responsible for storing the time the enemy will take to attack.
    /// </summary>
    private readonly float timeToAttack = 0.5f;

    /// <summary>
    /// Stores the animator component.
    /// </summary>
    private Animator animator;

    private void Awake()
    {
        Attacking = false;
        AttackDirection = Vector2.zero;
        animator = GetComponent<Entity>().animator;
    }

    /// <summary>
    /// The Update method is called every frame (Unity Method).
    /// In this method, we are check if the enemy is ready to attack,if it is the SetAttackArea() and  Attack() methods are called.
    /// It also calls the HandleAttackCooldown() method, to handle the enemy's attack cooldown.
    /// </summary>
    private void Update()
    {   
        if (AttackDirection != Vector2.zero && !Attacking)
        {          
            SetAttackArea();
            Attack();
        }

        HandleAttackCooldown();
    }

    /// <summary>
    /// The SetAttackArea method is responsible for setting the enemy's attack area based on the enemy's attack direction.
    /// The enemy's attack direction is the current direction the enemy is facing.
    /// </summary>
    private void SetAttackArea()
    {   
        if (AttackDirection == Vector2.left)
        {
            attackArea = meleeLeft;
        }
        else if (AttackDirection == Vector2.right)
        {   
            attackArea = meleeRight;
        }
        else if (AttackDirection == Vector2.up)
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
        if (Attacking)
        {   
            timer += Time.deltaTime;

            if (timer >= timeToAttack)
            {
                //Resets the timer
                timer = 0;

                Attacking = false;

                AttackDirection = Vector2.zero;

                // Desactivate the attack area
                attackArea.SetActive(false);

                animator.SetBool("isAttacking", false);
            }
        }
    }

    /// <summary>
    /// The Attack method is responsible for activating the enemy's attack area and updating the enemy's attack status.
    /// It also desactivates the other attack areas.
    /// </summary>
    private void Attack()
    {
        //GOTTA FIX THIS!!!!
        DeactivateAttackAreas();

        attackArea.SetActive(true);

        Attacking = true;

        animator.SetBool("isAttacking", true);
    }

    /// <summary>
    /// The DeactivateAttack method is responsible for deactivating the enemy's attack areas.
    /// </summary>
    public void DeactivateAttackAreas()
    {
        meleeLeft.SetActive(false);
        meleeRight.SetActive(false);
        meleeUp.SetActive(false);
        meleeDown.SetActive(false);
    }
}
