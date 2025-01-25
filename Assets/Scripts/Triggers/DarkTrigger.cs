using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject ambientLight;

    private bool isDark = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isDark)
        {
            ambientLight.GetComponent<Animator>().Play("ambient_Darken");

            isDark = true;

            this.gameObject.SetActive(false);
        } else if (collision.gameObject.CompareTag("Player") && isDark)
        {
            ambientLight.GetComponent<Animator>().Play("ambient_Lighten");

            isDark = false;

            this.gameObject.SetActive(false);
        }
    }
}
