using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMonsterController : MonsterBasic
{
    [SerializeField, Tooltip("몬스터 대기 시간")] private float idleTime = 0.3f;
    [SerializeField, Tooltip("몬스터 이동 시간")] private float moveTime = 0.5f;

    [SerializeField, Tooltip("몬스터 이동속도")] protected float moveSpeed = 2;

    // 한 상태가 지속되는 시간(추적 제외)
    protected float stateChangeTime = 0f;

    // 몬스터 이동 방향
    private Vector2 moveDir = Vector2.zero;

    // 애니메이터 해시값
    readonly private int IsMoveHash = Animator.StringToHash("IsMove");

    void Start()
    {
        base.Start();
        // 대기 상태로 시작
        stateChangeTime = idleTime;
    }

    void Update()
    {
        // 몬스터의 감지 범위에 플레이어가 있다면 추적 모드로
        if (monsterState != MonsterState.Trace)
        {
            // 한 상태의 유지 시간이 남아있다면
            if(stateChangeTime > 0)
            {
                // 상태 유지 시간 감소
                stateChangeTime -= Time.deltaTime;
            }
        else
            {// 상태 유지 시간이 끝났다면
                // 이번 상태가 아니었던 상태로 전환
                monsterState = monsterState == MonsterState.Move ? MonsterState.Idle : MonsterState.Move;
                stateChangeTime = monsterState == MonsterState.Idle ? idleTime : moveTime;
            }
        }

        switch(monsterState)
        {
            case MonsterState.Idle:
                anim.SetBool(IsMoveHash, false);
                rigid.velocity = Vector2.zero;
                break;

            case MonsterState.Move:
                MonsterMove();
                break;

            case MonsterState.Trace:
                MonsterTrace();
                break;
        }
    }

    void MonsterMove()
    {
        anim.SetBool(IsMoveHash, true);

        if (rigid.velocity == Vector2.zero)
        {
            float dirX = Random.Range(-1f, 1f);
            float dirY = Random.Range(-1f, 1f);
            moveDir = new Vector2(dirX, dirY).normalized;
            rigid.velocity = moveDir * moveSpeed;
        }
        else
        {
            Vector2 v = rigid.velocity;
            v.x = v.x - Time.deltaTime < 0 ? v.x + Time.deltaTime : v.x - Time.deltaTime;
            v.y = v.y - Time.deltaTime < 0 ? v.y + Time.deltaTime : v.y - Time.deltaTime;
            rigid.velocity = v;
        }
    }

    void MonsterTrace()
    {
        // 플레이어의 방향으로 이동
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 오브젝트가 플레이어라면 추적 모드로
    }
}