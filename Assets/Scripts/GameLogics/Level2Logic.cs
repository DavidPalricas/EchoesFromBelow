using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class Level2Logic : MonoBehaviour
{

    private GameObject graveyardHorde, graveyardTrigger;


    private SpeechTrigger speechTrigger;

    private SpawnHorde graveyardHordeScript;


    private void OnEnable()
    {
        graveyardHorde = GameObject.Find("GraveyardHorde");
        graveyardHordeScript = graveyardHorde.GetComponent<SpawnHorde>();
        graveyardTrigger = GameObject.Find("GraveyardHordeTrigger");
        speechTrigger = GameObject.Find("SpeechTriggersLevel2").GetComponentInChildren<SpeechTrigger>();
    }


    private void Update()
    {
        if(!graveyardHorde.IsDestroyed() && GraveYardEnemiesDead())
        {
            speechTrigger.ChangeSpeech("graveyardEnemiesDead");
            Destroy(graveyardHorde);
            Destroy(graveyardTrigger);
        }
    }


    private bool GraveYardEnemiesDead()
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        SpawnHorde graveyardHordeScript = graveyardHorde.GetComponent<SpawnHorde>();

        if (graveyardHordeScript.EnemiesToSpawn != 0)
        {
            return false;
        }

        string graveyardEnemiesId = graveyardHordeScript.HordeID.ToString();

        return allEnemies.Where(enemy => enemy.name.Contains("Enemy " + graveyardEnemiesId)).Count() == 0;
    }
}
