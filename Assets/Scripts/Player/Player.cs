using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    [SerializeField] private GameObject tear;

    private Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get => movementDirection; }

    private Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get => lookDirection; }

    private Vector2 desiredVelocity = Vector2.zero;

    private Vector2 velocity;

    [SerializeField][Range(0f, 100f)]
    private float maxSpeed = 10f;

    [SerializeField][Range(0f, 100f)]
    private float maxAccelertaion = 10f;

    [SerializeField][Range(0f, 100f)]
    private float maxSlideDistance = 0.5f;

    private AnimationHandler animationHandler;

    private PlayerInputAction inputActions;

    private Coroutine onIdleHead = null;

    private float timeSinceLastAttack = 1;

    [SerializeField][Range(0f, 500f)]
    private float attackSpeed = 0.1f;

    [SerializeField][Range(0f, 500f)]
    private float projectileSpeed = 200f;

    [SerializeField] [Range(0f, 1f)]
    private float projectileVelocityAngle = 0.2f;

    private bool isAttack = false;

    private void Awake()
    {
        inputActions = new PlayerInputAction();

        _rigidbody2D = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();

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
        lookDirection = inputActions.Player.Attack.ReadValue<Vector2>();

        animationHandler.PlayMoveAnim(movementDirection);
        animationHandler.PlayLookAnim(movementDirection);

        desiredVelocity = movementDirection * maxSpeed;

        if (timeSinceLastAttack <= attackSpeed)
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        if (isAttack)
        {
            animationHandler.PlayLookAnim(lookDirection);
            HandleAttackDelay(lookDirection);
        }
    }

    private void FixedUpdate()
    {
        AdjustVelocity();
    }

    #region Main Methods

    private void HandleAttackDelay(Vector2 direction)
    {
        if (timeSinceLastAttack > attackSpeed)
        {
            timeSinceLastAttack = 0;
            Attack(direction);
        }
    }

    private void Attack(Vector2 attackDirection)
    {
        Vector2 velocity = _rigidbody2D.velocity;
        Vector2 desiredDirection = (attackDirection + velocity * 0.2f);

        GameObject gameObject = Instantiate(tear);
        gameObject.transform.position = transform.position;
        gameObject.GetComponent<Tear>().Init(desiredDirection, projectileSpeed);

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
        float maxSpeedChange = maxAccelertaion * Time.fixedDeltaTime;

        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.y = Mathf.MoveTowards(velocity.y, desiredVelocity.y, maxSpeedChange);

        if(desiredVelocity == Vector2.zero)
        {
            float maxSlideSpeed = Mathf.Sqrt(2f * maxAccelertaion * maxSlideDistance);
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

    #endregion

    #region Input Methods

    private void OnAttack(InputAction.CallbackContext context)
    {
        isAttack = true;
    }

    private void OffAttack(InputAction.CallbackContext context)
    {
        isAttack = false;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        if (movementDirection != Vector2.zero) return;


    }

    #endregion
}
