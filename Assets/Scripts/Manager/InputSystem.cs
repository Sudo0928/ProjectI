using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystem : Singleton<InputSystem>
{
    public InputActionAsset inputAction;
    public InputActionReference move;
    public InputActionReference space;

	protected override void Awake()
	{
		inputAction.Enable();
	}
}
