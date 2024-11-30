using UnityEngine;

/// <summary>
/// The PlayerAttack class is responsible for handling the player's attack.
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    /// <summary>
    /// The attackArea variable is responsible for storing the attack area to use.
    /// </summary>
    private GameObject attackArea;

    /// <summary>
    /// The meleeLeft variable is responsible for storing the left attack area.
    /// </summary>
    public GameObject meleeLeft;

    /// <summary>
    /// The meleeRight variable is responsible for storing the right attack area.
    /// </summary>
    public GameObject meleeRight;

    /// <summary>
    /// The meleeUpp variable is responsible for storing the up attack area.
    /// </summary>
    public GameObject meleeUp;

    /// <summary>
    /// The meleeDown variable is responsible for storing the down attack area.
    /// </summary>
    public GameObject meleeDown;

    /// <summary>
    /// The attacking variable is responsible for storing whether the player is attacking.
    /// </summary>
    private bool attacking = false;

    /// <summary>
    /// The timeToAttack variable is responsible for storing the atack's time
    /// </summary>
    private readonly float timeToAttack = 0.25f;

    /// <summary>
    /// The timer variable is responsible for storing the time.
    /// The timer is used to check if the player is attacking.
    /// </summary>
    private float timer = 0f;

    /// <summary>
    /// The Update method is called every frame (Unity Method).
    /// In this method, the player's attack logic is handled.
    /// By calling methods to get the attack direction, handle the attack cooldown and attack if the player pressed the key to attack.
    /// </summary>
    /// 

    /// <summary>
    /// Guarda a informação do animator do jogador
    /// </summary>
    [SerializeField]
    private Animator animator;

    private void Awake()
    {   
        DeactivateAttack();
    }
    private void Update()
    {
        GetAttackDirection();

        if (Input.GetKeyDown(KeyCode.Space)){
            Attack();
        }

        HandleAttackCooldown();
    }


    /// <summary>
    /// The GetAttackDirection method is responsible for getting the attack direction.
    /// </summary>
    private void GetAttackDirection()
    {
        //Verifica para que lado está virado o jogador de modo a que ataque virado para esse lado
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {

            attackArea = meleeUp;

        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {

            attackArea = meleeDown;

        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {

            attackArea = meleeLeft;

        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {

            attackArea = meleeRight;

        }
    }

    /// <summary>
    /// The HandleAttackCooldown method is responsible for handling the attack cooldown.
    /// If the player is attacking, the timer is incremented, and if the timer is greater than the maximum attack time, the player stops attacking.
    /// </summary>
    private void HandleAttackCooldown()
    {
        if (attacking)
        {
            timer += Time.deltaTime;

            if (timer >= timeToAttack)
            {
                //Resets the timer
                timer = 0;

                attacking = false;

                // Desactivate the attack area
                attackArea.SetActive(false);

                //Diz ao animator que já não está a atacar
                animator.SetBool("IsAttacking", false);
            }
        }
    }

    /// <summary>
    /// The Attack method is responsible for handling the player's attack.
    /// It calls the DeactivateAttack method to deactivate the previous attack areas,activates the new attack area  ands updates the player's attacking status.
    /// </summary>
    private void Attack()
    {
        //GOTTA FIX THIS!!!!
        DeactivateAttack();

        attackArea.SetActive(true);

        attacking = true;

        //Diz ao animator que está a atacar
        animator.SetBool("IsAttacking", true);

    }

    /// <summary>
    /// The DeactivateAttack method is responsible for deactivating the previous attack areas.
    /// This method needs to be fixed.
    /// </summary>
    private void DeactivateAttack()
    {
        meleeLeft.SetActive(false);
        meleeRight.SetActive(false);
        meleeUp.SetActive(false);
        meleeDown.SetActive(false);
    }

}
