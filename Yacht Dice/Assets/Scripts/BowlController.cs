using System.Collections;
using UnityEngine;

public class BowlController : MonoBehaviour
{
    private Vector3 originalPosition;
    public float zMoveDistance;
    public float delay;
    public bool isReady;
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
                StartCoroutine(ApplyForceCoroutine());
            }
        }
    }

    private IEnumerator ApplyForceCoroutine()
    {
        isMove = true;

        Vector3 targetPosition = originalPosition + new Vector3(0, 0, zMoveDistance);

        // 지정된 시간 동안 이동합니다.
        float elapsedTime = 0f;
        while (elapsedTime < delay)
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / delay);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;

        // 원래 위치로 부드럽게 되돌아가기
        elapsedTime = 0f;
        while (elapsedTime < delay)
        {
            transform.position = Vector3.Lerp(targetPosition, originalPosition, elapsedTime / delay);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 최종적으로 원래 위치로 설정합니다.
        transform.position = originalPosition;

        isMove = false;
    }

    private IEnumerator ThrowDice()
    {
        yield return null;
    }
}