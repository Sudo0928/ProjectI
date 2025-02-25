using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    [SerializeField] private SpriteRenderer characterHeadRenderer;
    [SerializeField] private SpriteRenderer characterBodyRenderer;

    private Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get => movementDirection; }

    private Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get => lookDirection; }

    public float speed = 50;

    private PlayerInputAction inputActions;

    private Animator playerHeadAnim;
    private Animator playerBodyAnim;

    private void Awake()
    {
        inputActions = new PlayerInputAction();

        _rigidbody2D = GetComponent<Rigidbody2D>();
        playerHeadAnim = GetComponent<Animator>();
        playerBodyAnim = characterBodyRenderer.GetComponent<Animator>();
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
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = movementDirection * speed * Time.fixedDeltaTime;
    }

    #region Reusable Methods

    //private void AddInputActionsCallbacks()
    //{
    //    inputActions.Player.Attack.started += Attack;
    //}

    //private void RemoveInputActionsCallbacks()
    //{
    //    inputActions.Player.Attack.started -= Attack;
    //}

    //#endregion

    //#region Input Methods

    //private void Attack(InputAction.CallbackContext context)
    //{
    //    Vector2 direction = context.ReadValue<Vector2>();

    //    if (dir.magnitude <= 0)
    //    {
    //        isAttack = false;
    //        return;
    //    }
    //    isAttack = true;

    //    head.flipX = dir.x < 0;
    //    anim.SetFloat("dirX", Math.Abs(dir.x));
    //    anim.SetFloat("dirY", dir.y);

    //    var go = Instantiate<GameObject>(tear);
    //    go.transform.position = transform.position;
    //    go.GetComponent<TearCtrl>().InitTear(dir, 10.0f, 10.0f); 
    //}

    #endregion
}
