using UnityEngine;
using TMPro;

public class TimeLabel : MonoBehaviour
{
    /// <summary>
    /// The label text property is responsible for storing the text which will display the gameplay time on the screen.
    /// </summary>
    private TMP_Text labelText;

    /// <summary>
    /// The time property is responsible for storing the gameplay time.
    /// </summary>
    private float time = 0f;

    /// <summary>
    /// The Awake method is called when the script instance is being loaded (Unity Method).
    /// In this method, we are initializing the label text property.
    /// </summary>
    private void Awake()
    {
        labelText = GetComponent<TMP_Text>();
    }

    /// <summary>
    /// The Update method is called every frame (Unity Method).
    /// In this method, we are updating the gameplay time and calling the UpdateTimeLabel() method.
    /// </summary>

    private void Update()
    {
        time += Time.deltaTime;
        UpdateTimeLabel();
    }

    /// <summary>
    /// The UpdateTimeLabel method is responsible for updating the gameplay time on the screen.
    /// The time is formatted in hours, minutes, and seconds.
    /// </summary>
    private void UpdateTimeLabel()
    {
        int hours = Mathf.FloorToInt(time / 3600F);
        int minutes = Mathf.FloorToInt((time / 3600) % 60F);
        int seconds = Mathf.FloorToInt((time % 3600) % 60F);

        labelText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }
}
