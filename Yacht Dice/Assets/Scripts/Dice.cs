using System;
using UnityEngine;

public class Dice : MonoBehaviour
{

    private Rigidbody _rb;
    public Vector3 check;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        check = _rb.velocity;
    }

    public void GetDiceResult(int index)
    {
        //Debug.Log(gameObject.name + " : " + index);
    }

    public void Keep()
    {
        
    }

    public void ReThrow()
    {
        
    }
}
