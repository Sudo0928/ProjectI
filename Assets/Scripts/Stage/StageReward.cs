using System.Collections.Generic;
using UnityEngine;

public class StageReward : MonoBehaviour
{
    [SerializeField] List<GameObject> itemTables;

    [Space(10)]
    [SerializeField] List<Item> items;
    [SerializeField] List<int> gachaPools;

    BoxCollider2D myCollider; 
	private void Awake() 
	{
        DisableItems();
		myCollider = GetComponent<BoxCollider2D>();
        myCollider.enabled = false;
	}

	public void SpawnItem() 
    {
        myCollider.enabled = true;
		// TODO
		// 중복된 아이템이 <최대한>안나오도록 수정

		for (int i = 0; i < gachaPools.Count; i++)
        { 
            int idx = i;
			itemTables[i].SetActive(true);
            items[idx].SetActiveCollider(false);
			items[i].gameObject.SetActive(true);

			items[idx].SetItem(DataManager.gachaSystem.GetItem(gachaPools[idx]));
			items[idx].onPickupItem.RemoveAllListeners();
            items[idx].onPickupItem.AddListener(DisableItems);
		}

        GameManager.Instance.SetTimer(() => { 
            myCollider.enabled = false;
			for (int i = 0; i < gachaPools.Count; i++)
				items[i].SetActiveCollider(true);
             
		}, 1.0f);
	} 

    void DisableItems()
    {
        for (int i = 0; i < gachaPools.Count; i++)
        {
            if (items[i].isPickUp) continue;

            itemTables[i].SetActive(false);
            items[i].gameObject.SetActive(false);
		} 
    }
}
 