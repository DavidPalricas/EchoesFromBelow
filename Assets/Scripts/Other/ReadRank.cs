using System;
using TMPro;
using UnityEngine;

public class ReadRank : MonoBehaviour
{

    private Rank playerRank;

    [SerializeField]
    private TextMeshProUGUI currentRankText, nextRankText, timeSpentText, deaths, healItemsUsed, enemiesKilled;

    private void Awake()
    {
        playerRank = GameObject.Find("GameLogic").GetComponent<Rank>();

        UpdateText();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Exit();
        }
    }


    private void UpdateText()
    {
        Tuple<string, int> currentRank = playerRank.GetRank();
        Tuple<string, int> nextRank = playerRank.GetNextRank(currentRank);

        string timeSpent = playerRank.GetTimeSpentFormatted();

        currentRankText.text = "Your obtained rank is: " + currentRank.Item1 + " " + currentRank.Item2 + " points.";
        nextRankText.text = "Your next rank is: " + nextRank.Item1 + " you need " + nextRank.Item2 + " points.";
        timeSpentText.text = "Time to completion: " + timeSpent;
        deaths.text = "Deaths: " + playerRank.DeathsNumber;
        healItemsUsed.text = "Heal items used: " + playerRank.HealItemsUsed;
        enemiesKilled.text = "Enemies killed: " + playerRank.EnemiesKilled;
    }


    private void Exit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
