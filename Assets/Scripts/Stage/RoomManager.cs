using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] public List<RoomDoor> myDoor = new List<RoomDoor>();
	CinemachineConfiner2D virtualCamera;
	[SerializeField] StageReward stageReward;
	[SerializeField] bool isStartRoom = false;	
	

	int monsterCnt = 0;
	public bool isClear => monsterCnt == 0;

	private void Awake()
	{
		virtualCamera = FindFirstObjectByType<CinemachineConfiner2D>();
		
	}
	private void Start()
	{
		if (isStartRoom == false)
			GameManager.Instance.SetTimer(()=>gameObject.SetActive(false), 0.1f);
	} 

	public void EnterRoom()
	{
		virtualCamera.m_BoundingShape2D = GetComponent<PolygonCollider2D>(); 
		gameObject.SetActive(true);
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
		}
	}


}
