using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonClickEvent : MonoBehaviour
{
    public Button acesChange;
    public Button deucesChange;
    public Button threesChange;
    public Button foursChange;
    public Button fivesChange;
    public Button sixesChange;

    public Button choiceChange;
    public Button kindChange;
    public Button houseChange;
    public Button sStraightChange;
    public Button lStraightChange;
    public Button yachtChange;

    public Button reStart;
    
    public Button rollButton;
    public Button addTorqueButton;

    public Button gameCrash;

    public List<Dice> diceList = new List<Dice>();

    
    
    /// 람다식을 활용한 버튼 이벤트 구현
    private void Awake()
    {
        if (acesChange) {acesChange.onClick.AddListener(() => GameManager.Instance.AddValue(ScoreCalculator.Instance.upperValue[0], 0, 1));}
        if (deucesChange) {deucesChange.onClick.AddListener(() => GameManager.Instance.AddValue(ScoreCalculator.Instance.upperValue[1], 1,1));}
        if (threesChange) {threesChange.onClick.AddListener(() => GameManager.Instance.AddValue(ScoreCalculator.Instance.upperValue[2], 2,1));}
        if (foursChange) {foursChange.onClick.AddListener(() => GameManager.Instance.AddValue(ScoreCalculator.Instance.upperValue[3], 3,1));}
        if (fivesChange) {fivesChange.onClick.AddListener(() => GameManager.Instance.AddValue(ScoreCalculator.Instance.upperValue[4], 4,1));}
        if (sixesChange) {sixesChange.onClick.AddListener(() => GameManager.Instance.AddValue(ScoreCalculator.Instance.upperValue[5], 5,1));}
        
        if (choiceChange) {choiceChange.onClick.AddListener(() => GameManager.Instance.AddValue(ScoreCalculator.Instance.lowerValue[0], 0,2));}
        if (kindChange) {kindChange.onClick.AddListener(() => GameManager.Instance.AddValue(ScoreCalculator.Instance.lowerValue[1], 1,2));}
        if (houseChange) {houseChange.onClick.AddListener(() => GameManager.Instance.AddValue(ScoreCalculator.Instance.lowerValue[2], 2,2));}
        if (sStraightChange) {sStraightChange.onClick.AddListener(() => GameManager.Instance.AddValue(ScoreCalculator.Instance.lowerValue[3], 3,2));}
        if (lStraightChange) {lStraightChange.onClick.AddListener(() => GameManager.Instance.AddValue(ScoreCalculator.Instance.lowerValue[4], 4,2));}
        if (yachtChange) {yachtChange.onClick.AddListener(() => GameManager.Instance.AddValue(ScoreCalculator.Instance.lowerValue[5], 5,2));}
        
        if (reStart) {reStart.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));}
        
        if (rollButton) { rollButton.onClick.AddListener(RollAllDices); }
        if (addTorqueButton) { addTorqueButton.onClick.AddListener(AddTorqueToAllDices); }
        
        if (gameCrash) {gameCrash.onClick.AddListener(() => GameManager.Instance.GameCrash());}

    }
    
    private void RollAllDices()
    {
        foreach (var dice in diceList)
        {
            dice.RollDiceTowardsLookAt();
        }
    }

    // 모든 주사위에 랜덤 토크를 추가하는 메서드
    private void AddTorqueToAllDices()
    {
        foreach (var dice in diceList)
        {
            dice.AddRandomTorque();
        }
    }
}