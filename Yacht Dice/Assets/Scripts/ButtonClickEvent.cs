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
        if (acesChange) {acesChange.onClick.AddListener(() => ChangeUpperValue(0, 1));}
        if (deucesChange) {deucesChange.onClick.AddListener(() => ChangeUpperValue(1, 1));}
        if (threesChange) {threesChange.onClick.AddListener(() => ChangeUpperValue(2, 1));}
        if (foursChange) {foursChange.onClick.AddListener(() => ChangeUpperValue(3, 1));}
        if (fivesChange) {fivesChange.onClick.AddListener(() => ChangeUpperValue(4, 1));}
        if (sixesChange) {sixesChange.onClick.AddListener(() => ChangeUpperValue(5, 1));}

        if (choiceChange) { choiceChange.onClick.AddListener(() => ChangeLowerValue(0, 2));}
        if (kindChange) {kindChange.onClick.AddListener(() => ChangeLowerValue(1, 2));}
        if (houseChange) {houseChange.onClick.AddListener(() => ChangeLowerValue(2, 2));}
        if (sStraightChange) {sStraightChange.onClick.AddListener(() => ChangeLowerValue(3, 2));}
        if (lStraightChange) {lStraightChange.onClick.AddListener(() => ChangeLowerValue(4, 2));}
        if (yachtChange) {yachtChange.onClick.AddListener(() => ChangeLowerValue(5, 2));}
        
        if (reStart) {reStart.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));}
        
        if (rollButton) { rollButton.onClick.AddListener(RollAllDices); }
        if (addTorqueButton) { addTorqueButton.onClick.AddListener(AddTorqueToAllDices); }
        
        if (gameCrash) {gameCrash.onClick.AddListener(() => GameManager.Instance.GameCrash());}

    }

    private void ChangeUpperValue(int index, int type)
    {
        GameManager.Instance.AddValue(ScoreCalculator.Instance.upperValue[index], index, type);
    }

    private void ChangeLowerValue(int index, int type)
    {
        GameManager.Instance.AddValue(ScoreCalculator.Instance.lowerValue[index], index, type);
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