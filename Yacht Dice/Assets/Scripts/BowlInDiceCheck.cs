using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlInDiceCheck : MonoBehaviour
{

    public static BowlInDiceCheck Instance = null;
    
    [Header("컵안에 있는 주사위 정보")]
    public List<GameObject> diceInside = new List<GameObject>();
    public int maxDiceCount;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {

        if (diceInside.Count == maxDiceCount)
        {
            BowlController.Instance.isReady = true;
        }
        else
        {
            BowlController.Instance.isReady = false;
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Dice") || diceInside.Contains(other.gameObject))
            return;
        diceInside.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Dice"))
        {
            diceInside.Remove(other.gameObject);
        }
    }
}
