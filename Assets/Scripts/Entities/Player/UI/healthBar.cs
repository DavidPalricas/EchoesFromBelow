using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{   
    [SerializeField]
    private Slider slider;

    private void Awake(){

        slider.maxValue = GameObject.Find("Player"). GetComponent<Entity>().Health;
        slider.value = slider.maxValue;

    }    

    public void UpdateLabel(int health){

        slider.value = health;

    }

}