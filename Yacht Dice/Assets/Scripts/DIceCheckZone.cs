using System;
using UnityEngine;

public class DIceCheckZone : MonoBehaviour
{
    private Vector3 diceVelocity;
    private DiceController _diceController;

    private void Start()
    {
        _diceController = FindObjectOfType<DiceController>();
    }

    private void FixedUpdate()
    {
        diceVelocity = _diceController.diceVelocity;
    }

    private void OnTriggerStay(Collider col)
    {
        if (diceVelocity.x <= 0 && diceVelocity.y <= 0 && diceVelocity.z <= 0)
        {
            GameManager.Instance.diceNumber = Int32.Parse(col.gameObject.name);
        }
    }
}
