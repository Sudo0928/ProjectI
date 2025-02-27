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
    //        // A와 B 사이의 선형 보간
    //        Vector2 currentPos = Vector2.Lerp(startPos, endPos, t);
    //        // 포물선 오프셋: t=0과 t=1에서 0, t=0.5에서 arcHeight가 최대값
    //        float offset = 4f * heigh * t * (1 - t);
    //        // 여기서는 단순히 Y축에 오프셋을 추가합니다.
    //        currentPos.y += offset;
    //        tearSprite.transform.position = currentPos;

    //        elapsed += Time.deltaTime;
    //        yield return null;
    //    }
    //    // 이동 완료 후 보정
    //    tearSprite.transform.position = endPos;
    //}
}
