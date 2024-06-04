using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlController : MonoBehaviour
{
    public static BowlController Instance = null;
    
    [Header("컵 정보들")]
    [SerializeField]
    private Vector3 originalPosition;
    public BoxCollider boxCollider;
    
    [Header("\nSpace : 컵 흔듬, C : 주사위 부음\n")]
    [Header("회전, 이동할 위치")]
    public float xRotationValue;
    public float rotationTime;
    public float zMoveDistance;

    [Header("각종 조건들")]
    [Tooltip("컵이 움직일 준비가 되어있는지")]
    public bool isReady;
    [Tooltip("컵이 움직이는지")]
    public bool isMove;
    
    

    private void Awake()
    {

        if (Instance == null) {
            Instance = this;
        }
        
        originalPosition = transform.position;
        isMove = false;
    }

    private void Update()
    {
        if (!isReady)
            return;

        if (!isMove)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(RollDice(0.1f, true));
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                StartCoroutine(RollDice(1f, false));
            }
        }
    }

    // rollDiceType : true면 주사위 흔드는거, false면 주사위 부워버림
    private IEnumerator RollDice(float moveDelay, bool rollDiceType)
    {
        isMove = true;
        Vector3 targetPosition;
        Quaternion targetRotation;

        boxCollider.isTrigger = !rollDiceType;
        
        if (rollDiceType)
        {
            targetPosition = originalPosition + new Vector3(0, 0, zMoveDistance);
            targetRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            targetPosition = originalPosition + new Vector3(0, 0, (zMoveDistance * 3));
            targetRotation = Quaternion.Euler(xRotationValue, 0, 0);
        }

        float elapsedTime = 0f;
        while (elapsedTime < moveDelay)
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / moveDelay);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 자연스러운 회전 애니메이션
        if (!rollDiceType)
        {
            elapsedTime = 0f;
            while (elapsedTime < rotationTime / (rotationTime / 3))
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(xRotationValue, 0, 0), elapsedTime / rotationTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        } 

        transform.position = targetPosition;
        transform.rotation = targetRotation;

        elapsedTime = 0f;
        while (elapsedTime < moveDelay)
        {
            transform.position = Vector3.Lerp(targetPosition, originalPosition, elapsedTime / moveDelay);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, elapsedTime / moveDelay);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;
        transform.rotation = Quaternion.identity;

        isMove = false;
    }
}