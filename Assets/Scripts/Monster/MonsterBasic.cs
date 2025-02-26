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

public class MonsterBasic : MonoBehaviour
{
    // 몬스터 상태
    [SerializeField] protected MonsterState monsterState = MonsterState.Idle;

    protected Rigidbody2D rigid;
    protected Animator anim;

    [Space]
    [SerializeField, Tooltip("몬스터 최대 체력")] protected int monsterMaxHP = 5;
    // 몬스터 현재 체력
    protected int monsterCurrentHP = 0;
    [SerializeField, Tooltip("몬스터 공격력")] protected int monsterAtk = 1;

    protected void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

     protected void Start()
    {
        monsterCurrentHP = monsterMaxHP;
    }

    protected void GetDamage(int _damage)
    {

    }
}
