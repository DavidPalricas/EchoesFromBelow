using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {  
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject level1 = GameObject.Find("Level1");

            level1.GetComponent<Level1Logic>().enabled = false;

            level1.GetComponent<Rank>().StopTimer = true;

            SceneManager.LoadScene("Ending");

            /*
            Tuple<string, int> playerRank = rank.GetRank();
            Tuple<string, int> nextRank = rank.GetNextRank(playerRank);

            Debug.Log("Congratulations! You have completed the aplha version of our game!");

            Debug.Log("Your Rank is: " + playerRank.Item1 + "(" + playerRank.Item2 + " points)");

            Debug.Log("Next Rank: " + nextRank.Item1 + "\nPoints needed: " + nextRank.Item2);

            Debug.Log("Time spent: " + rank.GetTimeSpentFormatted());

            Debug.Log("Deaths: " + rank.DeathsNumber);

            Debug.Log("Heal Items used: " + rank.HealItemsUsed);

            Debug.Log("Skeletons killed: " + rank.SkeletonsKilled);

            Debug.Log("Boss killed: " + (rank.BossKilled ? "yes" : "no"));

            Debug.Log("Game made by:\n-David Palricas\n-Eva Aguiar\n-Hugo Tavares\n-Pedro Soares");

            */
        }

        gameObject.SetActive(false);
    }
}
