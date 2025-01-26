using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The MainMenu class is responsible for having the buttons logic in the main menu.
/// </summary>
public class MainMenu : MonoBehaviour
{
    // Referencia ao objecto com o script que gere o fade in/out
    [SerializeField]
    private GameObject fadeManager;

    // Variavel que vai verificar qual a cena atual, DEVE EXISTIR NOS SCRIPTS DE CANVAS DAS CENAS QUE NECESSITAM DE DAR FADE IN/OUT
    private int currentSceneIndex;
    
    /// <summary>
    /// The PlayGame method is responsible for loading the first level of the game.
    /// </summary>
    public void PlayGame()
    {
        Utils.PlaySoundEffect("fall");
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        fadeManager.GetComponent<LevelChanger>().FadeToLevel(currentSceneIndex + 1);
    }

    /// <summary>
    /// The ControlsMenu method is responsible for loading the controls menu.
    /// </summary>
    public void ControlsMenu()
    {
      SceneManager.LoadScene("ControlsMenu");
    }

    /// <summary>
    /// The QuitGame method is responsible for quitting the game.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
