using UnityEngine;

/// <summary>
/// The BoneTrigger class is responsible for triggering a horde spawn when the player enters in the "bone" area.
/// </summary>
public class Trigger_C : MonoBehaviour
{

    public GameObject spawnHord_01;
    public GameObject spawnHord_02;

    /// <summary>
    ///
    /// </summary>
    /// <param name="collider">The collider.</param>
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {

            spawnHord_01.GetComponent<SpawnHorde>().enabled = true;
            spawnHord_02.GetComponent<SpawnHorde>().enabled = true;
            gameObject.SetActive(false);

            collider.gameObject.GetComponent<Player>().spawnPoint = new Vector2(transform.position.x, transform.position.y);
        }
    }
}
