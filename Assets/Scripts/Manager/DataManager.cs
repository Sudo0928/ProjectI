using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static class DataManager
{
	public static PlayerData playerData = new PlayerData();
	public static GachaTableLoader gachaTable = new GachaTableLoader();
	public static ItemInfoLoader itemInfoLoader = new ItemInfoLoader();
	public static ItemOptionLoader itemOptionLoader = new ItemOptionLoader();


//	public List<(int, Image)> itemImages = new List<(int, Image)>();
	//public Dictionary<int, Image> test = new Dictionary<int, Image>();
}
 