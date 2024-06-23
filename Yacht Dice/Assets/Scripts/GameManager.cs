using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public GameObject table;

    private int turnCount = 0;
    private int turn;

    private List<int> oneUpperValue = new List<int>();
    private int oneSubtotal;
    private int oneBonus;
    private List<int> oneLowerValue = new List<int>();
    private int oneTotal;

    private List<int> onePlayerScore;
    
    private List<int> twoUpperValue = new List<int>();
    private int twoSubtotal;
    private int twoBonus;
    private List<int> twoLowerValue = new List<int>();
    private int twoTotal;

    private List<int> twoPlayerScore;
    
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

    private void Start()
    {
        turn = 1;
        turnCount = 1;
        
        GameSetting();
        ScoreSetting(); 
    }

    private void GameSetting()
    {
        UIManager.Instance.ResetDiceTexts();
        DiceController.Instance.GameSetting(); 
        ScoreCalculator.Instance.Setting();
        DiceCheck.Instance.ResetDiceData();
    }
    
    private void ScoreSetting()
    {
        for (int i = 0; i < 6; i++)
        {
            oneUpperValue.Add(0);
            oneLowerValue.Add(0);
            twoUpperValue.Add(0);
            twoLowerValue.Add(0);
        }

        oneSubtotal = 0;
        oneBonus = 0;
        oneTotal = 0;
        twoSubtotal = 0;
        twoBonus = 0;
        twoTotal = 0;

        ValueReset();
        
        UIManager.Instance.SetTurnCount(turnCount);
        UIManager.Instance.SetPlayerScores(onePlayerScore, 1);
        UIManager.Instance.SetPlayerScores(twoPlayerScore, 2);
    }

    private void ValueReset()
    {
        onePlayerScore = new List<int>(oneUpperValue) { oneSubtotal, oneBonus };
        onePlayerScore.AddRange(oneLowerValue);
        onePlayerScore.Add(oneTotal);
        
        twoPlayerScore = new List<int>(twoUpperValue) { twoSubtotal, twoBonus };
        twoPlayerScore.AddRange(twoLowerValue);
        twoPlayerScore.Add(twoTotal);
    }

    public void AddValue(int value, int index, int type)
    {
        if (turn == 1)
        {
            UpdatePlayerScore(oneUpperValue, oneLowerValue, ref oneSubtotal, ref oneBonus, ref oneTotal, value, index, type);
            onePlayerScore = GetUpdatedScoreList(oneUpperValue, oneSubtotal, oneBonus, oneLowerValue, oneTotal);
        }
        else
        {
            UpdatePlayerScore(twoUpperValue, twoLowerValue, ref twoSubtotal, ref twoBonus, ref twoTotal, value, index, type);
            twoPlayerScore = GetUpdatedScoreList(twoUpperValue, twoSubtotal, twoBonus, twoLowerValue, twoTotal);
        }

        turn = turn == 2 ? 1 : turn + 1;
        if (turn == 1)
        {
            turnCount++;
            if (turnCount == 12)
            {
                UIManager.Instance.ShowGameEnd(oneTotal > twoTotal ? "1" : "2");
            }
        }
        
        UIManager.Instance.SetTurnCount(turnCount);
        UIManager.Instance.SetPlayerScores(onePlayerScore, 1);
        UIManager.Instance.SetPlayerScores(twoPlayerScore, 2);
        
        GameSetting();
    }

    private void UpdatePlayerScore(List<int> upperValues, List<int> lowerValues, ref int subtotal, ref int bonus, ref int total, int value, int index, int type)
    {
        if (type == 1)
        {
            upperValues[index] = value;
        }
        else
        {
            lowerValues[index] = value;
        }

        subtotal = upperValues.Sum();
        if (subtotal > 63)
        {
            bonus = 35;
            subtotal += bonus;
        }

        total = upperValues.Sum() + bonus + lowerValues.Sum();
    }

    private List<int> GetUpdatedScoreList(List<int> upperValues, int subtotal, int bonus, List<int> lowerValues, int total)
    {
        List<int> updatedScore = new List<int>(upperValues) { subtotal, bonus };
        updatedScore.AddRange(lowerValues);
        updatedScore.Add(total);
        return updatedScore;
    }

    public void GameCrash()
    {
        var rb = table.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.AddForce(new Vector3(10000, 10000, 10000));
    }
}
