using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrevBossRoom : RoomManager
{
	[Space(10)]
	[SerializeField] public List<RoomDoor> leftDoor_wood;
	[SerializeField] public List<RoomDoor> rightDoor_wood;
	[SerializeField] public List<RoomDoor> upDoor_wood;
	[SerializeField] public List<RoomDoor> downDoor_wood;

	public override bool GetDoor(int dir, out RoomDoor door, bool prevBossRoom = false)
	{
		if (prevBossRoom)
			return GetWoodDoor(dir, out door);
		else
			return base.GetDoor(dir, out door); 
	}

	bool GetWoodDoor(int dir, out RoomDoor door)
	{
		door = null;
		List<RoomDoor> list;
		if (dir == 0)
			list = upDoor_wood;
		else if (dir == 1)
			list = downDoor_wood;
		else if (dir == 2)
			list = leftDoor_wood;
		else
			list = rightDoor_wood;

		HashSet<int> idxs = new HashSet<int>();
		int idx = 0;
		do
		{
			if (idxs.Count == list.Count)
				return false;

			idx = UnityEngine.Random.Range(0, list.Count);
			idxs.Add(idx);

		} while (list[idx].isLinked);

		door = list[idx];
		return true;
	}
}
