using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerController : MonsterBasic
{
    private SpriteRenderer renderer;

    [SerializeField, Tooltip("���� �̵� �ӵ�")] private float moveSpeed = 3f;
    [SerializeField, Tooltip("���� ���� �ӵ�")] private float dashSpeed = 8f;
    [SerializeField, Tooltip("���� ���� ��ȯ �ð� �ּҰ�")] private float minChangeDirectionTime = 0.5f;
    [SerializeField, Tooltip("���� ���� ��ȯ �ð� �ִ밪")] private float maxChangeDirectionTime = 1f;
    [SerializeField, Tooltip("�÷��̾ �����ϴ� ���� ����")] private float eyeSight = 1f;

    private Vector2 moveDir = Vector2.zero;

    // ������ �� �������� �̵��ϴ� �ð�
    private float changeDirectionTime = 0;

    void Start()
    {
        // �⺻������ Idle�� �Ǿ��ֱ� ������
        // ��� ���°� ���� Charger�� Move�� �ٲ��ְ� ����
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

        // �÷��̾� ������ ���� ����
        // ���� ���·δ� ��, ��ֹ��� ���ؼ��� ����
        if(moveDir.x != 0)
        {
            if(Physics2D.Raycast(transform.position, Vector2.up, eyeSight))
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
                renderer.flipX = false;
                break;

                case 1:
                moveDir = Vector2.left;
                renderer.flipX = true;
                break;

                case 2:
                moveDir = Vector2.up;
                renderer.flipX = false;
                break;

                case 3:
                moveDir = Vector2.down;
                renderer.flipX = false;
                break;
        }
    }

    void SetDashDirection(Vector2 _dir)
    {
        moveDir = _dir;
        monsterState = MonsterState.Trace;
    }
}