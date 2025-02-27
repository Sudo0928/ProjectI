using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyplumController : MonsterBasic
{
    [SerializeField, Tooltip("����ü ������")] private GameObject bullet;
    [SerializeField, Tooltip("���̺� �÷��� �÷��̾ ���� �̵��ϴ� �ӵ�")] private float moveSpeed = 0.5f;
    [SerializeField, Tooltip("����3�� ���� �ӵ�")] private float pattern3DashSpeed = 3f;
    [SerializeField, Tooltip("���� ������ �ϴ� �ּ� �ð�")] private float minAttackTime = 2f;
    [SerializeField, Tooltip("���� ������ �ϴ� �ִ� �ð�")] private float maxAttackTime = 4f;
    [SerializeField, Tooltip("�� ���̾�")] private LayerMask wallLayer;

    // �÷��̾� ��ġ
    private Transform playerTrs;
    private SpriteRenderer renderer;

    // �̵� ����
    private Vector2 moveDir = Vector2.zero;
    // ���� ���ݱ��� ���� �ð�
    private float nextAttackTime;
    // ����3���� �������ΰ�?
    private bool pattern3 = false;

    // �ִϸ����� �ؽ�
    private readonly int Pattern_1 = Animator.StringToHash("Pattern_1");
    private readonly int Pattern_2 = Animator.StringToHash("Pattern_2");
    private readonly int Pattern_3 = Animator.StringToHash("Pattern_3");


    void Start()
    {
        base.Start();
        renderer = GetComponentInChildren<SpriteRenderer>();
        playerTrs = GameObject.FindAnyObjectByType<Player>().transform;
        nextAttackTime = Random.Range(minAttackTime, maxAttackTime);
    }

    void Update()
    {
        if( monsterState == MonsterState.Move && nextAttackTime > 0f)
        {
            nextAttackTime -= Time.deltaTime;
        }
        else
        {
            nextAttackTime = Random.Range(minAttackTime, maxAttackTime);
            monsterState = MonsterState.Attack;
        }

        switch(monsterState)
        {
            case MonsterState.Move:
                PlumMove();
                break;

                case MonsterState.Attack:
                ChoiceAttackPattern();
                break;
        }
    }

    private void PlumMove()
    {
        moveDir = (playerTrs.position - transform.position).normalized;
        rigid.velocity = moveDir * moveSpeed;
    }

    private void ChoiceAttackPattern()
    {
        int patternNum = Random.Range(0, 3);

        switch(patternNum)
        {
            case 0:
                PlumPattern1();
                break;

            case 1:
                PlumPattern2();
                break;

            case 2:
                moveDir = Vector2.zero;
                PlumPattern3();
                break;
        }
    }

    private void PlumPattern1()
    {
        // �������� ����ü �߻�
    }

    private void PlumPattern2()
    {
        PlumMove();
        SetPlumFlipX();

        // ȸ���ϸ鼭 ����ü �߻�
    }

    private void PlumPattern3()
    {
        if(moveDir == Vector2.zero)
        {
            float dirX = Random.Range(-1f, 1f);
            float dirY = Random.Range(-1f, 1f);
            moveDir = new Vector2(dirX, dirY);
            SetPlumFlipX();
        }

        rigid.velocity = moveDir * pattern3DashSpeed;
        
        // �����ϸ鼭 ����ü ��Ѹ���
    }

    // ��������Ʈ ������ �ø� ����
    void SetPlumFlipX()
    {
        if(moveDir.x > 0)
        {
            renderer.flipX = false;
        }
        else
        {
            renderer.flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == wallLayer)
        {
            moveDir = -moveDir;
            renderer.flipX = !renderer.flipX;
        }
    }
}
