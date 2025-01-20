using Unity.VisualScripting;
using UnityEngine;

public class EnemyCanvas : MonoBehaviour
{
    [SerializeField]
    private GameObject healthBar;

    GameObject enemy;

    private GameObject player;

    private void Start()
    {
        enemy = transform.parent.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (player.transform.position.y < enemy.transform.position.y)
        {
            enemy.GetComponent<SpriteRenderer>().sortingOrder = 2;
        } else if (player.transform.position.y > enemy.transform.position.y)
        {
            enemy.GetComponent<SpriteRenderer>().sortingOrder = 4;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            healthBar.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            healthBar.SetActive(false);
        }
    }
}
