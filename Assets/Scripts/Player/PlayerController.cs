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

	private void Awake()
	{
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
	}

	void Action(InputAction.CallbackContext obj)
	{
		Vector2 dir = obj.ReadValue<Vector2>();
		if (dir.magnitude <= 0)
			return;

		var go = Instantiate<GameObject>(tear);
		go.transform.position = transform.position;
		go.GetComponent<TearCtrl>().InitTear(dir, 10.0f, 10.0f);

		//IActiveSkill skill = DataManager.playerData.GetActiveSkill();
		//skill.Action();
	}



}


// 1. �÷��̾ ���� ��ư�� ���� ��, � ����ü�� ������� �ҷ�����
// 2. ��¡ Event ����
// 3. ��¡ ���� ��, ���ٸ� ���� ��, ���� Event ����,
// 4. ����ü�� ���̳� ���� ������ ���� Event�� ����
