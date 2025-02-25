using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class AddItemEvent : Event
{
	public PlayerData playerData;
	public ItemInfo itemInfo;
	public Action action;
	public AddItemEvent(PlayerData pd, ItemInfo itemInfo)
	{
		playerData = pd;
		this.itemInfo = itemInfo;

	}


}

