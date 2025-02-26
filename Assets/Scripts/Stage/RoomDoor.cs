using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RoomDoor : MonoBehaviour
{
	[SerializeField] public Transform front;
	RoomManager nextRoom;
	RoomDoor linkedDoor;
	RoomManager myRoom;

	bool isOpend = true;
	private void Awake() 
	{
		myRoom = transform.parent.GetComponent<RoomManager>();

	}

	public RoomManager Room => myRoom;

	private void OnTriggerEnter2D(Collider2D collision)
	{ 
		if (isOpend && collision.CompareTag("Player"))
		{
			var playerForward = collision.GetComponent<Player>().GetMoveDir;
			Vector2 toPlayer = (collision.transform.position - transform.position).normalized;

			float dot = Vector2.Dot(playerForward, toPlayer);

            if (dot < - 0.6f)
            {
				collision.transform.position = linkedDoor.front.position;
				nextRoom.EnterRoom();
				myRoom.ExitRoom();
			}

			
		}


		RoomDoor door = collision.GetComponent<RoomDoor>();
		if (door != null)
		{
			linkedDoor = door;
			nextRoom = linkedDoor.Room;
		}

	}



	public void OpenDoor()
	{

	}

	public void CloseDoor()
	{

	}
}
