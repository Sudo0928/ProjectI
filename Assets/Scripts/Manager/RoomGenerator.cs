using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomGenerator : MonoBehaviour 
{



	[SerializeField] GameObject miniRoom;
	[SerializeField] GameObject LRoom;
	[SerializeField] GameObject longRoom;
	[SerializeField] GameObject bigRoom;

	[SerializeField] GameObject bossRoom;
	[SerializeField] GameObject bossPrevRoom; 
	[Space(10)]
	[SerializeField] RoomManager startRoom; 

	[Space(10)]
	[SerializeField] int roomDepth;

	[Space(20)]
	[SerializeField] List<GameObject> monsters;
	[SerializeField] GameObject boss;

	int[][] dungeonDirs =
	{
		new int[]{0, 2},
		new int[]{0, 3},
		new int[]{1, 2},
		new int[]{1, 3},
	};

	private void Awake()
	{
		GenerateRoom(startRoom, dungeonDirs[Random.Range(0, 4)]);
	}

	// ╩С, го, аб, ©Л,
	int[] doorDir = { 1, 0, 3, 2 };
	void GenerateRoom(RoomManager prevRoom, int[] dirs, int depth = 1)
	{
		int dir = dirs[Random.Range(0, dirs.Length)];
		if (depth >= roomDepth)
			return; 

		int cnt = 0;
		RoomDoor prevDoor = null;
		while (!prevRoom.GetDoor(dir, out prevDoor)) { 
			dir = dirs[Random.Range(0, dirs.Length)];
			if (cnt++ >= 5) 
				return; 
		}
		cnt = 0;

		var room = Instantiate<GameObject>(GetRandomRoom(depth));
		room.transform.position = new Vector3(-1000, -1000, 0);
		var curRoomManager = room.GetComponent<RoomManager>();

		List<GameObject> list = new List<GameObject>();
		if (depth + 1 == roomDepth)
		{
			list.Add(boss);
			curRoomManager.SpawnMonster(list);
		}
		else
			curRoomManager.SpawnMonster(monsters);

		RoomDoor curDoor = null;
		while (!curRoomManager.GetDoor(doorDir[dir], out curDoor, depth +2== roomDepth)) {
			if (cnt++ >= 5)
				return; 
		}

		prevDoor.SetLinkedDoor(curDoor);
		curDoor.SetLinkedDoor(prevDoor);
		 
		GenerateRoom(curRoomManager, dirs, depth + 1);
	}  

	GameObject GetRandomRoom(int depth)
	{
		if (roomDepth - 2 == depth)
			return bossPrevRoom;
		else if (roomDepth -1 == depth)
			return bossRoom;


		int rand = Random.Range(0, 100);
		if (rand < 70)
			return miniRoom;
		else if (rand < 80)
			return LRoom;
		else if (rand < 90)
			return longRoom;
		else
			return bigRoom;
	} 

}
