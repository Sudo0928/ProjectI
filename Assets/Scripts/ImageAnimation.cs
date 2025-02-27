using UnityEngine;
using System.Collections;

public class ImageAnimation : MonoBehaviour
{
    public enum AnimationType { FromLeft, FromRight, FogAnimation }
    public float rotationDuration = 2f;  // 2초 동안 2바퀴 회전
    public AnimationType animationType;  // 선택한 애니메이션 타입
    public float moveDuration = 0.5f;  // 이미지가 들어오는 시간 (빠르게 설정)
    public float returnDuration = 0.5f; // 원래 자리로 돌아가는 시간 (빠르게 설정)
    public float scaleDuration = 0.1f;  // 찌그러짐과 원상태 복귀 시간
    public float scaleDuration2 = 3f;  // 찌그러짐과 원상태 복귀 시간
    public float maxScale = 1.05f;  // 찌그러짐 크기
    public GameObject fogImage;         // 회전하는 안개 이미지
    public Vector3 leftOffScreenPosition = new Vector3(-15f, 0f, 0f);  // 왼쪽 화면 밖 위치
    public Vector3 rightOffScreenPosition = new Vector3(15f, 0f, 0f);  // 오른쪽 화면 밖 위치
    public Vector3 targetPosition;  // 원래 자리
    private Vector3 originalScale;  // 원래 크기

    private bool isMoving = false;  // 애니메이션 진행 중 여부
    private bool hasRotated = false; // 회전 애니메이션이 끝났는지 여부

    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;
        targetPosition = transform.position;

        // 애니메이션 종류에 따라 시작
        switch (animationType)
        {
            case AnimationType.FromLeft:
                StartCoroutine(AnimateImageFromLeft());
                break;

            case AnimationType.FromRight:
                StartCoroutine(AnimateImageFromRight());
                break;

            case AnimationType.FogAnimation:
                StartCoroutine(RotateAndScaleFogImage());
                break;
        }
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
        Vector3 squishedScale = new Vector3(1f, 1f, 1f); // 찌그러진 상태
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

        // 5. 원래 자리에서 오른쪽으로 조금 더 이동
        elapsedTime = 0f;

        Vector3 rightMovePosition = targetPosition + new Vector3(50f, 0f, 0f); // 오른쪽으로 조금 이동
        while (elapsedTime < scaleDuration2)
        {
            transform.position = Vector3.Lerp(targetPosition, rightMovePosition, elapsedTime / scaleDuration2);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = rightMovePosition;

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
        Vector3 squishedScale = new Vector3(1f, 1f, 1f); // 찌그러진 상태
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

        // 5. 원래 자리에서 왼쪽으로 조금 더 이동
        elapsedTime = 0f;

        Vector3 leftMovePosition = targetPosition - new Vector3(50f, 0f, 0f); // 왼쪽으로 조금 이동
        while (elapsedTime < scaleDuration2)
        {
            transform.position = Vector3.Lerp(targetPosition, leftMovePosition, elapsedTime / scaleDuration2);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = leftMovePosition;

        isMoving = false;  // 애니메이션 완료
    }


    // 안개의 이미지 애니메이션 (회전하는 이미지)
    IEnumerator RotateAndScaleFogImage()
    {
        
        float totalRotation = 720f;   // 두 바퀴 회전 (360 * 2)
        float maxScale = 2f;          // 최대 크기 (원래 크기보다 커지는 값)
        float minScale = 1f;          // 최소 크기 (원래 크기)
        float elapsedTime = 0f;

        // 시작 크기 (원래 크기)
        Vector3 originalScale = fogImage.transform.localScale;

        while (elapsedTime < rotationDuration)
        {
            float progress = elapsedTime / rotationDuration;

            // 크기 변화 (Lerp 사용)
            float scale = Mathf.Lerp(minScale, maxScale, Mathf.PingPong(progress * 2f, 1f)); // 크기가 커졌다 작아짐

            // 회전 변화 (Lerp 사용)
            float angle = Mathf.Lerp(0f, totalRotation, progress);

            // 적용
            fogImage.transform.localScale = originalScale * scale;  // 크기 변경
            fogImage.transform.rotation = Quaternion.Euler(0f, 0f, angle);  // 회전

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 회전과 크기 최종 적용
        fogImage.transform.rotation = Quaternion.Euler(0f, 0f, totalRotation); // 두 바퀴 회전 완료
        fogImage.transform.localScale = originalScale;  // 원래 크기로 복귀
    }
}
