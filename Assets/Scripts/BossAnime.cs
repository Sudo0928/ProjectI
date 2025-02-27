using UnityEngine;
using System.Collections;

public class ImageAnimation : MonoBehaviour
{
    public enum AnimationType { FromLeft, FromRight, FogAnimation }
    public float rotationDuration = 2f;  // 2�� ���� 2���� ȸ��
    public AnimationType animationType;  // ������ �ִϸ��̼� Ÿ��
    public float moveDuration = 0.5f;  // �̹����� ������ �ð� (������ ����)
    public float returnDuration = 0.5f; // ���� �ڸ��� ���ư��� �ð� (������ ����)
    public float scaleDuration = 0.1f;  // ��׷����� ������ ���� �ð�
    public float scaleDuration2 = 3f;  // ��׷����� ������ ���� �ð�
    public float maxScale = 1.05f;  // ��׷��� ũ��
    public GameObject fogImage;         // ȸ���ϴ� �Ȱ� �̹���
    public Vector3 leftOffScreenPosition = new Vector3(-15f, 0f, 0f);  // ���� ȭ�� �� ��ġ
    public Vector3 rightOffScreenPosition = new Vector3(15f, 0f, 0f);  // ������ ȭ�� �� ��ġ
    public Vector3 targetPosition;  // ���� �ڸ�
    private Vector3 originalScale;  // ���� ũ��

    private bool isMoving = false;  // �ִϸ��̼� ���� �� ����
    private bool hasRotated = false; // ȸ�� �ִϸ��̼��� �������� ����

    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;
        targetPosition = transform.position;

        // �ִϸ��̼� ������ ���� ����
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

    // ���ʿ��� �̹����� ������ �ִϸ��̼�
    IEnumerator AnimateImageFromLeft()
    {
        if (isMoving) yield break;  // �ִϸ��̼��� ���� ���̸� �ߺ� ���� ����
        isMoving = true;

        // 1. ���ʿ��� ���������� ������ �ִϸ��̼�
        float elapsedTime = 0f;
        Vector3 startPosition = leftOffScreenPosition;
        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition; // ��Ȯ�� ��ġ ����

        // 2. ��׷��� �ִϸ��̼� (�¿� ��׷���)
        elapsedTime = 0f;
        Vector3 squishedScale = new Vector3(1f, 1f, 1f); // ��׷��� ����
        while (elapsedTime < scaleDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, squishedScale, elapsedTime / scaleDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 3. ���� ũ��� ���ƿ��� �ִϸ��̼�
        elapsedTime = 0f;
        while (elapsedTime < scaleDuration)
        {
            transform.localScale = Vector3.Lerp(squishedScale, originalScale, elapsedTime / scaleDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale;

        // 4. ���� �ڸ��� ���ư��� �ִϸ��̼�
        elapsedTime = 0f;
        Vector3 returnPosition = transform.position;
        while (elapsedTime < returnDuration)
        {
            transform.position = Vector3.Lerp(returnPosition, targetPosition, elapsedTime / returnDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;

        // 5. ���� �ڸ����� ���������� ���� �� �̵�
        elapsedTime = 0f;

        Vector3 rightMovePosition = targetPosition + new Vector3(50f, 0f, 0f); // ���������� ���� �̵�
        while (elapsedTime < scaleDuration2)
        {
            transform.position = Vector3.Lerp(targetPosition, rightMovePosition, elapsedTime / scaleDuration2);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = rightMovePosition;

        isMoving = false;  // �ִϸ��̼� �Ϸ�
    }

    // �����ʿ��� �̹����� ������ �ִϸ��̼�
    IEnumerator AnimateImageFromRight()
    {
        if (isMoving) yield break;  // �ִϸ��̼��� ���� ���̸� �ߺ� ���� ����
        isMoving = true;

        // 1. �����ʿ��� �������� ������ �ִϸ��̼�
        float elapsedTime = 0f;
        Vector3 startPosition = rightOffScreenPosition;
        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition; // ��Ȯ�� ��ġ ����

        // 2. ��׷��� �ִϸ��̼� (�¿� ��׷���)
        elapsedTime = 0f;
        Vector3 squishedScale = new Vector3(1f, 1f, 1f); // ��׷��� ����
        while (elapsedTime < scaleDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, squishedScale, elapsedTime / scaleDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 3. ���� ũ��� ���ƿ��� �ִϸ��̼�
        elapsedTime = 0f;
        while (elapsedTime < scaleDuration)
        {
            transform.localScale = Vector3.Lerp(squishedScale, originalScale, elapsedTime / scaleDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale;

        // 4. ���� �ڸ��� ���ư��� �ִϸ��̼�
        elapsedTime = 0f;
        Vector3 returnPosition = transform.position;
        while (elapsedTime < returnDuration)
        {
            transform.position = Vector3.Lerp(returnPosition, targetPosition, elapsedTime / returnDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;

        // 5. ���� �ڸ����� �������� ���� �� �̵�
        elapsedTime = 0f;

        Vector3 leftMovePosition = targetPosition - new Vector3(50f, 0f, 0f); // �������� ���� �̵�
        while (elapsedTime < scaleDuration2)
        {
            transform.position = Vector3.Lerp(targetPosition, leftMovePosition, elapsedTime / scaleDuration2);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = leftMovePosition;

        isMoving = false;  // �ִϸ��̼� �Ϸ�
    }


    // �Ȱ��� �̹��� �ִϸ��̼� (ȸ���ϴ� �̹���)
    IEnumerator RotateAndScaleFogImage()
    {
        
        float totalRotation = 720f;   // �� ���� ȸ�� (360 * 2)
        float maxScale = 2f;          // �ִ� ũ�� (���� ũ�⺸�� Ŀ���� ��)
        float minScale = 1f;          // �ּ� ũ�� (���� ũ��)
        float elapsedTime = 0f;

        // ���� ũ�� (���� ũ��)
        Vector3 originalScale = fogImage.transform.localScale;

        while (elapsedTime < rotationDuration)
        {
            float progress = elapsedTime / rotationDuration;

            // ũ�� ��ȭ (Lerp ���)
            float scale = Mathf.Lerp(minScale, maxScale, Mathf.PingPong(progress * 2f, 1f)); // ũ�Ⱑ Ŀ���� �۾���

            // ȸ�� ��ȭ (Lerp ���)
            float angle = Mathf.Lerp(0f, totalRotation, progress);

            // ����
            fogImage.transform.localScale = originalScale * scale;  // ũ�� ����
            fogImage.transform.rotation = Quaternion.Euler(0f, 0f, angle);  // ȸ��

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ȸ���� ũ�� ���� ����
        fogImage.transform.rotation = Quaternion.Euler(0f, 0f, totalRotation); // �� ���� ȸ�� �Ϸ�
        fogImage.transform.localScale = originalScale;  // ���� ũ��� ����
    }
}
