using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCalculator : MonoBehaviour
{
    public static ScoreCalculator Instance = null; // ScoreCalculator의 인스턴스를 저장할 정적 변수
    
    public List<int> upperValue = new List<int>();
    public List<int> lowerValue = new List<int>();

    public List<Button> upperButton = new List<Button>();
    public List<Button> lowerButton = new List<Button>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // 인스턴스가 없으면 자신을 할당
        }
    }

    public void Setting()
    {
        upperValue.Clear();
        lowerValue.Clear();
        foreach (var obj in upperButton)
        {
            obj.gameObject.SetActive(false);
        }
        foreach (var obj in lowerButton)
        {
            obj.gameObject.SetActive(false);
        }
    }

    // 주사위 값들을 받아서 각 항목별 점수를 계산하는 메서드
    public void ScoreCalculation(List<int> dicesValue)
    {
        Setting();

        // Upper Section (Aces to Sixes)
        upperValue.Add(CalculateUpperSection(dicesValue, 1)); // Aces
        upperValue.Add(CalculateUpperSection(dicesValue, 2)); // Twos
        upperValue.Add(CalculateUpperSection(dicesValue, 3)); // Threes
        upperValue.Add(CalculateUpperSection(dicesValue, 4)); // Fours
        upperValue.Add(CalculateUpperSection(dicesValue, 5)); // Fives
        upperValue.Add(CalculateUpperSection(dicesValue, 6)); // Sixes

        // Lower Section
        lowerValue.Add(CalculateChance(dicesValue)); // Chance
        lowerValue.Add(CalculateThreeOfAKind(dicesValue)); // Three of a Kind
        lowerValue.Add(CalculateFourOfAKind(dicesValue)); // Four of a Kind
        lowerValue.Add(CalculateFullHouse(dicesValue)); // Full House
        lowerValue.Add(CalculateSmallStraight(dicesValue)); // Small Straight
        lowerValue.Add(CalculateLargeStraight(dicesValue)); // Large Straight
        lowerValue.Add(CalculateYahtzee(dicesValue)); // Yahtzee

        for (var i = 0; i < upperValue.Count; i++)
        {
            if (upperValue[i] != 0)
            {
                upperButton[i].gameObject.SetActive(true);
            }
        }
        
        for (var i = 0; i < lowerValue.Count; i++)
        {
            if (lowerValue[i] != 0)
            {
                lowerButton[i].gameObject.SetActive(true);
            }
        }
    }

    // Upper Section (Aces to Sixes) 계산 메서드
    private int CalculateUpperSection(List<int> dicesValue, int targetValue)
    {
        // 주사위 값 리스트에서 특정 값(targetValue)의 총 합을 반환
        return dicesValue.Where(dice => dice == targetValue).Sum(); 
    }

    // Three of a Kind 계산 메서드
    private int CalculateThreeOfAKind(List<int> dicesValue)
    {
        // 주사위 값 리스트에서 3개 이상 동일한 값이 있는지 확인 후 총 합 반환
        return HasNOfAKind(dicesValue, 3) ? dicesValue.Sum() : 0;
    }

    // Four of a Kind 계산 메서드
    private int CalculateFourOfAKind(List<int> dicesValue)
    {
        // 주사위 값 리스트에서 4개 이상 동일한 값이 있는지 확인 후 총 합 반환
        return HasNOfAKind(dicesValue, 4) ? dicesValue.Sum() : 0;
    }

    // Full House 계산 메서드
    private int CalculateFullHouse(List<int> dicesValue)
    {
        // 주사위 값 리스트에서 같은 값이 3개와 2개씩 있는지 확인 후 점수 반환
        var grouped = dicesValue.GroupBy(dice => dice).Select(group => group.Count()).OrderByDescending(count => count).ToList();
        return (grouped.Count == 2 && grouped[0] == 3 && grouped[1] == 2) ? 25 : 0;
    }

    // Small Straight 계산 메서드
    private int CalculateSmallStraight(List<int> dicesValue)
    {
        // 주사위 값 리스트에서 작은 스트레이트인지 확인 후 점수 반환
        var uniqueDices = dicesValue.Distinct().OrderBy(dice => dice).ToList();
        if (uniqueDices.ContainsSequence(new List<int> { 1, 2, 3, 4 }) || uniqueDices.ContainsSequence(new List<int> { 2, 3, 4, 5 }) || uniqueDices.ContainsSequence(new List<int> { 3, 4, 5, 6 }))
        {
            return 30;
        }
        return 0;
    }

    // Large Straight 계산 메서드
    private int CalculateLargeStraight(List<int> dicesValue)
    {
        // 주사위 값 리스트에서 큰 스트레이트인지 확인 후 점수 반환
        var uniqueDices = dicesValue.Distinct().OrderBy(dice => dice).ToList();
        if (uniqueDices.SequenceEqual(new List<int> { 1, 2, 3, 4, 5 }) || uniqueDices.SequenceEqual(new List<int> { 2, 3, 4, 5, 6 }))
        {
            return 40;
        }
        return 0;
    }

    // Yahtzee 계산 메서드
    private int CalculateYahtzee(List<int> dicesValue)
    {
        // 주사위 값 리스트에서 다섯 개 동일한 값인지 확인 후 점수 반환
        return HasNOfAKind(dicesValue, 5) ? 50 : 0;
    }

    // Chance 계산 메서드
    private int CalculateChance(List<int> dicesValue)
    {
        // 주사위 값 리스트의 총 합을 반환
        return dicesValue.Sum();
    }

    // 주어진 리스트에서 n개 이상 동일한 값이 있는지 확인하는 메서드
    private bool HasNOfAKind(List<int> dicesValue, int n)
    {
        return dicesValue.GroupBy(dice => dice).Any(group => group.Count() >= n);
    }
}

// List<int>에 대한 확장 메서드 정의
public static class Extensions
{
    // 주어진 리스트에서 순차적으로 일치하는 부분이 있는지 확인하는 메서드
    public static bool ContainsSequence(this List<int> source, List<int> sequence)
    {
        if (sequence.Count > source.Count)
            return false;

        for (int i = 0; i <= source.Count - sequence.Count; i++)
        {
            bool match = true;
            for (int j = 0; j < sequence.Count; j++)
            {
                if (source[i + j] != sequence[j])
                {
                    match = false;
                    break;
                }
            }
            if (match)
                return true;
        }
        return false;
    }
}
