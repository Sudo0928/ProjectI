using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMonsterController : MonsterBasic
{
    [SerializeField, Tooltip("몬스터의 상대가 변경되는 최소 시간")] protected float minStateChangeTime = 1f;
    [SerializeField, Tooltip("몬스터의 상태가 변경되는 최대 시간")] protected float maxStateChangeTime = 3f;

    [SerializeField, Tooltip("몬스터 이동속도")] protected float moveSpeed = 2;

    protected float stateChangeTime = 0f;

    private Vector2 moveDir = Vector2.zero;

    void Start()
    {
        base.Start();
        stateChangeTime = UnityEngine.Random.Range(minStateChangeTime, maxStateChangeTime);
    }

    void Update()
    {
        // 몬스터의 감지 범위에 플레이어가 있다면 
        if (monsterState != MonsterState.Trace && monsterState != MonsterState.Attack)
        {
            if(stateChangeTime > 0)
            {
                stateChangeTime -= Time.deltaTime;
            }
        else
            {
                stateChangeTime = UnityEngine.Random.Range(minStateChangeTime, maxStateChangeTime);
                monsterState = UnityEngine.Random.Range(0, 2) == 0 ? MonsterState.Idle : MonsterState.Move;
            }
        }

        switch(monsterState)
        {
            case MonsterState.Idle:
                rigid.velocity = Vector2.zero;
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
        if (rigid.velocity != Vector2.zero)
        {
            float dirX = UnityEngine.Random.Range(0f, 1f);
            float dirY = UnityEngine.Random.Range(0f, 1f);
            moveDir = new Vector2(dirX, dirY).normalized;
            rigid.velocity = moveDir * moveSpeed;
        }
        else
        {
            Vector2 v = rigid.velocity;
            v.x = v.x - Time.deltaTime < 0 ? 0 : v.x - Time.deltaTime;
            v.y = v.y - Time.deltaTime < 0 ? 0 : v.y - Time.deltaTime;
            rigid.velocity = v;
        }
    }

    void MonsterAttack()
    {
        
    }

    void MonsterTrace()
    {
        
    }
}