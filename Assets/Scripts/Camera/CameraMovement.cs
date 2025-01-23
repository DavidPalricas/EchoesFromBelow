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

    private void Start()
    {
        currentTarget = player;

        if (PlayerPrefs.HasKey("musicVolume"))
        {
            AudioListener.volume = PlayerPrefs.GetFloat("musicVolume") / 10;
        }

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        currentTarget = player; // Initialize currentTarget in Start method
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


    public void ChangeTarget(GameObject newTarget, float transitionTime)
    {
        StartCoroutine(SmoothTransition(newTarget, transitionTime));
    }


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
