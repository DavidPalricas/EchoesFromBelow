using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCanvas : MonoBehaviour
{
    [SerializeField]
    private GameObject healthBar;

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
