using UnityEngine;
using TMPro;

/// <summary>
/// The Timer class is responsible for keeping track of the time that has passed since the game started.
/// </summary>
public class Timer : MonoBehaviour
{
    /// <summary>
    /// The TextMeshProUGUI object that will display the time that has passed since the game started.
    /// </summary>
    [SerializeField] 
    private TextMeshProUGUI timerText;

    /// <summary>
    /// The elapsedTime variable will keep track of the time that has passed since the game started.
    /// </summary>
    private float elapsedTime;

    /// <summary>
    /// The Update method is called every frame (Unity Method).
    /// This method will increment the elapsedTime variable by the time that has passed since the last frame.
    /// And then it will update the timerText object with the new time, in the format HH:MM:SS on the screen.
    /// </summary>
    private void Update()
    {     
        elapsedTime += Time.deltaTime;

        int hours = Mathf.FloorToInt(elapsedTime / 3600);
        int minutes = Mathf.FloorToInt((elapsedTime / 3600) / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }
}
