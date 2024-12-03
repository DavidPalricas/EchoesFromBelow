using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {  
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject level1 = GameObject.Find("Level1");

            level1.GetComponent<Level1Logic>().enabled = false;

            level1.GetComponent<Rank>().StopTimer = true;

            SceneManager.LoadScene("Ending");
        }

        gameObject.SetActive(false);
    }
}
