using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    //VAR que define qual a área de ataque a utilizar
    public GameObject attackArea;
    //VARS que definem para que lado ataca
    public GameObject melee_left;
    public GameObject melee_right;
    public GameObject melee_up;
    public GameObject melee_down;
    //VAR que verifica se o jogador está a atacar
    private bool attacking = false;
    //VAR que limita o tempo de ataque
    private float timeToAttack = 0.25f;
    //VAR de timer para o ataque
    private float timer = 0f;










    // Start is called before the first frame update
    void Start()
    {
        
        

    }

    // Update is called once per frame
    void Update()
    {

        //Verifica para que lado está virado o jogador de modo a que ataque virado para esse lado
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)){

            attackArea = melee_up;

        } else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)){

            attackArea = melee_down;

        } else if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){

            attackArea = melee_left;

        } else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)){

            attackArea = melee_right;

        }

        //Verifica se a tecla de ataque foi pressionada
        if(Input.GetKeyDown(KeyCode.Space)){

            //Chama a função de desativar as zonas de ataque anteriores                         GOTTA FIX THIS!!!!
            DeactivateAttack();
            //Chama a função de ataque
            Attack();

        }

        //Verificação do ataque e tempo de ataque
        if(attacking){

            //Faz com que o timer para ver se o jogador está a atacar comece
            timer += Time.deltaTime;
            //Se o timer estiver maior que o limite máximo de tempo para atacar
            if(timer >= timeToAttack){

                //Reset no timer
                timer = 0;
                //Define que não está a atacar
                attacking = false;
                //Desativa a zona de ataque
                attackArea.SetActive(false);

            }

        }
        
    }










    //Função de ataque
    public void Attack(){

        //Ativa o GameObject que verifica a colisão, e assim o ataque
        attackArea.SetActive(true);
        //Jogador atacou
        attacking = true;

    }

    //Desativa as zonas de ataque anteriores                                          GOTTA FIX THIS!!!
    public void DeactivateAttack(){

        melee_left.SetActive(false);
        melee_right.SetActive(false);
        melee_up.SetActive(false);
        melee_down.SetActive(false);

    }

}
