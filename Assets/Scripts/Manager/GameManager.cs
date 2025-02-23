using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	private void Start()
	{
		DataManager.itemInfoLoader.GetByIndex(0);
		DataManager.itemOptionLoader.GetByIndex(0);
		DataManager.gachaTable.GetByIndex(0);
	}
}
