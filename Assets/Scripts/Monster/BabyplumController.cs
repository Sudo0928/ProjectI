using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyplumController : MonsterBasic
{
    [SerializeField, Tooltip("투사체 프리팹")] private GameObject bullet;
    [SerializeField, Tooltip("베이비 플럼이 플레이어를 향해 이동하는 속도")] private float moveSpeed = 0.5f;
    [SerializeField, Tooltip("패턴3의 돌진 속도")] private float pattern3DashSpeed = 3f;
    [SerializeField, Tooltip("다음 공격을 하는 최소 시간")] private float minAttackTime = 2f;
    [SerializeField, Tooltip("다음 공격을 하는 최대 시간")] private float maxAttackTime = 4f;
    [SerializeField, Tooltip("벽 레이어")] private LayerMask wallLayer;

    // 플레이어 위치
    private Transform playerTrs;
    private SpriteRenderer renderer;

    // 이동 방향
    private Vector2 moveDir = Vector2.zero;
    // 다음 공격까지 남은 시간
    private float nextAttackTime;
    // 공격할 패턴을 0, 1, 2로 판정
    // 공격 상태가 아닐 때는 -1을 값으로
    private int patternNum = -1;
    // 패턴3으로 돌진중인가?
    private bool pattern3 = false;

    // 애니메이터 해시
    private readonly int Pattern_1 = Animator.StringToHash("Pattern_1");
    private readonly int Pattern_2 = Animator.StringToHash("Pattern_2");
    private readonly int Pattern_3 = Animator.StringToHash("Pattern_3");
    private readonly int IsAttack = Animator.StringToHash("IsAttack");

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
            anim.SetBool(IsAttack, true);
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
        if (patternNum == -1)
        {
            patternNum = Random.Range(0, 3);
        }

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
        anim.SetBool(Pattern_1, true);
        // 원형으로 투사체 발사
    }

    private void PlumPattern2()
    {
        anim.SetBool(Pattern_2, true);
        PlumMove();
        SetPlumFlipX();

        // 회전하면서 투사체 발사
    }

    private void PlumPattern3()
    {
        if(moveDir == Vector2.zero)
        {
            anim.SetBool(Pattern_3, true);
            float dirX = Random.Range(-1f, 1f);
            float dirY = Random.Range(-1f, 1f);
            moveDir = new Vector2(dirX, dirY);
            SetPlumFlipX();
        }

        rigid.velocity = moveDir * pattern3DashSpeed;
        
        // 돌진하면서 투사체 흩뿌리기
    }

    // 스프라이트 렌더러 플립 제어
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

    public void EndAttack()
    {
        anim.SetBool(IsAttack, false);
        anim.SetBool(Pattern_1, false);
        anim.SetBool(Pattern_2, false);
        anim.SetBool(Pattern_3, false);
    }
}
