using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class Level2Logic : MonoBehaviour
{

    private GameObject graveyardHorde, graveyardTrigger;

    private SpeechTrigger speechTrigger;

    private void OnEnable()
    {
        graveyardHorde = GameObject.Find("GraveyardHorde");
        graveyardTrigger = GameObject.Find("GraveyardHordeTrigger");
        speechTrigger = GameObject.Find("SpeechTriggersLevel2").GetComponentInChildren<SpeechTrigger>();

        AddKeyToACoffin(GameObject.FindGameObjectsWithTag("Coffin"));
    }

   private void AddKeyToACoffin(GameObject[] coffins)
    {
        GameObject coffinWithKey = coffins[Random.Range(0, coffins.Length)];

        coffinWithKey.GetComponentInChildren<CoffinTrigger>().hasKey = true;
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


    public bool GraveYardEnemiesDead()
    {
        if (graveyardHorde.IsDestroyed())
        {
            return true;
        }

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
