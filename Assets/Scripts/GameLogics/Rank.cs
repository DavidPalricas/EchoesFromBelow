using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// The Rank class is responsible for getting the player's rank status.
/// </summary>
public class Rank : MonoBehaviour
{
    /// <summary>
    /// The ranks property is responsible for storing the ranks of the player (rank name and rank value).
    /// </summary>
    private readonly Dictionary<string, int> ranks = new()
    {
        {"The Lost Child ", 0},
        {"Crying Tourist", 150},
        {"Explorer", 300},
        {"Monster Hunter", 500},
        {"The Dark Knight", 700},
        {"The Catacombs Monster", 850},
        {"The Paris Nigthmare", 950},
    };

    /// <summary>
    /// The TimeSpent property, gets the time spent by the player in the game.
    /// </summary>
    /// <value>
    /// The time spent by the player in the game.
    /// </value>
    private float timeSpent;

    /// <summary>
    /// The HealItemsUsed property, gets or sets the heal items used.
    /// </summary>
    /// <value>
    /// The number of heal items used.
    /// </value>
    public int HealItemsUsed { get; set; }

    /// <summary>
    /// The DeathsNumber property, gets or sets the number of deaths of the player.
    /// </summary>
    /// <value>
    /// The number of death of the player.
    /// </value>
    public int DeathsNumber { get; set; }

    /// <summary>
    /// Gets or sets the number of skeletons killed
    /// </summary>
    /// <value>
    /// The number of skeletons killed during the player's gameplay.
    /// </value>
    public int SkeletonsKilled { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the boss was killed.
    /// </summary>
    /// <value>
    ///   <c>true</c> if [boss killed]; otherwise, <c>false</c>.
    /// </value>
    public bool BossKilled { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the timer should stop.
    /// </summary>
    /// <value>
    ///   <c>true</c> if [stop timer]; otherwise, <c>false</c>.
    /// </value>
    public bool StopTimer { get; set; }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        HealItemsUsed = 0;
        DeathsNumber = 0;
        timeSpent = 0F;
        SkeletonsKilled = 0;
        BossKilled = false;
    }

    /// <summary>
    /// The Update method is called every frame (Unity Method).
    /// In this method, we are updating the time spent by the player, if the timer is not stopped.
    /// </summary>
    private void Update()
    {
        if (!StopTimer)
        {
            timeSpent += Time.deltaTime;
        }  
    }

    /// <summary>
    /// The CalculateRank method is responsible for calculating the rank of the player.
    /// It uses the time spent, the number of deaths,the number of heal items, skeletons killed and boss killed are used to calculate the rank.
    /// For each of these values, we have a weight that is used to calculate the rank.
    /// </summary>
    /// <returns> The rank value</returns>
    private int CalculateRankValue()
    {
        // Measure the time spent in the game in minutes
        int timeComponent = (int)(60 * (timeSpent / 60.0)); 
        int deathsComponent = 5 * DeathsNumber;
        int skeletonsComponent = 4 * SkeletonsKilled;
        int healItemsComponent = 3 * HealItemsUsed;
        int bossComponent = BossKilled ? 5 : 0;

        int rankValue = 1000 - (timeComponent + deathsComponent + healItemsComponent) + skeletonsComponent + bossComponent;

        return rankValue > 0 ? rankValue : 0;
    }

    /// <summary>
    /// The GetRank method is responsible for calculating the player's rank.
    /// It uses the CalculateRankValue method to get the rank value.
    /// After getting the rank value, the method compares it with the ranks dictionary to get the rank name of the player.
    /// </summary>
    /// <returns> The rank of the player (Name and Value) or null if the rank is not found</returns>
    public Tuple<string,int> GetRank()
    {
        int rankValue = CalculateRankValue();

        Debug.Log($"Rank Value: {rankValue}");

        const int MAXRANK = 1000;

        int lastTypeRankValue = ranks.Values.Last();

        foreach (string rankName in ranks.Keys)
        {
            if (rankValue <= ranks[rankName])
            {
                return new Tuple<string, int>(rankName, rankValue);
            }
            else if (rankValue > lastTypeRankValue && rankValue <= MAXRANK)
            {
                return new Tuple<string, int>(rankName, rankValue);
            }
        }

        return null;
    }

    /// <summary>
    /// The GetNextRank method is responsible for getting the next rank of the player (name) and the points needed to reach it.
    /// </summary>
    /// <param name="currentRank">The current rank of the player.</param>
    /// <returns> A Tuple with the next rank name and the points neede to reach it </returns>
    public Tuple<string, int> GetNextRank(Tuple<string,int> currentRank)
    {   
        if (currentRank == null)
        {
           Debug.LogError("Error getting the player's rank");
        }

        string currentRankName = currentRank.Item1;
        int currentRankValue = currentRank.Item2;

        string[] rankNames = ranks.Keys.ToArray();

        int rankIndex = Array.IndexOf(rankNames, currentRankName);

        if (rankIndex == rankNames.Length - 1)
        {
            return new Tuple<string, int>(currentRankName, currentRankValue);
        }

        return new Tuple<string, int>(rankNames[rankIndex + 1], ranks[rankNames[rankIndex + 1]] - currentRankValue);
    }


    /// <summary>
    /// The GetTimeSpentFormatted method is responsible for getting the time spent by the player in a formatted way (hours, minutes and seconds).
    /// </summary>
    /// <returns>A formated string of the time</returns>
    public string GetTimeSpentFormatted()
    {
        int hours = Mathf.FloorToInt((int)timeSpent / 3600);
        int minutes = Mathf.FloorToInt((timeSpent % 3600) / 60);
        int seconds = Mathf.FloorToInt((int)timeSpent % 60);

        return $"{hours} hours {minutes} minutes {seconds} seconds";
    }
}
