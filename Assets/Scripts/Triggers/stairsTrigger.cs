using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stairsTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<EntityFSM>().entityProprieties.speed = 3;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<EntityFSM>().entityProprieties.speed = 10;
    }
}
