using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[Serializable]
public class Pos
{
	public int y;
	public int x; 
}

[Serializable]
public class DoorInfo
{
	public RoomDoor door;
	public Pos pos;
}

public class RoomManager : MonoBehaviour
{
    [SerializeField] public List<RoomDoor> myDoor = new List<RoomDoor>();
	CinemachineConfiner2D virtualCamera;
	[SerializeField] StageReward stageReward;
	[SerializeField] bool isStartRoom = false;

	[SerializeField] public List<DoorInfo> leftDoor;
	[SerializeField] public List<DoorInfo> rightDoor;
	[SerializeField] public List<DoorInfo> upDoor;
	[SerializeField] public List<DoorInfo> downDoor;  

	[SerializeField] public List<Pos> roomStructure = new List<Pos>(); 
	int monsterCnt = 0;  
	public bool isClear => monsterCnt == 0; 

	public List<DoorInfo> GetDoor(int dir)
	{
		if (dir == 0)
			return upDoor;
		else if (dir == 1)
			return downDoor;
		else if (dir == 2)
			return leftDoor;
		else
			return rightDoor;
	} 

	public RoomDoor GetDoor((int, int) idx, int dir)
	{
		List<DoorInfo> list;
		if (dir == 0)
			list = upDoor;
		else if (dir == 1)
			list = downDoor;
		else if (dir == 2)
			list = leftDoor;
		else
			list = rightDoor; 

		foreach (var door in list)
		{
			if (door.pos.y == idx.Item1 && door.pos.x == idx.Item2)
				return door.door;
		}
		return null;
	}
	private void Awake()
	{
		virtualCamera = FindFirstObjectByType<CinemachineConfiner2D>();
		
	}
	private void Start()
	{
		if (isStartRoom == false)
			GameManager.Instance.SetTimer(()=>gameObject.SetActive(false), 0.3f);
	}  

	public void EnterRoom()
	{
		virtualCamera.m_BoundingShape2D = GetComponent<PolygonCollider2D>(); 
		gameObject.SetActive(true);

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

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Monster"))
		{
			monsterCnt++;
			// TODO
			// 몬스터가 죽을 때 호출되는 Event에 monsterCnt를  -1
			// monster Cnt 가 0이 될 때, SpawnItem 함수 실행

			collision.GetComponent<MonsterBasic>().onDie.AddListener(() => 
			{
				monsterCnt--;
				if (monsterCnt == 0)
					ClearRoom(); 
			});
		}
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
 