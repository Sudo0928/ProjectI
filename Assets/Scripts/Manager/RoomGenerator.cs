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

	[Space(10)]
	[SerializeField] int roomCnt;

	
	HashSet<(int, int)> grid = new HashSet<(int, int)>();

	RoomManager miniRoomMng;
	RoomManager LRoomMng;
	RoomManager longRoomMng;
	RoomManager bigRoomMng;
	private void Awake()
	{
		miniRoomMng = miniRoom.GetComponent<RoomManager>();
		LRoomMng = LRoom.GetComponent<RoomManager>();
		longRoomMng = longRoom.GetComponent<RoomManager>();
		bigRoomMng = bigRoom.GetComponent<RoomManager>();

		grid.Add((0, 0));
		GenerateRoom((0, 0), Random.Range(0, 4));
	}

	// ╩С, го, аб, ©Л,
	int[] dir = { 1, 0, 3, 2 };
	int[] dy = { -1, 1, 0, 0 };
	int[] dx = { 0, 0, 1, -1 };
	void GenerateRoom((int, int) pos, int dir)
	{

		var room = GetRandomRoom();
		var structure = room.roomStructure;

		var Idxs = Check(structure, pos, dir);



	}

	RoomManager GetRandomRoom()
	{
		int rand = Random.Range(0, 4);
		if (rand == 0)
			return miniRoomMng;
		else if (rand == 1)
			return LRoomMng;
		else if (rand == 2)
			return longRoomMng;
		else
			return bigRoomMng;
	}

	List<(int, int)> RotateRoom(RoomManager room, int rot)
	{
		List<(int, int)> ret = new List<(int, int)>();

 		foreach (var pos in room.roomStructure)
			ret.Add((pos.y, pos.x));

		if (rot == 0)
			return ret;

		for (int i = 0; i < rot; i++)
		{
			List<(int, int)> temp = new List<(int, int)>();
			foreach (var pos in ret)
				ret.Add((-pos.Item2, pos.Item1));

			ret = temp;
		}
		 
		return ret;
	}
	
	List<(int, int)> Check(List<Pos> structure, (int, int) pos, int dir)
	{
		List<(int, int)> ret = new List<(int, int)> ();
		for (int i = 0; i < structure.Count; i++)
		{
			bool flag = true;

			Pos loc = structure[i];

			(int, int) curPos = (pos.Item1 + dy[dir], pos.Item2 + dx[dir]);
			(int, int) startIdx = (loc.y, loc.x);

			foreach (var p in structure)
			{
				(int, int) nextPos = (curPos.Item1 + (p.y - startIdx.Item1), curPos.Item2 + (p.x - startIdx.Item2));
				if (grid.Contains(nextPos))
				{
					flag = false;
					break;
				}
			}

			if (flag == true)
			{
				foreach (var p in structure)
					ret.Add((p.y - startIdx.Item1, p.x - startIdx.Item2));

				break;
			}

		}
		return ret;
	}

	// 00 -> 10
	// 10 -> 11
	// 11-> 01
	// 01 -> 00

	// -x, y
	// 00, 01, -1,1
	// 00- >00
	// 01 -> -10
	// -1,1-> -1,-1
}
