using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class ItemInfo
{
    /// <summary>
    /// ID
    /// </summary>
    public int key;

    /// <summary>
    /// 이름
    /// </summary>
    public string Name;

    /// <summary>
    /// 등급
    /// </summary>
    public DesignEnums.Grade Grade;

    /// <summary>
    /// 옵션
    /// </summary>
    public List<int> AvailableOptions;

    /// <summary>
    /// 값
    /// </summary>
    public List<float> OptionValues;

    /// <summary>
    /// 특수 효과
    /// </summary>
    public List<int> SpecialOptions;

    /// <summary>
    /// 0
    /// </summary>
    public int Gauge;

    /// <summary>
    /// 설명
    /// </summary>
    public string Description;

    /// <summary>
    /// 습득시 메시지
    /// </summary>
    public string Massage;

    /// <summary>
    /// 아이템 이미지 링크
    /// </summary>
    public string Image;

}
public class ItemInfoLoader
{
    public List<ItemInfo> ItemsList { get; private set; }
    public Dictionary<int, ItemInfo> ItemsDict { get; private set; }

    public ItemInfoLoader(string path = "JSON/ItemInfo")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, ItemInfo>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<ItemInfo> Items;
    }

    public ItemInfo GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public ItemInfo GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
