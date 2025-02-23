using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GachaSystem
{
	public ItemInfo GetItem(int key)
	{
		GachaTable table = DataManager.gachaTable.GetByKey(key);
		List<ItemInfo> items = table.Items.Select(id => DataManager.itemInfoLoader.GetByKey(id)).ToList();

		int randomValue = Random.Range(0, 100);
		DesignEnums.Grade grade = randomValue < table.LegendaryRate ? DesignEnums.Grade.Legendary :
			randomValue < table.LegendaryRate + table.RareRate ? DesignEnums.Grade.Rare : DesignEnums.Grade.Common;

		List<ItemInfo> gradeItems = items.FindAll(item => item.Grade == grade);
		ItemInfo selectedItem = gradeItems[Random.Range(0, gradeItems.Count)];

		return selectedItem;  
	}

}
