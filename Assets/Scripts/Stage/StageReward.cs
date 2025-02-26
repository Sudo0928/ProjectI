using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;


public class StageReward : MonoBehaviour
{
    [SerializeField] List<GameObject> itemTables;

    [Space(10)]
    [SerializeField] List<Item> items;

    [Space(10)]
    [SerializeField] List<int> gachaPools;

    int monsterCnt = 0;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            monsterCnt++;
            // TODO
            // 몬스터가 죽을 때 호출되는 Event에 monsterCnt를  -1
            // monster Cnt 가 0이 될 때, SpawnItem 함수 실행
        }
    }



}
 