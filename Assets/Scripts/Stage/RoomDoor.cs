using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RoomDoor : MonoBehaviour
{
	[SerializeField] public Transform front;
	RoomManager nextRoom;
	RoomDoor linkedDoor;
	public RoomManager myRoom;


	bool isOpend = false;
	Animator anim;

	private void Awake() 
	{
		anim = GetComponent<Animator>();
		myRoom = transform.parent.GetComponent<RoomManager>();
		myRoom?.myDoor.Add(this);
		OpenDoor();
	}

	public RoomManager Room => myRoom;

	private void OnTriggerEnter2D(Collider2D collision)
	{ 
		if (isOpend && collision.CompareTag("Player"))
		{
			var playerForward = collision.GetComponent<PlayerController>().GetMoveDir;
			Vector2 forward = (front.position - transform.position).normalized;

			float dot = Vector2.Dot(playerForward, forward);

            if (dot < - 0.6f)
            {
				if (linkedDoor == null)
				{
					Debug.Log("여기 아닌데~");
					return; 
				}

				Vector3 nextPos = transform.position + new Vector3(-forward.x, -forward.y, 0) * 5;
				Vector3 nextRoomMoveVector = nextPos -  linkedDoor.transform.position;
				nextRoom.EnterRoom(nextRoomMoveVector); 
				myRoom.ExitRoom();
				collision.transform.position = linkedDoor.front.position;
			} 
		}
	}

	public void SetLinkedDoor(RoomDoor door)
	{
		linkedDoor = door;
		nextRoom = linkedDoor.myRoom;
	}

	public bool isLinked => linkedDoor != null;

	public void OpenDoor()
	{
		string animName = "OpenDoor";
		if (isOpend)
			animName = "OpenDoorState";

		anim.Play(animName); 
		isOpend = true; 
	}

	public void CloseDoor()
	{
		string animName = "CloseDoor";
		if (!isOpend)
			animName = "CloseDoorState";
		  
		anim.Play(animName);
		isOpend = false;
	}
}
