using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {   
        Rigidbody2D enemy = GameObject.Find("Enemy").GetComponent<Rigidbody2D>();

         Vector2 originPosition = (enemy.position.x >= 0) ?  new Vector2(enemy.position.x - enemy.position.x / 2, enemy.position.y) : new Vector2(enemy.position.x + enemy.position.x / 2, enemy.position.y);
         new GameGrid(20,8,2.5f, originPosition);
    }
    

   
}
