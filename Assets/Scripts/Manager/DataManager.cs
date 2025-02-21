using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class DataManager : Singleton<DataManager>
{
   public List<Item> items = new List<Item>();
	public PlayerData playerData = new PlayerData();  

}
