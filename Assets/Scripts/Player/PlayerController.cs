using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 moveDir = Vector2.zero;

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
			float speed = DataManager.playerData.options[DesignEnums.Option.Speed];
			transform.position = pos + (new Vector3(moveDir.x, moveDir.y, 0) * speed * Time.deltaTime);
		}
	}

	void OnMove(InputAction.CallbackContext obj)
    {
		moveDir = obj.ReadValue<Vector2>();
	}

	void Action(InputAction.CallbackContext obj)
	{
		//IActiveSkill skill = DataManager.playerData.GetActiveSkill();
		//skill.Action();

	}
}
