using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected Vector2 attackDirection = Vector2.zero;
    public Vector2 AttackDirection { get { return attackDirection; } }

    public GameObject projectilePrefab;
    public GameObject projectileAPrefab;
    public GameObject projectileBPrefab;

    public float speed = 1;

    public List<Effect> effects = new List<Effect>();

    private PlayerInputAction inputActions;

    [SerializeField] private float currentHP;
    public float CurrentHP { get => currentHP; set => currentHP = value; }

    [SerializeField] private float maxHP;
    public float MaxHP { get => maxHP; set => maxHP = value; }

    private void Awake()
    {
        inputActions = new PlayerInputAction();
    }

    private void Start()
    {
        inputActions.Player.Attack.started += Attack;
    }

    // Update is called once per frame
    void Update()
    {
        ReadMoveInput();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void FixedUpdate()
    {
        transform.Translate(movementDirection * speed * Time.fixedDeltaTime);
    }

    private void Attack(InputAction.CallbackContext context)
    {

    }

    private void ReadMoveInput()
    {
        movementDirection = inputActions.Player.Move.ReadValue<Vector2>();
    }
}
