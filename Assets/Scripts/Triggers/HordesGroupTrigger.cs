using System.Linq;
using UnityEngine;

/// <summary>
/// The HordesGroupTrigger class is responsible for triggering the hordes group.
/// </summary>
public class HordesGroupTrigger : MonoBehaviour
{
    /// <summary>
    /// The player inventory property is responsible for storing the player's inventory.
    /// </summary>
    [SerializeField]
    private PlayerInventory playerInventory;

    /// <summary>
    /// The horde group property is responsible for storing the horde group.
    /// </summary>
    [SerializeField]
    private GameObject hordesGroup;

    [SerializeField]
    private CameraMovement mainCameraMovement;

    [SerializeField]
    private SpeechTrigger speechTrigger;



    private void OnEnable()
    {
        if (playerInventory == null)
        {
            playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        }

        if (mainCameraMovement == null)
        {
            mainCameraMovement = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>();
        }
    }

    /// <summary>
    /// The OnTriggerEnter2D method is called when the Collider2D other enters the trigger (Unity Method).
    /// In this method, we are checking if the player entered in trigger and has the lever item and if so, we are activating the hordes.
    /// </summary>
    /// <param name="collision">The collider of the game object that collided.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && playerInventory.Items["Lever"] == 1)
        {   

            GameObject bossHealthBar = GameObject.FindGameObjectWithTag("HUD").GetComponent<LevelChanger>().bossHealthBar;

            if (bossHealthBar != null && !bossHealthBar.activeSelf)
            {
                bossHealthBar.SetActive(true);
                bossHealthBar.GetComponent<EnemyBossHealthBar>().SetHealthBar();
            }

            ActiveHordes();
            ChangePlayerSpawn(collision.gameObject);

            FocusOnBoss();
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// The ActiveHordes method is responsible for activating the hordes inside the horde group.
    /// It randomly selects one of the hordes to spawn the boss.
    /// </summary>
    private void ActiveHordes()
    {
       int hordesNumber = hordesGroup.transform.childCount;

       var hordes = new SpawnHorde[hordesNumber];

        for (int i = 0; i < hordesNumber; i++)
        {
            SpawnHorde spawnHorde = hordesGroup.transform.GetChild(i).GetComponent<SpawnHorde>();
            spawnHorde.enabled = true;
            hordes[i] = spawnHorde;
        }

        hordes[Random.Range(0, hordes.Length)].SpawnBoss();
    }

    /// <summary>
    /// The ChangePlayerSpawn method is responsible for changing the player spawn point.
    /// </summary>
    /// <param name="player">The player game object.</param>
    private void ChangePlayerSpawn(GameObject player)
    {
        Player playerProprieties = player.GetComponent<EntityFSM>().entityProprieties as Player;

        playerProprieties.spawnPoint = new Vector2(transform.position.x, transform.position.y);
    }


    private void FocusOnBoss()
    {   
        GameObject boss = null;
        
        do
        {
            boss = GameObject.FindGameObjectsWithTag("Enemy").Where(enemy => enemy.name.Contains("Skeleton Boss")).FirstOrDefault();

        } while(boss == null);

        speechTrigger.ChangeSpeech("bossRecounterSpeech");
        mainCameraMovement.ChangeTarget(boss, 0.8f);
    }
}
