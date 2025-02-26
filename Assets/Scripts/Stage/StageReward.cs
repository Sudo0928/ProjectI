using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;


public class StageReward : MonoBehaviour
{
    [SerializeField] List<GameObject> itemTables;

    [Space(10)]
    [SerializeField] List<Item> items;
    [SerializeField] List<int> gachaPools;

	private void Awake() 
	{
        DisableItems();
        SpawnItem();
	}

    void SpawnItem() 
    {
        for (int i = 0; i < gachaPools.Count; i++)
        {
            int idx = i;
			itemTables[i].SetActive(true);
			items[i].gameObject.SetActive(true);

			items[idx].SetItem(DataManager.gachaSystem.GetItem(gachaPools[idx]));
            items[idx].onPickupItem.RemoveAllListeners();
            items[idx].onPickupItem.AddListener(DisableItems);
		}
	} 

    void DisableItems()
    {
        for (int i = 0; i < gachaPools.Count; i++)
        {
            itemTables[i].SetActive(false);
            items[i].gameObject.SetActive(false);
		} 
    }
}
 