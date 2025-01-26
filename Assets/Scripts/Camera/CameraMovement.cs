using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// The CameraMovement class is responsible for handling the camera's movement.
/// </summary>
public class CameraMovement : MonoBehaviour
{
    /// <summary>
    /// The player property is responsible for storing the player's GameObject.
    /// </summary>
    [SerializeField]
    private GameObject player;

    /// <summary>
    /// The currentTarget property is responsible for storing the current target of the camera.
    /// </summary>
    private GameObject currentTarget;

    /// <summary>
    /// The currentSceneIndex property is responsible for storing the current scene index.
    /// </summary>
    private int currentSceneIndex;

    /// <summary>
    /// The Start method is called before the first frame update (Unity Method).
    /// In this method, the current target is initialized (player) the music volume and scene index are set.
    /// </summary>
    private void Start()
    {
        currentTarget = player;

        if (PlayerPrefs.HasKey("musicVolume"))
        {
            AudioListener.volume = PlayerPrefs.GetFloat("musicVolume") / 10;
        }

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        currentTarget = player; // Initialize currentTarget in Start method

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// The Update method is called every frame (Unity Method).
    /// In this method, the camera follows the player.
    /// </summary>
    private void Update()
    {
        if (currentSceneIndex != 0)
        {
            if (currentTarget != player && !Utils.isSpeechActive)
            {
                currentTarget = player;
                Utils.isSpeechActive = false;
            }
            

            transform.position = GetTargetPosition(currentTarget);
        }
    }

    /// <summary>
    /// The GetTargetPosition method is responsible for getting the target's position.
    /// </summary>
    /// <param name="target">The target of the camera.</param>
    /// <returns>A new position for the camera (Vector3)</returns>
    private Vector3 GetTargetPosition(GameObject target)
    {
        return new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }

    /// <summary>
    /// The ChangeTarget method is responsible for changing the camera's target.
    /// It starts a coroutine to make a smooth transition between the current target and the new target.
    /// </summary>
    /// <param name="newTarget">The new target to the camera to focus.</param>
    /// <param name="transitionTime">The transition time.</param>
    public void ChangeTarget(GameObject newTarget, float transitionTime)
    {
        StartCoroutine(SmoothTransition(newTarget, transitionTime));
    }

    /// <summary>
    /// The SmoothTransition method, smoothly transitions the GameObject's position from its current location to the position of the new target over a specified duration.
    /// </summary>
    /// <param name="newTarget">The GameObject to transition towards.</param>
    /// <param name="transitionTime">The duration of the transition.</param>
    /// <returns>An IEnumerator that can be used in a coroutine to execute the transition over multiple frames.</returns>
    private IEnumerator SmoothTransition(GameObject newTarget, float transitionTime)
    {
        Utils.isSpeechActive = true;

        float elapsedTime = 0f;

        Vector3 startPosition = transform.position;
        var targetPosition = new Vector3(newTarget.transform.position.x, newTarget.transform.position.y, transform.position.z);

        while (elapsedTime < transitionTime)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / transitionTime);
            elapsedTime += Time.deltaTime;

            yield return null; 
        }

        transform.position = targetPosition;

        currentTarget = newTarget; 
    }
}
