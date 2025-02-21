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
            // TODO ����
            // ���� �����۰� ������ �������� üũ
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

// �ߺ��Ǵ� Ư���� ���� �������� ������ ��,
// 2���� ������ �� �ϳ��� �����ϸ� �ߺ��� Ư���� �״�� �����ؾ���
// 
