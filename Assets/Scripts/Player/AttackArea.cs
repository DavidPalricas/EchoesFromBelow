using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackArea : MonoBehaviour
{

    //VAR que define a força do ataque
    public float meleeDamage = 3f;

    //Verifica a colisão
    public void OnTriggerEnter2D (Collider2D collider){

        //Se a colisão é feita com um inimigo, definido pelas TAGS
        if(collider.gameObject.tag == "Enemy"){

            //Acessa o script do objeto com que colidiu e retira vida ao objeto
            collider.GetComponent<EnemyHealth>().health -= meleeDamage;

        }

    }

}
