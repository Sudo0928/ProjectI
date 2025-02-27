using UnityEngine;
using System.Collections;

public class ImageAnimation : MonoBehaviour
{
    public float moveDuration = 1f;  // �̹����� ������ �ð�
    public float returnDuration = 1f; // ���� �ڸ��� ���ư��� �ð�
    public float scaleDuration = 0.2f;  // ��׷����� ������ ���� �ð�
    public Vector3 leftOffScreenPosition = new Vector3(-10f, 0f, 0f);  // ���� ȭ�� �� ��ġ
    public Vector3 rightOffScreenPosition = new Vector3(10f, 0f, 0f);  // ������ ȭ�� �� ��ġ
    public Vector3 targetPosition;  // ���� �ڸ�
    public float maxScale = 1.2f;  // ��׷��� ũ��

    private Vector3 originalScale;  // ���� ũ��
    private bool isMoving = false;  // �ִϸ��̼� ���� �� ����

    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;
        targetPosition = transform.position;

        // ���÷� ���ʿ��� ������ �ִϸ��̼� ����
        StartCoroutine(AnimateImageFromLeft());
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
        Vector3 squishedScale = new Vector3(maxScale, 1f, 1f); // ��׷��� ����
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
        Vector3 squishedScale = new Vector3(maxScale, 1f, 1f); // ��׷��� ����
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

        isMoving = false;  // �ִϸ��̼� �Ϸ�
    }

    // �Ȱ��� �̹��� �ִϸ��̼�
    IEnumerator AnimateFogImage()
    {
        if (isMoving) yield break;  // �ִϸ��̼��� ���� ���̸� �ߺ� ���� ����
        isMoving = true;

        // 1. ũ�� ��ȭ�� ȸ�� (ū ���¿��� �۾����� ȸ��)
        float elapsedTime = 0f;
        Vector3 startScale = new Vector3(2f, 2f, 1f); // ū ����
        Quaternion startRotation = Quaternion.Euler(0f, 0f, 180f); // �ݹ��� ȸ��
        while (elapsedTime < moveDuration)
        {
            transform.localScale = Vector3.Lerp(startScale, originalScale, elapsedTime / moveDuration);
            transform.rotation = Quaternion.Lerp(startRotation, Quaternion.identity, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 2. ���� ũ��� ȸ������ ���ƿ��� �ִϸ��̼�
        transform.localScale = originalScale;
        transform.rotation = Quaternion.identity;

        isMoving = false;  // �ִϸ��̼� �Ϸ�
    }
}
