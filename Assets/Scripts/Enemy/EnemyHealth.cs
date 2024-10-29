using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    //VAR que define a quantidade de vida do inimigo
    public float health = 9f;










    // Start is called before the first frame update
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {
        
        //Se a health do inimigo acabar ou for inferior a 0 o inimigo desaparece
        if(health <= 0f){

            this.gameObject.SetActive(false);

        }

    }

}
