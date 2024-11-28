using UnityEngine;

/// <summary>
/// The BoneTrigger class is responsible for triggering a horde spawn when the player enters in the "bone" area.
/// </summary>
public class BoneTrigger : MonoBehaviour
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="collider">The collider.</param>
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            GameObject spawnHord = GameObject.Find("SpawnHorde");

            spawnHord.GetComponent<SpawnHorde>().enabled = true;
            gameObject.SetActive(false);
        }
    }
}
