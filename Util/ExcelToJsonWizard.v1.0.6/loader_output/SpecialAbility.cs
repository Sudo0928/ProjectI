using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class SpecialAbility
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
public class SpecialAbilityLoader
{
    public List<SpecialAbility> ItemsList { get; private set; }
    public Dictionary<int, SpecialAbility> ItemsDict { get; private set; }

    public SpecialAbilityLoader(string path = "JSON/SpecialAbility")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, SpecialAbility>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<SpecialAbility> Items;
    }

    public SpecialAbility GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public SpecialAbility GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
