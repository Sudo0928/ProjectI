using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brimstone : BaseAttackHandler
{
    public override void Init(GameObject owner, float speed, float distance, float size, Vector2 attackDirection)
    {
        base.Init(owner, speed, distance, size, attackDirection);
    }

    //IEnumerator MoveAlongParabola(Vector2 startPos, Vector2 endPos, float lerpTime)
    //{
    //    float elapsed = 0f;
    //    while (elapsed < lerpTime)
    //    {
    //        List<Vector2> segments = new List<Vector2>();
    //        segments.GetSegmentsPerCurve(segments, 20);
    //        float t = elapsed / lerpTime;
    //        // A�� B ������ ���� ����
    //        Vector2 currentPos = Vector2.Lerp(startPos, endPos, t);
    //        // ������ ������: t=0�� t=1���� 0, t=0.5���� arcHeight�� �ִ밪
    //        float offset = 4f * heigh * t * (1 - t);
    //        // ���⼭�� �ܼ��� Y�࿡ �������� �߰��մϴ�.
    //        currentPos.y += offset;
    //        tearSprite.transform.position = currentPos;

    //        elapsed += Time.deltaTime;
    //        yield return null;
    //    }
    //    // �̵� �Ϸ� �� ����
    //    tearSprite.transform.position = endPos;
    //}
}
