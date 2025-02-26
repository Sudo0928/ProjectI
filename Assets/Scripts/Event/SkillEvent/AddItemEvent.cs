using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class AddItemEvent : Event
{
	public PlayerData playerData;
	public ItemInfo itemInfo;
	public PickupItemInfoUI pickupItemInfoUI;

	public AddItemEvent(PlayerData pd, ItemInfo itemInfo, PickupItemInfoUI pickupItemInfoUI)
	{
		playerData = pd;
		this.itemInfo = itemInfo;
		this.pickupItemInfoUI = pickupItemInfoUI;
	}
}

