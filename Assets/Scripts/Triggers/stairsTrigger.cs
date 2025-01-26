using UnityEngine;

public class stairsTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {    

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().speed = 4;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {    
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().speed = 10;
        }
    }
}
