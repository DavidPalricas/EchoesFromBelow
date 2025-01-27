using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingTrigger : MonoBehaviour
{

    [SerializeField]
    private GameObject hud;

    // Variavel que vai verificar qual a cena atual, DEVE EXISTIR NOS SCRIPTS DE CANVAS DAS CENAS QUE NECESSITAM DE DAR FADE IN/OUT
    private int currentSceneIndex;


    private void OnEnable()
    {
        if (hud == null)
        {
            hud = GameObject.FindGameObjectWithTag("HUD");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (collision.gameObject.CompareTag("Player"))
        {
            hud.GetComponent<Animator>().Play("fadeIn");
            GameObject gameLogic = GameObject.Find("GameLogic");


            if (gameLogic.GetComponent<Level1Logic>().enabled)
            {
                gameLogic.GetComponent<Level1Logic>().enabled = false;
            }
            else
            {
                gameLogic.GetComponent<Rank>().StopTimer = true;

                DestroyObjects();

                SceneManager.LoadScene("Ending");

                return;
            }
             
            hud.GetComponent<LevelChanger>().FadeToLevel(currentSceneIndex + 1);
        }

        gameObject.SetActive(false);
    }

    private void DestroyObjects()
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (allEnemies.Length > 0)
        {
            foreach (GameObject enemy in allEnemies)
            {
                Destroy(enemy);
            }
        }

        string[] gameObjectsToDestroy = {"HUD","Audio","MainCamera"};

        foreach (string gameObjectName in gameObjectsToDestroy)
        {
            Destroy(GameObject.FindGameObjectWithTag(gameObjectName));

        }
    }
}
