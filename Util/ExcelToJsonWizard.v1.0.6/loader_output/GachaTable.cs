using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class GachaTable
{
    /// <summary>
    /// ID
    /// </summary>
    public int key;

    /// <summary>
    /// Name
    /// </summary>
    public string name;

    /// <summary>
    /// 확률
    /// </summary>
    public int LegendaryRate;

    /// <summary>
    /// 확률
    /// </summary>
    public int RareRate;

    /// <summary>
    /// 확률
    /// </summary>
    public int CommonRate;

    /// <summary>
    /// 아이템 리스트
    /// </summary>
    public List<int> Items;

}
public class GachaTableLoader
{
    public List<GachaTable> ItemsList { get; private set; }
    public Dictionary<int, GachaTable> ItemsDict { get; private set; }

    public GachaTableLoader(string path = "JSON/GachaTable")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, GachaTable>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<GachaTable> Items;
    }

    public GachaTable GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public GachaTable GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
