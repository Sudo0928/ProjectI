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
        // Ÿ���� 2�� �̻� �־�� ��θ� ������ �� ����
        if (targets.Count < 2)
            return;

        List<Vector2> curvePoints = new List<Vector2>();

        // �� ������ Ÿ�� �������� Catmull-Rom ���ö��� ����
        for (int i = 0; i < targets.Count - 1; i++)
        {
            Vector2 p1 = targets[i];
            Vector2 p2 = targets[i + 1];

            // �� �� ��迡���� p0, p3�� �ߺ� ó��
            Vector2 p0 = (i == 0) ? p1 : targets[i - 1];
            Vector2 p3 = (i + 2 < targets.Count) ? targets[i + 2] : p2;

            // �� ������ segmentsPerCurve�� ���ø�
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
