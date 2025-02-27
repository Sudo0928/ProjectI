using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tear : BaseAttackHandler
{
    private float virtualY = 0f;

    [SerializeField] private SpriteRenderer tearSprite;
    [SerializeField] private SpriteRenderer shadowSprite;
    [SerializeField]private Vector2 projectileHeigh = Vector2.zero;

    private List<Vector2> segments = new List<Vector2>();

    private Vector2 startPos = Vector2.zero;
    private Vector2 endPos = Vector2.zero;
    private Vector2 yHeigh = Vector2.zero;

    private float heigh = 0;

    private bool isParbolic = true;

    public override void Init(Player owner, Vector2 attackDirection)
    {
        base.Init(owner, attackDirection);

        startPos = transform.position;
        transform.localScale = Vector2.one * size;

        Vector2 worldAttackDirection = (Vector2)transform.TransformDirection(this.attackDirection);
        endPos = startPos + (worldAttackDirection * distance);

        heigh = Vector2.Distance(startPos, endPos) * 0.5f;
    }

    protected override void Start()
    {
        base.Start();

        if(isParbolic)
        {
            Vector2 pos = tearSprite.transform.position;
            pos.y -= 0.5f;
            tearSprite.transform.position = pos;
            StartCoroutine(MoveAlongParabola(startPos, endPos - new Vector2(0, 0.2f), lerpTime));
            StartCoroutine(MoveInStraightLine(startPos, endPos, lerpTime));
        }
        else
        {
            StartCoroutine(MoveInStraightLine(startPos, endPos, lerpTime));
        }
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        
    }

    public List<Vector2> GetSegmentsPerCurve(List<Vector2> targets, int segmentCount)
    {
        // 타겟이 2개 이상 있어야 경로를 생성할 수 있음
        if (targets.Count < 2)
            return null;

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

        return curvePoints;
    }

    protected Vector3 CatmullRom(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        return 0.5f * ((2f * p1) +
            (-p0 + p2) * t +
            (2f * p0 - 5f * p1 + 4f * p2 - p3) * t * t +
            (-p0 + 3f * p1 - 3f * p2 + p3) * t * t * t);
    }

    IEnumerator MoveInStraightLine(Vector2 startPos, Vector2 endPos, float lerpTime)
    {
        float elapsed = 0f;
        while (elapsed < lerpTime)
        {
            float t = elapsed / lerpTime;
            // 선형 보간으로 위치를 계산하여 업데이트
            transform.position = Vector2.Lerp(startPos, endPos, t);
            if(lerpTime - elapsed < 0.2f) tearSprite.transform.position += Vector3.down * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
        // 최종 위치로 보정
        transform.position = endPos;

        Remove();
    }

    IEnumerator MoveAlongParabola(Vector2 startPos, Vector2 endPos, float lerpTime)
    {
        float elapsed = 0f;
        while (elapsed < lerpTime)
        {
            float t = elapsed / lerpTime;
            // A와 B 사이의 선형 보간
            Vector2 currentPos = Vector2.Lerp(startPos, endPos, t);
            // 포물선 오프셋: t=0과 t=1에서 0, t=0.5에서 arcHeight가 최대값
            float offset = 4f * heigh * t * (1 - t);
            // 여기서는 단순히 Y축에 오프셋을 추가합니다.
            currentPos.y += offset;
            tearSprite.transform.position = currentPos;

            elapsed += Time.deltaTime;
            yield return null;
        }
        // 이동 완료 후 보정
        tearSprite.transform.position = endPos;

        Remove();
    }

    public void Remove()
    {
        TearDestroyEvent tearDestroyEvent = new TearDestroyEvent(this);
        EventManager.DispatchEvent(tearDestroyEvent);

        Destroy(gameObject);
    }
}
