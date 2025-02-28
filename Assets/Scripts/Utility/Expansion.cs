using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Expansion
{
    public static T GetOrAnyComponent<T>(this GameObject obj) where T : Component
    {
        if(obj.TryGetComponent<T>(out var source))
        {
            return source;
        }
        return obj.AddComponent<T>();
    }

    public static float GetRange(this float value, float min, float max)
    {
        float random;
        do{
            random = Random.Range(min, max);
        } while (random == 0);

        return random;
    }

    public static void CatmullRom(ref this Vector2 mathf, float t, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
    {
        mathf = 0.5f * ((2f * p1) +
            (-p0 + p2) * t +
            (2f * p0 - 5f * p1 + 4f * p2 - p3) * t * t +
            (-p0 + 3f * p1 - 3f * p2 + p3) * t * t * t);
    }

    public static void GetSegmentsPerCurve(this List<Vector2> list, List<Vector2> targets, int segmentCount)
    {
        // 타겟이 2개 이상 있어야 경로를 생성할 수 있음
        if (targets.Count < 2)
            return;

        List<Vector2> curvePoints = new List<Vector2>();

        // 각 인접한 타겟 구간마다 Catmull-Rom 스플라인 보간
        for (int i = 0; i < targets.Count - 1; i++)
        {
            Vector2 p1 = targets[i];
            Vector2 p2 = targets[i + 1];

            // 양 끝 경계에서는 p0, p3를 중복 처리
            Vector2 p0 = (i == 0) ? p1 : targets[i - 1];
            Vector2 p3 = (i + 2 < targets.Count) ? targets[i + 2] : p2;

            // 각 구간을 segmentsPerCurve로 샘플링
            for (int j = 0; j <= segmentCount; j++)
            {
                float t = j / (float)segmentCount;
                Vector2 point = Vector2.zero;
                point.CatmullRom(t, p0, p1, p2, p3);
                curvePoints.Add(point);
            }
        }

        list = curvePoints;
    }
}
