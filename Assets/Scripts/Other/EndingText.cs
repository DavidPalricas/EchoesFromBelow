using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The EndingText class is responsible for displaying the ending text on the screen.
/// Which includes the player's rank status and the game credits.
/// </summary>
public class EndingText : MonoBehaviour
{
    /// <summary>
    /// The sceneText object is responsible for displaying the text on the screen.
    /// Its SerializeField attribute allows it to be accessed in the Unity Editor.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI sceneText;

    /// <summary>
    /// The Awake method is called when the script instance is being loaded (Unity Method).
    /// In this method, we are setting the ending text that will be displayed on the screen.
    /// </summary>
    private void Awake()
    {   
        string endingText = "\t\tCongratulations! You have completed the level!\n";
   
        endingText += WriteRankStatus(GameObject.Find("GameLogic").GetComponent<Rank>());

        //endingText += $"Game made by:\n" +
        //              $" - David Palricas\n" +
        //              $" - Eva Aguiar\n" +
        //              $" - Hugo Tavares\n" + 
        //              $" - Pedro Soares\n\n\n";

        endingText += "\t\t\t\t\tPress Enter to return to the main menu.";

        sceneText.text = endingText;

        SetTextStyle();
    }

    /// <summary>
    /// The Update method is called every frame (Unity Method).
    /// It will check if the player has pressed the Escape key, and if so, it will call the ExitGame method.
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            this.gameObject.GetComponent<LevelChanger>().FadeToLevel(0);
        }
    }

    /// <summary>
    /// The WriteRankStatus method is responsible for writing a string with the player's rank status.
    /// </summary>
    /// <param name="rank">The rank class.</param>
    /// <returns> A string with the player's rank status. </returns>
    private string WriteRankStatus(Rank rank)
    {
        Tuple<string, int> playerRank = rank.GetRank();
        Tuple<string, int> nextRank = rank.GetNextRank(playerRank);

        string rankStatusText =
         $"Your Rank is: {playerRank.Item1} ({playerRank.Item2} points)\n" +
         $"Next Rank: {nextRank.Item1}, points needed to level up: {nextRank.Item2}\n" +
         $"Time spent: {rank.GetTimeSpentFormatted()}\n" +
         $"Deaths: {rank.DeathsNumber}\n" +
         $"Heal Items used: {rank.HealItemsUsed}\n" +
         $"Skeletons killed: {rank.SkeletonsKilled}\n" +
         $"Boss killed: {(rank.BossKilled ? "yes" : "no")}\n\n\n";

        return rankStatusText;
    }

    private void SetTextStyle()
    {
        sceneText.fontSize = 36;
        sceneText.alignment = TextAlignmentOptions.Left;
        sceneText.color = Color.white;
        sceneText.fontStyle = FontStyles.Bold;
    }
}
