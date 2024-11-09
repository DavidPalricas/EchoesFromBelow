using UnityEngine;
using TMPro;

/// <summary>
/// The InventoryLabel class is responsible for displaying the number of player's heal items on the screen.
/// </summary>
public class InventoryLabel : MonoBehaviour
{
    /// <summary>
    /// The labelText property is responsible for storing the text which will display the player's health on the screen.
    /// </summary>
    private TMP_Text labelText;

    /// <summary>
    /// The Awake method is called when the script instance is being loaded (Unity Method).
    /// In this method, we are initializing the label text property.
    /// </summary>
    private void Awake()
    {
        labelText = GetComponentInChildren<TMP_Text>();
    }

    /// <summary>
    /// The Update method is called every frame (Unity Method).
    /// In this method,we are calling the UpdateIventoryLabel() method.
    /// </summary>
    private void Update()
    {   
       UpdateIventoryLabel();
    }


    /// <summary>
    /// The UpdateIventoryLabel method is responsible for updating the number of player's heal items on the screen.
    /// </summary>
    private void UpdateIventoryLabel()
    {
        int playerHealItems = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>().HealItems;

        if (playerHealItems == PlayerInventory.MaxHealItems)
        {
            labelText.color = Color.red;
        }

        labelText.text = "x" + playerHealItems;
    }
}
