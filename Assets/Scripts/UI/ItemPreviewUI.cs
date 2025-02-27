using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Sse4_2;

public class ItemPreviewUI : MonoBehaviour
{
    [SerializeField, Space(10)] GameObject parent;
    [SerializeField] List<Sprite> tierIcons = new List<Sprite>();
	 
	List<GameObject> slots = new List<GameObject>();
	Dictionary<GameObject, ItemInfo> items = new Dictionary<GameObject, ItemInfo> ();
	public GameObject player { private get; set; }  = null;
	GameObject prevItem = null;
	Coroutine checkNearestItem = null;

	private void Awake()
	{
		int cnt = parent.transform.childCount;
		for (int i = 0; i < cnt; i++)
			slots.Add(parent.transform.GetChild(i).gameObject);

		CloseUI();
	}
	public void EnterItem(GameObject go, ItemInfo item)
	{
		if (!items.ContainsKey(go))
		{
			items.Add(go, item);
			if (checkNearestItem == null)
				checkNearestItem = StartCoroutine(CheckNearestItem());

		}
	}   
	 
	public void ExitItem(GameObject go)
	{
		items.Remove(go);
		if (items.Count == 0)
		{
			if (checkNearestItem != null)
				StopCoroutine(checkNearestItem);
			checkNearestItem = null;
			CloseUI();
		}
	}  
	  

	void FindNearestItem() 
	{
	 

		float dist = float.MaxValue;
		GameObject key = null;

		foreach (var it in items)
		{
			float nxt = (it.Key.transform.position - player.transform.position).magnitude;
			if (dist > nxt)
			{
				key = it.Key;
				dist = nxt; 
			}
		}

		if (key != prevItem && key != null)
		{ 
			prevItem = key;
			OpenUI(items[key]);
		} 
	}

	public void OpenUI(ItemInfo item)
    {
		int idx = 0;
		var slot = slots[idx++];
		slot.SetActive(false);

		var icon = slot.transform.Find("ItemIcon");
        var name = slot.transform.Find("ItemName");
        var tierIcon = slot.transform.Find("TierIcon");

		icon.gameObject.SetActive(false); 
		name.gameObject.SetActive(false);
		tierIcon.gameObject.SetActive(false);

		icon.GetComponent<Image>().sprite = GameManager.Instance.GetItemSprite(item);
        name.GetComponent<Text>().text = " - " + item.Name; 
        tierIcon.GetComponent<Image>().sprite = tierIcons[(int)item.Grade];

		icon.gameObject.SetActive(true);
		name.gameObject.SetActive(true);
		tierIcon.gameObject.SetActive(true);
		slot.SetActive(true);
		 
		for (int i = 0; i < item.AvailableOptions.Count; i++)
        {
            ItemOption op = DataManager.itemOptionLoader.GetByKey(item.AvailableOptions[i]);
            float value = item.OptionValues[i];
			if (value == 0.0f)
				continue;

			if (op == null)
				continue;


			slot = slots[idx++];
			slot.SetActive(true);
		//	slot.transform.SetParent(parent.transform);

			slot.GetComponent<Text>().text = " - " +  string.Format(op.Description, value);
		}

		for (int i = 0; i < item.SpecialOptions.Count; i++)
		{
			SpecialAbilityInfo sa = DataManager.specialAbilityInfoLoader.GetByKey(item.SpecialOptions[i]);

			if (sa == null)
				continue;


			slot = slots[idx++];
			slot.SetActive(true);
			//slot.transform.SetParent(parent.transform); 

		//	name = slot.transform.Find("ItemName");
			slot.GetComponent<Text>().text =  " - " + sa.Description;
		}

		for (int i = idx; i < slots.Count; i++)
			slots[i].SetActive(false); 
		
	}
	 
	void CloseUI()  
	{  
		for (int i = 0; i < slots.Count; i++)
			slots[i].SetActive(false);
		prevItem = null;
	}


	IEnumerator CheckNearestItem()
	{ 
		while(true)
		{
			FindNearestItem();
			yield return new WaitForSeconds(0.1f);
		} 
	}

}
