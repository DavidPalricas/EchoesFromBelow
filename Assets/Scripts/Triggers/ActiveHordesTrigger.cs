
using UnityEngine;

public class ActiveHordesTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private SpawnHorde horde;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            horde.enabled = true;
            Destroy(gameObject);
        }
    }
}
