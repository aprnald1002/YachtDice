using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceController : MonoBehaviour
{
    public static DiceController Instance = null;

    [Header("Dice Settings")]
    public List<GameObject> dice = new List<GameObject>();
    public List<Vector3> dicePosition = new List<Vector3>();
    private Vector3 usedPosition;
    public Vector3 setPosition;
    public float usedInterval;
    public float setInterval;
    
    private List<GameObject> useDice = new List<GameObject>();
    private List<int> usedDice = new List<int>();

    public int reRollCount;
    public int UseDiceCount { get { return useDice.Count; } }

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

    public void GameSetting()
    {
        reRollCount = 0;
        useDice = new List<GameObject>(dice); // use a copy of the initial dice list
        usedDice = new List<int>();
        DiceSetting();
    }

    public void DiceSetting()
    {
        reRollCount++;
        for (int i = 0; i < useDice.Count; i++)
        {
            ResetDice(useDice[i], dicePosition[i]);
            useDice[i].GetComponent<Dice>()._isRoll = false;
        }
    }

    public void ThrowAndMoveDice()
    {
        for (int i = 0; i < useDice.Count; i++)
        {
            GameObject currentDice = useDice[i];
            ResetRigidbody(currentDice.GetComponent<Rigidbody>());
            SetDicePositionAndRotation(currentDice, i);
        }
    }

    public void AddUsedDice(GameObject dice, int value)
    {
        useDice.Remove(dice);
        usedDice.Add(value);

        // Get all values from the dictionary and convert them to a list
        List<int> diceValues = new List<int>(usedDice);
        UIManager.Instance.SetDiceTexts(diceValues);
        
        float positionZ = usedPosition.z + usedInterval * (usedDice.Count - 1);
        dice.transform.position = new Vector3(usedPosition.x, usedPosition.y, positionZ);
        
        if (useDice.Count == 0)
        {
            ScoreCalculator.Instance.ScoreCalculation(diceValues);
        }
    }

    private void ResetDice(GameObject dice, Vector3 position)
    {
        dice.SetActive(true);
        dice.GetComponent<BoxCollider>().isTrigger = false;
        dice.transform.position = position;
    }

    private void ResetRigidbody(Rigidbody rb)
    {
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    private void SetDicePositionAndRotation(GameObject dice, int index)
    {
        dice.transform.position = new Vector3(setPosition.x, setPosition.y, setPosition.z + (index * setInterval));
        Vector3 currentRotation = dice.transform.eulerAngles;
        dice.transform.eulerAngles = new Vector3(
            SnapToNearest90(currentRotation.x),
            SnapToNearest90(currentRotation.y),
            SnapToNearest90(currentRotation.z)
        );
    }

    private float SnapToNearest90(float angle)
    {
        return Mathf.Round(angle / 90) * 90;
    }
}
