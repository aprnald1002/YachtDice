using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public List<TextMeshProUGUI> diceTexts = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> player1Scores = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> player2Scores = new List<TextMeshProUGUI>();

    public TextMeshProUGUI turnCountText;

    public Canvas winUi;
    public TextMeshProUGUI winPlayerText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetDiceTexts()
    {
        foreach (var text in diceTexts)
        {
            text.text = string.Empty;
        }
    }

    public void SetDiceTexts(List<int> diceValues)
    {
        for (int i = 0; i < diceValues.Count && i < diceTexts.Count; i++)
        {
            diceTexts[i].text = diceValues[i].ToString();
        }
    }

    public void SetTurnCount(int value)
    {
        turnCountText.text = value.ToString();
    }

    public void SetPlayerScores(List<int> scores, int playerIndex)
    {
        var targetScores = playerIndex == 1 ? player1Scores : player2Scores;
        
        for (int i = 0; i < scores.Count && i < targetScores.Count; i++)
        {
            targetScores[i].text = scores[i].ToString();
        }
    }

    public void ShowGameEnd(string player)
    {
        winUi.gameObject.SetActive(true);
        winPlayerText.text = player + " Player Win";
    }
}