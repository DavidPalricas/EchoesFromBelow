using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class Level2Logic : MonoBehaviour
{
    [SerializeField]
    private GameObject leverPrefab;

    private GameObject graveyardHorde, graveyardTrigger;

    [SerializeField]
    private SpeechTrigger speechTrigger;


    private void OnEnable()
    {
        graveyardHorde = GameObject.Find("GraveyardHorde");
        graveyardTrigger = GameObject.Find("GraveyardHordeTrigger");

        AddKeyToACoffin(GameObject.FindGameObjectsWithTag("Coffin"));
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

    private void AddKeyToACoffin(GameObject[] coffins)
    {   
        GameObject coffinWithKey = coffins[Random.Range(0, coffins.Length)];

        coffinWithKey.GetComponentInChildren<CoffinTrigger>().hasKey = true;
    }


    public Vector3 GetNewLeverPosition()
    {
        GameObject[] leversSpawn = GameObject.FindGameObjectsWithTag("LeverSpawn");

        return leversSpawn[Random.Range(0, leversSpawn.Length)].transform.position;   
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

    public void PuzzleItemGrabbed(bool isLever)
    {   
        speechTrigger.ChangeSpeech(isLever ? "leverPieceFoundSpeech" : "keyFoundSpeech");
    }


    public void BossKilled()
    {
        GameObject bossHealthBar = GameObject.FindGameObjectWithTag("HUD").GetComponent<LevelChanger>().bossHealthBar;

        bossHealthBar.SetActive(false);

        speechTrigger.ChangeSpeech("bossDeathSpeech");
    }
}
