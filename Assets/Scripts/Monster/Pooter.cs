using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooter : MonsterBasic
{
    [SerializeField, Tooltip("투사체 프리팹")] private GameObject bullet;

    [SerializeField, Tooltip("푸터 이동 속도")] private float moveSpeed = 0.5f;
    [SerializeField, Tooltip("푸터 공격 범위")] private float attackRange = 5f;
    [SerializeField, Tooltip("플레이어에게 접근하는 걸 멈출 최소 거리")] private float stopDistance = 2f;
    [SerializeField, Tooltip("공격을 후 대기 시간")] private float attackRestTime = 2f;
    [SerializeField, Tooltip("움직이는 시간")] private float moveTime = 0.2f;
    [SerializeField, Range(0f, 1f)] private float randomMaxX = 0.5f;
    [SerializeField, Range(0f, 1f)] private float randomMaxY = 0.5f;

    // 플레이어 위치
    private Transform playerTrs;

    // 공격 대기 시간 체크 용도
    private float checkRestTime = 0f;
    // 이동 시간 체크 용도
    private float checkMoveTime = 0f;

    void Start()
    {
        base.Start();
        playerTrs = GameObject.FindAnyObjectByType<Player>().transform;
        monsterState = MonsterState.Trace;
        checkMoveTime = moveTime;
        checkRestTime = attackRestTime;
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, playerTrs.position);
        PooterTrace(distance);
    }

    // Pooter의 전체적인 행동
    private void PooterTrace(float _distance)
    {
        if (_distance < attackRange)
        {
            if (checkRestTime > 0f)
            {
                checkRestTime -= Time.deltaTime;
            }
            else
            {
                PooterShot();
            }
        }

        if (_distance > stopDistance)
        {
            if (checkMoveTime > 0f)
            {
                checkMoveTime -= Time.deltaTime;
                
                Vector2 direction = playerTrs.position - transform.position;
                float randomX = Random.Range(0f, randomMaxX);
                float randomY = Random.Range(0f, randomMaxY);
                direction.x = direction.x > 0f ? direction.x + randomX : direction.x - randomX;
                direction.y = direction.y > 0f ? direction.y + randomY : direction.y - randomY;

                rigid.velocity = direction * moveSpeed;
            }
            else
            {
                checkMoveTime = moveTime;
                rigid.velocity = Vector2.zero;
            }
        }
        else
        {
            rigid.velocity = Vector2.zero;
        }
    }

    private void PooterShot()
    {
        checkRestTime = attackRestTime;
        // 투사체 발사
    }
}
