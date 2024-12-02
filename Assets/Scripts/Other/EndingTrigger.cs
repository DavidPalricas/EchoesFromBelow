using System;
using UnityEngine;

public class EndingTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {   
            Rank rank = GameObject.Find("Level1").GetComponent<Rank>();

            Tuple<string, int> playerRank = rank.GetRank();
            Tuple<string, int> nextRank = rank.GetNextRank(playerRank);

            Debug.Log("Your Rank is: " + playerRank.Item1 + "(" + playerRank.Item2 + " points)");

            Debug.Log("Next Rank: " + nextRank.Item1 + "\nPoints needed: " + nextRank.Item2);

            Debug.Log("Time spent: " + rank.TimeSpent + " seconds");

            Debug.Log("Deaths: " + rank.DeathsNumber);

            Debug.Log("Heal Items used: " + rank.HealItemsUsed);
        }


        gameObject.SetActive(false);
    }
}
