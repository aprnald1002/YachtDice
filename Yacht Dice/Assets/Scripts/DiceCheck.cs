using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCheck : MonoBehaviour
{
    public static DiceCheck Instance { get; private set; }

    private List<GameObject> checkedDice = new List<GameObject>();
    private List<int> diceValues = new List<int>();
    private int count = 0;

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

    public void ResetDiceData()
    {
        checkedDice.Clear();
        diceValues.Clear();
        count = 0;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("CheckZone"))
            return;

        var parentTransform = other.transform.parent;
        var parentRigidbody = parentTransform.GetComponent<Rigidbody>();

        // 주사위가 움직이지 않는 상태에서만 체크
        if (parentRigidbody.velocity != Vector3.zero || checkedDice.Contains(parentTransform.gameObject))
            return;

        var diceValue = int.Parse(other.gameObject.name);
        parentTransform.gameObject.GetComponent<Dice>().diceValue = diceValue;
        
        checkedDice.Add(parentTransform.gameObject);
        diceValues.Add(diceValue);
        count++;

        // 모든 주사위가 체크되었는지 확인
        if (count == DiceController.Instance.UseDiceCount)
        {
            DiceController.Instance.ThrowAndMoveDice();
        }
    }

    public void Remove()
    {
        count--;

        // 모든 주사위가 다시 설정될 때
        if (count == 0)
        {
            ResetDiceData();
            DiceController.Instance.DiceSetting();
        }
    }
}