using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 moveDir = Vector2.zero;
	public PlayerData playerData;
	[SerializeField] GameObject tear;

	[SerializeField] SpriteRenderer head;
	[SerializeField] SpriteRenderer body;

	public Animator anim;
	Animator bodyAnim;
	Rigidbody2D rigid;

	bool isAttack = false;
	bool isMove = false;


	[SerializeField, Range(0f, 1f)] float sliderFactor = 0.2f;


	private void Awake()
	{
		playerData = new PlayerData(this);

		bodyAnim = body.gameObject.GetComponent<Animator>();
		rigid = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}
	void Start()
    {
        InputSystem.Instance.move.action.performed += OnMove;
		InputSystem.Instance.space.action.performed += Action;
	}

	private void Update()
	{
		if (moveDir.magnitude > 0) 
		{
			var pos = transform.position; 
			float speed = playerData.GetOptionValue(DesignEnums.Option.Speed);
			transform.position = pos + (new Vector3(moveDir.x, moveDir.y, 0) * speed * Time.deltaTime);

			if (isMove == false)
				moveDir *= Mathf.Pow(sliderFactor, Time.deltaTime);
		}   
	}

	void OnMove(InputAction.CallbackContext obj)
    {
		Vector2 dir = obj.ReadValue<Vector2>();
		isMove = dir.magnitude > 0;
		BodyAnim(dir);

	}

	void Action(InputAction.CallbackContext obj)
	{
		Vector2 dir = obj.ReadValue<Vector2>();
		HeadAnim(dir);

		var go = Instantiate<GameObject>(tear);
		go.transform.position = transform.position;
		go.GetComponent<TearCtrl>().InitTear(dir, 10.0f, 10.0f);
	}

	Coroutine onIdleHead = null;
	void HeadAnim(Vector2 dir)
	{
		if (dir.magnitude <= 0)
		{
			onIdleHead = StartCoroutine(OnIdleHead());
			isAttack = false;
			return;
		}

		if (onIdleHead != null)
			StopCoroutine(onIdleHead);


		isAttack = true;
		anim.SetBool("isMove", true);

		head.flipX = dir.x < 0;
		anim.SetFloat("dirX", Math.Abs(dir.x));
		anim.SetFloat("dirY", dir.y);
	}
	void BodyAnim(Vector2 dir)
	{
		bodyAnim.SetBool("isMove", isMove);

		if (dir.magnitude > 0)
		{
			moveDir = dir;
			body.flipX = moveDir.x < 0;

			bodyAnim.SetFloat("dirX", Math.Abs(moveDir.x));
			bodyAnim.SetFloat("dirY", Math.Abs(moveDir.y));
		}


		if (!isAttack)
		{
			head.flipX = moveDir.x < 0;
			anim.SetFloat("dirX", Math.Abs(moveDir.x));
			anim.SetBool("isMove", isMove);
			anim.SetFloat("dirY", moveDir.y);
		}
	}
	

	IEnumerator OnIdleHead()
	{
		yield return new WaitForSeconds(0.5f);
		anim.SetFloat("dirX", -1);
		anim.SetFloat("dirY", -1);
	}

}


// 1. 플레이어가 공격 버튼을 누를 때, 어떤 투사체를 사용할지 불러오기
// 2. 차징 Event 실행
// 3. 차징 종료 후, 없다면 생략 후, 공격 Event 실행,
// 4. 투사체가 벽이나 적에 닿으면 종료 Event가 실행
