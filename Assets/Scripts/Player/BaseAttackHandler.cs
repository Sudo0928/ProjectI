using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAttackHandler : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer mainSprite;

    protected Rigidbody2D _rigidbody2D;

    protected GameObject owner;
    public GameObject Owner => owner;

    [SerializeField] protected float damage = 3;
    public float Damage => damage;

    [SerializeField]protected float distance = 3;
    public float Distance => distance;

    [SerializeField]protected float speed = 3f;
    public float Speed => speed;

    [SerializeField]protected float size = 1;
    public float Size => size;

    [SerializeField]protected bool canPenetrate = false;

    [SerializeField]protected bool canIgnoreObstacle = false;

    [SerializeField]protected Vector2 attackDirection = Vector2.zero;

    protected float lerpTime = 0;
    protected float currentTime = 0;

    public virtual void Init(GameObject owner, float damage, float speed, float distance, float size, Vector2 attackDirection)
    {
        this.owner = owner;
        this.damage = damage;
        this.speed = speed;
        this.distance = distance;
        this.size = size;
        this.attackDirection = attackDirection.normalized;
        this.lerpTime = distance / speed;
    }

    protected virtual void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnEnable()
    {
        
    }

    protected virtual void OnDisable()
    {
        
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        
    }

    protected virtual void FixedUpdate()
    {
        
    }
}
