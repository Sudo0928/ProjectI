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
		DesignEnums.Grade grade =
			randomValue < table.Rank0Rate ? DesignEnums.Grade.Rank0 : // 15
			randomValue < table.Rank0Rate + table.Rank1Rate ? DesignEnums.Grade.Rank1 : // 30
			randomValue < table.Rank0Rate + table.Rank1Rate + table.Rank2Rate ? DesignEnums.Grade.Rank2 : // 30
			randomValue < table.Rank0Rate + table.Rank1Rate + table.Rank2Rate + table.Rank3Rate ? DesignEnums.Grade.Rank3 : // 20
			DesignEnums.Grade.Rank4; // 5


		List<ItemInfo> gradeItems = items.FindAll(item => item.Grade == grade);
		ItemInfo selectedItem = gradeItems[Random.Range(0, gradeItems.Count)];

		return selectedItem;  
	}

}
