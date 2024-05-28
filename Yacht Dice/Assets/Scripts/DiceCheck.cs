using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCheck : MonoBehaviour
{

    public GameObject dice;
    private Dice _Dice;
    private Vector3 direction;

    private void OnEnable()
    {
        _Dice = dice.GetComponent<Dice>();
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (!other.CompareTag("CheckZone"))
            return;
        direction = _Dice.GetComponent<Rigidbody>().velocity;
        if (direction == Vector3.zero)
        {
            _Dice.GetDiceResult(int.Parse(gameObject.name));
        }
    }
}
