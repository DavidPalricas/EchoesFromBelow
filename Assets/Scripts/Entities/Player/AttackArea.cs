using UnityEngine;

/// <summary>
/// The AttackArea class is responsible for handling the player's attack area.
/// </summary>  
public class AttackArea : MonoBehaviour
{
    //VAR que define a força do ataque
    private float meleeDamage;

    private bool isPlayer;

    private void Awake()
    {
        meleeDamage = GetComponentInParent<Entity>().AttackDamage;

        isPlayer =  transform.parent.CompareTag("Player");

    }

    //Verifica a colisão
    private void OnTriggerEnter2D (Collider2D collider){
        if (collider.gameObject.CompareTag("Enemy") && isPlayer || collider.gameObject.CompareTag("Player") && !isPlayer)
        {
            Debug.Log("Colidiu com " + collider.gameObject.name);
            //Acessa o script do objeto com que colidiu e retira vida ao objeto
            collider.GetComponent<Entity>().Health -= (int) meleeDamage;

        }
    }
}
