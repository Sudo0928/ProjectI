using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomManager : MonoBehaviour
{
	[SerializeField] List<Transform> monsterSpawnPos;
	[Space(20)]
    [SerializeField] public List<RoomDoor> myDoor = new List<RoomDoor>();
	CinemachineConfiner2D virtualCamera;
	[SerializeField] StageReward stageReward;
	[SerializeField] bool isStartRoom = false;

	[SerializeField] public List<RoomDoor> leftDoor;
	[SerializeField] public List<RoomDoor> rightDoor;
	[SerializeField] public List<RoomDoor> upDoor;
	[SerializeField] public List<RoomDoor> downDoor;  

	int monsterCnt = 0;   
	public bool isClear => monsterCnt == 0;

	List<GameObject> roomMonsters = new List<GameObject>();
	public virtual bool GetDoor(int dir, out RoomDoor door, bool prevBossRoom = false)
	{
		door = null;
		List<RoomDoor> list;
		if (dir == 0)
			list = upDoor;
		else if (dir == 1)
			list = downDoor;
		else if (dir == 2)
			list = leftDoor;
		else
			list = rightDoor;

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
	
	public void SpawnMonster(List<GameObject> monsters)
	{
		if (monsterSpawnPos.Count <= 0)
			return;

		int spawnCnt = Random.Range(monsterSpawnPos.Count - 1, monsterSpawnPos.Count + 1);

		while (spawnCnt-- > 0) 
		{
			var mst = Instantiate(monsters[Random.Range(0, monsters.Count)]);
			mst.transform.SetParent(transform, false);
			mst.transform.position = monsterSpawnPos[Random.Range(0, monsterSpawnPos.Count)].position;
			mst.SetActive(false);
			roomMonsters.Add(mst);
			monsterCnt++;
			mst.GetComponent<MonsterBasic>().onDie.AddListener(() =>
			{
				monsterCnt--;
				if (monsterCnt == 0)
					ClearRoom();
			});
		}
		 
	} 

	private void Awake()
	{
		virtualCamera = FindFirstObjectByType<CinemachineConfiner2D>();

		foreach (RoomDoor door in leftDoor)
			myDoor.Add(door);

		foreach (RoomDoor door in rightDoor)
			myDoor.Add(door);

		foreach (RoomDoor door in upDoor)
			myDoor.Add(door);
		 
		foreach (RoomDoor door in downDoor)
			myDoor.Add(door);
		 
	}
	private void Start()
	{
		if (isStartRoom == false)
			GameManager.Instance.SetTimer(()=>gameObject.SetActive(false), 0.3f);

		foreach (RoomDoor door in myDoor)
		{
			if (door.isLinked == false)
				door.gameObject.SetActive(false);
		}
	}  

	public void EnterRoom(Vector3 movePos)
	{
		transform.position += movePos;
		virtualCamera.m_BoundingShape2D = GetComponent<PolygonCollider2D>(); 
		gameObject.SetActive(true);

		foreach (var go in roomMonsters)
			go.SetActive(true);

		if (monsterCnt > 0)
			CloseDoor(); 
		else
			OpenDoor(); 
	} 
	  
	public void ExitRoom()
	{
		//virtualCamera.SetActive(false);
		gameObject.SetActive(false);
	}


	void CloseDoor()
	{
		foreach (var door in myDoor)
			door.CloseDoor();
	}
	void OpenDoor()
	{
		foreach (var door in myDoor)
			door.OpenDoor();
	}

	void ClearRoom()
	{
		stageReward.SpawnItem();
		OpenDoor(); 

	}
}
 