using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The HealthBar class is responsible for showing the player's health bar on the screen.
/// </summary>
public class HealthBar : MonoBehaviour
{
    /// <summary>
    /// The slider property is responsible for showing the player's health bar on the screen.
    /// It is serialized so that it can be set in the Unity Editor.
    /// </summary>
    public Slider slider;

    /// <summary>
    /// The fill property is responsible for filling the player's health bar.
    /// It is serialized so that it can be set in the Unity Editor.
    /// </summary>
    [SerializeField]
    private Image fill;

    /// <summary>
    /// The Awake method is called when the script instance is being loaded (Unity Method).
    /// In this method, we are setting the maximum value of the slider to the player's health.
    /// The slider current value is also set, in this case, it is equal to the maximum value.
    /// </summary>
    private void Awake(){
        slider.maxValue = GameObject.Find("Player"). GetComponent<Entity>().Health;
        slider.value = slider.maxValue;
    }

    /// <summary>
    /// The UpdateLabel method is responsible for updating the player's health bar.
    /// If the player's health is less than or equal to 30, the health bar will be red, otherwise it will be green.
    /// </summary>
    /// <param name="health">The player's health value.</param>
    public void UpdateLabel(int health){

        slider.value = health;

        fill.color = slider.value <= 30 ? Color.red : Color.green;   
    }
}