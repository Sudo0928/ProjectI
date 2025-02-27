using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTear : BaseAttackHandler
{
    private float virtualY = 0f;

    [SerializeField] private SpriteRenderer tearSprite;
    public SpriteRenderer TearSprite => tearSprite;

    [SerializeField] private SpriteRenderer shadowSprite;
    public SpriteRenderer ShadowSprite => shadowSprite;

    [SerializeField]private Vector2 projectileHeigh = Vector2.zero;

    [SerializeField] private LayerMask hitMask;
    public LayerMask HitMask => hitMask;

    private List<Vector2> segments = new List<Vector2>();

    private Vector2 startPos = Vector2.zero;
    private Vector2 endPos = Vector2.zero;

    private float heigh = 0;

    private bool isParbolic = true;
    public bool IsParbolic => isParbolic;

    public void Init(GameObject owner, float speed, float distance, float size, Vector2 attackDirection, bool isParbolic = false)
    {
        base.Init(owner, speed, distance, size, attackDirection);

        startPos = transform.position;
        transform.localScale = Vector2.one * size;

        Vector2 worldAttackDirection = (Vector2)transform.TransformDirection(this.attackDirection);
        endPos = startPos + (worldAttackDirection * this.distance);

        heigh = Vector2.Distance(startPos, endPos) * 0.5f;

        this.isParbolic = isParbolic;

        tearSprite.transform.localPosition = new Vector3(0, 0.4f);
    }

    protected override void Start()
    {
        base.Start();

        TearLaunchEvent tearLaunchEvent = new TearLaunchEvent(this);
        EventManager.DispatchEvent(tearLaunchEvent);

        if (isParbolic)
        {
            Vector2 pos = tearSprite.transform.position;
            pos.y -= 0.5f;
            tearSprite.transform.position = pos;
            StartCoroutine(MoveAlongParabola(startPos, endPos - new Vector2(0, 0.1f) * size, lerpTime));
            StartCoroutine(MoveInStraightLine(startPos, endPos, lerpTime));
        }
        else
        {
            StartCoroutine(MoveInStraightLine(startPos, endPos, lerpTime));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == Owner.tag) 
                return; 
         
		if (Owner.CompareTag("Monster") && collision.gameObject.CompareTag("Player"))
			collision.gameObject.GetComponent<Player>().TakeDamage(damage);
         
		if ((HitMask.value | (1 << collision.gameObject.layer)) == HitMask.value)
        {
            TearHitObstacleEvent tearHit = new TearHitObstacleEvent(this, collision, Vector2.zero);
            InvokeEvent(tearHit);
        }
    }

    IEnumerator MoveInStraightLine(Vector2 startPos, Vector2 endPos, float lerpTime)
    {
        float elapsed = 0f;
        while (elapsed < lerpTime)
        {
            float t = elapsed / lerpTime;
            // ���� �������� ��ġ�� ����Ͽ� ������Ʈ
            transform.position = Vector2.Lerp(startPos, endPos, t);
            if(lerpTime - elapsed < 0.2f) tearSprite.transform.position += Vector3.down * size * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
        // ���� ��ġ�� ����
        transform.position = endPos;

        TearDropEvent tearDrop = new TearDropEvent(this);
        InvokeEvent(tearDrop);
    }

    IEnumerator MoveAlongParabola(Vector2 startPos, Vector2 endPos, float lerpTime)
    {
        float elapsed = 0f;
        while (elapsed < lerpTime)
        {
            float t = elapsed / lerpTime;
            // A�� B ������ ���� ����
            Vector2 currentPos = Vector2.Lerp(startPos, endPos, t);
            // ������ ������: t=0�� t=1���� 0, t=0.5���� arcHeight�� �ִ밪
            float offset = 4f * heigh * t * (1 - t);
            // ���⼭�� �ܼ��� Y�࿡ �������� �߰��մϴ�.
            currentPos.y += offset;
            tearSprite.transform.position = currentPos;

            elapsed += Time.deltaTime;
            yield return null;
        }
        // �̵� �Ϸ� �� ����
        tearSprite.transform.position = endPos;
    }

    public void InvokeEvent<T>(T tearEvent) where T : Event
    {
        EventManager.DispatchEvent(tearEvent);

        Destroy(gameObject);
    }
}
