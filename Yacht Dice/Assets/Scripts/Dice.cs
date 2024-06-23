using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dice : MonoBehaviour
{
    private Rigidbody rb;
    private BoxCollider boxCollider;

    public GameObject lookAt;
    public int diceValue = 0;
    public bool _isRoll = false;

    private Vector3 originalPosition;
    private bool isAboveHeight = false;
    private bool isLeftOfPosition = false;
    private readonly float heightThreshold = 1f; // 특정 높이
    private readonly float leftThreshold = 2f; // 특정 좌측 위치

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            AddRandomTorque();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            RollDiceTowardsLookAt();
        }
    }

    private void OnMouseDown()
    {
        if (!_isRoll)
            return;

        boxCollider.isTrigger = true;
        originalPosition = transform.position;
    }

    private void OnMouseDrag()
    {
        if (!_isRoll)
            return;

        // 현재 마우스 위치를 가져옵니다. 마우스 위치는 2D 좌표이므로 y 값을 고정합니다.
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z; // z 값을 현재 오브젝트의 z 좌표로 설정

        // 마우스 위치를 월드 좌표로 변환합니다.
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // 오브젝트의 위치를 새로운 x, z 좌표로 업데이트하고 y 좌표는 고정합니다.
        transform.position = new Vector3(worldPosition.x, transform.position.y, worldPosition.z);
    }

    private void OnMouseUp()
    {
        if (!_isRoll)
            return;

        float movePositionZ = Mathf.Abs(transform.position.z - originalPosition.z);
        bool canRoll = DiceController.Instance.reRollCount < 4;

        isAboveHeight = transform.position.x < heightThreshold;
        isLeftOfPosition = movePositionZ > leftThreshold;

        bool shouldResetPosition = (!isAboveHeight && !isLeftOfPosition) || (isAboveHeight && isLeftOfPosition);

        if (!canRoll)
        {
            HandleDiceUsed();
            return;
        }

        if (shouldResetPosition)
        {
            transform.position = originalPosition;
        }
        else if (isAboveHeight)
        {
            HandleDiceUsed();
        }
        else if (isLeftOfPosition)
        {
            gameObject.SetActive(false);
            DiceCheck.Instance.Remove();
            _isRoll = false;
        }
    }

    public void AddRandomTorque()
    {
        if (_isRoll)
            return;
        
        rb.AddTorque(transform.up * GetRandom(300, 700), ForceMode.Force);
        rb.AddTorque(transform.right * GetRandom(300, 700), ForceMode.Force);
        rb.AddTorque(transform.forward * GetRandom(300, 700), ForceMode.Force);
    }

    public void RollDiceTowardsLookAt()
    {
        if (_isRoll)
            return;
        
        Vector3 direction = (lookAt.transform.position - transform.position).normalized;
        _isRoll = true;
        rb.useGravity = true;
        rb.AddForce(direction * GetRandom(500, 800));
    }

    private void HandleDiceUsed()
    {
        DiceCheck.Instance.Remove();
        DiceController.Instance.AddUsedDice(gameObject, diceValue);
    }

    private float GetRandom(int min, int max)
    {
        return Random.Range(min, max);
    }
}
