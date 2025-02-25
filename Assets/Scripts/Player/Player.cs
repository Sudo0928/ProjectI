using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField] private SpriteRenderer characterHeadRenderer;
    [SerializeField] private SpriteRenderer characterBodyRenderer;

    private Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get => movementDirection; }

    private Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get => lookDirection; }

    public float speed = 50;

    private PlayerInputAction inputActions;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        inputActions = new PlayerInputAction();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void Update()
    {
        movementDirection = inputActions.Player.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Debug.Log(movementDirection);
        _rigidbody.AddForce(movementDirection * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}
