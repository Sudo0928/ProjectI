using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMonsterController : MonsterBasic
{
    [SerializeField, Tooltip("몬스터의 상대가 변경되는 최소 시간")] protected float minStateChangeTime = 1f;
    [SerializeField, Tooltip("몬스터의 상태가 변경되는 최대 시간")] protected float maxStateChangeTime = 3f;
    [SerializeField, Tooltip("몬스터 상태 가지 수")] protected int stateCount = 0;

    [SerializeField, Tooltip("몬스터 이동속도")] protected float moveSpeed = 2;

    protected float stateChangeTime = 0f;

    private Vector2 moveDir = Vector2.zero;

    void Start()
    {
        base.Start();
        stateCount = Enum.GetValues(typeof(MonsterState)).Length;
        stateChangeTime = UnityEngine.Random.Range(minStateChangeTime, maxStateChangeTime);
    }

    void Update()
    {
        // 몬스터의 감지 범위에 플레이어가 있다면 
        if(monsterState != MonsterState.Trace && monsterState != MonsterState.Attack && stateChangeTime > 0)
        {
            stateChangeTime -= Time.deltaTime;
        }
        else
        {
            stateChangeTime = UnityEngine.Random.Range(minStateChangeTime, maxStateChangeTime);
        }

        switch(monsterState)
        {
            case MonsterState.Idle:
                break;

            case MonsterState.Move:
                MonsterMove();
                break;

            case MonsterState.Trace:
                MonsterTrace();
                break;

            case MonsterState.Attack:
                MonsterAttack();
                break;
        }
    }

    void MonsterMove()
    {
        float dirX = UnityEngine.Random.Range(0f, 1f);
        float dirY = UnityEngine.Random.Range(0f, 1f);
        moveDir = new Vector2(dirX, dirY).normalized;

        rigid.velocity = moveDir * moveSpeed;
    }

    void MonsterAttack()
    {
        
    }

    void MonsterTrace()
    {
        
    }
}