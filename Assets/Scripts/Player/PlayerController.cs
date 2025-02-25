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

	Animator anim;
	Animator bodyAnim;

	bool isAttack = false;

	private void Awake()
	{
		anim = GetComponent<Animator>();
		bodyAnim = body.gameObject.GetComponent<Animator>();	
		playerData = new PlayerData(this);
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
		} 
	}

	void OnMove(InputAction.CallbackContext obj)
    {

		moveDir = obj.ReadValue<Vector2>();
		body.flipX = moveDir.x < 0;

		if (!isAttack)
		{
			head.flipX = moveDir.x < 0;
			anim.SetFloat("dirX", Math.Abs(moveDir.x));
			anim.SetFloat("dirY", moveDir.y);
		}
		
		bodyAnim.SetFloat("dirX", Math.Abs(moveDir.x));
		bodyAnim.SetFloat("dirY", Math.Abs(moveDir.y)); 
	} 

	void Action(InputAction.CallbackContext obj)
	{
		Vector2 dir = obj.ReadValue<Vector2>();
		if (dir.magnitude <= 0)
		{
			isAttack = false;
			return;
		}
		isAttack = true; 

		head.flipX = dir.x < 0;
		anim.SetFloat("dirX", Math.Abs(dir.x));
		anim.SetFloat("dirY", dir.y); 

		var go = Instantiate<GameObject>(tear);
		go.transform.position = transform.position;
		go.GetComponent<TearCtrl>().InitTear(dir, 10.0f, 10.0f);
	}



}


// 1. 플레이어가 공격 버튼을 누를 때, 어떤 투사체를 사용할지 불러오기
// 2. 차징 Event 실행
// 3. 차징 종료 후, 없다면 생략 후, 공격 Event 실행,
// 4. 투사체가 벽이나 적에 닿으면 종료 Event가 실행
