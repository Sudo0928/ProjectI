using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerController : MonsterBasic
{
    private SpriteRenderer renderer;

    [SerializeField, Tooltip("차저 이동 속도")] private float moveSpeed = 3f;
    [SerializeField, Tooltip("차저 돌진 속도")] private float dashSpeed = 8f;
    [SerializeField, Tooltip("차저 방향 전환 시간 최소값")] private float minChangeDirectionTime = 0.5f;
    [SerializeField, Tooltip("차저 방향 전환 시간 최대값")] private float maxChangeDirectionTime = 1f;
    [SerializeField, Tooltip("플레이어를 감지하는 레이 길이")] private float eyeSight = 1f;

    private Vector2 moveDir = Vector2.zero;

    // 차저가 한 방향으로 이동하는 시간
    private float changeDirectionTime = 0;

    // 애니메이터 해시 값들
    private readonly int IsDash = Animator.StringToHash("IsDash");
    private readonly int MoveX = Animator.StringToHash("MoveX");
    private readonly int MoveY = Animator.StringToHash("MoveY");

    void Start()
    {
        // 기본적으로 Idle로 되어있기 때문에
        // 대기 상태가 없는 Charger는 Move로 바꿔주고 시작
        monsterState = MonsterState.Move;
        changeDirectionTime = Random.Range(minChangeDirectionTime, maxChangeDirectionTime);
        renderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        if (monsterState == MonsterState.Move)
        {
            if (changeDirectionTime > 0)
            {
                changeDirectionTime -= Time.deltaTime;
            }
            else
            {
                changeDirectionTime = Random.Range(minChangeDirectionTime, maxChangeDirectionTime);
                SetMoveDirection();
            }
        }

        switch (monsterState)
        {
            case MonsterState.Move:
                ChargerMove();
                break;

            case MonsterState.Trace:
                ChargerDash();
                break;
        }
    }

    private void FixedUpdate()
    {
        if (moveDir == Vector2.zero) return;

        // 플레이어 구분이 아직 없음
        // 지금 상태로는 벽, 장애물을 향해서도 돌진
        if(moveDir.x != 0)
        {
            if(Physics2D.Raycast(transform.position, Vector2.up, eyeSight).transform.gameObject.CompareTag("Player"))
            {
                SetDashDirection(Vector2.up);
                return;
            }
            else if(Physics2D.Raycast(transform.position, Vector2.down, eyeSight))
            {
                SetDashDirection(Vector2.down);
                return;
            }
        }
        else
        {
            if(Physics2D.Raycast(transform.position, Vector2.right, eyeSight))
            {
                SetDashDirection(Vector2.right);
                return;
            }
            else if(Physics2D.Raycast(transform.position, Vector2.left, eyeSight))
            {
                SetDashDirection(Vector2.left);
                return;
            }
        }

        if(Physics2D.Raycast(transform.position, moveDir, eyeSight))
        {
            SetDashDirection(moveDir);
            return;
        }
    }

    void ChargerMove()
    {
        rigid.velocity = moveDir * moveSpeed;
    }

    void ChargerDash()
    {
        rigid.velocity = moveDir * dashSpeed;
    }

    void SetMoveDirection()
    {
        int dir = Random.Range(0, 4);

        switch (dir)
        {
            case 0:
                moveDir = Vector2.right;
                break;

                case 1:
                moveDir = Vector2.left;
                break;

                case 2:
                moveDir = Vector2.up;
                break;

                case 3:
                moveDir = Vector2.down;
                break;
        }

        anim.SetFloat(MoveX, moveDir.x);
        anim.SetFloat(MoveY, moveDir.y);
        SetFlipX();
    }

    void SetDashDirection(Vector2 _dir)
    {
        moveDir = _dir;
        monsterState = MonsterState.Trace;
        anim.SetBool(IsDash, true);
        anim.SetFloat(MoveX, moveDir.x);
        anim.SetFloat(MoveY, moveDir.y);
        SetFlipX();
    }

    void SetFlipX()
    {
        if(moveDir.x < 0)
        {
            renderer.flipX = true;
        }
        else
        {
            renderer.flipX = false;
        }
    }
}