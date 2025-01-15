using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torchTutorialCollider : MonoBehaviour
{
    [SerializeField]
    private GameObject text;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            text.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            text.SetActive(false);
        }
    }
}
