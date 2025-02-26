using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAttackHandler : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer mainSprite;

    protected Rigidbody2D _rigidbody2D;

    protected Player owner;

    [SerializeField]protected float distance = 3;
    [SerializeField]protected float speed = 3f;
    [SerializeField]protected float size = 1;
    [SerializeField]protected bool canPenetrate = false;
    [SerializeField]protected bool canIgnoreObstacle = false;

    [SerializeField]protected Vector2 attackDirection = Vector2.zero;

    protected float lerpTime = 0;
    protected float currentTime = 0;

    public virtual void Init(Player owner, Vector2 attackDirection)
    {
        this.owner = owner;
        this.speed = owner.ProjectileSpeed;
        this.distance = owner.ProjectileDistance;
        this.size = owner.ProjectileSize;
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
