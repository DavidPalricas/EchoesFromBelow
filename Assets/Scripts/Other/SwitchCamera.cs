using UnityEngine;
using System.Collections;

public class SwitchCamera : MonoBehaviour
{
    /// <summary>
    /// The mainCamera property is responsible for storing the game's main camera.
    /// </summary>
    private Camera mainCamera;

    /// <summary>
    /// The zoneCameraPrefab property is responsible for storing the zone camera prefab.
    /// It is serialized so that it can be set in the Unity Editor
    /// </summary>
    [SerializeField]
    private GameObject zoneCameraPrefab;

    /// <summary>
    /// The zoneCamera property is responsible for storing the zone camera.
    /// </summary>
    private Camera zoneCamera;

    /// <summary>
    /// The is transitioning property is responsible for storing a value indicating whether the camera is transitioning or not.
    /// </summary>
    private bool isTransitioning = false;

    /// <summary>
    /// The Awake method is called when the script instance is being loaded (Unity Method).s
    /// This method will get the main camera and call the SetupZoneCamera method to set up the zone camera.
    /// </summary>
    private void Awake()
    {
        mainCamera = Camera.main;

        SetupZoneCamera();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision">The collision.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isTransitioning)
        {
            StartCoroutine(SwitchCameraSmoothly(zoneCamera));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isTransitioning)
        {
            StartCoroutine(SwitchCameraSmoothly(mainCamera));
        }
    }

    /// <summary>
    /// Switches the camera smoothly.
    /// </summary>
    /// <param name="targetCamera">The target camera.</param>
    /// <returns></returns>
    private IEnumerator SwitchCameraSmoothly(Camera targetCamera)
    {
        isTransitioning = true;

        Camera currentCamera = mainCamera.enabled ? mainCamera : zoneCamera;

        float transitionDuration = 1.0f; 
        float elapsedTime = 0f;

        Vector3 startPosition = currentCamera.transform.position;
        Vector3 targetPosition = targetCamera.transform.position;

        float startSize = currentCamera.orthographicSize;
        float targetSize = targetCamera.orthographicSize;
    
        currentCamera.enabled = false;
   
        targetCamera.enabled = true;

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;
   
            float smoothT =  Mathf.Pow(t, 2);

            targetCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, smoothT);
            
            targetCamera.orthographicSize = Mathf.Lerp(startSize, targetSize, smoothT);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        targetCamera.transform.position = targetPosition;
        targetCamera.orthographicSize = targetSize;

        isTransitioning = false;
    }

    /// <summary>
    /// The SetupZoneCamera method is responsible for setting up the zone camera.
    /// The zone camera is instantied in the position of the zone and with the same size as the zone.
    /// </summary>
    private void SetupZoneCamera()
    {  
        var zoneCameraPosition = new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z);

        GameObject zoneCameraInstance = Instantiate(zoneCameraPrefab, zoneCameraPosition, Quaternion.identity);

        zoneCamera = zoneCameraInstance.GetComponent<Camera>();

        // Disable  the zone camera, because the main camera is the default camera
        zoneCamera.enabled = false;

        zoneCamera.orthographicSize = GetComponent<BoxCollider2D>().size.y / 2f;
    }
}
