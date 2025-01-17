using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingTrigger : MonoBehaviour
{
    // Referencia ao objecto com o script que gere o fade in/out
    [SerializeField]
    private GameObject fadeManager;

    // Variavel que vai verificar qual a cena atual, DEVE EXISTIR NOS SCRIPTS DE CANVAS DAS CENAS QUE NECESSITAM DE DAR FADE IN/OUT
    private int currentSceneIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject level1 = GameObject.Find("Level1");

            level1.GetComponent<Level1Logic>().enabled = false;

            level1.GetComponent<Rank>().StopTimer = true;

            fadeManager.GetComponent<LevelChanger>().FadeToLevel(currentSceneIndex + 1);

            //SceneManager.LoadScene("Ending");
        }

        gameObject.SetActive(false);
    }
}
