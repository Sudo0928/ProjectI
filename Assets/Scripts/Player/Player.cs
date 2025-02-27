using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static DesignEnums;
public class Player : MonoBehaviour, IDamagedable
{
    private Rigidbody2D _rigidbody2D;
    public Rigidbody2D Rigidbody2D => _rigidbody2D;

    private Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get => movementDirection; }

    private Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get => lookDirection; }

    private Vector2 desiredVelocity = Vector2.zero;

    private Vector2 velocity;

    [SerializeField][Range(0f, 100f)]
    private float maxSpeed = 10f;

    [SerializeField][Range(0f, 100f)]
    private float maxAcceleration = 10f;

    [SerializeField][Range(0f, 100f)]
    private float maxSlideDistance = 0.5f;

    private AnimationHandler animationHandler;

    private PlayerInputAction inputActions;

    private Coroutine onIdleHead = null;

    private float timeSinceLastAttack = 1;


    public float projectileDistance => stat.GetStat(Option.Range);
    public float projectileSpeed => stat.GetStat(Option.ProjectileSpeed);
    public float projectileSize => stat.GetStat(Option.ProjectileSize); 
    public float attackSpeed => 1.0f / stat.GetStat(Option.AttackSpeed);
     
	[SerializeField] [Range(0f, 1f)]
    private float projectileVelocityAngle = 0.2f;

    private bool isAttack = false;

    public PlayerUIHandler PlayerUIHandler => GetComponent<PlayerUIHandler>();
	private Stat stat = new Stat();

    public Stat Stat => stat;
	private Inventory inventory = new Inventory();
    public Inventory Inventory => inventory;

	[SerializeField][Range(0.001f, 10f)]
    private float maxChargingTime = 1f;

    [SerializeField]
    private bool isCharging = false;

    [SerializeField]
    private bool isParbolic = false;
    public bool IsParbolic => isParbolic;

    private float timeSincePressAttack = 0;
    public Vector2 GetMoveDir => inputActions.Player.Move.ReadValue<Vector2>();


    [SerializeField]
    private bool ignoreExplosions = false;
    public bool IgnoreExplosions => ignoreExplosions;

    [SerializeField]
    private bool autoAttack = false;

    [SerializeField]
    private LayerMask layerMask;

    private void Awake()
    {
        inputActions = new PlayerInputAction();

        _rigidbody2D = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();

        //    inventory.onAddItem.AddListener(() => { anim.SetTrigger("getItem")});
        inventory.InitInventory(stat, this);
        
		AddInputActionsCallbacks();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
     
    private void Update() 
    {
        movementDirection = inputActions.Player.Move.ReadValue<Vector2>();
        if (!autoAttack) lookDirection = inputActions.Player.Attack.ReadValue<Vector2>();

        animationHandler.PlayMoveAnim(movementDirection);
        animationHandler.PlayLookAnim(movementDirection);

        desiredVelocity = movementDirection * maxSpeed;

        if (timeSinceLastAttack <= attackSpeed)
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        if (lookDirection == Vector2.zero) return;

        if (isAttack || autoAttack)
        {
            if (isCharging)
            {
                animationHandler.PlayLookAnim(lookDirection);
                animationHandler.SetChargeSpeed(maxChargingTime);
                animationHandler.PlayerCharging(isAttack);
                timeSincePressAttack += Time.deltaTime;
            }
            else
            {
                animationHandler.PlayLookAnim(lookDirection);
                HandleAttackDelay();
            }
        }
    }

    private void FixedUpdate()
    {
        AdjustVelocity();

        if (autoAttack)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, projectileDistance, layerMask);

            if (colliders.Length == 0)
            {
                lookDirection = Vector2.zero;
                return;
            }

            Vector3 direction = colliders[0].transform.position - transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Debug.Log(angle);

            if (angle < 45 && angle > -45) lookDirection = Vector2.right;
            else if (angle > 45 && angle < 135) lookDirection = Vector2.up;
            else if (angle > 135 || angle < -135) lookDirection = Vector2.left;
            else if (angle > -135 && angle < -45) lookDirection = Vector2.down;
        }
        
    }

    #region Main Methods

    private void HandleAttackDelay()
    {
        if (timeSinceLastAttack > attackSpeed)
        {
            timeSinceLastAttack = 0;
            Attack();
        }
    }

    private void Attack()
    {
        PlayerAttackEvent playerAttackEvent;
        playerAttackEvent = new PlayerAttackEvent(this, lookDirection + _rigidbody2D.velocity * 0.2f);
        EventManager.DispatchEvent(playerAttackEvent);

        animationHandler.PlayAttackAnim();
    }

    private int GetMinusSign(float value)
    {
        if (value < 0) return -1;
        else return 1;
    }

    private void AdjustVelocity()
    {
        velocity = _rigidbody2D.velocity;
        float maxSpeedChange = maxAcceleration * Time.fixedDeltaTime;

        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.y = Mathf.MoveTowards(velocity.y, desiredVelocity.y, maxSpeedChange);

        if(desiredVelocity == Vector2.zero)
        {
            float maxSlideSpeed = Mathf.Sqrt(2f * maxAcceleration * maxSlideDistance);
            velocity = Vector2.ClampMagnitude(velocity, maxSlideSpeed);
        }

        _rigidbody2D.velocity = velocity;
    }

    #endregion

    #region Reusable Methods

    private void AddInputActionsCallbacks()
    {
        inputActions.Player.Attack.started += OnAttack;
        inputActions.Player.Attack.canceled += OffAttack;
        inputActions.Player.Move.performed += OnMove;
    }

    private void RemoveInputActionsCallbacks()
    {
        inputActions.Player.Attack.started -= OnAttack;
        inputActions.Player.Attack.canceled -= OffAttack;
        inputActions.Player.Move.performed -= OnMove;
    }

    //#endregion

    //#region Input Methods

    private void OnAttack(InputAction.CallbackContext context)
    {
        isAttack = true;
        timeSincePressAttack = 0;
    }

    private void OffAttack(InputAction.CallbackContext context)
    {
        bool upPress = Keyboard.current.upArrowKey.isPressed;
        bool downPress = Keyboard.current.downArrowKey.isPressed;
        bool LeftPress = Keyboard.current.leftArrowKey.isPressed;
        bool RightPress = Keyboard.current.rightArrowKey.isPressed;

        if (upPress || downPress || LeftPress || RightPress) return;

        isAttack = false;
        animationHandler.PlayerCharging(isAttack);
        if (timeSincePressAttack > maxChargingTime)
        {
            Attack();
            timeSincePressAttack = 0;
        }
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        if (movementDirection != Vector2.zero) return;


    }

    public bool TakeDamage(float damage)
    {
        Debug.Log("Damaged");
        return true;
    }

    public bool TakeBoomDamage(float damage)
    {
        if (ignoreExplosions) return false;

        Debug.Log("Boomb");
        return true;
    }

    #endregion
}
