using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int IsMovingX = Animator.StringToHash("IsMovingX");
    private static readonly int IsMovingY = Animator.StringToHash("IsMovingY");

    private static readonly int IsLookX = Animator.StringToHash("IsLookX");
    private static readonly int IsLookY = Animator.StringToHash("IsLookY");

    private static readonly int IsAttack = Animator.StringToHash("IsAttack");
    private static readonly int CanTrans = Animator.StringToHash("CanTrans");

    private static readonly int IsCharging = Animator.StringToHash("IsCharging");
    private static readonly int ChargeSpeed = Animator.StringToHash("ChargeSpeed");

    private static readonly int IsPickUp = Animator.StringToHash("IsPickUp");
    private static readonly int TakeDamage = Animator.StringToHash("TakeDamage");

    [SerializeField] private SpriteRenderer characterHeadRenderer;
    [SerializeField] private SpriteRenderer characterBodyRenderer;

    private Animator playerAnim;

    private Coroutine coroutine;

    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
    }

    public void PlayMoveAnim(Vector2 movementDirection)
    {
        if (movementDirection.x < 0) characterBodyRenderer.flipX = true;
        else characterBodyRenderer.flipX = false;

        playerAnim.SetFloat(IsMovingX, Mathf.Abs(movementDirection.x));
        playerAnim.SetFloat(IsMovingY, Mathf.Abs(movementDirection.y));
    }

    public void PlayLookAnim(Vector2 lookDirection)
    {
        if (lookDirection == Vector2.zero) return;

        if(coroutine != null) StopCoroutine(coroutine);

        coroutine = StartCoroutine(DelayTrans());

        playerAnim.SetFloat(IsLookX, Mathf.Abs(lookDirection.x));
        playerAnim.SetFloat(IsLookY, lookDirection.y);

        if (lookDirection.y <= 0) playerAnim.SetBool(CanTrans, true);
        else playerAnim.SetBool(CanTrans, false);

        if (lookDirection.x >= 0) characterHeadRenderer.flipX = false;
        else characterHeadRenderer.flipX = true;
    }

    public void PlayAttackAnim()
    {
        playerAnim.SetTrigger(IsAttack);
    }

    public void PlayerCharging(bool isAttack)
    {
        playerAnim.SetBool(IsCharging, isAttack);
    }

    public void PlayTakeDamageAnim()
    {
        playerAnim.SetTrigger(TakeDamage);
    }

    public void PlayPickUpAnim(bool isPickUp)
    {
        playerAnim.SetBool(IsPickUp, isPickUp);
    }

    public void SetChargeSpeed(float chargeSpeed)
    {
        playerAnim.SetFloat(ChargeSpeed, 1 / chargeSpeed);
    }

    private IEnumerator DelayTrans()
    {
        yield return new WaitForSeconds(0.5f);

        playerAnim.SetFloat(IsLookX, 0);
        playerAnim.SetFloat(IsLookY, 0);
        playerAnim.SetBool(CanTrans, true);
    }
}
