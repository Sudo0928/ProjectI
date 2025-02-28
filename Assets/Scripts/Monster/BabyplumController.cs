    using JetBrains.Annotations;
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

    Vector2 moveDir = Vector2.zero;
    float speed = 0.0f;

    void Start()
    {
        base.Start();
        renderer = GetComponentInChildren<SpriteRenderer>();
        playerTrs = GameObject.FindAnyObjectByType<PlayerController>().transform;
    }

    private void Update()
    {
        renderer.flipX = moveDir.x < 0;
        if (monsterState == MonsterState.Idle) 
            moveDir = (playerTrs.position - transform.position).normalized;
    
        transform.position += new Vector3(moveDir.x, moveDir.y, 0) * speed * Time.deltaTime ;

    }


    public void OnIdleMode()
    {
        OffAnims();
        monsterState = MonsterState.Idle;
        speed = 2.0f;
        
        if (shootStraight != null)
            StopCoroutine(shootStraight);

        int rand = Random.Range(0, 100);
        int nextSkill = rand < 40 ? 1 : rand < 80 ? 2 : 3;

        anim.Play("Idle"); 
        GameManager.Instance.SetTimer(()=> {
            AnimSetBool(nextSkill, true);   
            monsterState = MonsterState.Attack;
        }, Random.Range(2.0f, 3.0f));
    } 
    
    // 원형으로 투사체 발사 
    public void PlayPattern1()
    {
        StartCoroutine(ShootCircular(0, 20));
    } 


    // 회전하면서 투사체 발사
    public void PlayPattern2()
    {
        StartCoroutine(ShootCircular(1, 20));

    }

    Coroutine shootStraight;
    // 돌아댕기면서 투사체 발사
    public void PlayPattern3()
    {
        moveDir = -(playerTrs.position - transform.position).normalized;
        speed = 15.0f;
        shootStraight = StartCoroutine(ShootStraight(0.05f));
    }

    public void OffAnims() 
    {
        AnimSetBool(1, false);
        AnimSetBool(2, false);
        AnimSetBool(3, false);

    } 
    
    private void OnCollisionEnter2D(Collision2D collision)
    { 
        if (collision.gameObject.layer == 9)  
        {
            var normal = collision.contacts[0].normal; 
            if (Vector2.Dot(normal, moveDir) > 0.5f)
                return;

            moveDir = Vector2.Reflect(moveDir, normal); 
            renderer.flipX = !renderer.flipX;
        } 
    }



    public void AnimSetBool(int num, bool active)
    {
        string name = $"Pattern_{num}";
        anim.SetBool(name, active);

        if (num == 1)
            moveDir = Vector2.zero; 

        else if (num == 2)
        { 
            moveDir = (playerTrs.position - transform.position).normalized;
            speed = 3.0f;  
        }   

        else if (num == 3)
        {
        
        }
    }  

    IEnumerator ShootCircular(float time, int cnt)
    {
        float angleStep = 360f / cnt;

        for (int i = 0; i < cnt; i++)
        {
            float angle = i * angleStep;
            float angleInRadians = angle * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));
            BaseTear newTear = Instantiate(GameManager.Instance.tear, transform.position, Quaternion.identity);
            newTear.Init(gameObject, 1.0f, 6f, 10f, 0.5f, direction, false);

            if (time > 0)
                yield return new WaitForSeconds(time / cnt);
        
        }
    
        yield break;
    }


    IEnumerator ShootStraight(float time)
    {
        while(true)
        { 
            Vector2 direction = -moveDir;
            BaseTear newTear = Instantiate(GameManager.Instance.tear, transform.position, Quaternion.identity);
            newTear.Init(gameObject, 1.0f, 6f, 10f, 0.5f, direction, false);

            if (time > 0) 
                yield return new WaitForSeconds(time);  
        }
    }

}

