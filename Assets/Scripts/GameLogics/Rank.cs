using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// The Rank class is responsible for getting the player's rank status.
public class Rank : MonoBehaviour
{
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
    /// The TimeSpent property, gets the time spent by the player in the game.
    /// </summary>
    /// <value>
    /// The time spent by the player in the game.
    /// </value>
    public float TimeSpent { get; private set; }

    /// <summary>
    /// The ranks property is responsible for storing the ranks of the player (rank name and rank value).
    /// </summary>
    private readonly Dictionary<string,int> ranks = new ()
    {
        {"F", 50},
        {"E", 150},
        {"D", 300},
        {"C", 500},
        {"B", 750},
        {"A", 900},
        {"S", 950},
    };

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        HealItemsUsed = 0;
        DeathsNumber = 0;
        TimeSpent = 0F;
    }

    /// <summary>
    /// The Update method is called every frame (Unity Method).
    /// In this method, we are updating the time spent by the player.
    /// </summary>
    private void Update()
    {
        TimeSpent += Time.deltaTime;
    }

    /// <summary>
    /// The GetRank method is responsible for calculating the player's rank.
    /// It uses the time spent, the number of deaths, and the number of heal items used to calculate the rank.
    /// Each of these values has a weight that is used to calculate the rank.
    /// After calculating the rank value, the method compares it with the ranks dictionary to get the rank name of the player.
    /// </summary>
    /// <returns> The rank of the player (Name and Value) or null if the rank is not found</returns>
    public Tuple<string,int> GetRank()
    {
        int timeWeight, deathsNumberWeight, healItemsWeight;

        timeWeight = 5;
        deathsNumberWeight = 3;
        healItemsWeight = 1;

        int rankValue = (int)((timeWeight * TimeSpent) + (deathsNumberWeight * DeathsNumber) + (healItemsWeight * HealItemsUsed));

        foreach (string rankName in ranks.Keys)
        {
            if (rankValue <= ranks[rankName])
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
}
