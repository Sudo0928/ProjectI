using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMonsterController : MonsterBasic
{
    [SerializeField, Tooltip("몬스터 대기 시간")] private float idleTime = 0.3f;
    [SerializeField, Tooltip("몬스터 이동 시간")] private float moveTime = 0.5f;

    [SerializeField, Tooltip("몬스터 이동속도")] protected float moveSpeed = 2;

    protected float stateChangeTime = 0f;

    private Vector2 moveDir = Vector2.zero;

    readonly private int IsMoveHash = Animator.StringToHash("IsMove");

    void Start()
    {
        base.Start();
        stateChangeTime = idleTime;
    }

    void Update()
    {
        // 몬스터의 감지 범위에 플레이어가 있다면 거리에 따라서 추적 or 공격 모드로
        // 어쩌면 따로 공격 모드는 필요 없을지도?
        if (monsterState != MonsterState.Trace)
        {
            if(stateChangeTime > 0)
            {
                stateChangeTime -= Time.deltaTime;
            }
        else
            {
                stateChangeTime = monsterState == MonsterState.Idle ? idleTime : moveTime;
                monsterState = monsterState == MonsterState.Move ? MonsterState.Idle : MonsterState.Move;
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
            float dirX = UnityEngine.Random.Range(-1f, 1f);
            float dirY = UnityEngine.Random.Range(-1f, 1f);
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
        
    }
}