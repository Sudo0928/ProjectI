using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterState
{
    Idle,
    Move,
    Trace,
    Attack
}

// 이동하는 몬스터만 상속
public interface IMonsterMove
{
    [SerializeField, Tooltip("몬스터 이동속도")] protected float moveSpeed { get; set; }
    protected void Move();
}

// 추격하는 몬스터만 상속
public interface IMonsterTrace
{
    protected void Trace();
}

// 공격하는 몬스터만 상속
public interface IMonsterAttack
{
    protected void Attack();
}

public class MonsterBasic : MonoBehaviour
{
    // 몬스터 상태
    protected MonsterState monsterState = MonsterState.Idle;

    protected Rigidbody2D rigid;
    protected Animator anim;

    [SerializeField, Tooltip("몬스터의 상대가 변경되는 최소 시간")] protected float minStateChangeTime = 1f;
    [SerializeField, Tooltip("몬스터의 상태가 변경되는 최대 시간")] protected float maxStateChangeTime = 3f;
    [SerializeField, Tooltip("몬스터 상태 가지 수")] protected int stateCount = 0;

    [Space]
    [SerializeField, Tooltip("몬스터 최대 체력")] protected int monsterMaxHP = 5;
    // 몬스터 현재 체력
    protected int monsterCurrentHP = 0;
    [SerializeField, Tooltip("몬스터 공격력")] protected int monsterAtk = 1;

    protected float stateChangeTime = 0f;

    protected void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

     protected void Start()
    {
        stateCount = Enum.GetValues(typeof(MonsterState)).Length;
        stateChangeTime = UnityEngine.Random.Range(minStateChangeTime, maxStateChangeTime);
        monsterCurrentHP = monsterMaxHP;
    }

    protected void GetDamage(int _damage)
    {

    }
}
