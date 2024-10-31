using UnityEngine;

/// <summary>
/// The AttackArea class is responsible for handling the player's attack area.
/// </summary>  
public class AttackArea : MonoBehaviour
{

    //VAR que define a força do ataque
    public float meleeDamage = 3f;

    //Verifica a colisão
    public void OnTriggerEnter2D (Collider2D collider){

        //Se a colisão é feita com um inimigo, definido pelas TAGS
        if(collider.gameObject.CompareTag("Enemy")){

            //Acessa o script do objeto com que colidiu e retira vida ao objeto
            collider.GetComponent<Entity>().Health -= (int) meleeDamage;

        }
    }
}
