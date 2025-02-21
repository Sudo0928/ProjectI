using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public ActiveItem activeItem;
    public HashSet<PassiveItem> items = new HashSet<PassiveItem>();
     

    public void AddItem(Item item)
    {
        if (item is ActiveItem)
        {
            // TODO 조합
            // 현재 아이템과 조합이 가능한지 체크
            activeItem = (ActiveItem)item;
		} 
		else if  (items.Contains(item) == false)
        {
			items.Add((PassiveItem)item);

            foreach (var skill in ((PassiveItem)item).skills)
                skill.OnPassive();
		}

	} 



    public IActiveSkill GetActiveSkill()
    {
        if (activeItem == null)
            return null;

        return (IActiveSkill)activeItem.activeSkill;
    }
}

// 중복되는 특성을 가진 아이템이 존재할 때,
// 2개의 아이템 중 하나를 제거하면 중복된 특성은 그대로 존재해야함
// 
