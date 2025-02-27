using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiTargetLaserManager : MonoBehaviour
{
    [Header("Ÿ�� ���� ����")]
    public GameObject targetPrefab;         // Ÿ�� ������ (Collider�� "Target" �±� �ʼ�)
    public int numberOfTargets = 5;           // ������ Ÿ�� ����
    public Vector3 spawnArea = new Vector3(10, 5, 10);  // Ÿ�� ���� ����

    [Header("������ ����")]
    [Tooltip("������� ������ Ÿ�ٵ��� Transform")]
    public List<Transform> targetPoints = new List<Transform>(); // Ÿ�� ������Ʈ�� ������� ���� ����Ʈ
    public int segmentsPerCurve = 20;         // �� ���� �� ���ø��� ���� ����
    public float collisionRadius = 0.2f;      // �� ������ �浹 ������ ���� �� ������

    private LineRenderer lineRenderer;

    void Awake()
    {
        // LineRenderer ������Ʈ �߰� �� �⺻ ����
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
    }

    void Start()
    {
        // Ÿ�� �������� �Ҵ�Ǿ� ������ spawnArea ���� Ÿ�� ������Ʈ ���� �� ����Ʈ�� �߰�
        if (targetPrefab != null)
        {
            for (int i = 0; i < numberOfTargets; i++)
            {
                Vector3 randomPos = new Vector3(
                    Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
                    Random.Range(0, spawnArea.y),
                    Random.Range(-spawnArea.z / 2, spawnArea.z / 2)
                );
                GameObject target = Instantiate(targetPrefab, randomPos, Quaternion.identity);
                target.name = "Target_" + i;
                // ���� ������� ����Ʈ�� �߰�
                targetPoints.Add(target.transform);
            }
        }
        else
        {
            Debug.LogWarning("Ÿ�� �������� �Ҵ���� �ʾҽ��ϴ�.");
        }
    }

    // Catmull-Rom ���ö��� �Լ�
    Vector3 CatmullRom(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        return 0.5f * ((2f * p1) +
            (-p0 + p2) * t +
            (2f * p0 - 5f * p1 + 4f * p2 - p3) * t * t +
            (-p0 + 3f * p1 - 3f * p2 + p3) * t * t * t);
    }

    void Update()
    {
        // Ÿ���� 2�� �̻� �־�� ��θ� ������ �� ����
        if (targetPoints.Count < 2)
            return;

        List<Vector3> curvePoints = new List<Vector3>();

        // �� ������ Ÿ�� �������� Catmull-Rom ���ö��� ����
        for (int i = 0; i < targetPoints.Count - 1; i++)
        {
            Vector3 p1 = targetPoints[i].position;
            Vector3 p2 = targetPoints[i + 1].position;

            // �� �� ��迡���� p0, p3�� �ߺ� ó��
            Vector3 p0 = (i == 0) ? p1 : targetPoints[i - 1].position;
            Vector3 p3 = (i + 2 < targetPoints.Count) ? targetPoints[i + 2].position : p2;

            // �� ������ segmentsPerCurve�� ���ø�
            for (int j = 0; j <= segmentsPerCurve; j++)
            {
                float t = j / (float)segmentsPerCurve;
                Vector3 point = CatmullRom(t, p0, p1, p2, p3);
                curvePoints.Add(point);

                // �� ������ �浹 ���� (OverlapSphere)
                Collider[] hitColliders = Physics.OverlapSphere(point, collisionRadius);
                foreach (var hitCollider in hitColliders)
                {
                    if (hitCollider.CompareTag("Target"))
                    {
                        Debug.Log("Ÿ�� �߰�: " + hitCollider.name + " at position " + point);
                    }
                }
            }
        }

        // ���� ����� LineRenderer ������Ʈ
        lineRenderer.positionCount = curvePoints.Count;
        lineRenderer.SetPositions(curvePoints.ToArray());
    }

    // �����Ϳ��� ��θ� �̸� Ȯ���� �� �ֵ��� Gizmos�� ǥ�� (���û���)
    void OnDrawGizmos()
    {
        if (targetPoints == null || targetPoints.Count < 2)
            return;

        Gizmos.color = Color.green;
        List<Vector3> curvePoints = new List<Vector3>();
        for (int i = 0; i < targetPoints.Count - 1; i++)
        {
            Vector3 p1 = targetPoints[i].position;
            Vector3 p2 = targetPoints[i + 1].position;
            Vector3 p0 = (i == 0) ? p1 : targetPoints[i - 1].position;
            Vector3 p3 = (i + 2 < targetPoints.Count) ? targetPoints[i + 2].position : p2;
            for (int j = 0; j <= segmentsPerCurve; j++)
            {
                float t = j / (float)segmentsPerCurve;
                Vector3 point = CatmullRom(t, p0, p1, p2, p3);
                curvePoints.Add(point);
            }
        }
        for (int i = 0; i < curvePoints.Count - 1; i++)
        {
            Gizmos.DrawLine(curvePoints[i], curvePoints[i + 1]);
        }
    }
}
