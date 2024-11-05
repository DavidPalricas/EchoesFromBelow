using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject meleeLeft, meleeRight, meleeUp, meleeDown;

    private  GameObject attackArea;

    public bool Attacking { get; set; }

    public Vector2 AttackDirection { get; set; }

    private float timer = 0f;


    private readonly float timeToAttack = 0.5f;

    private void Awake()
    {
        Attacking = false;
        AttackDirection = Vector2.zero;
    }

    // Update is called once per frame
    private void Update()
    {   
        if (AttackDirection != Vector2.zero && !Attacking)
        {          
            GetAttackDiretion();
            Attack();
        }

        HandleAttackCooldown();
    }


    private void GetAttackDiretion()
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
            }
        }
    }

    private void Attack()
    {
        //GOTTA FIX THIS!!!!
        DeactivateAttack();

        attackArea.SetActive(true);

        Debug.Log("Attack Area " + attackArea.name + ", is active : " + meleeRight.gameObject.activeSelf);

        Attacking = true;
    }

    private void DeactivateAttack()
    {
        meleeLeft.SetActive(false);
        meleeRight.SetActive(false);
        meleeUp.SetActive(false);
        meleeDown.SetActive(false);
    }

}
