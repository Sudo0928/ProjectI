using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class SpecialAbilityInfo
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
    /// 컴포넌트이름
    /// </summary>
    public string ComponentName;

    /// <summary>
    /// 설명
    /// </summary>
    public string Description;

}
public class SpecialAbilityInfoLoader
{
    public List<SpecialAbilityInfo> ItemsList { get; private set; }
    public Dictionary<int, SpecialAbilityInfo> ItemsDict { get; private set; }

    public SpecialAbilityInfoLoader(string path = "JSON/SpecialAbilityInfo")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, SpecialAbilityInfo>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<SpecialAbilityInfo> Items;
    }

    public SpecialAbilityInfo GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public SpecialAbilityInfo GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
