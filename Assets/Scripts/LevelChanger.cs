using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.HID;
using TMPro;

public class LevelChanger : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private int levelToLoad;

    public GameObject speechBox;

    /// <summary>
    /// The currentSpeechText property is responsible for storing the current speech text.
    /// </summary>
    public TextMeshProUGUI currentSpeechText;

    private void OnEnable()
    {
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("fadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        animator.Play("fadeIn", 0, 0);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
