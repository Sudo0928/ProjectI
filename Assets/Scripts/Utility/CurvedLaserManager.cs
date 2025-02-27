using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiTargetLaserManager : MonoBehaviour
{
    [Header("타겟 스폰 설정")]
    public GameObject targetPrefab;         // 타겟 프리팹 (Collider와 "Target" 태그 필수)
    public int numberOfTargets = 5;           // 생성할 타겟 개수
    public Vector3 spawnArea = new Vector3(10, 5, 10);  // 타겟 생성 영역

    [Header("레이저 설정")]
    [Tooltip("순서대로 연결할 타겟들의 Transform")]
    public List<Transform> targetPoints = new List<Transform>(); // 타겟 오브젝트를 순서대로 넣을 리스트
    public int segmentsPerCurve = 20;         // 각 구간 당 샘플링할 점의 개수
    public float collisionRadius = 0.2f;      // 각 점에서 충돌 판정을 위한 구 반지름

    private LineRenderer lineRenderer;

    void Awake()
    {
        // LineRenderer 컴포넌트 추가 및 기본 설정
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
    }

    void Start()
    {
        // 타겟 프리팹이 할당되어 있으면 spawnArea 내에 타겟 오브젝트 생성 후 리스트에 추가
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
                // 생성 순서대로 리스트에 추가
                targetPoints.Add(target.transform);
            }
        }
        else
        {
            Debug.LogWarning("타겟 프리팹이 할당되지 않았습니다.");
        }
    }

    // Catmull-Rom 스플라인 함수
    Vector3 CatmullRom(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        return 0.5f * ((2f * p1) +
            (-p0 + p2) * t +
            (2f * p0 - 5f * p1 + 4f * p2 - p3) * t * t +
            (-p0 + 3f * p1 - 3f * p2 + p3) * t * t * t);
    }

    void Update()
    {
        // 타겟이 2개 이상 있어야 경로를 생성할 수 있음
        if (targetPoints.Count < 2)
            return;

        List<Vector3> curvePoints = new List<Vector3>();

        // 각 인접한 타겟 구간마다 Catmull-Rom 스플라인 보간
        for (int i = 0; i < targetPoints.Count - 1; i++)
        {
            Vector3 p1 = targetPoints[i].position;
            Vector3 p2 = targetPoints[i + 1].position;

            // 양 끝 경계에서는 p0, p3를 중복 처리
            Vector3 p0 = (i == 0) ? p1 : targetPoints[i - 1].position;
            Vector3 p3 = (i + 2 < targetPoints.Count) ? targetPoints[i + 2].position : p2;

            // 각 구간을 segmentsPerCurve로 샘플링
            for (int j = 0; j <= segmentsPerCurve; j++)
            {
                float t = j / (float)segmentsPerCurve;
                Vector3 point = CatmullRom(t, p0, p1, p2, p3);
                curvePoints.Add(point);

                // 각 점에서 충돌 판정 (OverlapSphere)
                Collider[] hitColliders = Physics.OverlapSphere(point, collisionRadius);
                foreach (var hitCollider in hitColliders)
                {
                    if (hitCollider.CompareTag("Target"))
                    {
                        Debug.Log("타겟 발견: " + hitCollider.name + " at position " + point);
                    }
                }
            }
        }

        // 계산된 점들로 LineRenderer 업데이트
        lineRenderer.positionCount = curvePoints.Count;
        lineRenderer.SetPositions(curvePoints.ToArray());
    }

    // 에디터에서 경로를 미리 확인할 수 있도록 Gizmos로 표시 (선택사항)
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
