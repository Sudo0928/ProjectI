using UnityEngine;
using System.Collections;

public class ImageAnimation : MonoBehaviour
{
    public float moveDuration = 1f;  // 이미지가 들어오는 시간
    public float returnDuration = 1f; // 원래 자리로 돌아가는 시간
    public float scaleDuration = 0.2f;  // 찌그러짐과 원상태 복귀 시간
    public Vector3 leftOffScreenPosition = new Vector3(-10f, 0f, 0f);  // 왼쪽 화면 밖 위치
    public Vector3 rightOffScreenPosition = new Vector3(10f, 0f, 0f);  // 오른쪽 화면 밖 위치
    public Vector3 targetPosition;  // 원래 자리
    public float maxScale = 1.2f;  // 찌그러짐 크기

    private Vector3 originalScale;  // 원래 크기
    private bool isMoving = false;  // 애니메이션 진행 중 여부

    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;
        targetPosition = transform.position;

        // 예시로 왼쪽에서 들어오는 애니메이션 시작
        StartCoroutine(AnimateImageFromLeft());
    }

    // 왼쪽에서 이미지가 들어오는 애니메이션
    IEnumerator AnimateImageFromLeft()
    {
        if (isMoving) yield break;  // 애니메이션이 진행 중이면 중복 실행 방지
        isMoving = true;

        // 1. 왼쪽에서 오른쪽으로 들어오는 애니메이션
        float elapsedTime = 0f;
        Vector3 startPosition = leftOffScreenPosition;
        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition; // 정확한 위치 설정

        // 2. 찌그러짐 애니메이션 (좌우 찌그러짐)
        elapsedTime = 0f;
        Vector3 squishedScale = new Vector3(maxScale, 1f, 1f); // 찌그러진 상태
        while (elapsedTime < scaleDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, squishedScale, elapsedTime / scaleDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 3. 원래 크기로 돌아오는 애니메이션
        elapsedTime = 0f;
        while (elapsedTime < scaleDuration)
        {
            transform.localScale = Vector3.Lerp(squishedScale, originalScale, elapsedTime / scaleDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale;

        // 4. 원래 자리로 돌아가는 애니메이션
        elapsedTime = 0f;
        Vector3 returnPosition = transform.position;
        while (elapsedTime < returnDuration)
        {
            transform.position = Vector3.Lerp(returnPosition, targetPosition, elapsedTime / returnDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;

        isMoving = false;  // 애니메이션 완료
    }

    // 오른쪽에서 이미지가 들어오는 애니메이션
    IEnumerator AnimateImageFromRight()
    {
        if (isMoving) yield break;  // 애니메이션이 진행 중이면 중복 실행 방지
        isMoving = true;

        // 1. 오른쪽에서 왼쪽으로 들어오는 애니메이션
        float elapsedTime = 0f;
        Vector3 startPosition = rightOffScreenPosition;
        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition; // 정확한 위치 설정

        // 2. 찌그러짐 애니메이션 (좌우 찌그러짐)
        elapsedTime = 0f;
        Vector3 squishedScale = new Vector3(maxScale, 1f, 1f); // 찌그러진 상태
        while (elapsedTime < scaleDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, squishedScale, elapsedTime / scaleDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 3. 원래 크기로 돌아오는 애니메이션
        elapsedTime = 0f;
        while (elapsedTime < scaleDuration)
        {
            transform.localScale = Vector3.Lerp(squishedScale, originalScale, elapsedTime / scaleDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale;

        // 4. 원래 자리로 돌아가는 애니메이션
        elapsedTime = 0f;
        Vector3 returnPosition = transform.position;
        while (elapsedTime < returnDuration)
        {
            transform.position = Vector3.Lerp(returnPosition, targetPosition, elapsedTime / returnDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;

        isMoving = false;  // 애니메이션 완료
    }

    // 안개의 이미지 애니메이션
    IEnumerator AnimateFogImage()
    {
        if (isMoving) yield break;  // 애니메이션이 진행 중이면 중복 실행 방지
        isMoving = true;

        // 1. 크기 변화와 회전 (큰 상태에서 작아지며 회전)
        float elapsedTime = 0f;
        Vector3 startScale = new Vector3(2f, 2f, 1f); // 큰 상태
        Quaternion startRotation = Quaternion.Euler(0f, 0f, 180f); // 반바퀴 회전
        while (elapsedTime < moveDuration)
        {
            transform.localScale = Vector3.Lerp(startScale, originalScale, elapsedTime / moveDuration);
            transform.rotation = Quaternion.Lerp(startRotation, Quaternion.identity, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 2. 원래 크기와 회전으로 돌아오는 애니메이션
        transform.localScale = originalScale;
        transform.rotation = Quaternion.identity;

        isMoving = false;  // 애니메이션 완료
    }
}
