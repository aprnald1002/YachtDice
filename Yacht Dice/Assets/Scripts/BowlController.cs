using System.Collections;
using UnityEngine;

public class BowlController : MonoBehaviour
{
    private Vector3 originalPosition;
    [Header("Space : 컵 흔듬, C : 주사위 부음\n")]   
    [Header("회전, 이동할 위치")]
    public float xRotationValue;
    public float zMoveDistance;

    [Header("각종 조건들")]
    [Tooltip("컵이 움직일 준비가 되어있는지")]
    public bool isReady;
    [Tooltip("컵이 움직이는지")]
    public bool isMove;

    private void Awake()
    {
        originalPosition = transform.position;
        isMove = false;
        isReady = true;
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
                StartCoroutine(RollDice(2f, false));
            }
        }
    }

    // rollDiceType : true면 주사위 흔드는거, false면 주사위 부워버림
    private IEnumerator RollDice(float moveDelay, bool rollDiceType)
    {
        isMove = true;
        Vector3 targetPosition;
        Quaternion targetRotation;
        
        
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

        if (!rollDiceType) {
            elapsedTime = 0f;
            while (elapsedTime < moveDelay)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, elapsedTime / moveDelay);
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